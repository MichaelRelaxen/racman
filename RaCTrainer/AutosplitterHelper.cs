using System;
using System.Collections.Generic;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Threading;

namespace racman
{
    public interface IAutosplitterAvailable
    {
        IEnumerable<(uint addr, uint size)> AutosplitterAddresses { get; }
    }

    public interface IAutosplitterWVariables
    {
        IEnumerable<(uint addr, uint size)> GetAutosplitterVariables();
    } 

    public class AutosplitterHelper
    {
        public static int mmfAddressBytes = 128;
        public static int mmfConfigBytes = 256;
        public static int mmfSize = mmfAddressBytes + mmfConfigBytes;

        MemoryMappedFile mmfFile;
        MemoryMappedViewStream mmfStream;
        BinaryWriter writer;

        private Timer UpdatingTimer = null;

        List<int> subscriptionIDs = new List<int>();

        IGame currentGame = null;

        public bool IsRunning { get; private set; } = false;

        public AutosplitterHelper()
        {
            mmfFile = MemoryMappedFile.CreateOrOpen("racman-autosplitter", mmfSize);
            mmfStream = mmfFile.CreateViewStream();
            writer = new BinaryWriter(mmfStream);
        }

        /// <summary>
        ///  Destructor/finalizer, called when the object is destroyed, on application close, garbage collection, etc.
        /// </summary>
        ~AutosplitterHelper()
        {
            if (writer != null && IsRunning)
            {
                this.Stop();
            }
        }

        public void Stop()
        {
            if (!IsRunning)
            {
                throw new InvalidOperationException("Must start autosplitter before stopping.");
            }

            IsRunning = false;
            mmfStream.Close();
            writer?.Close();
            writer = null;

            if (currentGame == null) return;

            foreach (int subID in subscriptionIDs)
            {
                this.currentGame.api.ReleaseSubID(subID);
            }

            // stop the update timer
            UpdatingTimer.Dispose();
            UpdatingTimer = null;
        }

        private static Mutex writeLock = new Mutex();
        private void WriteToMemory(int offset, byte[] value)
        {
            writeLock.WaitOne();

            if (writer != null)
            {
                writer.Seek(offset, SeekOrigin.Begin);
                writer.Write(value, 0, value.Length);
            }

            writeLock.ReleaseMutex();
        }

        // Probably will only be used for UYA
        public void WriteConfig(byte[] value)
        {
            writeLock.WaitOne();

            if (writer != null)
            {
                writer.Seek(mmfAddressBytes, SeekOrigin.Begin);
                writer.Write(value, 0, value.Length);
                writer.Write(Enumerable.Repeat((byte)0, mmfConfigBytes - value.Length).ToArray());
            }

            writeLock.ReleaseMutex();
        }

        public void StartAutosplitterForGame(IGame game)
        {
            if (!(game is IAutosplitterAvailable) && !(game is IAutosplitterWVariables)) throw new NotSupportedException("This game doesn't support an autosplitter yet.");
            currentGame = game;

            int pos = 0;

            // if the game has addresses that need to be written to memory
            if (game is IAutosplitterAvailable)
            {
                var autosplitter = game as IAutosplitterAvailable;
                foreach (var (addr, size) in autosplitter.AutosplitterAddresses)
                {
                    var _pos = pos; // If you can think of a better way to do this please tell me

                    // Write the initial value to the memory. This is necessary because the autosplitter will only
                    // trigger when the value changes. So at the start of the game all values will be 0;
                    var initialValue = game.api.ReadMemory(game.api.getCurrentPID(), addr, size).Reverse().ToArray();
                    WriteToMemory(_pos, initialValue);

                    subscriptionIDs.Add(game.api.SubMemory(game.api.getCurrentPID(), addr, size, (value) =>
                    {
                        WriteToMemory(_pos, value);
                    }));
                    pos += (int)size;
                }
            }
            
            // if the game has variables that need to be written to memory
            if (game is IAutosplitterWVariables)
            {
                var autosplitterWVariables = game as IAutosplitterWVariables;
                if (UpdatingTimer == null)
                {
                    UpdatingTimer = new Timer((state) =>
                    {
                        int _pos = pos;
                        foreach (var (value, size) in autosplitterWVariables.GetAutosplitterVariables())
                        {
                            byte[] bytes = BitConverter.GetBytes(value);
                            WriteToMemory(pos, bytes);
                            _pos += (int)size;
                        }
                    }, null, 0, 1000 / 120);
                }
            }

            IsRunning = true;
        }
    }
}
