using System;
using System.Collections.Generic;

namespace racman.offsets
{
    public  class ACITWeaponFactory
    {
        public static uint weaponCount = 22;
        public static uint weaponMemoryLenght = 24;

        private uint weaponUnlockOffset = 0x0;
        private uint weaponLevelOffset = 0x1;

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
                weapons[i].level = memoryArray[weaponLevelOffset + (i * weaponMemoryLenght)];
            }
        }

        public void updateWeaponState(uint index, bool isUnlocked)
        {
            weapons[(int)index].isUnlocked = isUnlocked;
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
