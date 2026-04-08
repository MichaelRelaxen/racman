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

        public static readonly Dictionary<string, int> LevelFlags = new Dictionary<string, int>
        {
            // Global
            { "RYNO_HAS4", 34 },
            { "RYNO_HAS7", 35 },
            { "RYNO_HAS10", 36 },
            { "RYNO_HAS13", 37 },
            // Kerwan
            { "LVL_METROPOLIS_GRINDRAIL_USED", 38 },
            { "MOVIE_METROPOLIS_INTRO", 183 },
            { "MOVIE_METROPOLIS_DEFENSE", 184 },
            { "MOVIE_METROPOLIS_TACHYON", 185 },
            // Cobalia
            { "LVL_COBALIA_BRIDGE_EXTENDED", 39 },
            { "LVL_COBALIA_GEL_FACTORY_ACTIVE", 40 },
            { "LVL_COBALIA_LEFT_LEVEL", 41 },
            { "LVL_COBALIA_LEVIATHAN_SOUL", 42 },
            { "LVL_COBALIA_ROCK_WALL", 43 },
            { "LVL_COBALIA_DIA_WHERE", 44 },
            { "LVL_COBALIA_DIA_TACHYON", 45 },
            { "LVL_COBALIA_LEV2_KILLED", 46 },
            { "LVL_COBALIA_LEV3_KILLED", 47 },
            { "LVL_COBALIA_EXPLORED", 48 },
            { "LVL_COBALIA_QUICK_SELECT", 49 },
            { "LVL_COBALIA_TRADEPORT_INTRO", 50 },
            { "LVL_COBALIA_PLATFORM1", 51 },
            { "LVL_COBALIA_PLATFORM2", 52 },
            { "LVL_COBALIA_PLATFORM3", 53 },
            { "LVL_COBALIA_PLATFORM4", 54 },
            { "LVL_COBALIA_PLATFORM5", 55 },
            { "LVL_COBALIA_PLATFORM6", 56 },
            { "LVL_COBALIA_PLATFORM7", 57 },
            { "LVL_COBALIA_LEV_INTRO", 58 },
            { "LVL_COBALIA_ITEMS_INTRO", 59 },
            { "LVL_COBALIA_ITEMS_GOLDEN", 60 },
            { "LVL_COBALIA_STRATUS_OFFER", 61 },
            { "LVL_COBALIA_SMUGGLER_CONV", 62 },
            { "LVL_COBALIA_VENDOR_CONV", 63 },
            { "LVL_COBALIA_WATERFALL1", 64 },
            { "LVL_COBALIA_WATERFALL2", 65 },
            { "LVL_COBALIA_AIRLOCK1", 66 },
            { "LVL_COBALIA_SINGLE", 67 },
            { "LVL_COBALIA_DOUBLE", 68 },
            { "LVL_COBALIA_TRIPLE", 69 },
            { "LVL_COBALIA_STEPPING", 70 },
            { "LVL_COBALIA_AIRLOCK2", 71 },
            { "MOVIE_COBALIA_INTRO", 186 },
            { "MOVIE_COBALIA_SMUGGLER", 187 },
            // Kortog
            { "LVL_STRATUS_CITY_LEFT_LEVEL", 104 },
            { "MOVIE_STRATUS_CITY_INTRO", 188 },
            { "MOVIE_STRATUS_CITY_SWIMMING", 189 },
            { "MOVIE_STRATUS_CITY_TRAVERSAL", 190 },
            { "LVL_STRATUS_CITY_SKYDIVE_COMPLETED", 196 },
            { "LVL_STRATUS_CITY_ZONI_CHALLENGE_COMPLETED", 197 },
            { "LVL_STRATUS_CITY_KILL_HELP_1", 198 },
            { "LVL_STRATUS_CITY_KILL_HELP_2", 199 },
            { "LVL_STRATUS_CITY_KILL_HELP_3", 200 },
            { "LVL_STRATUS_CITY_KILL_HELP_4", 201 },
            { "LVL_STRATUS_CITY_KILL_HELP_5", 202 },
            { "LVL_STRATUS_CITY_KILL_HELP_6", 203 },
            { "LVL_STRATUS_CITY_BRIDGE_BLOWN", 204 },
            // Fastoon
            { "LVL_FAST_GETTING_PARTS", 105 },
            { "LVL_FAST_SHIP_FIXED", 106 },
            { "LVL_FAST_LEFT_LEVEL", 107 },
            { "LVL_FAST_RANDOM_QWARK_DONE", 108 },
            { "LVL_FAST_AUTO_NPC", 109 },
            { "LVL_FAST_KILL_HELP_1", 110 },
            { "LVL_FAST_KILL_HELP_2", 111 },
            { "LVL_FAST_KILL_HELP_3", 112 },
            { "LVL_FASTOON_SHIP_PART_1", 221 },
            { "LVL_FASTOON_SHIP_PART_2", 222 },
            { "LVL_FASTOON_SHIP_PART_3", 223 },
            { "LVL_FASTOON_SHIP_PART_4", 224 },
            { "LVL_FASTOON_SHIP_PART_5", 225 },
            { "LVL_FASTOON_SHIP_PART_6", 226 },
            { "LVL_FAST_CLANK_MOVIE_1_PLAYED", 291 },
            // Voron
            { "LVL_SC1_COMPLETE", 266 },
            { "LVL_SC1_PLAYED_RUSTY_PETE_LINE", 269 },
            // Mukow
            { "LVL_IFF_HAS_ENTERED_ARENA", 72 },
            { "LVL_IFF_HEARD_COMPLETE_ARENA", 73 },
            { "LVL_IFF_CHALLENGE_1_DONE", 74 },
            { "LVL_IFF_CHALLENGE_2_DONE", 75 },
            { "LVL_IFF_LEFT_AFTER_CRUSHTO", 76 },
            { "LVL_IFF_INFLATOPOD_DONE", 77 },
            { "LVL_IFF_GOT_STATUE_1", 78 },
            { "LVL_IFF_GOT_STATUE_2", 79 },
            { "LVL_IFF_GOT_STATUE_3", 80 },
            { "LVL_IFF_GOT_STATUE_4", 81 },
            { "LVL_IFF_GOT_STATUE_5", 82 },
            { "LVL_IFF_GOT_STATUE_6", 83 },
            { "LVL_IFF_GOT_ALL_STATUES", 84 },
            { "LVL_IFF_CKPT_IN_ARENA", 85 },
            { "LVL_IFF_LEAVING_INFO", 86 },
            { "LVL_IFF_SET_TO_RETURN_CHALLENGES", 87 },
            { "LVL_IFF_RETURN_CHALLENGE_9_DONE", 88 },
            { "LVL_IFF_RETURN_CHALLENGE_10_DONE", 89 },
            { "LVL_IFF_RETURN_CHALLENGE_11_DONE", 90 },
            { "LVL_IFF_RETURN_CHALLENGE_12_DONE", 91 },
            { "LVL_IFF_RETURN_CHALLENGE_13_DONE", 92 },
            { "LVL_IFF_RETURN_CHALLENGE_14_DONE", 93 },
            { "LVL_IFF_RETURN_CHALLENGE_15_DONE", 94 },
            { "LVL_IFF_SAW_CRUSHTO_CINE", 95 },
            { "LVL_IFF_SAW_ZORTHAN_CINE", 96 },
            { "LVL_IFF_ENTERED_ENEMY_SEG", 97 },
            { "LVL_IFF_USED_INFLATOPOD_PLATFORM", 98 },
            { "LVL_IFF_DECRYPTOR_FORCEFIELD_DONE", 99 },
            { "LVL_IFF_ARENA_THEME_1_PLAYED", 100 },
            { "LVL_IFF_ARENA_THEME_2_PLAYED", 101 },
            { "LVL_IFF_ARENA_THEME_3_PLAYED", 102 },
            { "LVL_IFF_ARENA_THEME_4_PLAYED", 103 },
            // Nundac
            { "LVL_APOGEE_DIA_STATION", 127 },
            { "LVL_APOGEE_DIA_LOMBAX", 128 },
            { "LVL_APOGEE_DIA_WAR", 129 },
            { "LVL_APOGEE_DIA_CRAGMITE", 130 },
            { "LVL_APOGEE_DIA_KILL", 131 },
            { "LVL_APOGEE_DIA_PARROT", 132 },
            { "LVL_APOGEE_SOUL_COUNTER", 133 },
            { "LVL_APOGEE_SEG2_UNLOCKED", 134 },
            { "LVL_APOGEE_GEOPUZZLE_DONE", 135 },
            { "LVL_APOGEE_SHUTTLE_USED", 136 },
            { "LVL_APOGEE_SHUTTLE_POS", 137 },
            { "LVL_APOGEE_LEVS_DEAD", 138 },
            { "LVL_APOGEE_BIG_ROOM_CP", 139 },
            { "MOVIE_APOGEE_CLANK_UPGRADE", 191 },
            { "MOVIE_APOGEE_TALWYN", 192 },
            { "MOVIE_APOGEE_SMUGGLER", 193 },
            { "LVL_APOGEE_STATION_EXIT", 288 },
            { "LVL_APOGEE_EXTRA1", 289 },
            { "LVL_APOGEE_EXTRA2", 290 },
            // Ardolis
            { "LVL_PIRATE_BASE_GONDOLA", 113 },
            { "LVL_PIRATE_BASE_LEFT_LEVEL", 114 },
            { "LVL_PIRATE_BASE_GEOPUZZLE1_DONE", 115 },
            { "LVL_PIRATE_BASE_GEOPUZZLE2_DONE", 116 },
            { "LVL_PIRATE_BASE_GEOPUZZLE3_DONE", 117 },
            { "LVL_PIRATE_BASE_DESIGNER_CINEMA_1", 118 },
            { "LVL_PIRATE_BASE_DESIGNER_CINEMA_2", 119 },
            { "LVL_PIRATE_BASE_DESIGNER_CINEMA_3", 120 },
            { "LVL_PIRATE_BASE_DESIGNER_CINEMA_4", 121 },
            { "LVL_PIRATE_BASE_DESIGNER_CINEMA_5", 122 },
            { "LVL_PIRATE_BASE_PIRATE_GAME_1", 123 },
            { "LVL_PIRATE_BASE_PIRATE_GAME_2", 124 },
            { "LVL_PIRATE_BASE_MINIBOSS_SPAWNED", 125 },
            { "LVL_PIRATE_BASE_STARTPOINT_VO", 126 },
            { "MOVIE_PIRATE_BASE_MAPROOM", 194 },
            { "MOVIE_PIRATE_BASE_END", 195 },
            { "PIRATE_BASE_INFLATOPOD_DOOR_1", 272 },
            { "PIRATE_BASE_INFLATOPOD_DOOR_2", 273 },
            { "PIRATE_BASE_INFLATOPOD_GRINDRAIL", 277 },
            // Rakar
            { "LVL_SC2_COMPLETE", 267 },
            // Rykan V
            { "LVL_RYKANV_STARTED_RYKANV", 140 },
            { "LVL_RYKANV_SKYDIVE_DONE", 141 },
            { "LVL_RYKANV_MISSION_1_DONE", 142 },
            { "LVL_RYKANV_MISSION_2_DONE", 143 },
            { "LVL_RYKANV_BASE_TURRETS_DEAD", 144 },
            { "LVL_RYKANV_HAVE_ENTERED_TRADEPORT", 145 },
            { "LVL_RYKANV_CAVE_FACILITY_CINE", 146 },
            { "LVL_RYKANV_IN_TRADEPORT_SEG", 147 },
            { "LVL_RYKANV_IN_MAGCYCLE_SEG", 148 },
            { "LVL_RYKANV_MAGCYCLE_CHKPT_1", 149 },
            { "LVL_RYKANV_MAGCYCLE_CHKPT_2", 150 },
            { "LVL_RYKANV_IN_CAVE_FACILITY_SEG", 151 },
            { "LVL_RYKANV_GOT_MAGCYCLE", 152 },
            { "LVL_RYKANV_SAW_MAGCYCLE_EQUIP", 153 },
            { "LVL_RYKANV_HAVE_ENTERED_MAGCYCLE", 154 },
            { "LVL_RYKANV_HEARD_OPENER", 282 },
            { "LVL_RYKANV_HEARD_MAGCYCLE", 283 },
            // Sargasso
            { "LVL_SARGASSO_BRIDGE_EXTEND", 155 },
            { "LVL_SARGASSO_SOULS_COLLECTED", 244 },
            { "LVL_SARGASSO_ZONI_CHALLENGE_COMPLETED", 245 },
            { "LVL_SARGASSO_GOT_SMUGGLER_MINERALS", 246 },
            { "LVL_SARGASSO_DEC_1_DONE", 247 },
            { "LVL_SARGASSO_DEC_2_DONE", 248 },
            { "LVL_SARGASSO_DEC_3_DONE", 249 },
            { "LVL_SARGASSO_DEC_4_DONE", 250 },
            // Kreeli
            { "LVL_IRIS_PIRATE_GAME_1", 227 },
            { "LVL_IRIS_PIRATE_GAME_2", 228 },
            { "LVL_IRIS_PIRATE_GAME_3", 229 },
            { "LVL_IRIS_PIRATE_GAME_4", 230 },
            { "LVL_IRIS_GEOLASER_DONE", 231 },
            { "LVL_IRIS_ICEBRIDGE_DESTROYED", 232 },
            { "LVL_IRIS_SEG2_UNLOCKED", 233 },
            { "LVL_IRIS_SEG3_UNLOCKED", 234 },
            { "LVL_IRIS_TAXI_UNLOCKED", 235 },
            { "LVL_IRIS_LOCKINROOM_COMPLETE", 236 },
            { "LVL_IRIS_CLANK_COMPLETE", 237 },
            { "LVL_IRIS_INTRO_CINEMA", 238 },
            { "LVL_IRIS_DESIGNER_CINEMA_1", 239 },
            { "LVL_IRIS_DESIGNER_CINEMA_2", 240 },
            { "LVL_IRIS_DESIGNER_CINEMA_3", 241 },
            { "LVL_IRIS_DESIGNER_CINEMA_4", 242 },
            { "LVL_IRIS_DESIGNER_CINEMA_5", 243 },
            { "LVL_IRIS_SHAMUS1_THEME", 279 },
            { "LVL_IRIS_SHAMUS2_THEME", 280 },
            { "LVL_IRIS_SHAMUS3_THEME", 281 },
            // Viceron
            { "LVL_ZORDOOM_INTRO", 156 },
            { "LVL_ZORDOOM_BREAKOUT", 157 },
            { "LVL_ZORDOOM_RESCUED_TAL", 158 },
            { "LVL_ZORDOOM_DECRYPTOR_GRINDRAIL_DONE", 159 },
            { "LVL_ZORDOOM_DECRYPTOR_BRIDGE_DONE", 160 },
            { "LVL_ZORDOOM_DECRYPTOR_CELL_ELEV_DONE", 161 },
            { "LVL_ZORDOOM_DECRYPTOR_TRAPDOOR_DONE", 162 },
            { "LVL_ZORDOOM_DECRYPTOR_FREE_TAL_DONE", 163 },
            { "LVL_ZORDOOM_BREAKOUT_COMPLETE", 164 },
            // Verdigris
            { "LVL_SC3_COMPLETE", 268 },
            { "LVL_SC_PLAYED_APHELION_LINE", 270 },
            { "SC_PLAYED_BONUS_TARGET_MSG", 278 },
            // Jasindu
            { "LVL_KERCHU_BOSS_DEAD", 251 },
            { "LVL_KERCHU_BOSS_SEEN", 252 },
            { "LVL_KERCHU_SEG1_DOORSBROKEN", 253 },
            { "LVL_KERCHU_OPENINGCAMERA_DONE", 254 },
            // Ublik
            { "LVL_SLAG_PIRATEDOOR_1", 255 },
            { "LVL_SLAG_PIRATEDOOR_2", 256 },
            { "LVL_SLAG_PIRATEDOOR_3", 257 },
            { "LVL_SLAG_BOSS_DEFEATED", 258 },
            { "LVL_SLAG_DESIGNER_CINEMA_1", 259 },
            { "LVL_SLAG_DESIGNER_CINEMA_2", 260 },
            { "LVL_SLAG_DESIGNER_CINEMA_3", 261 },
            { "LVL_SLAG_DESIGNER_CINEMA_4", 262 },
            { "LVL_SLAG_DESIGNER_CINEMA_5", 263 },
            { "LVL_SLAG_CHECKPOINT", 264 },
            { "LVL_SLAG_ARENA_CHECKPOINT", 265 },
            // Reepor
            { "LVL_CRAGRUINS_SKYDIVE_DONE", 205 },
            { "LVL_CRAGRUINS_MISSION_1_DONE", 206 },
            { "LVL_CRAGRUINS_MISSION_2_DONE", 207 },
            { "LVL_CRAGRUINS_MISSION_3_DONE", 208 },
            { "LVL_CRAGRUINS_CLANK_DONE", 209 },
            { "LVL_CRAGRUINS_TRAVERSAL_CHKPT_1", 210 },
            { "LVL_CRAGRUINS_TRAVERSAL_DONE", 211 },
            // Igliak
            { "LVL_MERIDIAN_OPENING_CINE_DONE", 212 },
            { "LVL_MERIDIAN_ENEMY_CHKPT_1", 213 },
            { "LVL_MERIDIAN_ENEMY_DONE", 214 },
            { "LVL_MERIDIAN_MAGCYCLE_CHKPT_1", 215 },
            { "LVL_MERIDIAN_MAGCYCLE_DONE", 216 },
            { "LVL_MERIDIAN_ROBOWING_FORWARD_DONE", 217 },
            { "LVL_MERIDIAN_ROBOWING_BATTLE_DONE", 218 },
            { "LVL_MERIDIAN_ROBOWING_DONE", 219 },
            { "LVL_MERIDIAN_DC_DONE", 220 },
            // Fastoon 2
            { "LVL_FASTOON_RETURN_INTRO", 165 },
            { "LVL_FASTOON_RETURN_MIDWAY_MOVIE", 166 },
            { "LVL_FASTOON_RETURN_SKYDIVE_DONE", 167 },
            { "LVL_FASTOON_RETURN_MISSION_1_DONE", 168 },
            { "LVL_FASTOON_RETURN_MISSION_2_DONE", 169 },
            { "LVL_FASTOON_RETURN_MISSION_3_DONE", 170 },
            { "LVL_FASTOON_RETURN_GUN1_DESTROYED", 171 },
            { "LVL_FASTOON_RETURN_GUN2_DESTROYED", 172 },
            { "LVL_FASTOON_RETURN_GUN3_DESTROYED", 173 },
            { "LVL_FASTOON_RETURN_GUN4_DESTROYED", 174 },
            { "LVL_FASTOON_RETURN_GUN5_DESTROYED", 175 },
            { "LVL_FASTOON_RETURN_TAL_INTRO_CINE", 176 },
            { "LVL_FASTOON_RETURN_BUTTONDOOR_DONE", 177 },
            { "LVL_FASTOON_RETURN_DECRYPTOR_DONE", 178 },
            { "LVL_FASTOON_RETURN_BOLTCRANK_DONE", 179 },
            { "LVL_FASTOON_RETURN_EMD_ENEMY_SEG", 180 },
            { "LVL_FASTOON_RETURN_BOSS_1", 181 },
            { "LVL_FASTOON_RETURN_BOSS_2", 182 },
            // Other
            { "MEDIA_DAY_MODE", 271 },
            { "COMPLETED_1_GEOLASER", 274 },
            { "MAGNETIZER_ACQUIRED", 275 },
            { "MAPOMATIC_ACQUIRED", 276 },
            { "HERO_HAS_FIRED_IN_LOOK_MODE", 284 },
            { "BOXBASHER_ACQUIRED", 285 },
            { "GOLDENGROOVITRON_ACQUIRED", 286 },
            { "TREASURE_MAPPER_ACQUIRED", 287 },
        };

        public static readonly Dictionary<string, (int order, string displayName)> LevelFlagPlanetOrder = new Dictionary<string, (int, string)>
        {
            { "METROPOLIS", (0, "Kerwan") },
            { "MOVIE_METROPOLIS", (0, "Kerwan") },
            { "COBALIA", (1, "Cobalia") },
            { "MOVIE_COBALIA", (1, "Cobalia") },
            { "STRATUS_CITY", (2, "Kortog") },
            { "MOVIE_STRATUS_CITY", (2, "Kortog") },
            { "FAST", (3, "Fastoon") },
            { "FASTOON_SHIP", (3, "Fastoon") },
            { "SC1", (4, "Voron") },
            { "IFF", (5, "Mukow") },
            { "APOGEE", (6, "Nundac") },
            { "MOVIE_APOGEE", (6, "Nundac") },
            { "PIRATE_BASE", (7, "Ardolis") },
            { "MOVIE_PIRATE_BASE", (7, "Ardolis") },
            { "SC2", (8, "Rakar") },
            { "RYKANV", (9, "Rykan V") },
            { "SARGASSO", (10, "Sargasso") },
            { "IRIS", (11, "Kreeli") },
            { "ZORDOOM", (12, "Viceron") },
            { "SC3", (13, "Verdigris") },
            { "SC", (13, "Verdigris") },
            { "KERCHU", (14, "Jasindu") },
            { "SLAG", (15, "Ublik") },
            { "CRAGRUINS", (16, "Reepor") },
            { "MERIDIAN", (17, "Igliak") },
            { "FASTOON_RETURN", (18, "Fastoon 2") },
            { "RYNO", (19, "Other") },
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
    }
}