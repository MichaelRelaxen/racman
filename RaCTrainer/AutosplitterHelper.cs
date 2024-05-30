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

    public class AutosplitterHelper
    {
        public static int mmfAddressBytes = 128;
        public static int mmfConfigBytes = 256;
        public static int mmfSize = mmfAddressBytes + mmfConfigBytes;

        MemoryMappedFile mmfFile;
        MemoryMappedViewStream mmfStream;
        BinaryWriter writer;

        Timer UpdatingTimer;

        List<int> subscriptionIDs = new List<int>();

        IGame currentGame = null;

        public bool IsRunning { get; private set; } = false;

        public AutosplitterHelper()
        {
            mmfFile = MemoryMappedFile.CreateOrOpen("racman-autosplitter", mmfSize);
            mmfStream = mmfFile.CreateViewStream();
            writer = new BinaryWriter(mmfStream);
        }

        public BinaryWriter GetWriter()
        {
            return writer;
        }

        public Mutex GetWriteLock()
        {
            return writeLock;
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

        public int StartAutosplitterForGame(IGame game)
        {
            if (!(game is IAutosplitterAvailable)) throw new NotSupportedException("This game doesn't support an autosplitter yet.");
            currentGame = game;
            var autosplitter = game as IAutosplitterAvailable;

            int pos = 0;
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

            IsRunning = true;

            return pos;
        }
    }
}
