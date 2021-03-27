using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace racman
{
    class rac1
    {
        public static uint BoltCount = 0x969CA0;
        public static uint GhostRatchet = 0x969EAC;
        public static uint Coordinates = 0x969D60;
        public static uint CurrentPlanet = 0x969C70;
        public static uint LoadPlanet = 0xA10700; //destination planet at 0xEE9314 for example set 0000000100000002 to load florana
        public static uint State = 0x96BD64;
        public static uint TitaniumBoltsStart = 0xA0CA34; 
        public static uint UnlockTable = 0x96C140; 
    }
}
