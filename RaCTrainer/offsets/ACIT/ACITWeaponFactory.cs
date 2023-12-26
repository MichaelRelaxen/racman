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

        public static List<ACITWeapon> GetWeapons()
        {
            return new List<ACITWeapon>
            {
                new ACITWeapon("Omniwrench", 0, false),
                new ACITWeapon("Time bomb", 1, false),
                new ACITWeapon("Mr Zurkon", 2, true),
                new ACITWeapon("Buzz blades", 3, true),
                new ACITWeapon("Negotiator", 4, true),
                new ACITWeapon("Sonic eruptor", 5, true),
                new ACITWeapon("Magnet launcher", 6, true),
                new ACITWeapon("Cryomine glove", 7, true),
                new ACITWeapon("Plasma striker", 8, true),
                new ACITWeapon("Dynamo of doom", 9, true),
                new ACITWeapon("Rift inducer", 10, true),
                new ACITWeapon("Tesla spikes", 11, true),
                new ACITWeapon("Groovitron glove", 12, true),
                new ACITWeapon("Chimp-o-matic", 13, true),
                new ACITWeapon("Ryno V", 14, true),
                new ACITWeapon("Spiral of death", 15, true),
                new ACITWeapon("Constructo pistol", 16, true),
                new ACITWeapon("Constructo bomb", 17, true),
                new ACITWeapon("Constructo shotgun", 18, true),
                new ACITWeapon("Swingshot", 19, false),
                new ACITWeapon("Omnisoaker", 20, false),
                new ACITWeapon("Hoverboots", 21, false),
            };
        }

        public static void updateWeapons(byte[] memoryArray, List<ACITWeapon> weapons)
        {
            for (int i = 0; i < weaponCount; i++)
            {
                weapons[i].IsUnlocked = BitConverter.ToBoolean(memoryArray, (int)(weaponUnlockOffset + (i * weaponMemoryLenght)));
                weapons[i].UpdateLevel(BitConverter.ToUInt32(memoryArray, (int)(weaponLevelOffset + (i * weaponMemoryLenght))) + 1);
            }
        }
    }
}
