using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace racman
{
    public class RaC2Addresses : IAddresses
    {
        // Current bolt count
        public uint boltCount => 0x1329A90;

        // Ratchet's coordinates
        public uint playerCoords => 0x147F260;

        // Controller inputs mask address
        public uint inputOffset => 0x147A430;

        // Controller analog sticks address
        public uint analogOffset => 0x147A60C;

        // First 0x4 for if planet should be loaded, the 0x4 after for planet to load.
        public uint loadPlanet => 0x156B050;

        // Currently loaded planet.
        public uint currentPlanet => 0x1329A3C;

        //Frames until "Ghost Ratchet" runs out.
        public uint ghostTimer => 0x147F3ce;

        // Current raritanium
        public uint currentRaritanium => 0x1329A94;
    }

    public class rac2 : IGame, IAutosplitterAvailable
    {
        public static RaC2Addresses addr = new RaC2Addresses();

        public rac2(Ratchetron api) : base(api)
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
        private int ghostRatchetSubID = -1;

        public IEnumerable<(uint addr, uint size)> AutosplitterAddresses => new (uint, uint)[]
        {
            (0x0156B064, 4), // Game state
            (0x01481474, 4), // Ratchet state
            (0x43BD22B0, 4), // Protopet's health (Float)
            (0x1329A3C, 4), // current planet
            (0x156B054, 4) // destination planet
        };


        /// <summary>
        /// Resets level flag of destination planet
        /// </summary>
        public override void ResetLevelFlags()
        {

        }


        public override void ResetGoldBolts(uint planetIndex)
        {   }

        /// <summary>
        /// Ghost ratchet works by having a frame countdown, we hard enable ghost ratchet by freezing the frame countdown to 10.
        /// </summary>
        /// <param name="enabled">if true freezes frame countdown to 10, if false releases the freeze</param>
        public void SetGhostRatchet(bool enabled)
        {
            if (enabled)
            {
                ghostRatchetSubID = api.FreezeMemory(pid, addr.ghostTimer, 10);
            }
            else
            {
                api.ReleaseSubID(ghostRatchetSubID);
            }
        }

        /// <summary>
        /// Overwrites game code that decreases ammo when you use a weapon
        /// </summary>
        /// <param name="toggle">Overwrites ammo decreasement code with nops on true, restores original game code on false</param>
        public override void ToggleInfiniteAmmo(bool toggle = false)
        {

        }

        public override void SetupFile()
        {
            throw new NotImplementedException();
        }

        public override void CheckInputs(object sender, EventArgs e)
        {
            if (Inputs.RawInputs == ConfigureCombos.saveCombo && inputCheck)
            {
                SavePosition();
                inputCheck = false;
            }
            if (Inputs.RawInputs == ConfigureCombos.loadCombo && inputCheck)
            {
                LoadPosition();
                inputCheck = false;
            }
            if (Inputs.RawInputs == ConfigureCombos.dieCombo && inputCheck)
            {
                KillYourself();
                inputCheck = false;
            }
            if (Inputs.RawInputs == ConfigureCombos.loadPlanetCombo & inputCheck)
            {
                LoadPlanet();
                inputCheck = false;
            }
            if (Inputs.RawInputs == 0x00 & !inputCheck)
            {
                inputCheck = true;
            }
        }

        public override void SetFastLoads(bool enabled = false)
        {
            throw new NotImplementedException();
        }
    }
}
