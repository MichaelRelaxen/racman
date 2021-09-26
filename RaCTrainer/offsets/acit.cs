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
