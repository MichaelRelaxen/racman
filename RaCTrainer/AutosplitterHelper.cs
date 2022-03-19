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
        byte[] memoryMapContents = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

        System.IO.MemoryMappedFiles.MemoryMappedFile mmfFile;
        System.IO.MemoryMappedFiles.MemoryMappedViewStream mmfStream;
        BinaryWriter writer;

        List<int> subscriptionIDs = new List<int>();

        IGame currentGame = null;

        public AutosplitterHelper()
        {
            mmfFile = System.IO.MemoryMappedFiles.MemoryMappedFile.CreateOrOpen("racman-autosplitter", 32);
            mmfStream = mmfFile.CreateViewStream();
            writer = new BinaryWriter(mmfStream);
        }

        /// <summary>
        ///  Destructor/finalizer, called when the object is destroyed, on application close, garbage collection, etc.
        /// </summary>
        ~AutosplitterHelper()
        {
            if (writer != null)
            {
                this.Stop();
            }
        }

        public void Stop()
        {
            mmfStream.Close();
            writer.Close();

            writer = null;

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

            if (writer != null)
            {
                writer.Seek(offset, SeekOrigin.Begin);
                writer.Write(value, 0, value.Length);
            }

            writeLock.ReleaseMutex();
        }

        // Autosplitter for Ratchet & Clank - NPEA00385
        private void OpenAutosplitter(rac1 game)
        {
            int playerCoordsSubID = game.api.SubMemory(game.pid, rac1.addr.playerCoords, 8, (value) =>
            {
                WriteToMemory(0, value);
            });
            int destinationPlanetSubID = game.api.SubMemory(game.pid, rac1.addr.destinationPlanet + 3, 1, (value) => {
                WriteToMemory(8, value);
            });

            int currentPlanetSubID = game.api.SubMemory(game.pid, rac1.addr.currentPlanet + 3, 1, (value) =>
            {
                WriteToMemory(9, value);
            });

            int playerStateSubID = game.api.SubMemory(game.pid, rac1.addr.playerState, 4, (value) =>
            {
                WriteToMemory(10, new byte[] { value[0], value[1] });
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

            int goldBoltCountSubID = game.api.SubMemory(game.pid, 0x00aff000, 4, (value) =>
            {
                WriteToMemory(21, new byte[] { value[0] });
            });

            int skillpointSubID = game.api.SubMemory(game.pid, 0x00aff010, 4, (value) =>
            {
                WriteToMemory(22, new byte[] { value[0] });
            });

            int itemCountSubID = game.api.SubMemory(game.pid, 0x00aff020, 4, (value) =>
            {
                WriteToMemory(23, new byte[] { value[0] });
            });

            subscriptionIDs.AddRange(new int[] {
                destinationPlanetSubID,
                currentPlanetSubID,
                playerStateSubID,
                planetFrameCountSubID,
                gameStateSubID,
                loadingScreenSubID,
                playerCoordsSubID,
                goldBoltCountSubID,
                skillpointSubID,
                itemCountSubID
            });
        }

        // Autosplitter for Ratchet & Clank 3 - NPEA000387
        private void OpenAutosplitter(rac3 game)
        {
            int destinationPlanetSubID = game.api.SubMemory(game.pid, rac3.addr.destinationPlanet + 3, 1, (value) => 
            {
                WriteToMemory(0, value);
            });

            int currentPlanetSubID = game.api.SubMemory(game.pid, rac3.addr.currentPlanet + 3, 1, (value) =>
            {
                WriteToMemory(1, value);
            });

            int playerStateSubID = game.api.SubMemory(game.pid, rac3.addr.playerState + 2, 2, (value) =>
            {
                WriteToMemory(2, value);
            });

            int planetFrameCountSubID = game.api.SubMemory(game.pid, rac3.addr.planetFrameCount, 4, (value) =>
            {
                WriteToMemory(4, value);
            });

            int gameStateSubID = game.api.SubMemory(game.pid, rac3.addr.gameState, 4, (value) =>
            {
                WriteToMemory(8, value);
            });

            int loadingScreenSubID = game.api.SubMemory(game.pid, rac3.addr.loadingScreenID + 3, 1, (value) =>
            {
                WriteToMemory(12, value);
            });

            int marcadiaMissionSubID = game.api.SubMemory(game.pid, rac3.addr.marcadiaMission + 3, 1, (value) =>
            {
                WriteToMemory(13, value);
            });

            int neffyHealthSubID = game.api.SubMemory(game.pid, 0xC4DF80, 4, (value) =>
            {
                WriteToMemory(14, value);
            });

            int neffyDeadID = game.api.SubMemory(game.pid, 0xDA50FC, 4, (value) =>
            {
                WriteToMemory(18, value);
            });

            subscriptionIDs.AddRange(new int[] {
                destinationPlanetSubID,
                currentPlanetSubID,
                playerStateSubID,
                planetFrameCountSubID,
                gameStateSubID,
                loadingScreenSubID,
                marcadiaMissionSubID,
                neffyHealthSubID,
                neffyDeadID,
            });
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
            if (game is rac1)
            {
                this.OpenAutosplitter((rac1)game);
            }
            if (game is rac3)
            {
                this.OpenAutosplitter((rac3)game);
            }
            if (game is acit)
            {
                this.OpenAutosplitter((acit)game);
            }
        }
    }
}
