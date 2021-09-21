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

        static uint mask_offset;
        static uint analog_offset;

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
        public static void GetInputs()
        {
            if (gameID == "NPEA00385")
            {
                mask_offset = 0x964AF0;
                analog_offset = 0x964A40;
            }
            if (gameID == "NPEA00387")
            {
                mask_offset = 0xD99370;
                analog_offset = 0xD9954C;
            }

            int buttonMaskSubID = ((Ratchetron)func.api).SubMemory(AttachPS3Form.pid, mask_offset, 4, (value) =>
            {
                RawInputs = BitConverter.ToInt32(value, 0);
                Mask = DecodeMask(RawInputs);
            });

            int analogRSubID = ((Ratchetron)func.api).SubMemory(AttachPS3Form.pid, analog_offset, 8, (value) =>
            {
                ry = BitConverter.ToSingle(value, 0);
                rx = BitConverter.ToSingle(value, 4);
            });

            int analogYSubID = ((Ratchetron)func.api).SubMemory(AttachPS3Form.pid, analog_offset + 8, 8, (value) =>
            {
                ly = BitConverter.ToSingle(value, 0);
                lx = BitConverter.ToSingle(value, 4);
            });
        }


    }
}
