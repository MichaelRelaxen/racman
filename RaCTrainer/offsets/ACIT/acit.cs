using racman.offsets;
using System;
using System.Collections.Generic;
using racman.offsets.ACIT;
using System.Threading;
using System.Linq;

namespace racman
{
    public class acit : IGame, IAutosplitterAvailable
    {
        public static ACITAddresses addr;

        public bool HasInputDisplay => addr.inputOffset > 0 && addr.analogOffset > 0 && addr.currentPlanet > 0;
        public bool IsAutosplitterSupported => addr.IsAutosplitterSupported;
        public bool IsSelfKillSupported => addr.IsSelfKillSupported;
        public bool HasWeaponUnlock => addr.weapons > 0;
        public bool canRemoveCutscenes => addr.cutscenesArray != null && addr.cutscenesArray.Length > 0;

        private List<ACITWeapon> weapons;
        // array storing every cutscene path initial byte
        private byte[][] cutscenesInitByteArray;

        // This timer updates the current planet every second. It is used cuz some addresses are planet specific
        private Timer currentPlanetTimer;
        private uint currentPlanet;

        public acit(IPS3API api) : base(api)
        {
            addr = new ACITAddresses(api.getGameTitleID());
            weapons = ACITWeaponFactory.GetWeapons();
            if (canRemoveCutscenes)
            {
                cutscenesInitByteArray = ReadCutsceneStrings();
            }

            // creating a timer to update current planet every second
            currentPlanetTimer = new Timer((e) => updateCurrentPlanet(), null, 0, 1000);
        }

        public IEnumerable<(uint addr, uint size)> AutosplitterAddresses => new (uint, uint)[]
        {
            (addr.currentPlanet, 4),        // current planet
            (addr.gameStatePtr, 4),         // game state1
            (addr.cutsceneState1Ptr, 4),    // cutscene state1
            (addr.cutsceneState2Ptr, 4),    // cutscene state2
            (addr.cutsceneState3Ptr, 4),    // cutscene state3
            (addr.saveFileIDPtr, 4),        // save file ID
            (addr.azimuthHPPtr, 4),         // azimuth HP
            (addr.libraHPPtr, 4),           // libra HP
            (addr.vorselon1SpaceCombat, 4), // vorselon 1 space combat
            (addr.neffy1finalRoom, 4),      // neffy 1 final room
            (addr.wasGC2Visited, 4),        // neffy 2 final room
            (addr.timerPtr, 4),             // timer
            (addr.firstCutscene, 4),        // first cutscene
            (addr.loadSaveState, 4),        // load save state
        };

        public override void ResetLevelFlags()
        {
            throw new NotImplementedException();
        }

        public override void SetupFile()
        {
            throw new NotImplementedException();
        }

        public override void SetFastLoads(bool toggle = false)
        {
            throw new NotImplementedException();
        }

        public override void ToggleInfiniteAmmo(bool toggle = false)
        {
            throw new NotImplementedException();
        }

        public override void CheckInputs(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Updates current planet.
        /// </summary>
        private void updateCurrentPlanet()
        {
            uint newPlanet = BitConverter.ToUInt32(api.ReadMemory(pid, addr.currentPlanet, 4).Reverse().ToArray(), 0);
            if (newPlanet != currentPlanet)
            {
                currentPlanet = newPlanet;
                addr.planetValue = currentPlanet;
            }

            float coord = BitConverter.ToSingle(api.ReadMemory(pid, addr.playerCoords, 4).Reverse().ToArray(), 0);
            Console.WriteLine("coord: " + coord);
        }

        /// <summary>
        /// Updates internal list of unlocked items. There was a bug in the Ratchetron C# API that maked it unfeasibly slow to get each item as a single byte.
        /// </summary>
        private void UpdateUnlocks()
        {
            byte[] memory = api.ReadMemory(pid, addr.weapons, ACITWeaponFactory.weaponCount * ACITWeaponFactory.weaponMemoryLenght);
            ACITWeaponFactory.updateWeapons(memory, weapons);
        }

        /// <summary>
        /// Set the unlock state of a weapon (unlocked or not).
        /// </summary>
        /// <param name="weapon"></param>
        /// <param name="unlockState"></param>
        public void setUnlockState(ACITWeapon weapon, bool unlockState)
        {
            weapon.IsUnlocked = unlockState;
            api.WriteMemory(pid, addr.weapons + (weapon.index * ACITWeaponFactory.weaponMemoryLenght) + ACITWeaponFactory.weaponUnlockOffset, BitConverter.GetBytes(unlockState));

        }

        /// <summary>
        /// Get a list of all weapons.
        /// </summary>
        /// <returns></returns>
        public List<ACITWeapon> GetWeapons()
        {
            UpdateUnlocks();
            return HasWeaponUnlock ? weapons : null;
        }

        /// <summary>
        /// Set the level of a weapon.
        /// It's not perfect, it only works for level 1, 5 and 10.
        /// </summary>
        /// <param name="weapon"></param>
        /// <param name="level"></param>
        public void setWeaponLevel(ACITWeapon weapon, uint level)
        {
            if (!weapon.upgradealbe)
            {
                return;
            }
            level--;
            uint xp = (uint)(level == 0 ? 0 : 0xFF);
            uint weaponIndex = weapon.index;
            api.WriteMemory(pid, addr.weapons + (weaponIndex * ACITWeaponFactory.weaponMemoryLenght) + ACITWeaponFactory.weaponlevel1Offset, BitConverter.GetBytes(xp));
            api.WriteMemory(pid, addr.weapons + (weaponIndex * ACITWeaponFactory.weaponMemoryLenght) + ACITWeaponFactory.weaponlevel2Offset, BitConverter.GetBytes(xp));
            api.WriteMemory(pid, addr.weapons + (weaponIndex * ACITWeaponFactory.weaponMemoryLenght) + ACITWeaponFactory.weaponlevel3Offset, BitConverter.GetBytes(xp));
            api.WriteMemory(pid, addr.weapons + (weaponIndex * ACITWeaponFactory.weaponMemoryLenght) + ACITWeaponFactory.weaponlevel4Offset, BitConverter.GetBytes(xp));

            api.WriteMemory(pid, addr.weapons + (weaponIndex * ACITWeaponFactory.weaponMemoryLenght) + ACITWeaponFactory.weaponLevelOffset, BitConverter.GetBytes(level));

            UpdateUnlocks();
        }

        private byte[][] ReadCutsceneStrings()
        {
            byte[][] bytes = new byte[addr.cutscenesArray.Length][];
            for (int i = 0; i < addr.cutscenesArray.Length; i++)
            {
                bytes[i] = api.ReadMemory(pid, addr.cutscenesArray[i], 4);
            }
            return bytes;
        }

        /// <summary>
        /// Enable or disable cutscenes.
        /// </summary>
        /// <param name="enable"></param>
        public void EnableCutscenes(bool enable)
        {
            if (enable)
            {
                for (int i = 0; i < cutscenesInitByteArray.Length; i++)
                {
                    api.WriteMemory(pid, addr.cutscenesArray[i], cutscenesInitByteArray[i]);
                }
                Console.WriteLine("Cutscenes enabled!");
            }
            else
            {
                for (int i = 0; i < cutscenesInitByteArray.Length; i++)
                {
                    api.WriteMemory(pid, addr.cutscenesArray[i], new byte[] { 0 });
                }
                Console.WriteLine("Cutscenes disabled!");
            }
        }

        /// <summary>
        /// Inverting analog sticks offsets.
        /// </summary>
        protected override void SetupInputDisplayMemorySubsAnalogs()
        {
            int analogRSubID = api.SubMemory(pid, addr.analogOffset + 8, 8, (value) =>
            {
                Inputs.ry = BitConverter.ToSingle(value, 0);
                Inputs.rx = BitConverter.ToSingle(value, 4);
            });

            int analogYSubID = api.SubMemory(pid, addr.analogOffset, 8, (value) =>
            {
                Inputs.ly = BitConverter.ToSingle(value, 0);
                Inputs.lx = BitConverter.ToSingle(value, 4);
            });
        }
    }
}
