#ifndef TYPES_H
#define TYPES_H

typedef unsigned char undefined;
typedef unsigned char byte;
typedef unsigned int dword;
typedef long long longlong;
typedef unsigned long long qword;
typedef unsigned char uchar;
typedef unsigned int uint;
typedef unsigned long ulong;
typedef unsigned long long ulonglong;
typedef unsigned char undefined1;
typedef unsigned short undefined2;
typedef unsigned int undefined4;
typedef unsigned long long undefined8;
typedef unsigned short ushort;
typedef unsigned short word;
typedef struct Moby Moby, *PMoby;
typedef struct Vec4 Vec4, *PVec4;
typedef uchar uint8_t;
typedef char __int8_t;
typedef __int8_t int8_t;
typedef ushort __uint16_t;
typedef __uint16_t uint16_t;
typedef ulonglong uint64_t;
typedef uint uint32_t;
typedef int int32_t;

struct Vec4 {
    float x;
    float y;
    float z;
    float w;
};

struct Moby {
    byte bSphere[16];
    float pos[4];
    byte state;
    uint8_t group;
    int8_t mClass;
    int8_t alpha;
    void *pClass;
    struct Moby *pChain;
    float size;
    byte updateDistance;
    uint8_t drawn;
    uint16_t drawDistance;
    uint16_t modeBits1;
    uint16_t modeBits2;
    uint64_t lights;
    undefined1 field15_0x40;
    undefined1 unk_spawned_by_us;
    undefined1 field17_idc;
    undefined1 cur_animation_seq;
    undefined1 field19_0x44;
    undefined1 field20_0x45;
    undefined1 field21_0x46;
    undefined1 field22_0x47;
    float field23_0x48;
    float field24_0x4c;
    undefined1 field25_0x50;
    undefined1 field26_0x51;
    undefined1 unk_pending_slot_id;
    undefined1 field28_0x53;
    void* field29_0x54;
    void* prev_anim;
    void* curr_anim;
    undefined1 field32_0x60;
    undefined1 field33_0x61;
    undefined1 field34_0x62;
    undefined1 field35_0x63;
    void **pUpdate;
    void *pVar;
    undefined1 field38_0x6c;
    undefined1 field39_0x6d;
    undefined1 field40_0x6e;
    undefined1 field41_0x6f;
    float field42_0x70;
    float field43_0x74;
    int copiedFromModelHeader;
    undefined1 field45_0x7c;
    undefined1 field46_0x7d;
    undefined1 field47_0x7e;
    uint32_t field48_0x7f;
    undefined1 field49_0x83;
    undefined1 field50_0x84;
    undefined1 field51_0x85;
    undefined1 field52_0x86;
    undefined1 field53_0x87;
    undefined1 field54_0x88;
    undefined1 field55_0x89;
    undefined1 field56_0x8a;
    undefined1 field57_0x8b;
    undefined1 field58_0x8c;
    undefined1 field59_0x8d;
    undefined1 field60_0x8e;
    undefined1 field61_0x8f;
    undefined1 field62_0x90;
    undefined1 field63_0x91;
    undefined1 field64_0x92;
    byte subState;
    byte prevState;
    byte stateType;
    uint16_t stateTimer;
    float *collData;
    int32_t collActive;
    uint32_t collCnt;
    undefined field72_0xa4;
    undefined field73_0xa5;
    undefined field74_0xa6;
    undefined field75_0xa7;
    byte collDamage;
    undefined field77_0xa9;
    ushort oClass;
    uint32_t moby_counter_indexed;
    undefined field80_0xb0;
    undefined field81_0xb1;
    short UID;
    undefined field83_0xb4;
    undefined field84_0xb5;
    undefined field85_0xb6;
    undefined field86_0xb7;
    void* multimoby_part;
    undefined1 substate;
    int8_t field89_0xbd;
    byte field90_0xbe;
    undefined field91_0xbf;
    uint8_t rMtx[48];
    float rotation[4];
} __attribute__((packed));

typedef struct BoltVars BoltVars;

struct BoltVars {
    int32_t bolt_slot;
    int32_t cuboid1;
    int32_t cuboid2;
    byte idc1;
    byte skip_animation;
    undefined1 idc2[114];
} __attribute__((packed));

typedef enum planetId {
    MAIN_MENU = -1,
    ARANOS_TUTORIAL = 0,
    OOZLA,
    MAKTAR,
    ENDAKO,
    BARLOW,
    FELTZIN,
    NOTAK,
    SIBERIUS,
    TABORA,
    DOBBO,
    HRUGIS,
    JOBA,
    TODANO,
    BOLDAN,
    ARANOS_REVISIT,
    GORN,
    SNIVELAK,
    SMOLG,
    DAMOSEL,
    GRELBIN,
    YEEDIL,
    MUSEUM,
    DOBBO_ORBIT,
    DAMOSEL_ORBIT,
    SHIP_SHACK,
    WUPASH,
    JAMMING_ARRAY
} planetId;

typedef enum GameState {
    INGAME = 0,
    CINEMATIC = 1,
    IN_LEVEL_MOVIE = 2,
    PAUSED = 3,
    LOADING = 6,
    IN_ELECTROLYZER = 7
} GameState;

#endif