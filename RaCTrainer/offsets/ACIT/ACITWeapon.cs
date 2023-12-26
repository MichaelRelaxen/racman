using System;

namespace racman.offsets
{
    public class ACITWeapon
    {
        internal Action<ACITWeapon> levelChanged;

        public string name { get; private set; }
        // the index in the unlock array
        public uint index { get; private set; }
        public uint level { get; private set; }
        public bool IsUnlocked { get; set; }
        // if a gadget get's leveled up, it wont work properly
        public bool upgradealbe { get; private set; }

        public ACITWeapon(string name, uint index, bool upgradealbe)
        {
            this.name = name;
            this.index = index;
            this.level = 1;
            this.IsUnlocked = false;
            this.upgradealbe = upgradealbe;
        }

        public void UpdateLevel(uint level)
        {
            if (!upgradealbe)
            {
                return;
            }
            this.level = level;
            levelChanged?.Invoke(this);
        }
    }
}
