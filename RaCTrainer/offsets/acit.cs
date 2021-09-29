using System;
using System.Collections.Generic;
using System.Linq;

namespace racman
{
    public class ACITAddresses : IAddresses
    {
        public uint planetFrameCount => 0xD3F214;
        public uint weirdTimerThingy => 0xEB3C04;
        public uint gameState => 0xF70DEC;
        public uint isPaused => 0xEE7635;
        public uint isPaused2 => 0xF5B4AD;
        public uint boltCount => throw new NotImplementedException();

        public uint playerCoords => throw new NotImplementedException();

        public uint playerHealth => throw new NotImplementedException();

        public uint inputOffset => throw new NotImplementedException();

        public uint analogOffset => throw new NotImplementedException();

        public uint loadPlanet => throw new NotImplementedException();

        public uint currentPlanet => throw new NotImplementedException();

        public uint levelFlags => throw new NotImplementedException();

        public uint miscLevelFlags => throw new NotImplementedException();

        public uint infobotFlags => throw new NotImplementedException();

        public uint moviesFlags => throw new NotImplementedException();

        public uint unlockArray => throw new NotImplementedException();
    }

    public class acit : IGame
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
    }
}
