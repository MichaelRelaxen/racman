// -------
// special
// -------

// Used for syscalls.
// Arguments: call, ...
#define syscall ((int (*)(int, ...))0x980600)

// ---------
// constants
// ---------

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

// -----
// types
// -----

typedef struct {
    float x;
    float y;
    float z;
    float w;
} vec4;

// ---------
// functions
// ---------

// standard

// Used for copying memory.
// Arguments: dst, src, num
#define memcpy ((void (*)(void*, void*, int))0x99256C)

// save data

// Used for loading already-loaded save data.
// Arguments: unk, buf
#define perform_load ((void (*)(int, void*))0x1E1CF4)

// ---------
// variables
// ---------

// inputs

// The buttons that are currently pressed.
#define down_buttons (*((unsigned int*)0xD99370))
// The buttons that were pressed this frame.
#define pressed_buttons (*((unsigned int*)0xD99374))
// The buttons that were released this frame.
#define released_buttons (*((unsigned int*)0xD99378))

// world

// The currently loaded planet.
#define current_planet (*((int*)0xC1E438))
// Whether or not the game is currently loading `destination_planet`.
#define should_load (*((int*)0xEE9310))
// The planet being loaded.
#define destination_planet (*((int*)0xEE9314))
// The current UI screen.
#define ui_screen (*((int*)0xEE9334))
// Planet timer 
#define planet_timer (*(int*)0x1A70B30)

// player

// The player's current bolt count.
#define player_bolts (*((int*)0xC1E4DC))
// The player's current position.
#define player_pos (*((vec4*)0xDA2870))
// The player's current rotation in radians. Z is the most common axis.
#define player_rot (*((vec4*)0xDA2880))
// The player's neutral momentum.
#define player_neutral (*((float*)0xDA29A4))
// The player's current state.
#define player_state (*((int*)0xDA4DB4))
// The player's current HP.
#define player_health (*((int*)0xDA5040))

// save data

// A pointer to the current save data info in memory.
#define savedata_info (*((void**)0xCB0A98))
// A pointer path to the current save data buffer.
#define savedata_buf (*(void**)((int)savedata_info + 4))