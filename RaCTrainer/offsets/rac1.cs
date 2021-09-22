namespace racman
{
    public class rac1 : IGame
    {
        public rac1(Ratchetron api) : base(api)
        {
            this.boltCount = 0x969CA0;
            this.playerCoords = 0x969D60;

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

        ///////////// Player /////////////

        // The player's current bolt count.
        //public static uint bolt_Count = 0x969CA0;

        // The player's current health.
        public static uint player_health = 0x96BF88;

        // The player's current coordinates + rotation. We typically just copy 0x1E at a time for saving/loading positions.
        public static uint player_coords = 0x969D60;

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

        // Array of whether or not you've unlocked items.
        public static uint unlock_array = 0x96C140;

        // Array of whether or not you've unlocked any gold weapons.
        public static uint gold_weapons_array = 0x969CA8;

        // Array of level flags. 0x10 per planet. 20 planets. Found by doesthisusername.
        public static uint level_flags = 0xA0CA84;

        // Array of misc level flags. 0x100 per planet. 20 planets. Found by doesthisusername.
        public static uint misc_level_flags = 0xA0CD1C;

        // Array of watched ILMs. Found by doesthisusername.
        public static uint watched_ilms_array = 0x96BFF0;

        // Array of seen infobots. Found by doesthisusername.
        public static uint infobot_flags = 0x96CA0C;

        public override void LoadPlanet()
        {
            throw new System.NotImplementedException();
        }


        public override void ToggleFastLoad()
        {
            throw new System.NotImplementedException();
        }
    }
}
