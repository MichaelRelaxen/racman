using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace racman
{
    class AutosplitterHelper
    {
        byte[] memoryMapContents = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

        System.IO.MemoryMappedFiles.MemoryMappedFile mmfFile;
        System.IO.MemoryMappedFiles.MemoryMappedViewStream mmfStream;
        BinaryWriter writer;

        List<int> subscriptionIDs = new List<int>();

        IGame currentGame = null;

        public AutosplitterHelper()
        {
            mmfFile = System.IO.MemoryMappedFiles.MemoryMappedFile.CreateNew("racman-autosplitter", 21);
            mmfStream = mmfFile.CreateViewStream();
            writer = new BinaryWriter(mmfStream);
        }

        /// <summary>
        ///  Destructor/finalizer, called when the object is destroyed, on application close, garbage collection, etc.
        /// </summary>
        ~AutosplitterHelper()
        {
            this.Stop();
        }

        public void Stop()
        {
            mmfStream.Close();
            writer.Close();

            if (currentGame == null)
            {
                return;
            }

            foreach(int subID in subscriptionIDs)
            {
                this.currentGame.api.ReleaseSubID(subID);
            }
        }

        private static Mutex writeLock = new Mutex();
        private void WriteToMemory(int offset, byte[] value)
        {
            writeLock.WaitOne();

            writer.Seek(offset, SeekOrigin.Begin);
            writer.Write(value, 0, value.Length);

            writeLock.ReleaseMutex();
        }

        private void OpenRaC1Autosplitter(rac1 game)
        {
            int destinationPlanetSubID = game.api.SubMemory(game.pid, rac1.addr.destinationPlanet + 3, 1, (value) => {
                WriteToMemory(8, value);
            });

            int currentPlanetSubID = game.api.SubMemory(game.pid, rac1.addr.currentPlanet + 3, 1, (value) =>
            {
                WriteToMemory(9, value);
            });

            int playerStateSubID = game.api.SubMemory(game.pid, rac1.addr.playerState + 1, 2, (value) =>
            {
                WriteToMemory(10, value);
            });

            int planetFrameCountSubID = game.api.SubMemory(game.pid, rac1.addr.planetFrameCount, 4, (value) =>
            {
                WriteToMemory(12, value);
            });

            int gameStateSubID = game.api.SubMemory(game.pid, rac1.addr.gameState, 4, (value) =>
            {
                WriteToMemory(16, value);
            });

            int loadingScreenSubID = game.api.SubMemory(game.pid, rac1.addr.loadingScreenID + 3, 1, (value) =>
            {
                WriteToMemory(20, value);
            });

            int playerCoordsSubID = game.api.SubMemory(game.pid, rac1.addr.playerCoords, 8, (value) =>
            {
                WriteToMemory(0, value);
            });

            subscriptionIDs.AddRange(new int[] {
                destinationPlanetSubID,
                currentPlanetSubID,
                playerStateSubID,
                planetFrameCountSubID,
                gameStateSubID,
                loadingScreenSubID,
                playerCoordsSubID
            });
        }

        public void StartAutosplitterForGame(IGame game)
        {
            if (game is rac1)
            {
                this.OpenRaC1Autosplitter((rac1)game);
            }
        }

    }
}
