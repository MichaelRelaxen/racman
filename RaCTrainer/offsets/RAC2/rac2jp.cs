using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;

namespace racman
{
    public class RaC2JPAddresses : IAddresses
    {
        public uint boltCount => 0x1329E80;

        public uint currentRaritanium => 0x1329E87;

        // Luck table entry to get triple bolts on the slots machines.
        // (2nd element in int[6] at 0x1391020)
        public uint pBolts => 0x1391024;
        // Luck table entry to get jackpot on the slots machines.
        // After 38 attempts, this is increased by 40 (and pBolts is decreased by 40)
        public uint pJackpot => 0x1391034;

        // Number of times the slots have been hit.
        public uint slotsHits => 0x148716F;

        // idk why these are all required
        public uint playerCoords => throw new NotImplementedException();

        public uint inputOffset => 0x0147fdf0;

        public uint analogOffset => 0x0147fd40;

        // First 0x4 for if planet should be loaded, the 0x4 after for planet to load.
        public uint loadPlanet => 0x01570A10;

        public uint currentPlanet => 0x01329E2C;
    }

    public class rac2jp : IGame, IAutosplitterAvailable
    {
        public static RaC2JPAddresses addr = new RaC2JPAddresses();

        public rac2jp(IPS3API api) : base(api)
        {
            this.planetsList = new string[] {
                "Aranos",
                "Oozla",
                "Maktar",
                "Endako",
                "Barlow",
                "Feltzin",
                "Notak",
                "Siberius",
                "Tabora",
                "Dobbo",
                "Hrugis",
                "Joba",
                "Todano",
                "Boldan",
                "Aranos2",
                "Gorn",
                "Snivelak",
                "Smolg",
                "Damosel",
                "Grelbin",
                "Yeedil",
                "InsomniacMuseum",
                "DobboOrbit",
                "DamoselOrbit",
                "SlimCognito",
                "Wupash",
                "JammingArray",
            };
        }

        public IEnumerable<(uint addr, uint size)> AutosplitterAddresses => new (uint, uint)[]
        {
            // (0x0156B064, 4), // Game state
            (0x1486E34, 4), // Ratchet state
            // (0x0133EE7C, 4), // Protopet's health bar (Float, ranges 0-1)
            (0x01329E2C, 4), // current planet
            // (0x156B054, 4) // destination planet
        };

        public override void CheckInputs(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        public override void ResetLevelFlags()
        {
            throw new NotImplementedException();
        }

        public override void SetFastLoads(bool enabled = false)
        {
            throw new NotImplementedException();
        }

        public override void SetupFile()
        {
            throw new NotImplementedException();
        }

        public override void ToggleInfiniteAmmo(bool toggle = false)
        {
            throw new NotImplementedException();
        }
    }
}
