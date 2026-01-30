using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using racman.offsets;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace racman
{
    public class tod : IGame, IAutosplitterAvailable
    {
        public class ToDAddresses : IAddresses
        {
            public uint savePlanetId;
            public uint loadScreenType;

            // Player values
            public uint dumbRat; // allocated in different memory range than rpcs3.
            public uint boltCount => 0; //lmao

            public uint todBoltCount;

            public uint raritaniumCount;

            public uint leaviathanSoulCount;

            // Turns challenge mode on/off

            public uint challengeMode;

            // Planets

            // Weapons

            public uint weaponXP;

            public uint weaponAmmo;

            public uint weaponUpgrades;

            public uint weaponToggle;

            public uint weaponLevel;

            // Gadgets

            public uint helipods;

            public uint chargeBoots;

            // Inventory and Items

            // Armor

            public uint armorSkin;

            // Gold Bolts
            public uint goldBolts;

            // Skins

            public uint skinsUnlock;

            //public uint skinsSwitch => 0x101EFFA3; WIP

            // God Ratchet

            public uint godRatchet;

            // Ryno Parts
            public uint RYNOParts;

            // Random stuff

            public uint groovitronStorage;

            public uint playerCoords => throw new NotImplementedException();
            public uint inputOffset => throw new NotImplementedException();
            public uint analogOffset => throw new NotImplementedException();
            public uint loadPlanet => throw new NotImplementedException();
            public uint currentPlanet => savePlanetId;

            public uint mobyInstances => throw new NotImplementedException();
        }

        float[,] weaponXPValues = new float[15, 10]
                                  {{0, 1000, 2200, 3640, 5400, 5600, 20000, 37400, 58400, 83400},
                                  {0, 1000, 2200, 3640, 5500, 5600, 20000, 37400, 58400, 83400},
                                  {0, 800, 1760, 2920, 4300, 8500, 16000, 25000, 35800, 50000},
                                  {0, 1000, 2200, 3640, 5400, 6500, 14000, 23000, 33800, 46800},
                                  {0, 2500, 5500, 9100, 14000, 16600, 39000, 66000, 98400, 138000},
                                  {0, 1500, 3300, 5480, 8050, 8200, 23000, 41000, 62000, 88200},
                                  {0, 3500, 7700, 12800, 18800, 22500, 30000, 39000, 49800, 62800},
                                  {0, 4000, 8800, 14600, 22000, 33000, 48000, 66000, 87600, 114000},
                                  {0, 600, 1320, 2180, 3200, 6000, 14000, 23000, 33800, 46800},
                                  {0, 2000, 4400, 7300, 10800, 11200, 33800, 60600, 92900, 131800},
                                  {0, 6000, 13200, 21860, 32400, 40600, 63000, 90000, 122600, 161400},
                                  {0, 1500, 3300, 5480, 8050, 11200, 26000, 44000, 65600, 91600},
                                  {0, 3000, 6600, 11000, 16200, 32500, 40000, 49200, 60000, 72830},
                                  {0, 5000, 11000, 18200, 27000, 40000, 55000, 73000, 94000, 120600},
                                  {0, 15000, 33000, 54800, 80600, 81000, 111600, 147000, 190600, 243000}
        };

        public static ToDAddresses addr = new ToDAddresses();

        public IEnumerable<(uint addr, uint size)> AutosplitterAddresses => new (uint, uint)[]
        {
            (addr.savePlanetId, 1),
            (addr.loadScreenType, 1)
        };

        public tod(IPS3API api) : base(api)
        {
            string gameVersion = AttachPS3Form.game;
            if(gameVersion == "NPEA00452")
            {
                if (!AttachPS3Form.isEmulator)
                    addr.dumbRat = 0x61BF1984;
                else addr.dumbRat = 0x31BF1984;

                addr.savePlanetId = 0x1029C55B;
                addr.loadScreenType = 0x102034FB;
                addr.todBoltCount = 0x1020C28C;
                addr.raritaniumCount = 0x1020C290;
                addr.leaviathanSoulCount = 0x1020C1B4;
                addr.challengeMode = 0x1029C55D;
                addr.weaponXP = 0x1020BE88;
                addr.weaponAmmo = 0x1020BE8C;
                addr.weaponUpgrades = 0x1020BE92;
                addr.weaponToggle = 0x1020BE94;
                addr.weaponLevel = 0x1020BE95;
                addr.helipods = 0x1020C060;
                addr.chargeBoots = 0x1020C18B;
                addr.armorSkin = 0x1020C2CB;
                addr.goldBolts = 0x1020CAEF;
                addr.skinsUnlock = 0x1020C2D3;
                addr.godRatchet = 0x1020BD4B;
                addr.RYNOParts = 0x10214565;
                addr.groovitronStorage = 0x10385F8B;
            }
            else if(gameVersion == "BCES00052")
            {
                if (!AttachPS3Form.isEmulator)
                    addr.dumbRat = 0x61BD1904;
                else addr.dumbRat = 0x31BD1904; // 331C207E4

                addr.savePlanetId = 0x1028020B;
                addr.loadScreenType = 0x101E7293;
                addr.todBoltCount = 0x101EFF3C;
                addr.raritaniumCount = 0x101EFF40;
                addr.leaviathanSoulCount = 0x101EFE64;
                addr.challengeMode = 0x1028020F;
                addr.weaponXP = 0x101EFB38;
                addr.weaponAmmo = 0x101EFB3C;
                addr.weaponUpgrades = 0x101EFB42;
                addr.weaponToggle = 0x101EFB44;
                addr.weaponLevel = 0x101EFB45;
                addr.helipods = 0x101EFD10;
                addr.chargeBoots = 0x101EFE3B;
                addr.armorSkin = 0x101EFF7B;
                addr.goldBolts = 0x101F079F;
                addr.skinsUnlock = 0x101EFF83;
                addr.godRatchet = 0x101EFAF3;
                addr.RYNOParts = 0x101F8215;
                addr.groovitronStorage = 0x10369CA3;
            }
            addPlayerValueAddresses();
        }

        public Dictionary<string, uint> playerValues = new Dictionary<string, uint>
            {

            };

        public void addPlayerValueAddresses()
        {
            playerValues.Add("Bolts", addr.todBoltCount);
            playerValues.Add("Raritanium", addr.raritaniumCount);
            playerValues.Add("Leviathan Souls", addr.leaviathanSoulCount);
        } 

        public Dictionary<string, uint> planetList = new Dictionary<string, uint>
            {
                {"Cobalia", 0},
                {"Kortog", 1},
                {"Fastoon", 2},
                {"Voron Asteroid Belt", 3},
                {"Mukow", 4},
                {"Nundac Asteroid Ring", 5},
                {"Ardolis", 6},
                {"Rakar Star Cluster", 7},
                {"Rykan V", 8},
                {"Sargasso", 9},
                {"Kreeli Comet", 10},
                {"Viceron", 11},
                {"Verdegris Black Hole", 12},
                {"Jasidnu", 13},
                {"Ublik Passage", 14},
                {"Reepor", 15},
                {"Igliak", 16}
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

        public Dictionary<string, uint> skinList = new Dictionary<string, uint>
        {
            {"Dan Johnson", 0},
            {"Snowman", 1},
            {"Cragmite", 2},
            {"Rusty Pete", 3},
            {"Cronk", 4},
            {"Zephyr", 5},
            {"Convict Ratchet", 6},
            {"Mustachio Furioso", 7}
        };

        public Dictionary<string, uint> armorList = new Dictionary<string, uint>
        {
            {"No Armor", 0},
            {"Blackstar Armor", 1},
            {"Helios Armor", 2},
            {"Terraflux Armor", 3},
            {"Trillium Armor", 4}
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
            string position = api.ReadMemoryStr(pid, getRatPointer() + 0x1260, 64);
            func.ChangeFileLines("config.txt", position, "ToDSavedPos" + index);
        }

        public void LoadPosition(int index)
        {
            string position = func.GetConfigData("config.txt", "ToDSavedPos" + index);
            if (position != "")
            {
                api.WriteMemory(pid, getRatPointer() + 0x1260, 64, position);
            }
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

        public void WeaponLevel(string weapon, int level)
        {
            Convert.ToUInt32(level);
            api.WriteMemory(pid, tod.addr.weaponLevel + (weaponList[weapon] * 0x14), 1, new byte[] { Convert.ToByte(level - 1) });
            api.WriteMemory(pid, tod.addr.weaponXP + (weaponList[weapon] * 0x14), 4, BitConverter.GetBytes(weaponXPValues[weaponList[weapon], level - 1]).Reverse().ToArray());
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

        public void SetArmor(byte value)
        {
            float damageReduction = 1;
            api.WriteMemory(pid, tod.addr.armorSkin, 1, new byte[] { value });
            switch (value)
            {
                case 0:
                    {
                        damageReduction = 1.0f;
                    }
                    break;
                case 1:
                    {
                        damageReduction = 0.75f;
                    }
                    break;
                case 2:
                    {
                        damageReduction = 0.6f;
                    }
                    break;
                case 3:
                    {
                        damageReduction = 0.45f;
                    }
                    break;
                case 4:
                    {
                        damageReduction = 0.35f;
                    }
                    break;

            }
            api.WriteMemory(pid, getRatPointer() + 0x1798, 4, BitConverter.GetBytes(damageReduction).Reverse().ToArray());
        }

        public void UnlockSkins(string skin)
        {
            api.WriteMemory(pid, tod.addr.skinsUnlock + skinList[skin] * 0x4, 1, new byte[] { 1 });
        }

        public void ChangeSkins(uint value)
        {
            //api.WriteMemory(pid, tod.addr.skinsSwitch, value);
        }

        public void SetGodRatchet()
        {
            string gameVersion = AttachPS3Form.game;
            uint value = BitConverter.ToUInt32(api.ReadMemory(pid, tod.addr.godRatchet, 4).Reverse().ToArray(), 0);
            if (value == 0)
            {
                if (gameVersion == "NPEA00452")
                    value = 154;
                else if (gameVersion == "BCES00052")
                    value = 62;
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