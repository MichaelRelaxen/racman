using System;
using System.Collections.Generic;
using System.Linq;

namespace racman.offsets.ACIT
{
    public class ACITTimer
    {
        private class PlanetTimer
        {
            public uint ID { get; private set; }
            public uint Address { get; private set; }
            public uint SecondaryAddress { get; private set; }
            public uint IGT { get; set; }
            public uint ATimer { get; private set; }
            public uint OldATimer { get; private set; }

            public PlanetTimer(uint id, uint address, uint secondaryAddress)
            {
                ID = id;
                Address = address;
                SecondaryAddress = secondaryAddress;
                IGT = 0;
                ATimer = 0;
                OldATimer = 0;
            }

            public void UpdateTimer(uint timer)
            {
                OldATimer = ATimer;
                ATimer = timer;
            }

            public void Reset()
            {
                IGT = 0;
                ATimer = 0;
                OldATimer = 0;
            }
        }

        //TODO: for optimization, read only the planet where the player is
        private IPS3API API;
        private int PID;
        private static uint timersCount = 7;    //TODO: set properly
        private static uint timersOffset = 0x06C8;

        private List<PlanetTimer> Timers = new List<PlanetTimer>();

        public ACITTimer(IPS3API api, int pid, uint baseAddress, uint secondaryAddress)
        {
            //TODO: instead of using IPS3API and IGame, use an interface
            API = api;
            PID = pid;
            for (uint i = 0; i < timersCount; i++)
            {
                Timers.Add(new PlanetTimer(i+1, baseAddress + timersOffset * i, secondaryAddress + timersOffset * i));
            }
        }

        public void UpdateTimer(uint planetID)
        {
            foreach (PlanetTimer planetTimer in Timers)
            {
                if (planetTimer.ID == planetID)
                {
                    uint primaryTimer = (uint)BitConverter.ToSingle(API.ReadMemory(PID, planetTimer.Address, 4).Reverse().ToArray(), 0);
                    uint secondaryTimer = (uint)BitConverter.ToSingle(API.ReadMemory(PID, planetTimer.SecondaryAddress, 4).Reverse().ToArray(), 0);

                    // update the timer
                    planetTimer.UpdateTimer(primaryTimer);

                    if (planetTimer.ATimer == planetTimer.OldATimer)
                    {
                        return;
                    }

                    // if the game saved with time penalty. In this case, it checks if the primary
                    // timer was greater than the secondary timer and now it is equal.
                    if (planetTimer.OldATimer > secondaryTimer && primaryTimer == secondaryTimer)
                    {
                        planetTimer.IGT += planetTimer.OldATimer - primaryTimer;
                        Console.WriteLine("Death");
                        return;
                    }

                    // if the game saved without time penalty
                    if (primaryTimer == secondaryTimer)
                    {
                        planetTimer.IGT += planetTimer.ATimer - planetTimer.OldATimer;
                        Console.WriteLine("Save");
                        return;
                    }
                }
            }
        }

        /// <summary>
        /// Gets the In Game Timer.
        /// </summary>
        /// <returns> The timer. </returns>
        public uint GetTimer()
        {
            uint timer = 0;
            foreach (PlanetTimer planetTimer in Timers)
            {
                timer += planetTimer.IGT;
            }
            return timer;
        }

        /// <summary>
        /// Checks if a new run was detected.
        /// </summary>
        /// <returns> True if a new run was detected, false otherwise. </returns>
        public bool NewRun()
        {
            foreach (PlanetTimer planetTimer in Timers)
            {
                // Check if GC1 timer was greater than 0 and now it is 0
                if (planetTimer.ATimer == 0 && planetTimer.OldATimer > 0)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Resets all timers.
        /// </summary>
        public void ResetTimer()
        {
            foreach (PlanetTimer planetTimer in Timers)
            {
                planetTimer.Reset();
            }
        }
    }

    
}
