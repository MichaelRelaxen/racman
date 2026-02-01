using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Threading;

namespace racman
{
    public class RaC1Addresses : IAddresses
    {
        // Current bolt count
        public uint boltCount => 0x969CA0;

        // Ratchet's coordinates
        public uint playerCoords => 0x969D60;

        // The player's current health.
        public uint playerHealth => 0x96BF88;

        // Controller inputs mask address
        public uint inputOffset => 0x964AF0;

        // Controller analog sticks address
        public uint analogOffset => 0x964A40;

        // First 0x4 for if planet should be loaded, the 0x4 after for planet to load.
        public uint loadPlanet => 0xA10700;

        // Currently loaded planet.
        public uint currentPlanet => 0x969C70;

        // Main level flags
        public uint levelFlags => 0xA0CA84;

        // Other level flags
        public uint miscLevelFlags => 0xA0CD1C;

        // Array of infobots collected
        public uint infobotFlags => 0x96CA0C;

        // Movies that have been watched
        public uint moviesFlags => 0x96BFF0;

        // Array of unlocked unlockables like gadgets, weapons and other items
        public uint unlockArray => 0x96C140;

        // Planet we're going to.
        public uint destinationPlanet => 0xa10704;

        // Current player state. 
        public uint playerState => 0x96BD64;

        // Count of frames in current level
        public uint planetFrameCount => 0xA10710;

        // Current game state, like currently playing, in menu, in ILM, etc.
        public uint gameState => 0x00A10708;

        // Which loading screen type you're current at, or the last loading screen you got in last load
        public uint loadingScreenID => 0x9645C8;

        //Frames until "Ghost Ratchet" runs out.
        public uint ghostTimer => 0x969EAC;

        // Set single byte to enable/disable drek skip.
        public uint drekSkip => 0xFACC7B;

        // Set single byte to enable/disable goodies menu. Not related to challenge mode.
        public uint goodiesMenu => 0x969CD3;

        // Array of whether or not you've unlocked any gold weapons.
        public uint goldItems => 0x969CA8;

        // Force third loading screen by setting to 2. Not to be confused with instant loads
        public uint fastLoad => 0x9645CF;

        // Array of whether or not you've collected gold bolts. 4 per planet.
        public uint goldBolts => 0xA0CA34;

        public uint debugUpdateOptions => 0x95c5c8;

        public uint debugModeControl => 0x95c5d4;

        public uint mobyInstances => 0x0A390A0;

        public uint drekCutscene => 0xFACC74;

        // Jankpot
        // 1 = active, 0 = inactive
        public uint jankpotState => 0x00a15f2c;
        // Current jankpot bolt count used for boilt per minute calculations
        public uint jankpotBolts => 0x00a0fd18;
        // Time spent in Jankpot state active
        public uint jankpotTimer => 0x00a0fd14;

        // Goodies unlocked
        public uint ngPlusGoodies => 0x00969cd0; 
        // Challenge mode
        public uint ngPlusState => 0x0096c9fc; 
    }

    public class rac1 : IGame, IAutosplitterAvailable
    {
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct Vec4
        {
            public float x;
            public float y;
            public float z;
            public float w;
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct GamePtr
        {
            public uint addr;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public unsafe struct Moby
        {
            public Vec4 coll_pos;
            public Vec4 position;
            public sbyte state;
            public byte group;
            public byte m_class;
            public byte alpha;
            public GamePtr p_class;
            public GamePtr p_chain;
            public float scale;
            public byte update_distance;
            public byte enabled;
            public short draw_distance;
            public ushort mode_bits;
            public ushort field19_0x36;
            public uint color;
            public byte field21_0x3c;
            public byte field22_0x3d;
            public byte field23_0x3e;
            public byte field24_0x3f;
            public Vec4 rotation;
            public byte field26_0x50;
            public byte animation_frame;
            public byte update_id;
            public byte animation_id;
            public float field30_0x54;
            public float field34_0x58;
            public float field35_0x5c;
            public GamePtr field36_0x60;
            public GamePtr manipulator1;
            public GamePtr field41_0x68;
            public GamePtr field42_0x6c;
            public byte field43_0x70;
            public byte field44_0x71;
            public byte field45_0x72;
            public byte field46_0x73;
            public uint p_update;
            public uint vars;
            public byte field49_0x7c;
            public byte field50_0x7d;
            public byte field51_0x7e;
            public byte animStateMaybe;
            public GamePtr manipulator2;
            public int field54_0x84;
            public int field55_0x88;
            public byte field56_0x8c;
            public byte field57_0x8d;
            public byte field58_0x8e;
            public byte field59_0x8f;
            public uint field60_0x90;
            public GamePtr collision;
            public GamePtr collision_mesh;
            public uint field63_0x9c;
            public byte field64_0xa0;
            public byte field65_0xa1;
            public byte field66_0xa2;
            public byte field67_0xa3;
            public sbyte damage;
            public byte field69_0xa5;
            public short oClass;
            public int field71_0xa8;
            public uint field72_0xac;
            public byte field73_0xb0;
            public byte field74_0xb1;
            public ushort UID;
            public byte field76_0xb4;
            public byte field77_0xb5;
            public byte field78_0xb6;
            public byte field79_0xb7;
            public GamePtr field80_0xb8;
            public byte field81_0xbc;
            public byte field82_0xbd;
            public byte field83_0xbe;
            public byte field84_0xbf;
            public fixed byte transform[64]; // Vec4[4] = 4 * 16 bytes = 64

            public static unsafe Moby ByteArrayToMoby(byte[] bytes)
            {
                if (BitConverter.IsLittleEndian)
                {
                    var type = typeof(Moby);
                    foreach (var field in type.GetFields())
                    {
                        if (field.FieldType == typeof(byte))
                            continue;

                        var offset = Marshal.OffsetOf(type, field.Name).ToInt32();

                        if (field.FieldType == typeof(Vec4))
                        {
                            for (int i = 0; i < 4; i++)
                                Array.Reverse(bytes, offset + (i * 4), 4);
                        }

                        int numBytesToReverse = 0;
                        if (field.FieldType == typeof(short) || field.FieldType == typeof(ushort))
                            numBytesToReverse = 2;
                        else if (field.FieldType == typeof(int) || field.FieldType == typeof(uint) ||
                                 field.FieldType == typeof(float) || field.FieldType == typeof(GamePtr))
                            numBytesToReverse = 4;
                        else if (field.FieldType == typeof(long) || field.FieldType == typeof(ulong) ||
                                 field.FieldType == typeof(double))
                            numBytesToReverse = 8;

                        if (numBytesToReverse > 0)
                            Array.Reverse(bytes, offset, numBytesToReverse);
                    }
                }

                fixed (byte* ptr = &bytes[0])
                {
                    return (Moby)Marshal.PtrToStructure((IntPtr)ptr, typeof(Moby));
                }
            }
        }
        public enum DebugOption
        {
            UpdateRatchet, 
            UpdateMobys,
            UpdateParticles,
            UpdateCamera,
            NormalCamera,
            Freecam,
            FreecamCharacter
        }

        public static RaC1Addresses addr = new RaC1Addresses();

        private uint lastPlanetIndex = 100;
       

        public rac1(IPS3API api) : base(api)
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
        public enum Thing
        {
            Item = 0,
            Flag = 1,
            Cutscene = 2
        }

        public dynamic MobyFlags = new
        {
            /*
            // Items
            Zoomerator = ("Zoomerator", 0x96bff0, Thing.Item),
            Raritanium = ("Raritanium", 0x96bff1, Thing.Item),
            Codebot = ("Codebot", 0x96bff2, Thing.Item),
            PremiumNanotech = ("Premium Nanotech", 0x96bff4, Thing.Item),
            UltraNanotech = ("Ultra Nanotech", 0x96bff5, Thing.Item),
            */

            // Flags
            Unk_VeldinClankCsTrigger = ("Clank Cutscene Trigger (Veldin)", 0x96bff8, Thing.Flag),
            Unk_bffc = ("unk", 0x96bffc, Thing.Flag),
            Unk_Moby1347 = ("unk (Moby 1347)", 0x96bffd, Thing.Flag),
            Unk_Moby729 = ("unk (Moby 729)", 0x96bffe, Thing.Flag),
            Unk_Moby730_731_790 = ("unk (Moby 730/731/790)", 0x96bfff, Thing.Flag),
            Unk_c000 = ("unk", 0x96c000, Thing.Flag),
            Unk_Moby1324_AridiaSP = ("unk (Moby 1324 Aridia SP)", 0x96c004, Thing.Flag),
            Unk_Moby762 = ("unk (Moby 762)", 0x96c005, Thing.Flag),
            Unk_Moby1342 = ("unk (Moby 1342)", 0x96c008, Thing.Flag),
            Unk_c009 = ("unk", 0x96c009, Thing.Flag),
            Unk_Moby1342_2 = ("unk (Moby 1342)", 0x96c00a, Thing.Flag),
            Unk_Moby1343 = ("unk (Moby 1343)", 0x96c00c, Thing.Flag),
            Unk_Moby1343_2 = ("unk (Moby 1343)", 0x96c00d, Thing.Flag),
            Unk_Moby1343_3 = ("unk (Moby 1343)", 0x96c00e, Thing.Flag),
            Unk_Moby1343_4 = ("unk (Moby 1343)", 0x96c00f, Thing.Flag),
            Unk_Moby1061 = ("unk (Moby 1061)", 0x96c010, Thing.Flag),
            Unk_Moby1109 = ("unk (Moby 1109)", 0x96c011, Thing.Flag),
            Unk_c012 = ("unk", 0x96c012, Thing.Flag),
            Unk_Moby1051 = ("unk (Moby 1051)", 0x96c013, Thing.Flag),
            Unk_Moby1109_2 = ("unk (Moby 1109)", 0x96c014, Thing.Flag),
            Unk_Moby1348 = ("unk (Moby 1348)", 0x96c015, Thing.Flag),
            Unk_Moby436 = ("unk (Moby 436)", 0x96c018, Thing.Flag),
            Unk_Moby436_2 = ("unk (Moby 436)", 0x96c019, Thing.Flag),
            Unk_Moby436_3 = ("unk (Moby 436)", 0x96c01a, Thing.Flag),
            Unk_Moby435_BataliaCommando = ("unk (Moby 435 Batalia Commando)", 0x96c01c, Thing.Flag),
            Unk_Moby253_467_472 = ("unk (Moby 253/467/472)", 0x96c01e, Thing.Flag),
            Unk_Moby1349 = ("unk (Moby 1349)", 0x96c01f, Thing.Flag),
            Unk_Moby468_469 = ("unk (Moby 468/469)", 0x96c020, Thing.Flag),
            Unk_c021 = ("unk", 0x96c021, Thing.Flag),
            Unk_c022 = ("unk", 0x96c022, Thing.Flag),
            Unk_Moby467_472 = ("unk (Moby 467/472)", 0x96c025, Thing.Flag),
            Unk_c028 = ("unk", 0x96c028, Thing.Flag),
            Unk_Moby1172_1181 = ("unk (Moby 1172/1181)", 0x96c029, Thing.Flag),
            Unk_Moby1285_1286_1287_1288 = ("unk (Moby 1285/1286/1287/1288)", 0x96c033, Thing.Flag),
            Unk_c034 = ("unk", 0x96c034, Thing.Flag),
            Unk_c035 = ("unk", 0x96c035, Thing.Flag),
            Unk_c038 = ("unk", 0x96c038, Thing.Flag),
            Unk_c039 = ("unk", 0x96c039, Thing.Flag),
            Unk_c03a = ("unk", 0x96c03a, Thing.Flag),
            Unk_c03b = ("unk", 0x96c03b, Thing.Flag),
            Unk_Moby1015_1282 = ("unk (Moby 1015/1282)", 0x96c03c, Thing.Flag),
            Unk_c03d = ("unk", 0x96c03d, Thing.Flag),
            Unk_c03e = ("unk", 0x96c03e, Thing.Flag),
            Unk_c03f = ("unk", 0x96c03f, Thing.Flag),
            Unk_Moby353 = ("unk (Moby 353)", 0x96c040, Thing.Flag),
            Unk_c041 = ("unk", 0x96c041, Thing.Flag),
            Unk_Moby1243_TheGuy = ("unk (Moby 1243 The Guy)", 0x96c044, Thing.Flag),
            Unk_Moby1268_318 = ("unk (Moby 1268/318)", 0x96c045, Thing.Flag),
            Unk_Moby1156 = ("unk (Moby 1156)", 0x96c046, Thing.Flag),
            Unk_Moby1159 = ("unk (Moby 1159)", 0x96c047, Thing.Flag),
            Unk_c048 = ("unk", 0x96c048, Thing.Flag),
            Unk_c049 = ("unk", 0x96c049, Thing.Flag),
            Unk_c04a = ("unk", 0x96c04a, Thing.Flag),
            Unk_Moby1180 = ("unk (Moby 1180)", 0x96c04b, Thing.Flag),
            Unk_Moby1404 = ("unk (Moby 1404)", 0x96c04c, Thing.Flag),
            Unk_Moby282 = ("unk (Moby 282)", 0x96c051, Thing.Flag),
            Unk_Moby1353 = ("unk (Moby 1353)", 0x96c054, Thing.Flag),
            Unk_Moby1353_2 = ("unk (Moby 1353)", 0x96c055, Thing.Flag),
            Unk_Moby1353_3 = ("unk (Moby 1353)", 0x96c056, Thing.Flag),
            Unk_Moby684 = ("unk (Moby 684)", 0x96c058, Thing.Flag),
            Unk_Moby1430 = ("unk (Moby 1430)", 0x96c05d, Thing.Flag),
            Unk_Moby1469 = ("unk (Moby 1469)", 0x96c05e, Thing.Flag),
            Unk_Moby1469_2 = ("unk (Moby 1469)", 0x96c05f, Thing.Flag),
            Unk_Moby1377 = ("unk (Moby 1377)", 0x96c060, Thing.Flag),
            Unk_Moby1377_2 = ("unk (Moby 1377)", 0x96c061, Thing.Flag),
            Unk_Moby654 = ("unk (Moby 654)", 0x96c062, Thing.Flag),
            Unk_c063 = ("unk", 0x96c063, Thing.Flag),
            Unk_c064 = ("unk", 0x96c064, Thing.Flag),
            Unk_c065 = ("unk", 0x96c065, Thing.Flag),
            Unk_Moby1428 = ("unk (Moby 1428)", 0x96c068, Thing.Flag),
            Unk_Moby1402 = ("unk (Moby 1402)", 0x96c06e, Thing.Flag),
            Unk_Moby586 = ("unk (Moby 586)", 0x96c06f, Thing.Flag),
            Unk_c074 = ("unk", 0x96c074, Thing.Flag),
            Unk_c075 = ("unk", 0x96c075, Thing.Flag),
            Unk_c076 = ("unk", 0x96c076, Thing.Flag),
            Unk_c077 = ("unk", 0x96c077, Thing.Flag),
            Unk_c078 = ("unk", 0x96c078, Thing.Flag),
            Unk_c079 = ("unk", 0x96c079, Thing.Flag),
            Unk_Moby806 = ("unk (Moby 806)", 0x96c07a, Thing.Flag),
            Unk_c07b = ("unk", 0x96c07b, Thing.Flag),
            Unk_c07c = ("unk", 0x96c07c, Thing.Flag),
            Unk_c07d = ("unk", 0x96c07d, Thing.Flag),
            Unk_c07e = ("unk", 0x96c07e, Thing.Flag),
            Unk_c07f = ("unk", 0x96c07f, Thing.Flag),
            Unk_c080 = ("unk", 0x96c080, Thing.Flag),
        };

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

        /// <summary>
        /// Enables instant loads by overwriting code that starts loads somehow.
        /// </summary>
        /// <param name="toggle">if true, writes instant load code to the game, if false restores the original code</param>
        public override void SetFastLoads(bool toggle)
        {
            if (toggle)
            {
                api.WriteMemory(pid, 0x165490, 0x60000000); // skip loading screen [nop]
                api.WriteMemory(pid, 0x160060, 0x38600006); // mute sound on loading screen, play non-existent sound [li r3, 6]
                api.WriteMemory(pid, 0x1641f8, 0x38600006);
            }
            else
            {
                // reverts back to original instructions
                api.WriteMemory(pid, 0x165490, 0x4bffe519);
                api.WriteMemory(pid, 0x160060, 0x546307be);
                api.WriteMemory(pid, 0x1641f8, 0x546307be);
            }
        }

        /// <summary>
        /// Sets an unlockable item/gadget/weapon so that it's owned, or optionally unlocks it as gold instead.
        /// </summary>
        /// <param name="item">Item as tuple, needs item "id" in second tuple item, first item is string, but its value doesn't really matter.</param>
        /// <param name="unlocked">true if owned, false if not</param>
        /// <param name="gold">Whether to set item as golded (true) or to just unlock item (false)</param>
        public void SetUnlock((string, int) item, bool unlocked, bool gold = false)
        {
            api.WriteMemory(pid, (gold ? rac1.addr.goldItems : rac1.addr.unlockArray) + (uint)item.Item2, BitConverter.GetBytes(unlocked));
        }

        Dictionary<int, bool> ownedUnlocks = new Dictionary<int, bool>();
        Dictionary<int, bool> ownedGoldItems = new Dictionary<int, bool>();
        long lastUnlocksUpdate = 0;
        long lastGoldItemsUpdate = 0;

        public IEnumerable<(uint addr, uint size)> AutosplitterAddresses => new (uint, uint)[]
            {
                (addr.playerCoords, 8),
                (addr.destinationPlanet + 3, 1),
                (addr.currentPlanet + 3, 1),
                (addr.playerState+2, 2),
                (addr.planetFrameCount, 4),
                (addr.gameState, 4),
                (addr.loadingScreenID + 3, 1),
                (0x00aff000+3, 1),
                (0x00aff010+3, 1),
                (0x00aff020+3, 1),
                (0xa0ca75, 1),
                (0x00aff030+3, 1),
                (0x0096bff1, 2),
            };

        /// <summary>
        /// Updates internal list of unlocked items. There was a bug in the Ratchetron C# API that maked it unfeasibly slow to get each item as a single byte.
        /// </summary>
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

        /// <summary>
        /// Updates internal list of golded items. There's a bug in Ratchetron or the Ratchetron C# API that makes it unfeasibly slow to get each item as a single byte.
        /// This function can be called as often as you'd like, but it only updates every second or so, as to not overload the Ratchetron API. No idea why the API is so fucky, this might be fixed in the future, who knows. 
        /// </summary>
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

        /// <summary>
        /// If you do or do not have an item unlocked or golded.
        /// </summary>
        /// <param name="item">Tuple<string, int> of item to check</param>
        /// <param name="gold">Whether or not to check for golded state</param>
        /// <returns></returns>
        public bool HasUnlock((string, int) item, bool gold = false)
        {
            UpdateGoldItems();
            UpdateUnlocks();
            return gold ? ownedGoldItems[item.Item2] : ownedUnlocks[item.Item2];
        }

        /// <summary>
        /// Get list of unlockables in the game
        /// </summary>
        /// <returns></returns>
        public (string, int)[] GetUnlocks()
        {
            List<(string, int)> unlocks = new List<(string, int)>();
           
            foreach (var item in Unlocks.GetType().GetProperties())
            {
                unlocks.Add(item.GetValue(Unlocks));
            }


            return unlocks.ToArray();
        }

        /// <summary>
        /// Resets level flag of destination planet
        /// </summary>
        public override void ResetLevelFlags()
        {

            // Not working properly right now?
            api.WriteMemory(pid, rac1.addr.levelFlags + (planetToLoad * 0x10), 0x10, new byte[0x10]);
            api.WriteMemory(pid, rac1.addr.miscLevelFlags + (planetToLoad * 0x100), 0x100, new byte[0x100]);
            api.WriteMemory(pid, rac1.addr.infobotFlags + planetToLoad, 18, new byte[18]);

            foreach (var d in MobyFlags.GetType().GetProperties())
            {
                (string name, int addr, Thing type) flag = d.GetValue(MobyFlags);
                api.WriteMemory(pid, (uint)flag.addr, 1, new byte[1]);
            }

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


        public override void ResetGoldBolts(uint planetIndex)
        {
            api.WriteMemory(pid, rac1.addr.goldBolts + (planetIndex * 4), 0);
        }

        public void ResetAllGoldBolts()
        {
            api.WriteMemory(pid, rac1.addr.goldBolts, new byte[80]);
        }

        public void UnlockAllGoldBolts()
        {
            api.WriteMemory(pid, rac1.addr.goldBolts, Enumerable.Repeat((byte)1, 80).ToArray());
        }

        /// <summary>
        /// Whether or not goodies menu is enabled
        /// </summary>
        /// <returns>true if enabled false if not</returns>
        public bool GoodiesMenuEnabled()
        {
            return BitConverter.ToBoolean(api.ReadMemory(pid, rac1.addr.goodiesMenu, 1), 0);
        }

        /// <summary>
        /// Inifnite health is set by overwriting game code that deals health with nops.
        /// </summary>
        /// <param name="enabled">if true overwrites game code with nops, if false restores original game code</param>
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

        /// <summary>
        /// Ghost ratchet works by having a frame countdown, we hard enable ghost ratchet by freezing the frame countdown to 10.
        /// </summary>
        /// <param name="enabled">if true freezes frame countdown to 10, if false releases the freeze</param>
        public void SetGhostRatchet(bool enabled)
        {
            if (enabled) {
                ghostRatchetSubID = api.FreezeMemory(pid, addr.ghostTimer, 10);
            }
                else
            {
                api.ReleaseSubID(ghostRatchetSubID);
            }
        }

        /// <summary>
        /// Drek skip sets the destroyer to be up.
        /// </summary>
        /// <param name="enabled">Destroyer up or not</param>
        public void SetDrekSkip(bool enabled)
        {
            api.WriteMemory(pid, rac1.addr.drekSkip, 1, BitConverter.GetBytes(enabled));
        }

        /// <summary>
        /// Shows drek button cutscene.
        /// </summary>
        /// <param name="enabled">Destroyer up or not</param>
        public void SetDrekCutscene(bool enabled)
        {
            api.WriteMemory(pid, rac1.addr.drekCutscene, 1, BitConverter.GetBytes(enabled));
        }

        /// <summary>
        /// Goodies menu is the NG+ goodies menu at the bottom of the main pause menu. This does not set challenge mode, it only enables the goodies menu as if you "timewarped to before you beat Drek" on Veldin 2.
        /// </summary>
        /// <param name="enabled">Sets or unsets goodies menu</param>
        public void SetGoodies(bool enabled)
        {
            api.WriteMemory(pid, rac1.addr.goodiesMenu, 1, BitConverter.GetBytes(enabled));
        }

        /// <summary>
        /// Overwrites game code that decreases ammo when you use a weapon
        /// </summary>
        /// <param name="toggle">Overwrites ammo decreasement code with nops on true, restores original game code on false</param>
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

        /// <summary>
        /// Resets shooting skill points so that they don't carry over.
        /// </summary>
        /// <param name="reset">Sets or unsets shooting skill point variables.</param>
        public void SetShootSkillPoints(bool reset = false)
        {
            if (reset)
            {
                // Reset Batalia Sonic Summoner
                api.WriteMemory(pid, 0xA15F3C, 8, new string('0', 16));

                // Reset all other shooting SPs.
                api.WriteMemory(pid, 0x96C9DC, 32, new string('0', 64));
            }
            else
            {
                // Setup Batalia Sonic Summoner.
                api.WriteMemory(pid, 0xA15F3C, 8, string.Concat(Enumerable.Repeat("00000001", 2)));

                // Setup all shooting SPs.
                api.WriteMemory(pid, 0x96C9DC, 32,string.Concat(Enumerable.Repeat("00000020", 8)));
            }
        }


        public override void SetupFile()
        {
            throw new NotImplementedException();
        }

        public override void CheckInputs(object sender, EventArgs e)
        {
            if (Inputs.RawInputs == ConfigureCombos.saveCombo && inputCheck)
            {
                SavePosition();
                inputCheck = false;
            }
            if (Inputs.RawInputs == ConfigureCombos.loadCombo && inputCheck)
            {
                LoadPosition();
                inputCheck = false;
            }
            if (Inputs.RawInputs == ConfigureCombos.dieCombo && inputCheck)
            {
                KillYourself();
                inputCheck = false;
            }
            if (Inputs.RawInputs == ConfigureCombos.loadPlanetCombo & inputCheck)
            {
                LoadPlanet();
                inputCheck = false;
            }
            if (Inputs.RawInputs == ConfigureCombos.runScriptCombo && inputCheck)
            {
                AttachPS3Form.scripting?.RunCurrentCode();
                inputCheck = false;
            }
            if (Inputs.RawInputs == 0x00 & !inputCheck)
            {
                inputCheck = true;
            }
        }
        public List<DebugOption> DebugOptions()
        {
            List<DebugOption> options = new List<DebugOption>();

            uint debugUpdateOptions = BitConverter.ToUInt32(api.ReadMemory(pid, addr.debugUpdateOptions, 4).Reverse().ToArray(), 0);
            uint debugModeControl = BitConverter.ToUInt32(api.ReadMemory(pid, addr.debugModeControl, 4).Reverse().ToArray(), 0);

            if ((debugUpdateOptions & 1) > 0)
            {
                options.Add(DebugOption.UpdateRatchet);
            }

            if ((debugUpdateOptions & 2) > 0)
            {
                options.Add(DebugOption.UpdateMobys);
            }

            if ((debugUpdateOptions & 4) > 0)
            {
                options.Add(DebugOption.UpdateParticles);
            }

            if (debugModeControl == 0)
            {
                options.Add(DebugOption.NormalCamera);
            }

            if (debugModeControl == 1)
            {
                options.Add(DebugOption.Freecam);
            }

            if (debugModeControl == 2)
            {
                options.Add(DebugOption.FreecamCharacter);
            }

            return options;
        }

        public void SetDebugOption(DebugOption option, bool value)
        {
            uint debugUpdateOptions = BitConverter.ToUInt32(api.ReadMemory(pid, addr.debugUpdateOptions, 4).Reverse().ToArray(), 0);

            switch (option)
            {
                case DebugOption.UpdateRatchet:
                    api.WriteMemory(pid, addr.debugUpdateOptions, (value ? debugUpdateOptions | 1 : debugUpdateOptions ^ 1));
                    break;
                case DebugOption.UpdateMobys:
                    api.WriteMemory(pid, addr.debugUpdateOptions, (value ? debugUpdateOptions | 2 : debugUpdateOptions ^ 2));
                    break;
                case DebugOption.UpdateParticles:
                    api.WriteMemory(pid, addr.debugUpdateOptions, (value ? debugUpdateOptions | 4 : debugUpdateOptions ^ 4));
                    break;
                case DebugOption.UpdateCamera:
                    api.WriteMemory(pid, addr.debugUpdateOptions, (value ? debugUpdateOptions | 8 : debugUpdateOptions ^ 8));
                    break;

                case DebugOption.NormalCamera:
                    SetDebugOption(DebugOption.UpdateCamera, true);
                    api.WriteMemory(pid, addr.debugModeControl, 0);
                    break;
                case DebugOption.Freecam:
                    SetDebugOption(DebugOption.UpdateCamera, false);
                    api.WriteMemory(pid, addr.debugModeControl, 1);
                    break;
                case DebugOption.FreecamCharacter:
                    SetDebugOption(DebugOption.UpdateCamera, false);
                    api.WriteMemory(pid, addr.debugModeControl, 2);
                    break;
            }
        }
    }
}
