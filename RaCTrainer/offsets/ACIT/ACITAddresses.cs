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
                        IsAutosplitterSupported = true;
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

        // (0 = in game, 1 = in main menu, 2 = in pause) (NOTE: first pause will result in a 1 for a second)
        public uint gameState1Ptr => gameVersion[GameID].gameState1Ptr;
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
        // Map timer
        public uint mapTimerPtr => gameVersion[GameID].mapTimerPtr;
        // Current bolt count
        public uint boltCount => gameVersion[GameID].boltCount;
        // Ratchet's coordinates
        public uint playerCoords => gameVersion[GameID].playerCoords;
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
        // Weapon unlock
        public uint weapons => gameVersion[GameID].weapons;
        // Cutscenes array
        public uint[] cutscenesArray => gameVersion[GameID].cutscenesArray;

        private void InitializeAddresses()
        {
            // All addresses are from the US version of the game.

            gameVersion["BCUS98124"] = new Addresses
            {
                gameState1Ptr = 0xFBADC8,
                cutsceneState1Ptr = 0xF6B4AC,
                cutsceneState2Ptr = 0x40E9651C,
                cutsceneState3Ptr = 0xDF027C,
                saveFileIDPtr = 0xE47338,
                timerPtr = 0x40EBA460,
                boltCount = 0xE24FE8,
                playerCoords = 0xE240F0,
                currentPlanet = 0xEF7E90,
                azimuthHPPtr = 0x40E890AC
            };
            gameVersion["NPUA80966"] = new Addresses
            {
                gameState1Ptr = 0xFBA8C8,
                cutsceneState1Ptr = 0xF6B3AC,
                cutsceneState2Ptr = 0x40E9651C,
                cutsceneState3Ptr = 0x4A4E5428,
                saveFileIDPtr = 0xE472B8,
                timerPtr = 0x40EBA460,
                boltCount = 0xE24F68,
                playerCoords = 0x4A4E4800,
                inputOffset = 0xF6ABC8,
                analogOffset = 0xF6AA24,
                currentPlanet = 0xE896B4,
                azimuthHPPtr = 0x40E890AC,
                mapTimerPtr = 0x4BA17930,
                weapons = 0xE249F4,
                cutscenesArray = new uint[] { 0x409AE5BC, 0x409AE620, 0x409AE6F4, 0x409AE784, 0x409AE7B4, 0x409AE814, 0x409AE844, 0x409AE874, 0x409AED84, 0x409AEDE4 }
            };
            gameVersion["BCES00511"] = new Addresses
            {
                gameState1Ptr = 0xFBAE48,
                cutsceneState1Ptr = 0xF6B52C,
                cutsceneState2Ptr = 0x40E96E9C,
                cutsceneState3Ptr = 0x40E96E9C,
                saveFileIDPtr = 0xE473B8,
                timerPtr = 0x40EBADE0,
                boltCount = 0xE25068,
                playerCoords = 0xE24170,
                inputOffset = 0xF6AD48,
                analogOffset = 0xF6ABA4,
                currentPlanet = 0xE897B4,
                azimuthHPPtr = 0x40E89A2C
            };
        }

        private class Addresses
        {
            public uint gameState1Ptr { get; set; }
            public uint cutsceneState1Ptr { get; set; }
            public uint cutsceneState2Ptr { get; set; }
            public uint cutsceneState3Ptr { get; set; }
            public uint saveFileIDPtr { get; set; }
            public uint timerPtr { get; set; }
            public uint mapTimerPtr { get; set; }
            public uint boltCount { get; set; }
            public uint playerCoords { get; set; }
            public uint playerHealth { get; set; }
            public uint inputOffset { get; set; }
            public uint analogOffset { get; set; }
            public uint loadPlanet { get; set; }
            public uint currentPlanet { get; set; }
            public uint azimuthHPPtr { get; set; }
            public uint weapons { get; set; }
            public uint[] cutscenesArray { get; set; }
        }
    }
}
