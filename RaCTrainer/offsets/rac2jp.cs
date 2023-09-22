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

        // Number of time the slot machines on Maktar have been hit.
        public uint slotsManip => 0x148716F;

        // idk why these are all required
        public uint playerCoords => throw new NotImplementedException();

        public uint inputOffset => throw new NotImplementedException();

        public uint analogOffset => throw new NotImplementedException();

        public uint loadPlanet => throw new NotImplementedException();

        public uint currentPlanet => throw new NotImplementedException();
    }

    public class rac2jp : IGame
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
