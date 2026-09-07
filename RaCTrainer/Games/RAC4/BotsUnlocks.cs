namespace racman.offsets.RAC4
{
    public class BotsUnlocks
    {
        public string name { get; private set; }
        // the index in the unlock array
        public uint index { get; private set; }
        public bool IsUnlocked { get; set; }

        public BotsUnlocks(string name, uint index)
        {
            this.name = name;
            this.index = index;
            this.IsUnlocked = false;
        }
    }
}
