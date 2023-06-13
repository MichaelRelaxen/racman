using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace racman
{
    public interface IAutosplitterAvailable
    {
        IEnumerable<(uint addr, uint size)> AutosplitterAddresses { get; }
    }

    class AutosplitterHelper
    {
        MemoryMappedFile mmfFile;
        MemoryMappedViewStream mmfStream;
        BinaryWriter writer;

        List<int> subscriptionIDs = new List<int>();

        IGame currentGame = null;

        public bool IsRunning { get; private set; } = false;

        public AutosplitterHelper()
        {
            mmfFile = MemoryMappedFile.CreateOrOpen("racman-autosplitter", 256);
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
                throw new InvalidOperationException("Must start autosplitter before starting.");
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
                writer.Seek(128, SeekOrigin.Begin);
                writer.Write(value, 0, value.Length);
            }    

            writeLock.ReleaseMutex();
        }

        private void OpenAutosplitter(acit game)
        {
            int planetFrameCountSubID = game.api.SubMemory(game.pid, acit.addr.weirdTimerThingy, 4, (value) =>
            {
                WriteToMemory(0, value);
            });
            int isPausedSubID = game.api.SubMemory(game.pid, acit.addr.isPaused2, 1, (value) =>
            {
                WriteToMemory(4, value);
            });
            int gameStateSubID = game.api.SubMemory(game.pid, acit.addr.gameState + 3, 1, (value) =>
            {
                WriteToMemory(5, value);
            });
            int planetStringSubID1 = game.api.SubMemory(game.pid, 0xE20583, 8, (value) => 
            {
                WriteToMemory(6, new byte[] { 0x41 }); // Fuck livesplit
                WriteToMemory(7, value.Reverse().ToArray());
            });
            int planetStringSubID2 = game.api.SubMemory(game.pid, 0xE20583 + 8, 8, (value) => 
            {
                WriteToMemory(15, value.Reverse().ToArray());
            });

            subscriptionIDs.AddRange(new int[] {
                planetFrameCountSubID,
                isPausedSubID,
                gameStateSubID,
                planetStringSubID1,
                planetStringSubID2
            });
        }

        public void StartAutosplitterForGame(IGame game)
        {
            if (game is acit)
            {
                OpenAutosplitter(game as acit);
                return;
            }
            if (!(game is IAutosplitterAvailable)) throw new NotSupportedException("This game doesn't support an autosplitter yet.");
            currentGame = game;
            var autosplitter = game as IAutosplitterAvailable;

            int pos = 0;
            foreach (var (addr, size) in autosplitter.AutosplitterAddresses)
            {
                var _pos = pos; // If you can think of a better way to do this please tell me
                subscriptionIDs.Add(game.api.SubMemory(game.api.getCurrentPID(), addr, size, (value) =>
                {
                    WriteToMemory(_pos, value);
                }));
                pos += (int) size;
            }

            IsRunning = true;
        }
    }
}
