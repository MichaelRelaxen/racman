using System;
using System.Collections.Generic;
using System.Linq;

namespace racman
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
                        break;
                    case "BCES00511":
                        IsAutosplitterSupported = true;
                        break;
                    default:
                        IsAutosplitterSupported = false;
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

        private void InitializeAddresses()
        {
            // All addresses are from the US version of the game.

            gameVersion["BCUS98124"] = new Addresses
            {
                gameState1Ptr = 0xFBADC8, cutsceneState1Ptr = 0xF6B4AC, cutsceneState2Ptr = 0x40E9651C,
                cutsceneState3Ptr = 0xDF027C, saveFileIDPtr = 0xE47338, timerPtr = 0x40EBA460,
                boltCount = 0xE24FE8, playerCoords = 0xE240F0, currentPlanet = 0xEF7E90, azimuthHPPtr = 0x40E890AC
            };
            gameVersion["NPUA80966"] = new Addresses
            {
                gameState1Ptr = 0xFBA8C8, cutsceneState1Ptr = 0xF6B3AC, cutsceneState2Ptr = 0x40E9651C,
                cutsceneState3Ptr = 0x4A4E5428, saveFileIDPtr = 0xE472B8, timerPtr = 0x40EBA460,
                boltCount = 0xE24F68, playerCoords = 0xE24070, inputOffset = 0xF6ABC8, analogOffset = 0xF6AA24,
                currentPlanet = 0xE896B4, azimuthHPPtr = 0x40E890AC, mapTimerPtr = 0x4BA17930
            };
            gameVersion["BCES00511"] = new Addresses
            {
                gameState1Ptr = 0xFBAE48, cutsceneState1Ptr = 0xF6B52C, cutsceneState2Ptr = 0x40E96E9C,
                cutsceneState3Ptr = 0x40E96E9C, saveFileIDPtr = 0xE473B8, timerPtr = 0x40EBADE0,
                boltCount = 0xE25068, playerCoords = 0xE24170, inputOffset = 0xF6AD48, analogOffset = 0xF6ABA4,
                currentPlanet = 0xE897B4, azimuthHPPtr = 0x40E89A2C
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
        }
    }

    public class acit : IGame, IAutosplitterAvailable
    {
        public dynamic Planets = new
        {
            agorian_arena = ("Agorian Arena"),
            axiom_city = ("Axion City"),
            front_end = ("Front End"),
            galacton_ship = ("Vorselon Ship"),
            gimlick_valley = ("Gimlick Valey"),
            great_clock_a = ("Great Clock 1"),
            great_clock_b = ("Great Clock 2"),
            great_clock_c = ("Great Clock 3"),
            great_clock_d = ("Great Clock 4"),
            great_clock_e = ("Great Clock 5"),
            insomniac_museum = ("Insomniac Museum"),
            krell_canyon = ("Krell Canyon"),
            molonoth = ("Molonoth Fields"),
            nefarious_statio = ("Nefarious Station"),
            space_sector_1 = ("Space Sector 1"),
            space_sector_2 = ("Space Sector 2"),
            space_sector_3 = ("Space Sector 3"),
            space_sector_4 = ("Space Sector 4"),
            space_sector_5 = ("Space Sector 5"),
            tombli = ("Tombli Outpost"),
            valkyrie_fleet = ("Valkyrie Fleet"),
            zolar_forest = ("Zolar Forest")
        };

        public static ACITAddresses addr;

        public bool HasInputDisplay => addr.inputOffset > 0 && addr.analogOffset > 0 && addr.currentPlanet > 0;
        public bool IsAutosplitterSupported => addr.IsAutosplitterSupported;

        public acit(Ratchetron api) : base(api)
        {
            addr = new ACITAddresses(api.getGameTitleID());
        }

        public IEnumerable<(uint addr, uint size)> AutosplitterAddresses => new (uint, uint)[]
        {
            (addr.currentPlanet, 4),        // current planet
            (addr.gameState1Ptr, 4),        // game state1
            (addr.cutsceneState1Ptr, 4),    // cutscene state1
            (addr.cutsceneState2Ptr, 4),    // cutscene state2
            (addr.cutsceneState3Ptr, 4),    // cutscene state3
            (addr.saveFileIDPtr, 4),        // save file ID
            (addr.boltCount, 4),            // bolt count
            (addr.playerCoords, 4),         // player X coord
            (addr.playerCoords + 0x8, 4),   // player Y coord
            (addr.playerCoords + 0x4, 4),   // player Z coord
            (addr.azimuthHPPtr, 4),         // azimuth HP
            (addr.timerPtr, 4),             // timer
        };

        public override void ResetLevelFlags()
        {
            throw new NotImplementedException();
        }

        public override void SetupFile()
        {
            throw new NotImplementedException();
        }

        public override void SetFastLoads(bool toggle = false)
        {
            throw new NotImplementedException();
        }

        public override void ToggleInfiniteAmmo(bool toggle = false)
        {
            throw new NotImplementedException();
        }

        public override void CheckInputs(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        protected override void SetupInputDisplayMemorySubsAnalogs()
        {
            int analogRSubID = api.SubMemory(pid, addr.analogOffset + 8, 8, (value) =>
            {
                Inputs.ry = BitConverter.ToSingle(value, 0);
                Inputs.rx = BitConverter.ToSingle(value, 4);
            });

            int analogYSubID = api.SubMemory(pid, addr.analogOffset, 8, (value) =>
            {
                Inputs.ly = BitConverter.ToSingle(value, 0);
                Inputs.lx = BitConverter.ToSingle(value, 4);
            });
        }
    }
}
