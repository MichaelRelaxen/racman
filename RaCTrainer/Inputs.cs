using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace racman
{
    public static class Inputs
    {
        public enum Buttons : uint
        {
            l2 = 0x1,
            r2 = 0x2,
            l1 = 0x4,
            r1 = 0x8,
            triangle = 0x10,
            circle = 0x20,
            cross = 0x40,
            square = 0x80,
            select = 0x100,
            l3 = 0x200,
            r3 = 0x400,
            start = 0x800,
            up = 0x1000,
            right = 0x2000,
            down = 0x4000,
            left = 0x8000,
        }

        public static float rx = 0.0f;
        public static float ry = 0.0f;
        public static float lx = 0.0f;
        public static float ly = 0.0f;

        static string gameID = AttachPS3Form.game;

        public static int RawInputs;
        public static List<Buttons> Mask = new List<Buttons>();
        public static List<Buttons> DecodeMask(int mask)
        {
            var buttons = (Buttons)mask;
            var list = new List<Buttons>();

            foreach (Buttons button in Enum.GetValues(typeof(Buttons)))
            {
                if (buttons.HasFlag(button))
                    list.Add(button);
            }
            return list;
        }
    }
}
