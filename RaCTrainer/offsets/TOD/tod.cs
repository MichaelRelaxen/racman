using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using racman.offsets;

namespace racman
{
    public class tod : IGame, IAutosplitterAvailable
    {
        public class ToDAddresses : IAddresses
        {
            public uint savePlanetId => 0x1029C55B;
            public uint loadScreenType => 0x102034FB;

            // Player values
            public uint dumbRat => 0x31BF1984;

            //public uint health => dumbRatAddress + 0x758; WIP

            //public uint maxHealth => health + 0x4; WIP

            //public uint xCoordinate => dumbRatAddress + 0x234; WIP

            public uint boltCount => 0x1020C28C;

            public uint raritaniumCount => 0x1020C290;

            public uint leaviathanSoulCount => 0x1020C1B4;

            // Turns challenge mode on/off

            public uint challengeMode => 0x1029C55D;

            // Planets

            // Weapons

            public uint weaponXP => 0x1020BE88;

            public uint weaponAmmo => 0x1020BE8C;

            public uint weaponUpgrades => 0x1020BE92;

            public uint weaponToggle => 0x1020BE94;

            public uint weaponLevel => 0x1020BE95;

            // Gadgets

            public uint helipods => 0x1020C060;

            public uint chargeBoots => 0x1020C18B;

            // Inventory and Items

            // Armor

            //public uint armorSkin => 0x101EFF7B; WIP

            //public uint armorDamageReduction => dumbRatAddress + 0x76C; WIP

            // Gold Bolts
            public uint goldBolts => 0x1020CAEF;

            // Skins

            //public uint skinsUnlock => 0x101EFF83; WIP

            //public uint skinsSwitch => 0x101EFFA3; WIP

            // God Ratchet

            public uint godRatchet => 0x1020BD4B;

            // Ryno Parts
            public uint RYNOParts => 0x10214565;

            // Random stuff

            public uint groovitronStorage => 0x10385F8B;

            public uint playerCoords => throw new NotImplementedException();
            public uint inputOffset => throw new NotImplementedException();
            public uint analogOffset => throw new NotImplementedException();
            public uint loadPlanet => throw new NotImplementedException();
            public uint currentPlanet => savePlanetId;

            public uint mobyInstances => throw new NotImplementedException();
        }

        public static ToDAddresses addr = new ToDAddresses();

        public IEnumerable<(uint addr, uint size)> AutosplitterAddresses => new (uint, uint)[]
        {
            (addr.savePlanetId, 1),
            (addr.loadScreenType, 1)
        };

        public tod(IPS3API api) : base(api)
        {

        }

        public Dictionary<string, uint> playerValues = new Dictionary<string, uint>
            {
                {"Bolts", tod.addr.boltCount},
                {"Raritanium", tod.addr.raritaniumCount},
                {"Leviathan Souls", tod.addr.leaviathanSoulCount}
            };

        public Dictionary<string, uint> planetList = new Dictionary<string, uint>
            {
                {"Cobalia", 1},
                {"Kortog", 2},
                {"Fastoon", 3},
                {"Voron Asteroid Belt", 4},
                {"Mukow", 5},
                {"Nundac Asteroid Ring", 6},
                {"Ardolis", 7},
                {"Rakar Star Cluster", 8},
                {"Rykan V", 9},
                {"Sargasso", 10},
                {"Kreeli Comet", 11},
                {"Viceron", 12},
                {"Verdegris Black Hole", 13},
                {"Jasidnu", 14},
                {"Ublik Passage", 15},
                {"Reepor", 16},
                {"Igliak", 17}
            };

        public Dictionary<string, uint> weaponList = new Dictionary<string, uint>
        {
            {"Combuster", 0},
            {"Fusion Grenade", 1},
            {"Shock Ravager", 2},
            {"Tornado Launcher", 3},
            {"Buzz Blades", 4},
            {"Predator Launcher", 5},
            {"Alpha Disruptor", 6},
            {"Pyro Blaster", 7},
            {"Plasma Beasts", 8},
            {"Shard Reaper", 9},
            {"Negotiator", 10},
            {"Nano Swarmers", 11},
            {"Mag-Net Launcher", 12},
            {"Razor Claws", 13},
            {"Ryno IV", 14}
        };

        public Dictionary<string, uint> gadgetList = new Dictionary<string, uint>
        {
            {"Heli-Pods", 0},
            {"Swingshot", 1},
            {"Geo-Laser", 2},
            {"Gelanator", 3},
            {"Robo-Wings", 4},
            {"Gyro-Cycle", 5},
            {"Pirate", 6},
            {"Decryptor", 7},
            {"Charge Boots", 0},
            {"Map", 1},
            {"Box Breaker", 2},
            {"Armor Magnetizer", 3}
        };

        public uint getRatPointer()
        {
            return BitConverter.ToUInt32(api.ReadMemory(pid, tod.addr.dumbRat, 4).Reverse().ToArray(), 0);
        }

        public void PlayerValues(string option, uint value)
        {
            api.WriteMemory(pid, playerValues[option], value);
        }

        public void SavePosition(int index)
        {

        }

        public void LoadPosition(int index)
        {

        }
        public void SetChallengeMode()
        {
            bool value = BitConverter.ToBoolean(api.ReadMemory(pid, tod.addr.challengeMode, 1), 0);
            api.WriteMemory(pid, tod.addr.challengeMode, 1, new byte[] { Convert.ToByte(!value) });
        }

        public void DeathAbuse()
        {
            api.WriteMemory(pid, getRatPointer() + 0x1784, 0);
        }

        public void ResetGoldBolts(string planet)
        {
            //api.WriteMemory(pid, tod.addr.goldBolts + (planetList[planet] * 0x508), 0);
        }

        public void ResetAllGoldBolts()
        {
            foreach(var i in planetList)
                api.WriteMemory(pid, tod.addr.goldBolts + (i.Value * 0x408), 1, new byte[] {0});
        }

        public void SetInfiniteHealth()
        {

        }

        public void SetInfinteAmmo()
        {

        }

        //Weapons, Gadgets and Devices
        public void UnlockWeapon(string weapon)
        {
            bool value = BitConverter.ToBoolean(api.ReadMemory(pid, tod.addr.weaponToggle + (weaponList[weapon] * 0x14), 1), 0);
            api.WriteMemory(pid, tod.addr.weaponToggle + (weaponList[weapon] * 0x14), 1, new byte[] {Convert.ToByte(!value)});
        }

        public void WeaponLevel(string weapon)
        {

        }

        public void WeaponUpgrades(string weapon)
        {
            ushort value = BitConverter.ToUInt16(api.ReadMemory(pid, tod.addr.weaponUpgrades + (weaponList[weapon] * 0x14), 2).Reverse().ToArray(), 0);
            if(value == 65535)
                api.WriteMemory(pid, tod.addr.weaponUpgrades + (weaponList[weapon] * 0x14), 2, new byte[] { 0, 0 });
            else
                api.WriteMemory(pid, tod.addr.weaponUpgrades + (weaponList[weapon] * 0x14), 2, new byte[] { 255, 255 });
        }

        public void SetGadgetAndInventoryItems(string gadget, byte value)
        {
            if(gadget == "Charge Boots" || gadget == "Map" || gadget == "Box Breaker" || gadget == "Armor Magnetizer")
                api.WriteMemory(pid, tod.addr.chargeBoots + (gadgetList[gadget] * 0x4), 1, new byte[] {value});
            else
                api.WriteMemory(pid, tod.addr.helipods + (gadgetList[gadget] * 0x14), 1, new byte[] { value });
        }

        public void ResetRYNOPlans()
        {
            api.WriteMemory(pid, tod.addr.RYNOParts, 0);
        }

        public void SetArmor(uint value)
        {
            //api.WriteMemory(pid, tod.addr.armorSkin, value);
            //api.WriteMemory(pid, tod.addr.armorDamageReduction, 0); Placeholder number, need to get the proper value for damage reduction
        }

        public void UnlockSkins()
        {
            //for (uint i = 1; i <= 8; i++)
            //api.WriteMemory(pid, tod.addr.skinsUnlock + (i * 0x4), 1);
        }

        public void ChangeSkins(uint value)
        {
            //api.WriteMemory(pid, tod.addr.skinsSwitch, value);
        }

        public void SetGodRatchet()
        {
            uint value = BitConverter.ToUInt32(api.ReadMemory(pid, tod.addr.godRatchet, 4).Reverse().ToArray(), 0);
            if (value == 0)
            {
                value = 154;
            }
            else
            {
                value = 0;
            }
            api.WriteMemory(pid, tod.addr.godRatchet, 1, new byte[] {Convert.ToByte(value)});
        }
        public void ResetGoldenGrovitronStorage()
        {
            api.WriteMemory(pid, tod.addr.groovitronStorage, 1, new byte[] { 10 });
        }

        public override void CheckInputs(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        public override void ResetLevelFlags()
        {
            throw new NotImplementedException();
        }

        public override void SetFastLoads(bool enabled = false)
        {
            throw new NotImplementedException();
        }

        public override void SetupFile()
        {
            throw new NotImplementedException();
        }

        public override void ToggleInfiniteAmmo(bool toggle = false)
        {
            throw new NotImplementedException();
        }

        public override void CheckPlanetForDiscordRPC(object sender = null, EventArgs e = null)
        {
            throw new NotImplementedException();
        }
    }
}