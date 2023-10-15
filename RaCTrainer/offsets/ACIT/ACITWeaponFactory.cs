using System;
using System.Collections.Generic;

namespace racman.offsets
{
    public  class ACITWeaponFactory
    {
        public static uint weaponCount = 22;
        public static uint weaponMemoryLenght = 24;

        public static uint weaponlevel1Offset = 0x0;
        public static uint weaponlevel2Offset = 0x1;
        public static uint weaponlevel3Offset = 0x2;
        public static uint weaponlevel4Offset = 0x3;
        public static uint weaponAmmo1Offset = 0x4;
        public static uint weaponAmmo2Offset = 0x5;

        public static uint constructoOffset = 0x8;

        public static uint weaponUnlockOffset = 0x11;
        public static uint weaponLevelOffset = 0x12;

        public static uint weaponIndex = 0x17;

        public List<ACITWeapon> weapons { get; private set; }

        public ACITWeaponFactory()
        {
            weapons = new List<ACITWeapon>
            {
                new ACITWeapon("Omniwrench", 0, 0, false),
                new ACITWeapon("Time bomb", 1, 0, false),
                new ACITWeapon("Mr Zurkon", 2, 0, false),
                new ACITWeapon("Buzz blades", 3, 0, false),
                new ACITWeapon("Negotiator", 4, 0, false),
                new ACITWeapon("Sonic eruptor", 5, 0, false),
                new ACITWeapon("Magnet launcher", 6, 0, false),
                new ACITWeapon("Cryomine glove", 7, 0, false),
                new ACITWeapon("Plasma striker", 8, 0, false),
                new ACITWeapon("Dynamo of doom", 9, 0, false),
                new ACITWeapon("Rift inducer", 10, 0, false),
                new ACITWeapon("Tesla spikes", 11, 0, false),
                new ACITWeapon("Groovitron glove", 12, 0, false),
                new ACITWeapon("Chimp-o-matic", 13, 0, false),
                new ACITWeapon("Ryno V", 14, 0, false),
                new ACITWeapon("Spiral of death", 15, 0, false),
                new ACITWeapon("Constructo pistol", 16, 0, false),
                new ACITWeapon("Constructo bomb", 17, 0, false),
                new ACITWeapon("Constructo shotgun", 18, 0, false),
                new ACITWeapon("Swingshot", 19, 0, false),
                new ACITWeapon("Omnisoaker", 20, 0, false),
                new ACITWeapon("Hoverboots", 21, 0, false),
            };
        }

        public void updateWeapons(byte[] memoryArray)
        {
            for (int i = 0; i < weaponCount; i++)
            {
                weapons[i].isUnlocked = BitConverter.ToBoolean(memoryArray, (int)(weaponUnlockOffset + (i * weaponMemoryLenght)));
                weapons[i].level = (uint)memoryArray[weaponLevelOffset + (i * weaponMemoryLenght)] + 1;
                Console.WriteLine(weapons[i].name + " " + weapons[i].level);
            }
        }
    }

    public class ACITWeapon
    {
        public string name { get; private set; }
        // the index in the unlock array
        public uint index { get; private set; }
        public uint level { get; set; }
        public bool isUnlocked { get; set; }

        public ACITWeapon(string name, uint index, uint level, bool isUnlocked)
        {
            this.name = name;
            this.index = index;
            this.level = level;
            this.isUnlocked = isUnlocked;
        }
    }
}
