using racman.offsets;
using racman.offsets.RAC4;
using System;
using System.Collections.Generic;
using Timer = System.Windows.Forms.Timer;

namespace racman
{
    public class RaC4Addresses : IAddresses
    {
        // Input stuff
        public uint inputOffset => 0xB11F30;
        public uint analogOffset => 0xB1210C;

        // Currently not implemented, probably works a bit different in RaC3 anyway.
        public uint levelFlags => throw new System.NotImplementedException();
        public uint miscLevelFlags => throw new System.NotImplementedException();
        public uint infobotFlags => throw new System.NotImplementedException();
        public uint moviesFlags => throw new System.NotImplementedException();

        public uint botsUnlock => 0x9D2775;
        // unlocks are not saved, until the save flag is set to 1
        public uint botsUnlockSave => 0x9C3325;

        // Vox HP
        public uint voxHP => 0x449BEAD0;

        // Cutscene
        public uint cutscenePtr = 0xB36DE8;

        public uint boltCount => 0x9C32E8;

        public uint playerCoords => 0x10D7334;

        // In Game (0 in main menu | 1 in game)
        public uint inGame => 0xB1F460;

        // load planet
        public uint loadPlanet => 0x9C3240;

        // current planet   (it's 0 in main menu)
        public uint currentPlanet => 0x119353C;

        // 0 = tutorial not completed | 1 = tutorial completed
        public uint tutorialFlags => 0xB1F46C;

        // 0 = not loading, 1 = loading
        public uint isLoading => 0xB0FD84;

        // 0 = third person, 1 = lock-strafe, 2 = first person
        public uint cameraMode => 0x9C287C;
    }
    public class rac4 : IGame, IAutosplitterAvailable
    {
        public Timer fastloadTimer = new Timer();

        public static RaC4Addresses addr = new RaC4Addresses();

        private List<BotsUnlocks> botsUnlocks;

        int ghostRatchetSubID = -1;
        public rac4(IPS3API api) : base(api)
        {
            botsUnlocks = BotsUnlocksFactory.GetUpgrades();
        }

        public IEnumerable<(uint addr, uint size)> AutosplitterAddresses => new (uint, uint)[]
        {
            (addr.currentPlanet, 4),    // current planet
            (addr.loadPlanet, 4),       // load planet
            (addr.voxHP, 4),            // Vox HP
            (addr.cutscenePtr, 4),      // cutscene
            (addr.inGame, 4),           // in game boolean
            (addr.tutorialFlags, 4),    // tutorial flags
            (addr.isLoading, 4),        // loading boolean
        };

        public void UpdateUnlocks()
        {
            byte[] memory = api.ReadMemory(pid, addr.botsUnlock, ACITWeaponFactory.weaponCount * ACITWeaponFactory.weaponMemoryLenght);

            for (int i = 0; i < botsUnlocks.Count; i++)
            {
                botsUnlocks[i].IsUnlocked = BitConverter.ToBoolean(memory, i);
            }
        }

        public void SetUnlockState(BotsUnlocks unlock, bool state)
        {
            api.WriteMemory(pid, addr.botsUnlock + unlock.index, BitConverter.GetBytes(state));
            api.WriteMemory(pid, addr.botsUnlockSave + unlock.index, BitConverter.GetBytes(state));
        }

        public List<BotsUnlocks> GetBotsUnlocks()
        {
            UpdateUnlocks();
            return botsUnlocks;
        }

        public override void ResetLevelFlags()
        {
            throw new NotImplementedException();
        }

        public override void SetFastLoads(bool enabled = false)
        {
            throw new NotImplementedException();
        }

        public override void ToggleInfiniteAmmo(bool toggle = false)
        {
            throw new NotImplementedException();
        }

        public override void SetupFile()
        {
            throw new NotImplementedException();
        }

        public override void CheckInputs(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
