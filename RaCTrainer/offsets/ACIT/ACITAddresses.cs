using System;
using System.Collections.Generic;

namespace racman.offsets.ACIT
{
    /// <summary>
    /// Factory class for creating IAddresses objects.
    /// </summary>
    public class ACITAddresses : IAddresses
    {

        private Dictionary<string, Addresses> gameVersion = new Dictionary<string, Addresses>();

        public ACITAddresses(string gameID)
        {
            InitializeAddresses();

            if (gameVersion.ContainsKey(gameID))
            {
                GameID = gameID;

                switch (GameID)
                {
                    case "BCUS98124":
                        break;
                    case "NPUA80966":
                        IsAutosplitterSupported = true;
                        IsSelfKillSupported = true;
                        break;
                    case "BCES00511":
                        IsAutosplitterSupported = true;
                        break;
                    default:
                        IsAutosplitterSupported = false;
                        IsSelfKillSupported = false;
                        break;
                }
            }
            else
            {
                throw new ArgumentException("Selected GameID is not available.");
            }
        }

        public string GameID { get; }

        public bool IsAutosplitterSupported { get; private set; }
        public bool IsSelfKillSupported { get; private set; }
        public uint planetValue { get; set; }
        //ratchet's / clank's health
        public uint ratchetHP => gameVersion[GameID].ratchetHP;

        // (0 = in game, 1 = in main menu, 2 = in pause) (NOTE: first pause will result in a 1 for a second)
        public uint gameStatePtr => gameVersion[GameID].gameStatePtr;
        // indicates last opened scene (1 = save screen, 2 = load screen, 4 playing)
        public uint loadSaveState => gameVersion[GameID].loadSaveState;
        // Main Cutscenes (0 = in game, 1 = in cutscene)
        public uint cutsceneState1Ptr => gameVersion[GameID].cutsceneState1Ptr;
        // Animation Cutscenes (0 = in game, 1 = in cutscene)
        public uint cutsceneState2Ptr => gameVersion[GameID].cutsceneState2Ptr;
        // Mini Cutscenes (0 = in game, 1 = in cutscene)
        public uint cutsceneState3Ptr => gameVersion[GameID].cutsceneState3Ptr;
        // Save File ID
        public uint saveFileIDPtr => gameVersion[GameID].saveFileIDPtr;
        // Timer
        public uint timerPtr => gameVersion[GameID].timerPtr;
        // 1 if the game is loading, 0 otherwise
        public uint isLoading => gameVersion[GameID].isLoading;
        // Map timer
        public uint mapTimerPtr => gameVersion[GameID].mapTimerPtr;
        // Current bolt count
        public uint boltCount => gameVersion[GameID].boltCount;
        // Ratchet's coordinates
        public uint playerCoords
        {
            get
            {
                switch (planetValue)
                {
                    case 1:
                        return gameVersion[GameID].pCoordsGC1;
                    case 2:
                        return gameVersion[GameID].pCoordsZolar;
                    case 3:
                        return gameVersion[GameID].pCoordsPhylaxS;
                    case 4:
                        return gameVersion[GameID].pCoordsVorselon;
                    case 5:
                        return gameVersion[GameID].pCoordsGC2;
                    case 6:
                        return gameVersion[GameID].pCoordsVelaS;
                    case 7:
                        return gameVersion[GameID].pCoordsMolonoth;
                    case 8:
                        return gameVersion[GameID].pCoordsAxiom;
                    case 9:
                        return gameVersion[GameID].pCoordsGC3;
                    case 10:
                        return gameVersion[GameID].pCoordsKorthosS;
                    case 11:
                        return gameVersion[GameID].pCoordsKrell;
                    case 12:
                        return gameVersion[GameID].pCoordsBattlePlex;
                    case 13:
                        return gameVersion[GameID].pCoordsZanifar;
                    case 14:
                        return gameVersion[GameID].pCoordsGC4;
                    case 15:
                        return gameVersion[GameID].pCoordsBerniliusS;
                    case 16:
                        return gameVersion[GameID].pCoordsVapedia;
                    case 17:
                        return gameVersion[GameID].pCoordsNeffy;
                    case 18:
                        return gameVersion[GameID].pCoordsCorvusS;
                    case 19:
                        return gameVersion[GameID].pCoordsGimlick;
                    case 20:
                        return gameVersion[GameID].pCoordsGC5;
                    default:
                        return 0;
                }
            }
        }
        // The player's current health.
        public uint playerHealth => gameVersion[GameID].playerHealth;
        // Controller inputs mask address
        public uint inputOffset => gameVersion[GameID].inputOffset;
        // Controller analog sticks address
        public uint analogOffset => gameVersion[GameID].analogOffset;
        // Loading planet
        public uint loadPlanet => gameVersion[GameID].currentPlanet;
        // Currently loaded planet.
        public uint currentPlanet => gameVersion[GameID].currentPlanet;
        // Azimuth HP
        public uint azimuthHPPtr => gameVersion[GameID].azimuthHPPtr;
        // Libra HP
        public uint libraHPPtr => gameVersion[GameID].libraHPPtr;
        // 1 if Vorselon 1 space combat is done (or in PAL version it indicates if the V1 ship is still on Sector1), 0 otherwise
        public uint vorselon1SpaceCombat => gameVersion[GameID].vorselon1SpaceCombat;
        // 2 if Neffy 1 final room is done
        public uint neffy1finalRoom => gameVersion[GameID].neffy1finalRoom;
        // 1 if GC2 was already visited
        public uint wasGC2Visited => gameVersion[GameID].wasGC2Visited;
        // 1 if the first cutscene of the game is playing 0 otherwise (NOTE: this workds only on GC1)
        public uint firstCutscene => gameVersion[GameID].firstCutscene;
        // Weapon unlock
        public uint weapons => gameVersion[GameID].weapons;
        // Cutscenes array
        public uint[] cutscenesArray => gameVersion[GameID].cutscenesArray;

        private void InitializeAddresses()
        {
            // All addresses are from the US version of the game.

            gameVersion["BCUS98124"] = new Addresses
            {
                gameStatePtr = 0xFBADC8,
                cutsceneState1Ptr = 0xF6B4AC,
                cutsceneState2Ptr = 0x40E9651C,
                cutsceneState3Ptr = 0xDF027C,
                saveFileIDPtr = 0xE47338,
                timerPtr = 0x40EBA460,
                boltCount = 0xE24FE8,
                //playerCoords = 0xE240F0,
                currentPlanet = 0xEF7E90,
                azimuthHPPtr = 0x40E890AC
            };
            gameVersion["NPUA80966"] = new Addresses
            {
                pCoordsGC1 = 0xDE5590,
                pCoordsZolar = 0x4A4E4800,
                pCoordsPhylaxS = 0x490E2E80,
                pCoordsVorselon = 0x4A392A70,
                pCoordsGC2 = 0xDE5590,
                pCoordsVelaS = 0x0,
                pCoordsMolonoth = 0x0,
                pCoordsAxiom = 0x0,
                pCoordsGC3 = 0x4740C2B0,
                pCoordsKorthosS = 0x0,
                pCoordsKrell = 0x0,
                pCoordsBattlePlex = 0x0,
                pCoordsZanifar = 0x0,
                pCoordsGC4 = 0x47D9C5B0,
                pCoordsBerniliusS = 0x0,
                pCoordsVapedia = 0x0,
                pCoordsNeffy = 0x4A5E1980,
                pCoordsCorvusS = 0x0,
                pCoordsGimlick = 0x0,
                pCoordsGC5 = 0x0,

                ratchetHP = 0x40E8AD44,
                gameStatePtr = 0xFBA8C8,
                loadSaveState = 0xE472C4,
                cutsceneState1Ptr = 0xF6B3AC,
                cutsceneState2Ptr = 0x40E9651C,
                cutsceneState3Ptr = 0x4A4E5428,
                saveFileIDPtr = 0xE472B8,
                timerPtr = 0x40EBA460,
                isLoading = 0xF23584,
                boltCount = 0xE24F68,
                inputOffset = 0xF6ABC8,
                analogOffset = 0xF6AA24,
                currentPlanet = 0xE896B4,
                azimuthHPPtr = 0x40E890AC,
                libraHPPtr = 0x40E893CC,    // (backup 0x40E89510)

                vorselon1SpaceCombat = 0xE26B20,
                neffy1finalRoom = 0xE2C3A0,
                wasGC2Visited = 0xE271E8,
                firstCutscene = 0x40047224,     // (backups 0x400473A4, 0x40047524, 0x4896B7C8)

                //mapTimerPtr = 0x4BA17930,
                weapons = 0xE249F4,
                cutscenesArray = new uint[] { 0x409AE5BC, 0x409AE620, 0x409AE6F4, 0x409AE784, 0x409AE7B4, 0x409AE814, 0x409AE844, 0x409AE874, 0x409AED84, 0x409AEDE4 }
            };
            gameVersion["BCES00511"] = new Addresses
            {
                pCoordsGC1 = 0x0,
                pCoordsZolar = 0x0,
                pCoordsPhylaxS = 0x0,
                pCoordsVorselon = 0x0,
                pCoordsGC2 = 0x0,
                pCoordsVelaS = 0x0,
                pCoordsMolonoth = 0x0,
                pCoordsAxiom = 0x0,
                pCoordsGC3 = 0x0,
                pCoordsKorthosS = 0x0,
                pCoordsKrell = 0x0,
                pCoordsBattlePlex = 0x0,
                pCoordsZanifar = 0x0,
                pCoordsGC4 = 0x0,
                pCoordsBerniliusS = 0x0,
                pCoordsVapedia = 0x0,
                pCoordsNeffy = 0x0,
                pCoordsCorvusS = 0x0,
                pCoordsGimlick = 0x49F48100,
                pCoordsGC5 = 0x0,

                gameStatePtr = 0xFBAE48,
                loadSaveState = 0xE473C4,
                cutsceneState1Ptr = 0xF6B52C,
                cutsceneState2Ptr = 0x40E96E9C,
                cutsceneState3Ptr = 0x40E96E9C,
                saveFileIDPtr = 0xE473B8,
                timerPtr = 0x40EBADE0,
                boltCount = 0xE25068,
                //playerCoords = 0xE24170,
                inputOffset = 0xF6AD48,
                analogOffset = 0xF6ABA4,
                currentPlanet = 0xEF7F10,  // (backup 0xE472B4)
                azimuthHPPtr = 0x40E89A2C,
                libraHPPtr = 0x40E89E90,    // (backup 0x40E89D4C)

                vorselon1SpaceCombat = 0xE26C20,    // (backups 0xE26C4C 0xE26C78 0xE69120 0xE6914C 0xE69178)
                neffy1finalRoom = 0xE2C4A0,    // (backups 0xE6E9A0)
                wasGC2Visited = 0xE272E8,   // (backups 0xE697E8 0xE279B0)
                firstCutscene = 0xF49450,  // (backups 0x40950BD4 0x41FABA08) (experimental 0xF23704)

                weapons = 0xE24AF4,
                cutscenesArray = new uint[] { 0x409AE5D0, 0x409AE634, 0x409AE740, 0x409AE7D0, 0x409AE800, 0x409AE860, 0x409AE890, 0x409AE8C0, 0x409AEDD0, 0x409AEE30, 0x409AE264 }  // gc1 intro cutscene is activated by 2 addresses
            };
        }

        private class Addresses
        {
            public uint pCoordsGC1 { get; set; }
            public uint pCoordsZolar { get; set; }
            public uint pCoordsPhylaxS { get; set; }
            public uint pCoordsVorselon { get; set; }
            public uint pCoordsGC2 { get; set; }
            public uint pCoordsVelaS { get; set; }
            public uint pCoordsMolonoth { get; set; }
            public uint pCoordsAxiom { get; set; }
            public uint pCoordsGC3 { get; set; }
            public uint pCoordsKorthosS { get; set; }
            public uint pCoordsKrell { get; set; }
            public uint pCoordsBattlePlex { get; set; }
            public uint pCoordsZanifar { get; set; }
            public uint pCoordsGC4 { get; set; }
            public uint pCoordsBerniliusS { get; set; }
            public uint pCoordsVapedia { get; set; }
            public uint pCoordsNeffy { get; set; }
            public uint pCoordsCorvusS { get; set; }
            public uint pCoordsGimlick { get; set; }
            public uint pCoordsGC5 { get; set; }

            public uint ratchetHP { get; set; }
            public uint gameStatePtr { get; set; }
            public uint loadSaveState { get; set; }
            public uint cutsceneState1Ptr { get; set; }
            public uint cutsceneState2Ptr { get; set; }
            public uint cutsceneState3Ptr { get; set; }
            public uint saveFileIDPtr { get; set; }
            public uint timerPtr { get; set; }
            public uint isLoading { get; set; }
            public uint mapTimerPtr { get; set; }
            public uint boltCount { get; set; }
            public uint playerHealth { get; set; }
            public uint inputOffset { get; set; }
            public uint analogOffset { get; set; }
            public uint loadPlanet { get; set; }
            public uint currentPlanet { get; set; }
            public uint azimuthHPPtr { get; set; }
            public uint libraHPPtr { get; set; }
            public uint vorselon1SpaceCombat { get; set; }
            public uint neffy1finalRoom { get; set; }
            public uint wasGC2Visited { get; set; }
            public uint firstCutscene { get; set; }
            public uint weapons { get; set; }
            public uint[] cutscenesArray { get; set; }
        }
    }
}
