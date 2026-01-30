/* Globals */
moby_instance_table = 0x015927B0;
moby_instance_perm_end = 0x015927B4;
moby_instance_end = 0x015927B8;

frames_since_spawn = 0x1480470;
frames_since_reload = 0x1F67130;
death_count = 0x1569774;
gamestate = 0x156B064;
player_state = 0x1481474;
current_planet = 0x1329A3C;
player_coords = 0x147F260;
current_weapon = 0x14814C4;
neutral_speed = 0x147F394;

game_bolt_scale = 0x01373EA0;
ship_bolt_scale = 0x013182A0;
jump_pad_accel = 0x13185B8;

load_screen_type = 0x147A254;
load_screen_count = 0x147A258;
should_load = 0x156B050;
destination_planet = 0x156B054;

button_bitmask = 0x147A430;
RIGHT_STICK_X = 0x147A60C;
RIGHT_STICK_Y = 0x147A610;
LEFT_STICK_X = 0x147A614;
LEFT_STICK_Y = 0x147A618;

collected_bolts_array = 0x1562540;
game_max_bolts_table = 0x013453f8;

savedata_info = 0x1410E50;
savedata_buf = 0x1410E54;

render_all_region = 0x156B100;

/* Funcs */
memcpy = 0x119a97c;
memset = 0x11a4fec;
spawn_game_bolt = 0x0ccbe40;
spawn_ship_bolt = 0x0020f40;
despawn_moby = 0xb575ec;
text_medium_left = 0xb14624;
text_medium_center = 0xb14a44;
text_medium_right = 0xb142b8;
text_small_right = 0xb14784;
text_small_center = 0xb0e960;
text_small_left = 0xb0e758;
text_large_center = 0xb14d54;
text_large_right = 0xb1331c;
text_large_as_fuck = 0xb14ebc;
draw_box = 0xb2e98c;
toast_message = 0x0b55218;
sprintf = 0x11a2154;
printf = 0x10d4a1c;
cellPadGetData = 0x122479C;
perform_load = 0x83250;
save_handler = 0x82e08;