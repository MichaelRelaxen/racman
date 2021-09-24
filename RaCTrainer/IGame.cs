using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timer = System.Windows.Forms.Timer;

namespace racman
{
    public abstract class IGame
    {
        public Ratchetron api { get; }
        public uint boltCount;
        public uint playerCoords;
        public uint inputOffset;
        public uint analogOffset;
        public uint loadPlanet;
        public uint currentPlanet;

        public uint levelFlags;
        public uint miscLevelFlags;
        public uint infobotFlags;
        public uint moviesFlags;
        public uint unlockArray;


        public uint planetIndex;
        bool inputCheck = true;

        public string[] planetsList;

        public int pid;

        public Timer InputsTimer = new Timer();

        public int selectedPositionIndex { get; set; }
        public uint planetToLoad { get; set; }

        protected IGame(Ratchetron api)
        {
            this.api = api;
            this.pid = api.getCurrentPID();
            api.OpenDataChannel();

            InputsTimer.Interval = (int)16.66667;
            InputsTimer.Tick += new EventHandler(CheckInputs);
        }


        public virtual void SavePosition()
        {
            string position = api.ReadMemoryStr(pid, playerCoords, 30);
            func.ChangeFileLines("config.txt", position, planetsList[planetIndex] + "SavedPos" + selectedPositionIndex);
        }
        public virtual void LoadPosition()
        {
            string position = func.GetConfigData("config.txt", planetsList[planetIndex] + "SavedPos" + selectedPositionIndex);
            if (position != "")
            {
                api.WriteMemory(pid, playerCoords, 30, position);
            }
        }

        public virtual void KillYourself()
        {
            api.WriteMemory(pid, playerCoords + 8, 0xC2480000);
        }

        public virtual void LoadPlanet(bool resetFlags = false, bool resetGoldBolts = false)
        {
            if (resetFlags) ResetLevelFlags();

            if (resetGoldBolts) ResetGoldBolts();

            api.WriteMemory(pid, loadPlanet, 8, $"00000001000000{planetToLoad.ToString("X2")}");
        }
        public virtual void ResetGoldBolts()
        {
            // lol
        }

        public abstract void ResetLevelFlags();
        public abstract void ToggleFastLoad(bool toggle = false);

        public abstract void ToggleInfiniteAmmo(bool toggle = false);

        public virtual void SetBoltCount(string bolts)
        {
            api.WriteMemory(pid, boltCount, uint.Parse(bolts));
        }

        public virtual void SetupMemorySubs()
        {
            int buttonMaskSubID = api.SubMemory(pid, inputOffset, 4, (value) =>
            {
                Inputs.RawInputs = BitConverter.ToInt32(value, 0);
                Inputs.Mask = Inputs.DecodeMask(Inputs.RawInputs);
            });

            int analogRSubID = api.SubMemory(pid, analogOffset, 8, (value) =>
            {
                Inputs.ry = BitConverter.ToSingle(value, 0);
                Inputs.rx = BitConverter.ToSingle(value, 4);
            });

            int analogYSubID = api.SubMemory(pid, analogOffset + 8, 8, (value) =>
            {
                Inputs.ly = BitConverter.ToSingle(value, 0);
                Inputs.lx = BitConverter.ToSingle(value, 4);
            });

            int planetIndexSubID = api.SubMemory(pid, currentPlanet, 4, (value) =>
            {
                planetIndex = BitConverter.ToUInt32(value, 0);
            });
        }

        public virtual void CheckInputs(object sender, EventArgs e)
        {
            if (Inputs.RawInputs == 0xB && inputCheck)
            {
                SavePosition();
                inputCheck = false;
            }
            if (Inputs.RawInputs == 0x7 && inputCheck)
            {
                LoadPosition();
                inputCheck = false;
            }
            if (Inputs.RawInputs == 0x5 && inputCheck)
            {
                KillYourself();
                inputCheck = false;
            }
            if (Inputs.RawInputs == 0x600 & inputCheck)
            {
                LoadPlanet();
                inputCheck = false;
            }
            if (Inputs.RawInputs == 0x00 & !inputCheck)
            {
                inputCheck = true;
            }
        }
    }
}