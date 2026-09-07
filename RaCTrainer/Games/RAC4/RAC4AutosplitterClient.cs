using System;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Net.Sockets;
using System.Threading;

namespace racman.RAC4
{
    internal sealed class RAC4AutosplitterClient : IDisposable
    {
        private enum SplitterCommand
        {
            Reset = 1,
            Split = 2,
            Pause = 3,
            Unpause = 4,
        }

        private const int AutosplitterPort = 9672;
        private const int AutosplitterMmfSize = 256;
        private const string AutosplitterMmfName = "racman-autosplitter-lc";

        private readonly object writerLock = new object();
        private readonly Action pauseForGameExit;
        private readonly Action resumeAfterGameLoad;

        private MemoryMappedFile mmfFile;
        private MemoryMappedViewStream mmfStream;
        private BinaryWriter writer;

        private TcpClient client;
        private NetworkStream stream;
        private Thread dataThread;
        private volatile bool stopping;
        private int disconnectEventRaised;

        public event Action Disconnected;

        public RAC4AutosplitterClient(
            string ip,
            Action pauseForGameExit,
            Action resumeAfterGameLoad,
            Action disconnected)
        {
            this.pauseForGameExit = pauseForGameExit ??
                throw new ArgumentNullException(nameof(pauseForGameExit));
            this.resumeAfterGameLoad = resumeAfterGameLoad ??
                throw new ArgumentNullException(nameof(resumeAfterGameLoad));
            Disconnected = disconnected;

            try
            {
                mmfFile = MemoryMappedFile.CreateOrOpen(
                    AutosplitterMmfName,
                    AutosplitterMmfSize);
                mmfStream = mmfFile.CreateViewStream();
                writer = new BinaryWriter(mmfStream);

                // command, paused, planet
                WriteAutosplitterValues(new byte[] { 0, 0, 0 });

                client = new TcpClient(ip, AutosplitterPort);
                client.NoDelay = true;
                stream = client.GetStream();

                dataThread = new Thread(ManageConnection);
                dataThread.IsBackground = true;
                dataThread.Name = "RAC4 Autosplitter";
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
                    SplitterCommand splitterCommand = (SplitterCommand)command;

                    Console.WriteLine(
                        "Got RAC4 autosplitter command: {0} & packet: {1}",
                        command,
                        packet);

                    // Pause and unpause are also the game-lifecycle messages.
                    // Delay their acknowledgement until subscription teardown or
                    // restoration is complete.
                    if (splitterCommand != SplitterCommand.Pause &&
                        splitterCommand != SplitterCommand.Unpause)
                    {
                        stream.WriteByte(1);
                    }

                    switch (splitterCommand)
                    {
                        case SplitterCommand.Split:
                        case SplitterCommand.Reset:
                            WriteAutosplitterValue(2, packet);
                            PulseCommand(command);
                            break;

                        case SplitterCommand.Pause:
                            WriteAutosplitterValue(1, 1);
                            pauseForGameExit();
                            stream.WriteByte(1);
                            break;

                        case SplitterCommand.Unpause:
                            resumeAfterGameLoad();
                            WriteAutosplitterValue(1, 0);
                            stream.WriteByte(1);
                            break;
                    }
                }
            }
            catch (IOException)
            {
                if (!stopping)
                {
                    Console.WriteLine("RAC4 autosplitter connection closed.");
                    disconnectedUnexpectedly = true;
                }
            }
            catch (SocketException)
            {
                if (!stopping)
                {
                    Console.WriteLine("RAC4 autosplitter socket disconnected.");
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
                        "RAC4 autosplitter connection failed: {0}",
                        ex.Message);
                    disconnectedUnexpectedly = true;
                }
            }
            finally
            {
                if (disconnectedUnexpectedly && !stopping)
                {
                    CloseConnection();

                    try
                    {
                        pauseForGameExit();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(
                            "Could not tear down RAC4 subscriptions: {0}",
                            ex.Message);
                    }

                    RaiseDisconnected();
                }
            }
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

        private void CloseConnection()
        {
            try
            {
                if (stream != null)
                    stream.Close();
            }
            catch
            {
                // The socket may already have been closed by the remote side.
            }

            try
            {
                if (client != null)
                    client.Close();
            }
            catch
            {
                // The socket may already have been closed by the remote side.
            }
        }

        public void Dispose()
        {
            stopping = true;
            CloseConnection();

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
