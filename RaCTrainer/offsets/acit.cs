using System;
using System.Collections.Generic;
using System.Linq;

namespace racman
{
    public class ACITAddresses : IAddresses
    {
        // (0 = in game, 1 = in main menu, 2 = in pause) (NOTE: first pause will result in a 1 for a second)
        public uint gameState1Ptr = 0xFBAE48;

        // (6 = in main menu, 7 = in game)
        public uint gameState2Ptr = 0x4027CF70;

        // timer
        public uint timerPtr = 0x40EBADE0;         

        // Current bolt count
        public uint boltCount => 0xE25068;

        // Ratchet's coordinates
        public uint playerCoords => 0xE24170;

        // The player's current health.
        public uint playerHealth => throw new NotImplementedException();

        // Controller inputs mask address
        public uint inputOffset => 0xF6AD48;

        // Controller analog sticks address
        public uint analogOffset => 0xF6ABA4;

        public uint loadPlanet => throw new NotImplementedException();

        // Currently loaded planet.
        public uint currentPlanet => 0xE897B4;

        // Azimuth HP
        public uint azimuthHPPtr = 0x40E89A2C;

        public uint levelFlags => throw new NotImplementedException();

        public uint miscLevelFlags => throw new NotImplementedException();

        public uint infobotFlags => throw new NotImplementedException();

        public uint moviesFlags => throw new NotImplementedException();

        public uint unlockArray => throw new NotImplementedException();
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

        public static ACITAddresses addr = new ACITAddresses();

        public acit(Ratchetron api) : base(api)
        {

        }

        public IEnumerable<(uint addr, uint size)> AutosplitterAddresses => new (uint, uint)[]
        {
            (addr.currentPlanet, 4),        // current planet
            (addr.gameState1Ptr, 4),        // game state1
            (addr.gameState2Ptr, 4),        // game state2
            (addr.boltCount, 4),            // bolt count
            (addr.playerCoords, 4),         // player X coord
            (addr.playerCoords + 0x8, 4),   // player Y coord
            (addr.playerCoords + 0x4, 4),   // player Z coord
            (addr.azimuthHPPtr, 4),          // azimuth HP
            (addr.timerPtr, 4),             // timer0
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
