using System;
using System.Collections.Generic;
using System.Linq;

namespace racman
{
    public class RaC1Addresses : IAddresses
    {
        public uint boltCount => 0x969CA0;

        public uint playerCoords => 0x969D60;

        public uint inputOffset => 0x964AF0;

        public uint analogOffset => 0x964A40;

        public uint loadPlanet => 0xA10700;

        public uint currentPlanet => 0x969C70;

        public uint levelFlags => 0xA0CA84;

        public uint miscLevelFlags => 0xA0CD1C;

        public uint infobotFlags => 0x96CA0C;

        public uint moviesFlags => 0x96BFF0;

        public uint unlockArray => 0x96C140;

        public uint destinationPlanet => 0xa10704;

        public uint playerState => 0x96BD64;

        public uint planetFrameCount => 0xA10710;

        public uint gameState => 0x00A10708;

        public uint loadingScreenID => 0x9645C8;

        public uint ghostTimer => 0x969EAC;

        public uint drekSkip => 0xFACC7B;

        public uint goodiesMenu => 0x969CD3;

        public uint goldItems => 0x969CA8;
    }

    public class rac1 : IGame
    {
        public static RaC1Addresses addr = new RaC1Addresses();

        public rac1(Ratchetron api) : base(api)
        {
            this.planetsList = new string[] {
                "Veldin",
                "Novalis",
                "Aridia",
                "Kerwan",
                "Eudora",
                "Rilgar",
                "Blarg",
                "Umbris",
                "Batalia",
                "Gaspar",
                "Orxon",
                "Pokitaru",
                "Hoven",
                "Gemlik",
                "Oltanis",
                "Quartu",
                "Kalebo3",
                "Fleet",
                "Veldin2"
            };

        }

        public dynamic Unlocks = new
        {
            HeliPack = ("Heli-Pack", 2),
            ThrusterPack = ("Thruster-Pack", 3),
            HydroPack = ("Hydro-Pack", 4),
            SonicSummoner = ("Sonic Summoner", 5),
            O2Mask = ("O2 Mask", 6),
            PilotsHelmet = ("Pilots Helmet", 7),
            Wrench = ("Wrench", 8),
            SuckCannon = ("Suck Cannon", 9),
            BombGlove = ("Bomb Glove", 10),
            Devastator = ("Devastator", 11),
            Swingshot = ("Swingshot", 12),
            Visibomb = ("Visibomb", 13),
            Taunter = ("Taunter", 14),
            Blaster = ("Blaster", 15),
            Pyrocitor = ("Pyrocitor", 16),
            MineGlove = ("Mine Glove", 17),
            Walloper = ("Walloper", 18),
            TeslaClaw = ("Tesla Claw", 19),
            GloveOfDoom = ("Glove Of Doom", 20),
            MorphORay = ("Morph-O-Ray", 21),
            Hydrodisplacer = ("Hydrodisplacer", 22),
            RYNO = ("RYNO", 23),
            DroneDevice = ("Drone Device", 24),
            DecoyGlove = ("Decoy Glove", 25),
            Trespasser = ("Trespasser", 26),
            MetalDetector = ("Metal Detector", 27),
            Magneboots = ("Magneboots", 28),
            Grindboots = ("Grindboots", 29),
            Hoverboard = ("Hoverboard", 30),
            Hologuise = ("Hologuise", 31),
            PDA = ("PDA", 32),
            MapOMatic = ("Map-O-Matic", 33),
            BoltGrabber = ("Bolt Grabber", 34),
            Persuader = ("Persuader", 35)
        };

        private int ghostRatchetSubID = -1;

        ///////////// Player /////////////


        // The player's current health.
        public static uint player_health = 0x96BF88;

        // Current player state. 
        public static uint player_state = 0x96BD64;

        //Frames until "Ghost Ratchet" runs out.
        public static uint ghost_timer = 0x969EAC;

        // Currently loaded planet.
        public static uint current_planet = 0x969C70;

        // Planet we're going to.
        public static uint destination_planet = 0xa10704;


        ///////////// Misc. /////////////

        // Set single byte to enable/disable drek skip.
        public static uint drek_skip = 0xFACC7B;

        // Set single byte to enable/disable goodies menu. Not related to challenge mode.
        public static uint goodies_menu = 0x969CD3;

        // First 0x4 for if planet should be loaded, the 0x4 after for planet to load.
        public static uint load_planet = 0xA10700;

        // Force third loading screen by setting to 2.
        public static uint fast_load = 0x9645CF;

        ///////////// Arrays /////////////

        // Array of whether or not you've collected gold bolts. 4 per planet.
        public static uint gold_bolts_array = 0xA0CA34;

        // Array of whether or not you've unlocked any gold weapons.
        public static uint gold_weapons_array = 0x969CA8;



        public override void ToggleFastLoad(bool toggle)
        {
            if (toggle)
            {
                api.WriteMemory(pid, 0x0DF254, 0x60000000);
                api.WriteMemory(pid, 0x165450, 0x2C03FFFF);
            }
            else
            {
                api.WriteMemory(pid, 0x0DF254, 0x40820188);
                api.WriteMemory(pid, 0x165450, 0x2c030000);
            }
        }

        public void SetUnlock((string, int) item, bool unlocked, bool gold = false)
        {
            api.WriteMemory(pid, (gold ? rac1.addr.goldItems : rac1.addr.unlockArray) + (uint)item.Item2, BitConverter.GetBytes(unlocked));
        }

        Dictionary<int, bool> ownedUnlocks = new Dictionary<int, bool>();
        Dictionary<int, bool> ownedGoldItems = new Dictionary<int, bool>();
        long lastUnlocksUpdate = 0;
        long lastGoldItemsUpdate = 0;

        private void UpdateUnlocks()
        {
            if (DateTime.Now.Ticks < lastUnlocksUpdate + 10000000)
            {
                return;
            }

            byte[] memory = api.ReadMemory(pid, rac1.addr.unlockArray, 40);

            var i = 0;
            foreach (byte item in memory)
            {
                ownedUnlocks[i] = item != 0;
                i++;
            }

            lastUnlocksUpdate = DateTime.Now.Ticks;
        }

        private void UpdateGoldItems()
        {
            if (DateTime.Now.Ticks < lastGoldItemsUpdate + 10000000)
            {
                return;
            }

            byte[] memory = api.ReadMemory(pid, rac1.addr.goldItems, 40);

            var i = 0;
            foreach (byte item in memory)
            {
                ownedGoldItems[i] = item != 0;
                i++;
            }

            lastGoldItemsUpdate = DateTime.Now.Ticks;
        }

        public bool HasUnlock((string, int) item, bool gold = false)
        {
            UpdateGoldItems();
            UpdateUnlocks();
            return gold ? ownedGoldItems[item.Item2] : ownedUnlocks[item.Item2];
        }

        public (string, int)[] GetUnlocks()
        {
            List<(string, int)> unlocks = new List<(string, int)>();
           
            foreach (var item in Unlocks.GetType().GetProperties())
            {
                unlocks.Add(item.GetValue(Unlocks));
            }


            return unlocks.ToArray();
        }

        public override void ResetLevelFlags()
        {

            // Not working properly right now?
            api.WriteMemory(pid, rac1.addr.levelFlags + (planetToLoad * 0x10), 0x10, new byte[0x10]);
            api.WriteMemory(pid, rac1.addr.miscLevelFlags + (planetToLoad * 0x100), 0x100, new byte[0x100]);
            api.WriteMemory(pid, rac1.addr.infobotFlags + planetToLoad, 1, new byte[1]);
            api.WriteMemory(pid, rac1.addr.moviesFlags, 0xc0, new byte[0xC0]);

            if (planetToLoad == 3)
            {
                api.WriteMemory(pid, 0x96C378, 0xF0, new byte[0xF0]);
                SetUnlock(Unlocks.HeliPack, false);
                SetUnlock(Unlocks.Swingshot, false);
            }

            if (planetToLoad == 4)
            {
                api.WriteMemory(pid, 0x96C468, 0x40, new byte[0x40]);
                SetUnlock(Unlocks.SuckCannon, false);
            }

            if (planetToLoad == 5)
            {
                api.WriteMemory(pid, 0x96C498, 0xa0, new byte[0xA0]);
            }

            if (planetToLoad == 6)
            {
                SetUnlock(Unlocks.Grindboots, false);
            }

            if (planetToLoad == 8)
            {
                api.WriteMemory(pid, 0x96C5A8, 0x40, new byte[0x40]);
            }

            if (planetToLoad == 9)
            {
                api.WriteMemory(pid, 0x96C5E8, 0x20, new byte[0x20]);
                SetUnlock(Unlocks.PilotsHelmet, false);
            }

            if (planetToLoad == 10)
            {
                SetUnlock(Unlocks.Magneboots, false);

                if (HasUnlock(Unlocks.O2Mask))
                {
                    // Figure it out
                    api.WriteMemory(pid, rac1.addr.infobotFlags + 11, 1);
                }
            }

            if (planetToLoad == 11)
            {
                SetUnlock(Unlocks.ThrusterPack, false);
                SetUnlock(Unlocks.O2Mask, false);
            }
        }

        public bool GoodiesMenuEnabled()
        {
            return BitConverter.ToBoolean(api.ReadMemory(pid, rac1.goodies_menu, 1), 0);
        }

        public void SetInfiniteHealth(bool enabled)
        {
            if (enabled)
            {
                api.WriteMemory(pid, 0x7F558, 4, new byte[] { 0x30, 0x64, 0x00, 0x00 });
            }
            else
            {
                api.WriteMemory(pid, 0x7F558, 4, new byte[] { 0x30, 0x64, 0x9c, 0xe0 });
            }
        }

        public void SetGhostRatchet(bool enabled)
        {
            if (enabled) {
                ghostRatchetSubID = api.FreezeMemory(pid, rac1.addr.ghostTimer, 10);
            }
                else
            {
                api.ReleaseSubID(ghostRatchetSubID);
            }
        }

        public void SetDrekSkip(bool enabled)
        {
            api.WriteMemory(pid, rac1.addr.drekSkip, 1, BitConverter.GetBytes(enabled));
        }

        public void SetGoodies(bool enabled)
        {
            api.WriteMemory(pid, rac1.addr.goodiesMenu, 1, BitConverter.GetBytes(enabled));
        }

        public override void ToggleInfiniteAmmo(bool toggle = false)
        {
            if (toggle)
            {
                api.WriteMemory(pid, 0xAA2DC, 4, new byte[] { 0x60, 0x00, 0x00, 0x00 });
            }
            else
            {
                api.WriteMemory(pid, 0xAA2DC, 4, new byte[] { 0x7d, 0x05, 0x39, 0x2e });
            }
        }
    }
}
