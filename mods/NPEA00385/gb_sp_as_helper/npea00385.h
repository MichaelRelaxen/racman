//
// special
//

// Used for syscalls.
// Arguments: call, ...
#define syscall ((int (*)(int, ...))0x4F6500)

//
// constants
//

// syscalls
#define SYS_SLEEP 0x8D
#define SYS_TIME_GET_CURRENT_TIME 0x91
#define SYS_OPEN 0x321
#define SYS_READ 0x322
#define SYS_WRITE 0x323
#define SYS_CLOSE 0x324
#define SYS_RENAME 0x32C

// buttons
#define BTN_L2 0x0001
#define BTN_R2 0x0002
#define BTN_L1 0x0004
#define BTN_R1 0x0008
#define BTN_TRI 0x0010
#define BTN_CIR 0x0020
#define BTN_CRO 0x0040
#define BTN_SQU 0x0080
#define BTN_SEL 0x0100
#define BTN_L3 0x0200
#define BTN_R3 0x0400
#define BTN_STA 0x0800
#define BTN_UP 0x1000
#define BTN_RIGHT 0x2000
#define BTN_DOWN 0x4000
#define BTN_LEFT 0x8000

// planets
#define PLANET_MAIN_MENU -1
#define PLANET_VELDIN_1 0
#define PLANET_NOVALIS 1
#define PLANET_ARIDIA 2
#define PLANET_KERWAN 3
#define PLANET_EUDORA 4
#define PLANET_RILGAR 5
#define PLANET_BLARG 6
#define PLANET_UMBRIS 7
#define PLANET_BATALIA 8
#define PLANET_GASPAR 9
#define PLANET_ORXON 10
#define PLANET_POKITARU 11
#define PLANET_HOVEN 12
#define PLANET_GEMLIK 13
#define PLANET_OLTANIS 14
#define PLANET_QUARTU 15
#define PLANET_KALEBO_III 16
#define PLANET_FLEET 17
#define PLANET_VELDIN_2 18

// items
#define ITEM_HELIPACK 0x2
#define ITEM_THRUSTERPACK 0x3
#define ITEM_HYDROPACK 0x4
#define ITEM_SONIC_SUMMONER 0x5
#define ITEM_O2_MASK 0x6
#define ITEM_PILOTS_HELMET 0x7
#define ITEM_WRENCH 0x8
#define ITEM_SUCK_CANNON 0x9
#define ITEM_BOMB_GLOVE 0xA
#define ITEM_DEVASTATOR 0xB
#define ITEM_SWINGSHOT 0xC
#define ITEM_TAUNTER 0xD
#define ITEM_VISIBOMB 0xE
#define ITEM_BLASTER 0xF
#define ITEM_PYROCITOR 0x10
#define ITEM_MINE_GLOVE 0x11
#define ITEM_WALLOPER 0x12
#define ITEM_TESLA_CLAW 0x13
#define ITEM_GLOVE_OF_DOOM 0x14
#define ITEM_MORPH_O_RAY 0x15
#define ITEM_HYDRODISPLACER 0x16
#define ITEM_RYNO 0x17
#define ITEM_DRONE_DEVICE 0x18
#define ITEM_DECOY_GLOVE 0x19
#define ITEM_TRESPASSER 0x1A
#define ITEM_METAL_DETECTOR 0x1B
#define ITEM_MAGNEBOOTS 0x1C
#define ITEM_GRINDBOOTS 0x1D
#define ITEM_HOVERBOARD 0x1E
#define ITEM_HOLOGUISE 0x1F
#define ITEM_PDA 0x20
#define ITEM_MAP_O_MATIC 0x21
#define ITEM_BOLT_GRABBER 0x22
#define ITEM_PERSUADER 0x23

// player states
#define STATE_IDLE 0
#define STATE_FIRST_PERSON 1
#define STATE_WALK 2
#define STATE_STOP 3
#define STATE_CROUCH 4
#define STATE_FALL 6
#define STATE_NEUTRAL_JUMP 7
#define STATE_GLIDE 8
#define STATE_MOVING_JUMP 9
#define STATE_HELI_LONG_JUMP 0xA
#define STATE_FLIP 0xB
#define STATE_THRUSTER_HIGH_JUMP 0xD
#define STATE_DOUBLE_JUMP 0xE
#define STATE_HELI_HIGH_JUMP 0xF
#define STATE_THRUSTER_LONG_JUMP 0x10
#define STATE_WALLJUMP 0x11
#define STATE_WATER_JUMP 0x12
#define STATE_WRENCH_SWING 0x13
#define STATE_HYPER_STRIKE 0x14
#define STATE_COMET_STRIKE 0x15
#define STATE_TAKE_DAMAGE 0x16
#define STATE_LEDGEGRAB 0x18
#define STATE_IN_LEDGEGRAB 0x19
#define STATE_LEDGEGRAB_LEFT 0x1A
#define STATE_LEDGEGRAB_RIGHT 0x1B
#define STATE_LEDGEGRAB_UP 0x1C
#define STATE_VISIBOMB 0x1D
#define STATE_FIRST_PERSON_WEAPON 0x1E
#define STATE_ACTIVATE_SONIC_SUMMONER 0x1F
#define STATE_WALLOPER 0x20
#define STATE_THRUSTER_BONK 0x22
#define STATE_THROW 0x23
#define STATE_GREEN_SWINGSHOT_LATCH 0x24
#define STATE_GREEN_SWINGSHOT_PULL 0x25
#define STATE_GRIND 0x28
#define STATE_GRIND_JUMP 0x29
#define STATE_GRIND_SWITCH_RAIL 0x2A
#define STATE_GRIND_WRENCH_SWING 0x2B
// todo...

// text flags
#define TEXT_CENTER_H 0x01
#define TEXT_CENTER_V 0x02
#define TEXT_HIDE 0x04

// ui screens
#define UI_DEBUG -1
#define UI_NONE 0
#define UI_CINEMATIC 1
#define UI_ILM 2
#define UI_PAUSE 3
#define UI_MODAL 4
#define UI_PDA 5
#define UI_ILE 6

// mobys
#define MOBY_RATCHET 0
#define MOBY_VENDOR 0xB
#define MOBY_THEGUY 0x72
#define MOBY_QWACKORAY_CHICKEN 0x10E
#define MOBY_AMMO_RYNO 0x59E

//
// types
//

typedef struct {
    float x;
    float y;
    float z;
    float w;
} vec4;

typedef struct {
    char a;
    char b;
    char g;
    char r;
} color;

typedef struct {
    // The moby position for collision purposes. Usually should not be touched.
    vec4 coll_pos;
    // The moby position.
    vec4 pos;
    // The moby state.
    char state;
    // The texture mode.
    char texture_mode;
    // The moby opacity.
    unsigned short opacity;
    // The moby model.
    void* model;
    // The parent moby, if existing.
    struct moby* parent;
    // The 3D scaling applied to the model.
    float scale;
    // Unknown, 0x30
    char unk_30;
    // Whether or not the moby is visible (readonly).
    char visible;
    // The distance at which the moby will start fading out.
    short render_distance;
    // Unknown, 0x34
    void* unk_34;
    // Controls the coloring of the moby.
    color color;
    // Controls the shading of the moby, through mechanisms unknown.
    unsigned int shading;
    // The moby rotation in radians. Typically only Z needs to be changed.
    vec4 rot;
    // The previous frame number of the current animation.
    char prev_anim_frame;
    // The current frame number of the current animation.
    char curr_anim_frame;
    // asdf
    char asdf[0x26];
    // The moby's pVars.
    void* pvars;
    // asdf2
    char asdf2[0x2A];
    // The type of moby it is.
    unsigned short type;
    // asdf3
    char asdf3[0x58];
} moby;

typedef struct {
    short y_min;
    short y_max;
    short x_min;
    short x_max;
    short x;
    short y;
    int pad;
    short line_spacing;
    unsigned short flags;
} text_info;

// 
// functions
// 

// Used for drawing HUD backgrounds. Needs to be called at a specific point in the draw loop.
// Arguments: y1, y2, x1, x2, opacity
#define draw_rounded_box ((void (*)(int, int, int, int, char))0x06A718)

// Used for drawing right-aligned text. Needs to be called at a specific point in the draw loop.
// Arguments: x, y, color, text, max_length, scale
#define draw_right_text ((void (*)(int, int, unsigned int, char*, int, float))0x06FF88)

// Used for drawing right-aligned medium text. Needs to be called at a specific point in the draw loop.
// Arguments: x, y, color, text, max_length
#define draw_right_medium_text ((void (*)(int, int, unsigned int, char*, int))0x070130)

// Used for drawing right-aligned small text. Needs to be called at a specific point in the draw loop.
// Arguments: x, y, color, text, max_length
#define draw_right_small_text ((void (*)(int, int, unsigned int, char*, int))0x07027C)

// Used for drawing right-aligned big text. Needs to be called at a specific point in the draw loop.
// Arguments: x, y, color, text, max_length
#define draw_right_big_text ((void (*)(int, int, unsigned int, char*, int))0x0703C8)

// Used for drawing centered medium text. Needs to be called at a specific point in the draw loop.
// Arguments: x, y, color, text, max_length
#define draw_center_medium_text ((void (*)(int, int, unsigned int, char*, int))0x070514)

// Used for drawing centered text. Needs to be called at a specific point in the draw loop.
// Arguments: x, y, color, text, max_length, scale
#define draw_center_text ((void (*)(int, int, unsigned int, char*, int, float))0x070680)

// Used for drawing centered small text. Needs to be called at a specific point in the draw loop.
// Arguments: x, y, color, text, max_length
#define draw_center_small_text ((void (*)(int, int, unsigned int, char*, int))0x070834)

// Used for drawing centered big text. Needs to be called at a specific point in the draw loop.
// Arguments: x, y, color, text, max_length
#define draw_center_big_text ((void (*)(int, int, unsigned int, char*, int))0x070998)

// Used for drawing text which needs more control (line spacing, justification). Needs to be called at a specific point in the draw loop.
// Arguments: info, color, text, max_length, scale
#define draw_text ((void (*)(text_info*, unsigned int, char*, int, float))0x07100C)

// Used for getting localized versions of game strings.
// Argument: message_id
#define get_local_string ((char* (*)(int))0x07B740)

// Used for teleporting the player. Needs more investigation.
// Arguments: location, rotation
#define move_player ((void (*)(vec4*, vec4*))0x0A0C38)

// Used for drawing textures to the HUD. `data` is not yet fully understood. Needs to be called at a specific point in the draw loop.
// Arguments: data, texture_id, x, y, flags, opacity
#define draw_texture ((void (*)(void*, int, int, int, int, int))0x0CDEB8)

// Used for loading a save-file buffer.
// Arguments: unk, buf
#define perform_load ((void (*)(int, void*))0x0E8CA0)

// Used for spawning mobys.
// Argument: moby_id
#define spawn_moby ((void* (*)(int))0x0EFA28)

// Used for deleting mobys.
// Argument: moby
#define delete_moby ((void (*)(void*))0x0EFB38)

// Used for unlocking planets.
// Argument: planet_id
#define unlock_planet ((void (*)(int))0x112C20)

// Used for unlocking items.
// Argument: item_id
#define unlock_item ((void (*)(int))0x112E18)

// Used for querying gamepad inputs.
// Arguments: unk, pad_id, dst
#define query_pad ((void (*)(void*, int, void*))0x4EC160)

// Used for copying memory.
// Arguments: dst, src, num
#define memcpy ((void (*)(void*, void*, int))0x5C5AD0)

// Used for generating pseudo-random numbers.
// No arguments.
#define rand ((unsigned int (*)())0x5C8318)

// Used for logging.
// Arguments: format, ...
#define print_tty ((void (*)(char*, ...))0x5CD098)

// Used for formatting strings.
// Arguments: dst, fmt, ...
#define sprintf ((void (*)(char*, char*, ...))0x5CD2A8)

// Used for setting memory.
// Arguments: dst, val, num
#define memset ((void (*)(void*, char, int))0x5D0140)

//
// variables
//

// inputs

// A pointer to the input structure.
#define input_ptr ((void*)0x964A40)
// The buttons that are currently pressed.
#define down_buttons (*((unsigned int*)0x964AE0))
// The buttons that were pressed this frame.
#define pressed_buttons (*((unsigned int*)0x964AE4))
// The buttons that were released this frame.
#define released_buttons (*((unsigned int*)0x964AE8))


// world

// The currently loaded planet.
#define current_planet (*((int*)0x969C70))
// The current language.
#define current_language (*((int*)0x969C74))
// Whether or not the game is currently loading `destination_planet`.
#define should_load (*((int*)0xA10700))
// The planet being loaded.
#define destination_planet (*((int*)0xA10704))
// The current UI screen.
#define ui_screen (*((int*)0xA10708))
// The time since the UI screen last changed.
#define time_since_ui_change (*((int*)0xA1070C))
// The time since the world was last reloaded.
#define time_since_reload (*((int*)0xA10710))
// The current HUD menu.
#define current_hud_id (*((int*)0xA52BB0))
// A pointer to the current HUD menu.
#define current_hud_ptr (*((void*)0xA52BB4))


// save data

// Collected gold bolts. Space for four per planet.
#define gold_bolts ((char*)0xA0CA34)
// Boolean save flags (0xFF meaning set). 0x10 for each planet. 20 planets.
#define bool_save_flags ((char*)0xA0CA84)
// Misc save data. 0x100 for each planet. 20 planets.
#define misc_save_flags ((char*)0xA0CD1C)
// Array of whether or not you've "seen" planets.
#define seen_planets ((char*)0x96CA0C)
// Array of planets in the planet select.
#define planet_select ((int*)0x96C18C)
// Array of whether or not you've unlocked items.
#define unlocked_items ((char*)0x96C140)
// Array of whether or not you've seen ILMs.
#define watched_ilms ((char*)0x96BFF0)
// A pointer path to the current save data buffer.
#define savedata_buf (*(void**)((int)((void*)0xA10928)) + 0x100000)


// world objects

// A pointer to the array of moby instances in the level.
#define level_mobys (*((moby**)0xA390A0))
// A pointer to the array of cuboid instances in the level.
#define level_cuboids (*((void**)0xA51BD8))


// player

// The player's current bolt count.
#define player_bolts (*((int*)0x969CA0))
// The player's current position.
#define player_pos (*((vec4*)0x969D60))
// The player's current rotation in radians. Z is the most common axis.
#define player_rot (*((vec4*)0x969D70))
// The player's neutral momentum.
#define player_neutral (*((float*)0x969E74))
// The frames until "Ghost Ratchet" will run out.
#define player_ghost_frames (*((int*)0x969EAC))
// The player's current state.
#define player_state (*((int*)0x96BD64))
// The player's current HP.
#define player_health (*((int*)0x96BF88))
