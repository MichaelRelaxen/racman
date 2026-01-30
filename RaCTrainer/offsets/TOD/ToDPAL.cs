using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using racman.offsets;

namespace racman
{
    public class ToDPAL : IGame, IAutosplitterAvailable
    {
        public class ToDAddresses : IAddresses
        {
            public uint savePlanetId => 0x1029C55B;

            public uint loadScreenType => 0x102034FB;

            // Player values
            public IntPtr dumbRat => (IntPtr)0x10724C34;
            public uint dumbRatAddress => Convert.ToUInt32(Marshal.ReadInt32(dumbRat));

            public uint health => dumbRatAddress + 0x758;

            public uint maxHealth => health + 0x4;

            public uint xCoordinate => dumbRatAddress + 0x234;

            public uint boltCount => 0x101EFF3C;

            public uint raritaniumCount => 0x101EFF42;

            public uint leaviathanSoulCount => 0x101EFE66;

            // Turns challenge mode on/off

            public uint challengeMode => 0x1028020F;

            // Planets

            // Weapons

            public uint weaponXP => 0x101EFB38;

            public uint weaponAmmo => 0x101EFB3C;

            public uint weaponUpgrades => 0x101EFB42;

            public uint weaponToggle => 0x101EFB44;

            public uint weaponLevel => 0x101EFB45;

            // Gadgets

            public uint helipods => 0x101EFD10;

            // Inventory and Items

            // Armor

            public uint armorSkin => 0x101EFF7B;

            public uint armorDamageReduction => dumbRatAddress + 0x76C;

            // Gold Bolts

            public uint goldBolts => 0x101F079F;

            // Skins

            public uint skinsUnlock => 0x101EFF83;

            public uint skinsSwitch => 0x101EFFA3;

            // God Ratchet

            public uint godRatchet => 0x101EFAF3;

            // Ryno Parts

            public uint RYNOParts => 0x101F8215;

            public static ToDAddresses addr = new ToDAddresses();

            public uint playerCoords => throw new NotImplementedException();
            public uint inputOffset => throw new NotImplementedException();
            public uint analogOffset => throw new NotImplementedException();
            public uint loadPlanet => throw new NotImplementedException();
            public uint currentPlanet => savePlanetId;

            public uint mobyInstances => throw new NotImplementedException();
        }
        /*public ToDPAL(IPS3API api) : base(api)
        {

        }*/
        uint tempAddress;

        public Dictionary<string, uint> playerValues = new Dictionary<string, uint>
            {
                {"boltCount", 0x101EFF3C},
                {"raritaniumCount", 0x101EFF42},
                {"leaviathanSouls", 0x101EFE66}
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
            {"Combuster", 1},
            {"Fusion Grenade", 2},
            {"Shock Ravager", 3},
            {"Tornado Launcher", 4},
            {"Buzz Blades", 5},
            {"Predator Launcher", 6},
            {"Alpha Disruptor", 7},
            {"Pyro Blaster", 8},
            {"Plasma Beasts", 9},
            {"Shard Reaper", 10},
            {"Negotiator", 11},
            {"Nano Swarmers", 12},
            {"Mag-Net Launcher", 13},
            {"Razor Claws", 14},
            {"RYNO IV", 15}
        };

        public Dictionary<string, uint> inventoryList = new Dictionary<string, uint>
        {
            {"Heli-Pods", 1},
            {"Swingshot", 2},
            {"Geo-Laser", 3},
            {"Gelanator", 4},
            {"Robo-Wings", 5},
            {"Gyro-Cycle", 6},
            {"Pirate", 7},
            {"Decryptor", 8},
            {"Charge Boots", 1},
            {"Map", 2},
            {"Box Breaker", 3},
            {"Armor Magnetizer", 4}
        };

        uint[,] positions = new uint[3, 5];   

        public static ToDAddresses addr = new ToDAddresses();

        public IEnumerable<(uint addr, uint size)> AutosplitterAddresses => new (uint, uint)[]
        {
            (addr.savePlanetId, 1),
            (addr.loadScreenType, 1)
        };

        public ToDPAL(IPS3API api) : base(api)
        {

        }

        public void PlayerValues(string option, uint value)
        {
            //api.WriteMemory(pid, playerValues[option], value);
            
        }

        public void SavePosition(int index)
        {
            
        }

        public void LoadPosition(int index)
        {
            
        }
        public void SetChallengeMode()
        {
            //tempAddress = Convert.ToUInt32(api.ReadMemory(pid, ToDPAL.addr.challengeMode, 1));
            api.WriteMemory(pid, ToDPAL.addr.challengeMode, 1);
        }

        public void DeathAbuse()
        {
            //api.WriteMemory(pid, ToDPAL.addr.health, 0);
        }

        public void ResetGoldBolts(string planet)
        {
            //api.WriteMemory(pid, ToDPAL.addr.goldBolts + (planetList[planet] * 0x508), 0);
        }

        public void ResetAllGoldBolts()
        {
            /*for(uint i = 0; i < 10; i++)
                {
                    api.WriteMemory(pid, ToDPAL.addr.goldBolts + (i * 0x508), 0);
                }*/
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
            //tempAddress = Convert.ToUInt32(api.ReadMemory(pid, ToDPAL.addr.challengeMode, 1));
            //api.WriteMemory(pid, ToDPAL.addr.weaponToggle + (weaponList[weapon] * 0x14), (tempAddress + 1) % 2);
        }

        public void WeaponLevel(string weapon)
        {

        }

        public void WeaponUpgrades(string weapon)
        {
            //Figure out and algorithm to toggle weapon upgrades
            //tempAddress = Convert.ToUInt32(api.ReadMemory(pid, ToDPAL.addr.challengeMode, 1));
            //api.WriteMemory(pid, ToDPAL.addr.weaponUpgrades + (weaponList[weapon] * 0x14), (tempAddress + 1) % 2);
        }

        public void SetGadgetAndInventoryItems(string gadget, uint address)
        {
            /*tempAddress = Convert.ToUInt32(api.ReadMemory(pid, address, 1));
            if(gadget == "Charge Boots" || gadget == "Map" || gadget == "Box Breaker" || gadget == "Armor Magentizer")
                api.WriteMemory(pid, ToDPAL.addr.helipods + (weaponList[gadget] * 0x4), (tempAddress + 1) % 2);
            api.WriteMemory(pid, ToDPAL.addr.helipods + (weaponList[gadget] * 0x14), (tempAddress + 1) % 2);*/
        }

        public void ResetRYNOPlans()
        {
            api.WriteMemory(pid, ToDPAL.addr.RYNOParts, 0);
        }

        public void SetArmor(uint value)
        {
            api.WriteMemory(pid, ToDPAL.addr.armorSkin, value);
            //api.WriteMemory(pid, ToDPAL.addr.armorDamageReduction, 0); Placeholder number, need to get the proper value for damage reduction
        }

        public void UnlockSkins()
        {
            //for (uint i = 1; i <= 8; i++)
                //api.WriteMemory(pid, ToDPAL.addr.skinsUnlock + (i * 0x4), 1);
        }

        public void ChangeSkins(uint value)
        {
            //api.WriteMemory(pid, ToDPAL.addr.skinsSwitch, value);
        }

        public void SetGodRatchet()
        {
            //uint value;
            //tempAddress = Convert.ToUInt32(api.ReadMemory(pid, ToDPAL.addr.godRatchet, 1));
            /*if (tempAddress == 0)
            {
                value = 62;
            }
            else
            {
                value = 0;
            }*/
            //api.WriteMemory(pid, ToDPAL.addr.godRatchet, 62);
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

// 11