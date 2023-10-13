using racman.offsets;
using System;
using System.Collections.Generic;
using racman.offsets.ACIT;

namespace racman
{
    public class acit : IGame, IAutosplitterAvailable
    {
        public dynamic Planets = new
        {
            agorian_arena = ("Agorian Arena"),
            axiom_city = ("Axion City"),
            front_end = ("Front End"),
            galacton_ship = ("Vorselon Ship"),
            gimlick_valley = ("Gimlick Valey"),
            great_clock_a = ("Great Clock 1"),
            great_clock_b = ("Great Clock 2"),
            great_clock_c = ("Great Clock 3"),
            great_clock_d = ("Great Clock 4"),
            great_clock_e = ("Great Clock 5"),
            insomniac_museum = ("Insomniac Museum"),
            krell_canyon = ("Krell Canyon"),
            molonoth = ("Molonoth Fields"),
            nefarious_statio = ("Nefarious Station"),
            space_sector_1 = ("Space Sector 1"),
            space_sector_2 = ("Space Sector 2"),
            space_sector_3 = ("Space Sector 3"),
            space_sector_4 = ("Space Sector 4"),
            space_sector_5 = ("Space Sector 5"),
            tombli = ("Tombli Outpost"),
            valkyrie_fleet = ("Valkyrie Fleet"),
            zolar_forest = ("Zolar Forest")
        };

        public static ACITAddresses addr;

        public bool HasInputDisplay => addr.inputOffset > 0 && addr.analogOffset > 0 && addr.currentPlanet > 0;
        public bool IsAutosplitterSupported => addr.IsAutosplitterSupported;
        public bool IsSelfKillSupported => addr.IsSelfKillSupported;
        public bool HasWeaponUnlock => addr.weapons > 0;

        private long lastUnlocksUpdate = 0;
        private ACITWeaponFactory weaponFactory;

        public acit(IPS3API api) : base(api)
        {
            addr = new ACITAddresses(api.getGameTitleID());
            weaponFactory = new ACITWeaponFactory();
        }

        public IEnumerable<(uint addr, uint size)> AutosplitterAddresses => new (uint, uint)[]
        {
            (addr.currentPlanet, 4),        // current planet
            (addr.gameState1Ptr, 4),        // game state1
            (addr.cutsceneState1Ptr, 4),    // cutscene state1
            (addr.cutsceneState2Ptr, 4),    // cutscene state2
            (addr.cutsceneState3Ptr, 4),    // cutscene state3
            (addr.saveFileIDPtr, 4),        // save file ID
            (addr.boltCount, 4),            // bolt count
            (addr.playerCoords, 4),         // player X coord
            (addr.playerCoords + 0x8, 4),   // player Y coord
            (addr.playerCoords + 0x4, 4),   // player Z coord
            (addr.azimuthHPPtr, 4),         // azimuth HP
            (addr.timerPtr, 4),             // timer
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
        /// Updates internal list of unlocked items. There was a bug in the Ratchetron C# API that maked it unfeasibly slow to get each item as a single byte.
        /// </summary>
        private void UpdateUnlocks()
        {
            if (DateTime.Now.Ticks < lastUnlocksUpdate + 10000000)
            {
                return;
            }

            byte[] memory = api.ReadMemory(pid, addr.weapons, ACITWeaponFactory.weaponCount * ACITWeaponFactory.weaponMemoryLenght);

            weaponFactory.updateWeapons(memory);

            lastUnlocksUpdate = DateTime.Now.Ticks;
        }

        public void setUnlockState(ACITWeapon weapon, bool unlockState)
        {
            weaponFactory.updateWeaponState(weapon.index, unlockState);
            api.WriteMemory(pid, addr.weapons + (weapon.index * ACITWeaponFactory.weaponMemoryLenght), BitConverter.GetBytes(unlockState));

        }

        public List<ACITWeapon> GetWeapons()
        {
            UpdateUnlocks();
            return HasWeaponUnlock ? weaponFactory.weapons : null;
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
