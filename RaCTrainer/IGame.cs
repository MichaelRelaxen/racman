using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace racman
{
    public interface IAddresses
    {
        uint boltCount { get; }
        uint playerCoords { get; }
        uint inputOffset { get; }
        uint analogOffset { get; }
        uint loadPlanet { get; }
        uint currentPlanet { get; }
    }

    public abstract class IGame
    {
        public IPS3API api { get; }

        public uint planetIndex;
        public bool inputCheck = true;

        public string[] planetsList;
        public float[] coords = new float[3];
        public int pid;

        public Timer InputsTimer = new Timer();

        public int selectedPositionIndex { get; set; }
        public uint planetToLoad { get; set; }

        protected IGame(IPS3API api)
        {
            this.api = api;
            this.pid = api.getCurrentPID();

            if (api is Ratchetron)
            {
                ((Ratchetron)api).OpenDataChannel();
            }

            InputsTimer.Interval = (int)16.66667;
            InputsTimer.Tick += new EventHandler(CheckInputs);
        }

        /// <summary>
        /// Function to get addresses in IGame because static values and stuff, not needed outside of IGame or in any IGame-inherited classes because they just use <ClassName>.addr.<whatever>
        /// </summary>
        /// <returns></returns>
        private IAddresses Addr()
        {
             return (IAddresses)this.GetType().GetField("addr").GetValue(typeof(IAddresses));
        }


        public virtual void SavePosition()
        {
            string position = api.ReadMemoryStr(pid, Addr().playerCoords, 30);
            func.ChangeFileLines("config.txt", position, planetsList[planetIndex] + "SavedPos" + selectedPositionIndex);
        }
        public virtual void LoadPosition()
        {
            string position = func.GetConfigData("config.txt", planetsList[planetIndex] + "SavedPos" + selectedPositionIndex);
            if (position != "")
            {
                api.WriteMemory(pid, Addr().playerCoords, 30, position);
            }
        }

        public virtual void KillYourself()
        {
            api.WriteMemory(pid, Addr().playerCoords + 8, 0xC2480000);
        }

        public virtual void LoadPlanet(bool resetFlags = false, bool resetGoldBolts = false)
        {
            api.WriteMemory(pid, Addr().loadPlanet, 8, $"00000001000000{planetToLoad.ToString("X2")}");

            if (resetFlags) ResetLevelFlags();

            if (resetGoldBolts) ResetGoldBolts(planetToLoad);
        }
        public virtual void ResetGoldBolts(uint planetIndex)
        {
            // lol
        }

        public abstract void ResetLevelFlags();
        public abstract void SetFastLoads(bool enabled = false);

        public abstract void ToggleInfiniteAmmo(bool toggle = false);

        public virtual void SetBoltCount(uint bolts)
        {
            api.WriteMemory(pid, Addr().boltCount, bolts);
        }

        public virtual int Bolts()
        {
            return BitConverter.ToInt32(api.ReadMemory(pid, Addr().boltCount, 4).Reverse().ToArray(), 0);
        }

        public abstract void SetupFile();

        public virtual void SetupInputDisplayMemorySubs()
        {
            SetupInputDisplayMemorySubsButtons();

            SetupInputDisplayMemorySubsAnalogs();

            // Why the FUCK is this here?
            // TODO: Move it. This is a workaround.
            try
            {
                var addr = Addr().currentPlanet;
                int planetIndexSubID = api.SubMemory(pid, addr, 4, (value) =>
                {
                    planetIndex = BitConverter.ToUInt32(value, 0);
                });
            } catch { /* nah */ }
        }

        protected virtual void SetupInputDisplayMemorySubsButtons()
        {
            int buttonMaskSubID = api.SubMemory(pid, Addr().inputOffset, 4, (value) =>
            {
                Inputs.RawInputs = BitConverter.ToInt32(value, 0);
                Inputs.Mask = Inputs.DecodeMask(Inputs.RawInputs);
            });
        }

        protected virtual void SetupInputDisplayMemorySubsAnalogs()
        {
            int analogRSubID = api.SubMemory(pid, Addr().analogOffset, 8, (value) =>
            {
                Inputs.ry = BitConverter.ToSingle(value, 0);
                Inputs.rx = BitConverter.ToSingle(value, 4);
            });

            int analogYSubID = api.SubMemory(pid, Addr().analogOffset + 8, 8, (value) =>
            {
                Inputs.ly = BitConverter.ToSingle(value, 0);
                Inputs.lx = BitConverter.ToSingle(value, 4);
            });
        }

        public virtual void GetPlayerCoordinates()
        {
            int xSubID = api.SubMemory(pid, Addr().playerCoords, 4, (value) =>
            {
                coords[0] = BitConverter.ToSingle(value, 0);
            });
            int ySubID = api.SubMemory(pid, Addr().playerCoords + 4, 4, (value) =>
            {
                coords[1] = BitConverter.ToSingle(value, 0);
            });
            int zSubID = api.SubMemory(pid, Addr().playerCoords + 8, 4, (value) =>
            {
                coords[2] = BitConverter.ToSingle(value, 0);
            });
        }

        // 
        public abstract void CheckInputs(object sender, EventArgs e);

    }
}