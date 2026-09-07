using System;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Net.Sockets;
using System.Threading;

namespace racman.TOD
{
    internal sealed class ToDAutosplitterClient : IDisposable
    {
        private enum SplitterCommand
        {
            Reset = 1,
            Split = 2,
            Pause = 3,
            Unpause = 4,
            Status = 5,
        }

        private enum GameStatus
        {
            InGame = 0,
            QuitPending = 1,
        }

        private const int AutosplitterPort = 9672;
        private const int AutosplitterMmfSize = 256;
        private const string AutosplitterMmfName = "racman-autosplitter-lc";

        private readonly object writerLock = new object();
        private readonly object inputSubscriptionLock = new object();
        private readonly tod game;

        private MemoryMappedFile mmfFile;
        private MemoryMappedViewStream mmfStream;
        private BinaryWriter writer;

        private TcpClient client;
        private NetworkStream stream;
        private Thread dataThread;
        private volatile bool stopping;
        private bool inputSubscriptionsActive;
        private int disconnectEventRaised;

        public event Action Disconnected;
        public event Action<bool> InputSubscriptionStateChanged;

        public ToDAutosplitterClient(
            string ip,
            tod game,
            bool inputSubscriptionsActive,
            Action<bool> inputSubscriptionStateChanged,
            Action disconnected)
        {
            if (game == null)
                throw new ArgumentNullException(nameof(game));

            this.game = game;
            this.inputSubscriptionsActive = inputSubscriptionsActive;
            InputSubscriptionStateChanged = inputSubscriptionStateChanged;
            Disconnected = disconnected;

            try
            {
                mmfFile = MemoryMappedFile.CreateOrOpen(
                    AutosplitterMmfName,
                    AutosplitterMmfSize);
                mmfStream = mmfFile.CreateViewStream();
                writer = new BinaryWriter(mmfStream);

                // command, paused, planet, game status
                WriteAutosplitterValues(new byte[]
                {
                    0,
                    0,
                    0,
                    (byte)GameStatus.InGame,
                });

                client = new TcpClient(ip, AutosplitterPort);
                client.NoDelay = true;
                stream = client.GetStream();

                dataThread = new Thread(ManageConnection);
                dataThread.IsBackground = true;
                dataThread.Name = "ToD Autosplitter";
                dataThread.Start();
            }
            catch
            {
                Dispose();
                throw;
            }
        }

        private void ManageConnection()
        {
            bool disconnectedUnexpectedly = false;

            try
            {
                while (!stopping)
                {
                    int commandValue = stream.ReadByte();
                    if (commandValue == -1)
                    {
                        disconnectedUnexpectedly = true;
                        break;
                    }

                    int packetValue = stream.ReadByte();
                    if (packetValue == -1)
                    {
                        disconnectedUnexpectedly = true;
                        break;
                    }

                    byte command = (byte)commandValue;
                    byte packet = (byte)packetValue;

                    Console.WriteLine(
                        "Got autosplitter command: {0} & packet: {1}",
                        command,
                        packet);

                    // Status acknowledgements are delayed until subscription
                    // teardown/setup is complete. Other commands can be
                    // acknowledged immediately.
                    if ((SplitterCommand)command != SplitterCommand.Status)
                        stream.WriteByte(1);

                    switch ((SplitterCommand)command)
                    {
                        case SplitterCommand.Split:
                        case SplitterCommand.Reset:
                            WriteAutosplitterValue(2, packet);
                            PulseCommand(command);
                            break;

                        case SplitterCommand.Pause:
                            WriteAutosplitterValue(1, 1);
                            break;

                        case SplitterCommand.Unpause:
                            WriteAutosplitterValue(1, 0);
                            break;

                        case SplitterCommand.Status:
                            HandleGameStatus((GameStatus)packet);
                            stream.WriteByte(1);
                            break;
                    }
                }
            }
            catch (IOException)
            {
                if (!stopping)
                {
                    Console.WriteLine("ToD autosplitter connection closed.");
                    disconnectedUnexpectedly = true;
                }
            }
            catch (SocketException)
            {
                if (!stopping)
                {
                    Console.WriteLine("ToD autosplitter socket disconnected.");
                    disconnectedUnexpectedly = true;
                }
            }
            catch (ObjectDisposedException)
            {
                // Expected when the main form closes while this thread is reading.
            }
            catch (Exception ex)
            {
                if (!stopping)
                {
                    Console.WriteLine(
                        "ToD autosplitter connection failed: {0}",
                        ex.Message);
                    disconnectedUnexpectedly = true;
                }
            }
            finally
            {
                if (disconnectedUnexpectedly && !stopping)
                {
                    DisableInputSubscriptions();
                    RaiseDisconnected();
                }
            }
        }

        private void HandleGameStatus(GameStatus status)
        {
            switch (status)
            {
                case GameStatus.QuitPending:
                    DisableInputSubscriptions();
                    break;

                case GameStatus.InGame:
                    EnableInputSubscriptions();
                    break;
            }

            // Keep game-exit state separate from the loading pause. LiveSplit
            // uses this transition to apply the reboot penalty.
            WriteAutosplitterValue(3, (byte)status);
        }

        private void EnableInputSubscriptions()
        {
            lock (inputSubscriptionLock)
            {
                if (stopping ||
                    inputSubscriptionsActive ||
                    !game.HasInputDisplay)
                {
                    return;
                }

                game.pid = game.api.getCurrentPID();
                game.SetupInputDisplayMemorySubs();
                inputSubscriptionsActive = true;
                RaiseInputSubscriptionStateChanged(true);
            }
        }

        private void DisableInputSubscriptions()
        {
            lock (inputSubscriptionLock)
            {
                if (!inputSubscriptionsActive)
                    return;

                Ratchetron ratchetron = game.api as Ratchetron;
                if (ratchetron == null)
                    return;

                try
                {
                    ratchetron.ReleaseAllSubs();
                }
                catch (IOException)
                {
                    Console.WriteLine("Could not release input-display subscriptions.");
                }
                catch (SocketException)
                {
                    Console.WriteLine("Input-display subscription connection was lost.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(
                        "Could not release input-display subscriptions: {0}",
                        ex.Message);
                }

                inputSubscriptionsActive = false;
                RaiseInputSubscriptionStateChanged(false);
            }
        }

        private void RaiseInputSubscriptionStateChanged(bool active)
        {
            Action<bool> handler = InputSubscriptionStateChanged;
            if (handler != null)
                handler(active);
        }

        private void RaiseDisconnected()
        {
            if (Interlocked.Exchange(ref disconnectEventRaised, 1) != 0)
                return;

            Action handler = Disconnected;
            if (handler != null)
                handler();
        }

        private void PulseCommand(byte command)
        {
            WriteAutosplitterValue(0, command);
            Thread.Sleep(100);
            WriteAutosplitterValue(0, 0);
        }

        private void WriteAutosplitterValues(byte[] values)
        {
            lock (writerLock)
            {
                if (writer == null)
                    return;

                writer.Seek(0, SeekOrigin.Begin);
                writer.Write(values);
                writer.Flush();
            }
        }

        private void WriteAutosplitterValue(int offset, byte value)
        {
            lock (writerLock)
            {
                if (writer == null)
                    return;

                writer.Seek(offset, SeekOrigin.Begin);
                writer.Write(value);
                writer.Flush();
            }
        }

        public void Dispose()
        {
            stopping = true;

            if (stream != null)
                stream.Close();
            if (client != null)
                client.Close();

            if (dataThread != null &&
                dataThread != Thread.CurrentThread &&
                dataThread.IsAlive)
            {
                dataThread.Join(500);
            }

            lock (writerLock)
            {
                if (writer != null)
                    writer.Close();
                writer = null;

                if (mmfStream != null)
                    mmfStream.Close();
                mmfStream = null;

                if (mmfFile != null)
                    mmfFile.Dispose();
                mmfFile = null;
            }

            stream = null;
            client = null;
            dataThread = null;
        }
    }
}
