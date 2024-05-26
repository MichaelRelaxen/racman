using System;
using System.Collections.Generic;
using System.Linq;

namespace racman.offsets.ACIT
{
    public class ACITTimer
    {
        private class InnerTimer
        {
            public uint Address { get; private set; }
            public uint IGT { get; set; }

            public InnerTimer(uint address)
            {
                Address = address;
                IGT = 0;
            }
        }

        private IReadMemory MemoryReader;
        private static uint TimersCount;
        private static uint TimersOffset;

        private List<InnerTimer> Timers = new List<InnerTimer>();

        /// <summary>
        /// Constructor for the ACITTimer class.
        /// </summary>
        /// <param name="memroyReader"> The memory reader. </param>
        /// <param name="baseAddress"> The base address of the timer. </param>
        /// <param name="count"> The number of timers. </param>
        /// <param name="offset"> The offset between each timer. </param>
        public ACITTimer(IReadMemory memroyReader, uint baseAddress, uint count, uint offset)
        {
            MemoryReader = memroyReader;
            TimersCount = count;
            TimersOffset = offset;

            for (uint i = 0; i < TimersCount; i++)
            {
                Timers.Add(new InnerTimer(baseAddress + TimersOffset * i));
            }
        }

        /// <summary>
        /// Updates every timer.
        /// </summary>
        public void UpdateTimer()
        {
            foreach (InnerTimer timer in Timers)
            {
                uint t = (uint)BitConverter.ToInt32(MemoryReader.ReadMemory(timer.Address, 4).Reverse().ToArray(), 0);

                timer.IGT = t;
            }
        }

        /// <summary>
        /// Gets the In Game Timer.
        /// </summary>
        /// <returns> The timer. </returns>
        public uint GetTimer()
        {
            uint t = 0;
            foreach (InnerTimer timer in Timers)
            {
                t += timer.IGT;
            }
            return t;
        }
    }
}
