using racman;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace racman
{
    public partial class GCUnlocks : Form
    {
        public GCUnlocks()
        {
            InitializeComponent();
        }

        public rac2 game;

        // sorry
        private List<Label> levelLabels = new List<Label>();
        private List<ComboBox> levelCombos = new List<ComboBox>();
        private List<GCItem> weapons = new List<GCItem>();

        public static List<string> gcItems = new List<string>
        {
            "Plasma Coil", "Lava Gun", "Bouncer", "Miniturret", "Shield Charger"
        };

        private List<GCItem> items = new List<GCItem>
        {
            // new GCItem("#", 0x00, 0x4A8, 0x5F0),
            // new GCItem("#", 0x01, 0x4A9, 0x5F4),
            new GCItem("Heli Pack", 0x02, 0x4AA, 0x5F8),
            new GCItem("Thruster Pack", 0x03, 0x4AB, 0x5FC),
            // new GCItem("Hydro Pack", 0x04, 0x4AC, 0x600),
            // new GCItem("Map-o-matic", 0x05, 0x4AD, 0x604),
            // new GCItem("Commando Suit", 0x06, 0x4AE, 0x608),
            new GCItem("Bolt Grabber v2", 0x07, 0x4AF, 0x60C),
            // new GCItem("Levitator", 0x08, 0x4B0, 0x610),
            // new GCItem("Omniwrench", 0x09, 0x4B1, 0x614),
            new GCItem("Bomb Glove", 0x0A, 0x4B2, 0x618),
            new GCItem("Hypershot", 0x0B, 0x4B3, 0x61C),
            // new GCItem("Paradox ERR", 0x0C, 0x4B4, 0x620),
            new GCItem("Gravity Boots", 0x0D, 0x4B5, 0x624),
            new GCItem("Grindboots", 0x0E, 0x4B6, 0x628),
            // new GCItem("Glider", 0x0F, 0x4B7, 0x62C),
            new GCItem("Plasma Coil", 0x10, 0x4B8, 0x630, 8), // GC item
            new GCItem("Lava Gun", 0x11, 0x4B9, 0x634, 8), // GC item
            new GCItem("Refractor", 0x12, 0x4BA, 0x638),
            new GCItem("Bouncer", 0x13, 0x4BB, 0x63C, 8), // GC item
            new GCItem("The Hacker", 0x14, 0x4BC, 0x640),
            new GCItem("Miniturret", 0x15, 0x4BD, 0x644, 8), // GC item
            new GCItem("Shield Charger", 0x16, 0x4BE, 0x648, 8), // GC item
            // new GCItem("Paradox ERR", 0x17, 0x4BF, 0x64C),
            // new GCItem("Paradox ERR", 0x18, 0x4C0, 0x650),
            // new GCItem("Paradox ERR", 0x19, 0x4C1, 0x654),
            new GCItem("The Hacker", 0x1A, 0x4C2, 0x658),
            // new GCItem("#", 0x1B, 0x4C3, 0x65C),
            new GCItem("Grindboots", 0x1C, 0x4C4, 0x660),
            new GCItem("Charge Boots", 0x1D, 0x4C5, 0x664),
            new GCItem("Tyhrra Guise", 0x1E, 0x4C6, 0x668),
            new GCItem("Warp Pad", 0x1F, 0x4C7, 0x66C),
            new GCItem("Nano Pak", 0x20, 0x4C8, 0x670),
            // new GCItem("Star Map", 0x21, 0x4C9, 0x674),
            new GCItem("Master Plan", 0x22, 0x4CA, 0x678),
            new GCItem("PDA", 0x23, 0x4CB, 0x67C),
            // new GCItem("Third Person Mode", 0x24, 0x4CC, 0x680),
            // new GCItem("First Person Mode", 0x25, 0x4CD, 0x684),
            // new GCItem("Lock Strafe", 0x26, 0x4CE, 0x688),
            new GCItem("Shock Blaster", 0x27, 0x4CF, 0x68C, 8),
            new GCItem("N60 Storm", 0x2F, 0x4D7, 0x6AC, 8),
            new GCItem("Infector", 0x37, 0x4DF, 0x6CC, 8),
            new GCItem("Annihilator", 0x3F, 0x4E7, 0x6EC, 8),
            new GCItem("Spitting Hydra", 0x47, 0x4EF, 0x70C, 8),
            new GCItem("Disc Blade Gun", 0x4F, 0x4F7, 0x72C, 8),
            new GCItem("Agents of Doom", 0x57, 0x4FF, 0x74C, 8),
            new GCItem("Rift Inducer", 0x5F, 0x507, 0x76C, 8),
            new GCItem("Holoshield", 0x67, 0x50F, 0x78C, 8),
            new GCItem("Flux Rifle", 0x6F, 0x517, 0x7AC, 8),
            new GCItem("Nitro Launcher", 0x77, 0x51F, 0x7CC, 8),
            new GCItem("Plasma Whip", 0x7F, 0x527, 0x7EC, 8),
            new GCItem("Suck Cannon", 0x87, 0x52F, 0x80C, 8),
            new GCItem("Quack-O-Ray", 0x8F, 0x537, 0x82C, 8),
            new GCItem("R3YNO", 0x97, 0x53F, 0x84C, 5),

        };

        private List<string> itemNames => items.Select(obj => obj.name).ToList();
        private GCItem itemByName(string itemName) => items.Find(obj => obj.name == itemName);
    }
}

public class GCItem
{
    public string name;
    public uint id;
    public uint unlockOffset;
    public uint expOffset;
    public uint levels;

    public GCItem(string name, uint id, uint unlockOffset, uint expOffset)
        : this(name, id, unlockOffset, expOffset, 1) { }

    public GCItem(string name, uint id, uint unlockOffset, uint expOffset, uint levels)
    {
        this.name = name;
        this.id = id;
        // The first item has unlock value 0x4A8
        this.unlockOffset = unlockOffset - 0x4A8;
        // The first item has exp value 0x5F0
        this.expOffset = expOffset - 0x5F0;
        this.levels = levels;
    }

    public uint VersionNTableOffset(uint version)
    {
        // v1 weapons same as original offset in items table
        if (version == 1) return id;

        // What the fuck, insomniac? WHY????? WHAT IS WRONG WITH YOU?
        if (name == "Bouncer")
        {
            if (version == 2) return 0xA6;
            else return 0xB1 + version;
        }
        else if (name == "Plasma Coil")
        {
            if (version == 2) return 0xA0;
            else return 0xB7 + version;
        }
        else if (name == "Shield Charger")
        {
            if (version == 2) return 0xA7;
            else return 0xBD + version;
        }
        else if (name == "Lava Gun")
        {
            if (version == 2) return 0xA1;
            else return 0xAB + version;
        }
        else if (name == "Miniturret")
        {
            if (version == 2) return 0xA2;
            else return 0xA5 + version;
        }

        return id + version - 1;
    }

    public bool IsUnlocked(rac2 game)
    {
        var res = game.api.ReadMemory(game.pid, rac2.addr.unlockArray + unlockOffset, 1);
        return res[0] == 1;
    }

    public void LockOrUnlock(rac2 game, bool enable) =>
           game.api.WriteMemory(game.pid, rac2.addr.unlockArray + unlockOffset, new byte[] { Convert.ToByte(enable) });

    public void SetExp(rac2 game, uint value) =>
        game.api.WriteMemory(game.pid, rac2.addr.expArray + expOffset, value);

    public void SetVersion(rac2 game, uint version)
    {
        if (levels <= 1) return;
        game.api.WriteMemory(game.pid, rac2.addr.itemArray + id, new byte[] { (byte)VersionNTableOffset(version) });
    }

    // Doesn't work correctly for GC weapons because what the fuck are you talking about, insomniac?
    public uint GetVersion(rac2 game)
    {
        // if (GCUnlocks.gcItems.Contains(name)) return 0;
        // var val = game.api.ReadMemory(game.pid, rac2.addr.itemArray + id, 1);
        // return val[0] - id + 1;
        return 0;
    }
}