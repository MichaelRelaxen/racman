using racman.offsets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace racman
{
    public enum TodGameType
    {
        PALDL,
        PALDisc
    }

    public class ToD : IGame
    {
        TodGameType gameType;

        public ToD(IPS3API api, TodGameType game) : base(api)
        {
            gameType = game;
        }

        public string Version
        {
            get
            {
                if (gameType == TodGameType.PALDL)
                    return "NPEA00452";
                else
                    return "BCES00052";
            }
        }

        public uint SavePlanetId
        {
            get
            {
                if (gameType == TodGameType.PALDL) 
                    return 0x1029C55B;
                else 
                    return 0x10280208;
            }
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