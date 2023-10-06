using System;
using System.Collections.Generic;
using Timer = System.Windows.Forms.Timer;

namespace racman
{
    public class RaC4Addresses : IAddresses
    {
        // Input stuff
        public uint inputOffset => 0xB11F30;
        public uint analogOffset => 0xB1210C;

        // Currently not implemented, probably works a bit different in RaC3 anyway.
        public uint levelFlags => throw new System.NotImplementedException();
        public uint miscLevelFlags => throw new System.NotImplementedException();
        public uint infobotFlags => throw new System.NotImplementedException();
        public uint moviesFlags => throw new System.NotImplementedException();

        // Vox HP
        public uint voxHP => 0x449BEAD0;

        // Cutscene
        public uint cutscenePtr = 0xB36DE8;

        public uint boltCount => 0x9C32E8;

        public uint playerCoords => 0x10D7334;

        // In Game (0 in main menu | 1 in game)
        public uint inGame => 0xB1F460;

        // load planet
        public uint loadPlanet => 0x9C3240;

        // current planet   (it's 0 in main menu)
        public uint currentPlanet => 0x119353C;

        public uint tutorialFlags => 0x48613624;
    }
    public class rac4 : IGame, IAutosplitterAvailable
    {
        public Timer fastloadTimer = new Timer();

        public static RaC4Addresses addr = new RaC4Addresses();

        int ghostRatchetSubID = -1;
        public rac4(IPS3API api) : base(api)
        {

        }

        public IEnumerable<(uint addr, uint size)> AutosplitterAddresses => new (uint, uint)[]
        {
            (addr.currentPlanet, 4),    // current planet
            (addr.loadPlanet, 4),       // load planet
            (addr.voxHP, 4),            // Vox HP
            (addr.cutscenePtr, 4),      // cutscene
            (addr.inGame, 4),           // in game boolean
            (addr.tutorialFlags, 4),    // tutorial flags
        };

        public override void ResetLevelFlags()
        {
            throw new NotImplementedException();
        }

        public override void SetFastLoads(bool enabled = false)
        {
            throw new NotImplementedException();
        }

        public override void ToggleInfiniteAmmo(bool toggle = false)
        {
            throw new NotImplementedException();
        }

        public override void SetupFile()
        {
            throw new NotImplementedException();
        }

        public override void CheckInputs(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
