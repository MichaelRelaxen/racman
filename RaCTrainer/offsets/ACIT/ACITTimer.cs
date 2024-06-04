using System;
using System.Collections.Generic;
using System.Linq;

namespace racman.offsets.ACIT
{
    /// <summary>
    /// The IGT in ACIT is a sum of multiple timers stored in memory.
    /// This addressess are divided in 3 memory groups , each one with a different
    /// number of timers with an offset of 0x04 between them (cuz they are 4 bytes long).
    /// If you want to read the IGT, you have to read every 300+ timers every time, but
    /// it is very crashy, so the ChunkSizeProportion limits the number of timers to be read
    /// every time the timer is updated.
    /// </summary>
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

        /// <summary>
        /// Stores every timer.
        /// </summary>
        private List<InnerTimer> Timers = new List<InnerTimer>();
        private uint AnilizedIndex = 0;
        private uint MaxChunkSize = 0;

        /// <summary>
        /// Constructor for the ACITTimer class.
        /// </summary>
        /// <param name="memroyReader"> The memory reader. </param>
        /// <param name="baseAddress"> The base address of the timer. </param>
        /// <param name="count"> The number of timers. </param>
        /// <param name="offset"> The offset between each timer. </param>
        /// <param name="chunkSizeProportion"> The proportion of the chunk size.
        /// Default is 1, which means that there is only one chunk.
        /// This value must be between 0 and 1. </param>
        public ACITTimer(IReadMemory memroyReader, uint baseAddress, uint count, uint offset, double chunkSizeProportion=1)
        {
            MemoryReader = memroyReader;
            TimersCount = count;
            TimersOffset = offset;
            if (chunkSizeProportion > 1 || chunkSizeProportion < 0)
            {
                throw new ArgumentOutOfRangeException("The chunk size proportion must be between 0 and 1.");
            }
            MaxChunkSize = (uint)(TimersCount * chunkSizeProportion);

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
            for (uint i = 0; i < MaxChunkSize; i++)
            {
                uint t = (uint)BitConverter.ToInt32(MemoryReader.ReadMemory(Timers[(int)AnilizedIndex].Address, 4).Reverse().ToArray(), 0);
                Timers[(int)AnilizedIndex].IGT = t;
                AnilizedIndex++;

                if (AnilizedIndex >= Timers.Count)
                {
                    AnilizedIndex = 0;
                    break;
                }
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
