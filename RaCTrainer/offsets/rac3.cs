namespace racman
{
    class rac3
    {

        /*
         *             if (gameID == "NPEA00387")
            {
                mask_offset = 0xD99370;
                analog_offset = 0xD9954C;
            }*/

        ///////////// Player /////////////

        // Player's current coordinates. We typically copy 0x1E at a time for saving/loading positions.
        public static uint player_coords = 0xDA2870;

        // Player's current bolt count.
        public static uint bolt_count = 0xc1e4dc;

        // Player's current health.
        public static uint health = 0xda5040;

        // Player's current health EXP.
        public static uint health_exp = 0xc1e510;

        // Player's current state.
        public static uint player_state = 0xDA4DB4;

        // Player's currently used armor. 
        public static uint current_armor = 0xc1e51c;

        // Current challenge mode.
        public static uint challenge_mode = 0xC1E50D;

        // Frames until "Ghost Ratchet" runs out.
        public static uint ghost_timer = 0xDA29E0;

        // Currently loaded planet.
        public static uint current_planet = 0xC1E438;

        //Death count
        public static uint death_count = 0xED7F14;

        //Quick Switch Array
        public static uint quick_switch = 0xC1E4EC;
        

        ///////////// Misc. /////////////

        // First variable for Klunk Tuning, set to 7.
        public static uint klunk_tuning_var1 = 0xC9165C;

        // Second variable for Klunk Tuning, set to 3.
        public static uint klunk_tuning_var2 = 0xC36BCC;

        // Should load + planet to load. For example set 0000000100000002 to load Florana. Found by doesthisusername
        public static uint force_load_planet = 0xEE9310;

        // Current load screen. Can force to second loading screen by setting to 00000003
        public static uint fast_load = 0x134EBD4;

        // Load screen thing idk set to 0x100
        public static uint fast_load2 = 0x134EE70;

        // Bool which toggles if quick select is on or not.
        public static uint quick_select_pause = 0xC1E652;

        // Video Comic menu (0 1 2)
        public static uint vid_comic_menu = 0xC4F918;

        // Command Center Thyra Button help text turn on/ turn off
        public static uint cc_help_text = 0x148A100;

        ///////////// Arrays /////////////

        // Array of whether or not you've collected titanium bolts. 8 per planet.
        public static uint titanium_bolts_array = 0xECE53D;

        // Array of skill points.
        public static uint skill_points_array = 0xDA521d;

        // Array of unlockable items. Follows same structure as item array. Currently starts at lock strafe cuz im lazy but ill get around to this later.
        public static uint unlock_array = 0xDA5710;

        // Array of items.
        public static uint item_array = 0xc1e43c;

        // Array of ammo on weapons. Follows same structure as item array.
        public static uint ammo_array = 0xDA5240;

        // Currently unlocked vid comics. Follows 1, 4, 3, 2, 5 in order.
        public static uint vid_comics = 0xda650b;
    }
}
