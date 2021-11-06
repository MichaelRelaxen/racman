using System;
using System.Diagnostics;
using System.Linq;
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

        public uint boltCount => throw new NotImplementedException();

        public uint playerCoords => throw new NotImplementedException();

        public uint loadPlanet => throw new NotImplementedException();

        public uint currentPlanet => 0xB11F30; // just the input offset for now. I don't care this shit isnt used atm anyway lol
    }
    public class rac4 : IGame
    {
        public Timer fastloadTimer = new Timer();

        public static RaC4Addresses addr = new RaC4Addresses();

        int ghostRatchetSubID = -1;
        public rac4(Ratchetron api) : base(api)
        {
        }

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