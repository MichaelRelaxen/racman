using System;
using System.Collections.Generic;

namespace racman
{
    public static class Inputs
    {
        private static class ButtonSelector
        {
            private enum OgBtns : uint
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

            private enum AciBtns : uint
            {
                l2 = 0x1,
                r2 = 0x2,
                l1 = 0x4,
                r1 = 0x8,
                triangle = 0x10,
                circle = 0x20,
                cross = 0x40,
                square = 0x80,
                select = 0x10000,
                l3 = 0x20000,
                r3 = 0x40000,
                start = 0x80000,
                up = 0x100000,
                right = 0x200000,
                down = 0x400000,
                left = 0x800000,
            }

            private static Dictionary<OgBtns, Buttons> ogToButtonsMapping = new Dictionary<OgBtns, Buttons>
            {
                { OgBtns.l2, Buttons.l2 },
                { OgBtns.r2, Buttons.r2 },
                { OgBtns.l1, Buttons.l1 },
                { OgBtns.r1, Buttons.r1 },
                { OgBtns.triangle, Buttons.triangle },
                { OgBtns.circle, Buttons.circle },
                { OgBtns.cross, Buttons.cross },
                { OgBtns.square, Buttons.square },
                { OgBtns.select, Buttons.select },
                { OgBtns.l3, Buttons.l3 },
                { OgBtns.r3, Buttons.r3 },
                { OgBtns.start, Buttons.start },
                { OgBtns.up, Buttons.up },
                { OgBtns.right, Buttons.right },
                { OgBtns.down, Buttons.down },
                { OgBtns.left, Buttons.left },
            };

            private static Dictionary<AciBtns, Buttons> aciToButtonsMapping = new Dictionary<AciBtns, Buttons>
            {
                { AciBtns.triangle, Buttons.triangle },
                { AciBtns.circle, Buttons.circle },
                { AciBtns.cross, Buttons.cross },
                { AciBtns.square, Buttons.square },
                { AciBtns.l2, Buttons.l2 },
                { AciBtns.r2, Buttons.r2 },
                { AciBtns.l1, Buttons.l1 },
                { AciBtns.r1, Buttons.r1 },
                { AciBtns.select, Buttons.select },
                { AciBtns.l3, Buttons.l3 },
                { AciBtns.r3, Buttons.r3 },
                { AciBtns.start, Buttons.start },
                { AciBtns.up, Buttons.up },
                { AciBtns.right, Buttons.right },
                { AciBtns.down, Buttons.down },
                { AciBtns.left, Buttons.left },
            };

            private static Buttons ConvertToButtons(OgBtns ogBtn)
            {
                if (ogToButtonsMapping.TryGetValue(ogBtn, out Buttons convertedBtn))
                {
                    return convertedBtn;
                }
                throw new ArgumentException("Conversion not found.");
            }

            private static Buttons ConvertToButtons(AciBtns aciBtn)
            {
                if (aciToButtonsMapping.TryGetValue(aciBtn, out Buttons convertedBtn))
                {
                    return convertedBtn;
                }
                throw new ArgumentException("Conversion not found.");
            }

            /// <summary>
            /// Returns a list of buttons that are pressed.
            /// </summary>
            public static List<Buttons> GetButtons(uint mask)
            {
                Type enumType = (gameName == "ACIT") ? typeof(AciBtns) : typeof(OgBtns);
                var list = new List<Buttons>();

                foreach (var button in Enum.GetValues(enumType))
                {
                    var buttonValue = Convert.ToUInt32(button);

                    if (buttonValue != 0 && (mask & buttonValue) != 0)
                    {
                        list.Add(enumType == typeof(OgBtns) ? ConvertToButtons((OgBtns)button) : ConvertToButtons((AciBtns)button));
                    }
                }

                return list;
            }
        }

        public enum Buttons : uint
        {
            l2,
            r2,
            l1,
            r1,
            triangle,
            circle,
            cross,
            square,
            select,
            l3,
            r3,
            start,
            up,
            right,
            down,
            left,
        }

        public static float rx = 0.0f;
        public static float ry = 0.0f;
        public static float lx = 0.0f;
        public static float ly = 0.0f;

        static string gameID = AttachPS3Form.game;
        static string gameName = AttachPS3Form.gameName;

        public static int RawInputs;
        public static List<Buttons> Mask = new List<Buttons>();
        public static List<Buttons> DecodeMask(int mask)
        {
            return ButtonSelector.GetButtons((uint)mask);
        }
    }
}
