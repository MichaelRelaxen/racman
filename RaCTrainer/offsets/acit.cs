using System;
using System.Collections.Generic;
using System.Linq;

namespace racman
{
    public class ACITAddresses : IAddresses
    {

        private Dictionary<string, VersionAddr> gameVersion = new Dictionary<string, VersionAddr>();

        public ACITAddresses(string gameID)
        {
            InitializeAddresses();

            if (gameVersion.ContainsKey(gameID))
            {
                GameID = gameID;
            }
            else
            {
                throw new ArgumentException("Selected GameID is not available.");
            }
        }

        public string GameID { get; }

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
            gameVersion["BCUS98124"] = new VersionAddr
            {
                gameState1Ptr = 0xFBADC8, cutsceneState1Ptr = 0xF6B4AC, cutsceneState2Ptr = 0x40E9651C,
                cutsceneState3Ptr = 0xDF027C, saveFileIDPtr = 0xE47338, timerPtr = 0x40EBA460,
                boltCount = 0xE24FE8, playerCoords = 0xE240F0, currentPlanet = 0xEF7E90, azimuthHPPtr = 0x40E890AC
            };
            gameVersion["NPUA80966"] = new VersionAddr
            {
                gameState1Ptr = 0x00000, cutsceneState1Ptr = 0x00000, cutsceneState2Ptr = 0x00000,
                cutsceneState3Ptr = 0x00000, saveFileIDPtr = 0x00000, timerPtr = 0x00000,
                boltCount = 0x00000, playerCoords = 0x00000, inputOffset = 0x00000, analogOffset = 0x00000,
                currentPlanet = 0x00000, azimuthHPPtr = 0x00000
            };
        }

        private class VersionAddr
        {
            public uint gameState1Ptr { get; set; }
            public uint cutsceneState1Ptr { get; set; }
            public uint cutsceneState2Ptr { get; set; }
            public uint cutsceneState3Ptr { get; set; }
            public uint saveFileIDPtr { get; set; }
            public uint timerPtr { get; set; }
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

        public acit(Ratchetron api) : base(api)
        {
            addr = new ACITAddresses("NPUA80966");
            Console.WriteLine(addr.GameID);
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

        public bool hasInputDisplay()
        {
            return addr.inputOffset > 0 && addr.analogOffset > 0 && addr.currentPlanet > 0;
        }

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
