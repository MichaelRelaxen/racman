using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace racman
{
    public abstract class IGame
    {
        public Ratchetron api { get; }
        public uint boltCount;
        public uint playerCoords;

        public string[] planetsList;

        int pid;

        protected IGame(Ratchetron api)
        {
            this.api = api;
            this.pid = api.getCurrentPID();
            api.OpenDataChannel();
        }

        public virtual void SavePosition(int planetIndex, string selectedPosition)
        {
            string position = api.ReadMemoryStr(pid, playerCoords, 30);
            func.ChangeFileLines("config.txt", position, planetsList[planetIndex] + "SavedPos" + selectedPosition);
        }
        public virtual void LoadPosition(int planetIndex, string selectedPosition)
        {
            string position = func.GetConfigData("config.txt", planetsList[planetIndex] + "SavedPos" + selectedPosition);
            if (position != "")
            {
                api.WriteMemory(pid, playerCoords, 30, position);
            }
        }

        public virtual void KillYourself()
        {
            api.WriteMemory(pid, playerCoords + 8, 0xC2480000);
        }

        public abstract void LoadPlanet();

        public abstract void ToggleFastLoad();

        public virtual void SetBoltCount(string bolts)
        {
            api.WriteMemory(pid, boltCount, 4, uint.Parse(bolts).ToString("X8"));
        }

    }
}
