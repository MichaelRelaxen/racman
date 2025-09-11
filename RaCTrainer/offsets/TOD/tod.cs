using racman.offsets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace racman
{
    public class tod : IGame, IAutosplitterAvailable
    {
        public class ToDAddresses : IAddresses
        {
            public uint savePlanetId => 0x1029C55B;
            public uint loadScreenType => 0x102034FB;

            public uint boltCount => 0x1020C28C; // i think so
            public uint playerCoords => throw new NotImplementedException();
            public uint inputOffset => throw new NotImplementedException();
            public uint analogOffset => throw new NotImplementedException();
            public uint loadPlanet => throw new NotImplementedException();
            public uint currentPlanet => savePlanetId;

            public uint mobyInstances => throw new NotImplementedException();
        }

        public static ToDAddresses addr = new ToDAddresses();

        public IEnumerable<(uint addr, uint size)> AutosplitterAddresses => new (uint, uint)[]
        {
            (addr.savePlanetId, 1),
            (addr.loadScreenType, 1)
        };

        public tod(IPS3API api) : base(api)
        {

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

        public override void CheckPlanetForDiscordRPC(object sender = null, EventArgs e = null)
        {
            throw new NotImplementedException();
        }
    }
}