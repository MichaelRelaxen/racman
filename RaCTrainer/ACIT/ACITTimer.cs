using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;

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

        private IPS3API MemoryReader;
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
        public ACITTimer(IPS3API memroyReader, uint baseAddress, uint count, uint offset)
        {
            MemoryReader = memroyReader;
            TimersCount = count;
            TimersOffset = offset;

            for (uint i = 0; i < TimersCount; i++)
            {
                Timers.Add(new InnerTimer(baseAddress + TimersOffset * i));
            }

            SetupTimer();
        }

        private void SetupTimer()
        {
            foreach (InnerTimer timer in Timers)
            {
                timer.IGT = BitConverter.ToUInt32(MemoryReader.ReadMemory(MemoryReader.getCurrentPID(), timer.Address, 0x04).Reverse().ToArray(), 0);
                MemoryReader.SubMemory(MemoryReader.getCurrentPID(), timer.Address, 0x04, (value) =>
                {
                    timer.IGT = BitConverter.ToUInt32(value, 0);
                });
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
