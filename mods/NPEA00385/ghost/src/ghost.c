// RAC1 (NPEA00385) ghost: records Ratchet every gameplay frame to a file on the HDD
// and plays a saved recording back as a clone of Ratchet
//
// Controls (in game, or the same commands from RacMAN via the cmd byte
//   L3+R3     save this attempt as the planet's practice ghost and restart the level
//   L1+L3+R3  restart without saving
//   R1+L3+R3  arm a run (starts on the next load) / cancel / stop the run
//
// Files in USRDIR: ghost_PP.rgh (practice ghost of planet PP), ghost_tmp.rgh (current attempt),
// run_<ID>_<NNN>_<Planet>.rgh (run segments), run_last.bin (ID of the last run)

typedef unsigned char u8;
typedef signed char s8;
typedef unsigned short u16;
typedef unsigned int u32;
typedef int s32;
typedef unsigned long long u64;

#define offsetof(type, field) __builtin_offsetof(type, field)
#define AT(type, field, off) _Static_assert(offsetof(type, field) == (off), #type "." #field " offset")

typedef struct { float x, y, z, w; } Vec4;
typedef struct { float x, y; } Vec2;

typedef struct {
    u8 frame;
    u8 next_frame;
    u8 seq;
    u8 target_seq;
    float blend;
} MobyAnim;

typedef struct {
    u8 _pad_00[0x10];
    u8 frame_count;
    u8 _pad_11[0xB];
    u32 frames[];
} AnimSeq;
AT(AnimSeq, frame_count, 0x10);
AT(AnimSeq, frames, 0x1C);

typedef struct {
    u8 _pad_00[0x8];
    u8 seq_count;
    u8 _pad_09[0x3F];
    AnimSeq* seqs[];
} MobyClass;
AT(MobyClass, seq_count, 0x08);
AT(MobyClass, seqs, 0x48);

typedef struct {
    Vec4 bsphere;
    Vec4 pos;
    u8 state;
    u8 _pad_21[2];
    u8 alpha;
    MobyClass* cls;
    u8 _pad_28[0xC];
    u16 flags;
    u8 _pad_36[2];
    u64 slot_time;
    Vec4 rot;
    MobyAnim anim;
    u8 _pad_58[0xC];
    u32 player_anim_layer;
    u32 frame_cur;
    u32 frame_next;
    u8 _pad_70[4];
    u32 update_opd;
    u8 _pad_78[0x2E];
    u16 oclass;
    u8 _pad_a8[0xA];
    u16 uid;
    u8 _pad_b4[0xC];
    Vec4 matrix[3];
    u8 _pad_f0[0x10];
} Moby;
AT(Moby, state, 0x20);
AT(Moby, alpha, 0x23);
AT(Moby, cls, 0x24);
AT(Moby, flags, 0x34);
AT(Moby, slot_time, 0x38);
AT(Moby, rot, 0x40);
AT(Moby, anim, 0x50);
AT(Moby, player_anim_layer, 0x64);
AT(Moby, frame_cur, 0x68);
AT(Moby, update_opd, 0x74);
AT(Moby, oclass, 0xA6);
AT(Moby, uid, 0xB2);
AT(Moby, matrix, 0xC0);
_Static_assert(sizeof(Moby) == 0x100, "Moby size");

#define MOBY_STATE_GONE  0xFE
#define MOBY_HIDDEN      0x0001
#define SPAWN_INIT_START 0x70
#define SPAWN_INIT_END   0xC0

typedef struct {
    u32* begin;
    u32* end;
    u32* current;
} CellGcmContextData;

typedef struct {
    u8 u;
    u8 v;
    s8 y_offset;
    s8 advance;
} Glyph;

typedef struct {
    u32 tag;
    u32 size;
    u32 value;
} SaveBlock;

extern Moby* player_moby;
extern u32 down_buttons;
extern u32 current_planet;
extern u32 should_load;
extern u32 destination_planet;
extern u32 ui_screen;
extern u32 time_since_reload;
extern u8* savedata_base;
extern Moby* moby_first;
extern Moby* moby_last;
extern u8 class_index[];
extern MobyClass* class_table[];

extern CellGcmContextData* gcm_ctx;
extern u32 hud_textures;
extern u32* font_info;
extern Glyph glyphs[256];
extern u64 rs_blend;
extern u64 rs_filter;
extern Vec4 camera_pos;
extern float world_to_screen[16];

extern const u8 fn_spawn_moby[];
extern const u8 fn_perform_load[];
extern const u8 fn_memcpy[];
extern const u8 fn_draw_center_medium_text[];
extern const u8 fn_hud_debug_panel[];

extern s32 game_call(u32 fn, u32 a0, u32 a1, u32 a2, u32 a3, u32 a4);
extern s32 lv2(u32 num, u32 a0, u32 a1, u32 a2, u32 a3);
extern void gfx_state(CellGcmContextData* ctx, u32 texture);

#define UI_NONE  0
#define BTN_L1   0x0004
#define BTN_R1   0x0008
#define BTN_L3   0x0200
#define BTN_R3   0x0400

#define SYS_TIME     0x91
#define SYS_MEMINFO  0x160
#define SYS_MEMALLOC 0x15C
#define MEM_PAGE_1M  0x400
#define SYS_OPEN     0x321
#define SYS_READ     0x322
#define SYS_WRITE    0x323
#define SYS_CLOSE    0x324
#define SYS_RENAME   0x32C
#define SYS_UNLINK   0x32E
#define O_RDONLY     0x000
#define O_WRITE      0x241

#define GCM_COLOR          0x00041948
#define GCM_INVALIDATE_VTX 0x400C1714
#define GCM_BEGIN_END      0x00041808
#define GCM_TEXCOORD_2S    0x00041904
#define GCM_POS_2F         0x00081880
#define GCM_PRIM_QUADS     8
#define GCM_HEADROOM_WORDS 0x400

#define HUD_W           512.0f
#define HUD_H           416.0f
#define W2S_CENTER      2048.0f
#define W_NEAR          0.01f
#define HUD_TEXTURE_SIZE 0x24
#define GLYPH_CELL      16.0f
#define GLYPH_TEX       32
#define TEXT_SCALE      0.75f
#define SHADOW_OFFSET   0.6f
#define LABEL_HEAD_Z    0.3f
#define LABEL_OVERLAP_X 32.0f
#define LABEL_OVERLAP_Y 12.0f
#define MSG_X           256
#define MSG_Y           80

#define FLAG_COMBOS_OFF 1
#define FLAG_SPEED      2

#define GHOST_ALPHA    0x40
#define GHOST_UID      0x7FF0
#define MAX_FRAMES     (60 * 60 * 30)
#define MAX_SPAWNS     8
#define RACE_LOOKAHEAD 16
#define MSG_FRAMES     180
#define MSG_COLOR      0x80F0F0F0
#define SAVE_IMAGE_OFFSET 0x100000
#define SAVE_PLANET_BLOCK 0x10
#define SAVE_SIZE      0xB0000
#define SCRATCH_SIZE   0x100000
#define REC_BUF_SIZE   0x20000
#define REC_FRAMES     (REC_BUF_SIZE / sizeof(Frame))
#define FLUSH_FRAMES   64
#define PLAY_CHUNK     256
#define SETTLE_FRAMES  120  // no file io until the level has run this long. io during loads hung my ps3, maybe smarter way around this
#define MAX_PLANET     32
#define MAX_PENDING    4
#define PREV_WINDOW    180
#define INDEX_MASK     0xFFFFF
#define CLASS_SHIFT    20
#define FIND_RETRY     30
#define TB_PER_FRAME   (79800000 / 60)
#define MAX_GAP        300
#define MAX_LOAD_GAP   (60 * 60)
#define DEATH_HOLD     30
#define MAX_STEP       4.0f
#define MAX_STEP_GAP   8
#define SPEED_TIE      0.5f
#define COL_TEXT       0x80FFFFFF
#define COL_FASTER     0x8040FF40
#define COL_SLOWER     0x804040FF
#define COL_GHOST      0x80FFC080
#define COL_SHADOW     0x60000000

#define FILE_MAGIC 0x52474831
#define G_MAGIC    0x4748533A
#define EXT_MAGIC  0x45585432

enum { PH_IDLE, PH_RUNNING, PH_DONE };
enum { RESUME_NONE, RESUME_DEATH, RESUME_LOAD };
enum { MODE_PRACTICE, MODE_RACE, MODE_OFF, MODE_FILE };
enum { RUN_IDLE, RUN_ARMED, RUN_RECORDING };
enum { CMD_NONE, CMD_SAVE, CMD_RESTART, CMD_RUN_TOGGLE, CMD_RUN_ARM, CMD_RUN_STOP, CMD_SAVE_PREV, CMD_NEW_ATTEMPT };
enum { STAGE_NONE, STAGE_RUN_ARM, STAGE_LEAVE, STAGE_OPEN_PLAY, STAGE_SETTLE, STAGE_FLUSH, STAGE_FILL_PLAY, STAGE_PREFETCH };

typedef struct {
    u32 index;
    Vec4 bsphere;
    Vec4 pos;
    Vec4 rot;
    MobyAnim anim;
    Vec4 matrix[3];
} Frame;
_Static_assert(sizeof(Frame) == 108, "Frame is the on-disk record");

typedef struct {
    u32 magic;
    u32 planet;
    u32 frame_size;
} FileHeader;

typedef struct {
    s32 fd;
    u32 count;
    u32 planet;
    u32 run_id;
    u32 seg;
} Pending;

typedef struct {
    u32 ok;
    u32 index;
    Vec2 pos;
    float speed;
} Track;

typedef struct {
    u32 magic;
    u32 new_attempt;
    Track player;
    Track ghost;
} Ext;

typedef struct {
    u8 save[SAVE_SIZE];
    Frame rec[REC_FRAMES];
    Frame play[PLAY_CHUNK];
    Frame prefetch[PLAY_CHUNK];
    Ext ext;
} Scratch;
_Static_assert(sizeof(Scratch) <= SCRATCH_SIZE, "scratch overflow");
_Static_assert(PLAY_CHUNK > SETTLE_FRAMES, "prefetch must outlast the settle delay");

typedef struct {
    u32 magic;
    u8 cmd;
    u8 mode;
    u8 run_state;
    u8 flags;
    u32 race_id;
    u32 run_id;
    u32 run_seg;
    u32 race_seg;
    u32 mem_total;
    u32 mem_avail;
    char play_name[32];
    u32 nosplit;  // racman peeps everything up to here by offset, self reminder not to move shit
    u32 nosplit_loads;

    Scratch* scratch;
    u32 snap_ok;
    u32 race_last;
    u32 rec_practice;
    u32 phase;
    u32 last_reload_time;
    u32 planet;
    s32 rec_fd;
    s32 play_fd;
    s32 old_play_fd[2];
    s32 prefetch_fd;
    u32 prefetch_count;
    u32 prefetch_seg;
    u32 prefetch_planet;
    u32 prefetch_mode;
    u32 prev_ok;
    u32 prev_planet;
    u32 save_prev_req;
    u32 rec_on;
    u32 rec_count;
    u32 seg_run_id;
    u32 seg_index;
    u32 pending_count;
    u32 pending_frames;
    Pending pending[MAX_PENDING];
    u32 seg_frames;
    u32 settled;
    u32 resumed;
    u32 load_keep;
    u32 io_hold;
    u32 last_tb;
    u32 play_index;
    u32 play_count;
    Moby* ghost;
    u32 spawns;
    u32 last_frame_ptr;
    u32 prev_buttons;
    u32 msg_frames;
    s32 err;
    u32 stage;
    u64 io_bytes;
    char path[64];
    char msg[48];
    u32 ghost_class;
    u32 find_wait;
} Globals;
AT(Globals, cmd, 0x04);
AT(Globals, mode, 0x05);
AT(Globals, run_state, 0x06);
AT(Globals, flags, 0x07);
AT(Globals, race_id, 0x08);
AT(Globals, run_id, 0x0C);
AT(Globals, run_seg, 0x10);
AT(Globals, race_seg, 0x14);
AT(Globals, mem_total, 0x18);
AT(Globals, play_name, 0x20);
AT(Globals, nosplit, 0x40);
AT(Globals, nosplit_loads, 0x44);

// dead opd block nobody references
Globals ghost_globals __attribute__((section(".ghost_globals")));

// the empty asm hides the address from clang. without it clang does bit tricks on it that leave shit in the top half of 64-bit registers which -m32 doesn't need
static inline Globals* globals(void) {
    Globals* p = &ghost_globals;
    __asm__("" : "+r"(p));
    return p;
}
#define G (*globals())

static const char dir[] = "/dev_hdd0/game/NPEA00385/USRDIR/";

// volatile so clang can't *helpfully* turn this into a memcpy call that links to fuck all
static void __attribute__((noinline)) copy(void* dst, const void* src, u32 n) {
    volatile u32* d = dst;
    const volatile u32* s = src;
    for (n /= 4; n; n--) *d++ = *s++;
}

static void __attribute__((noinline)) zero(void* dst, u32 n) {
    volatile u32* d = dst;
    for (n /= 4; n; n--) *d++ = 0;
}

static Moby* spawn_moby(u32 oclass) {
    return (Moby*)game_call((u32)fn_spawn_moby, oclass, 0, 0, 0, 0);
}

static void perform_load(void* save) {
    game_call((u32)fn_perform_load, 0, (u32)save, 0, 0, 0);
}

static void game_memcpy(void* dst, const void* src, u32 n) {
    game_call((u32)fn_memcpy, (u32)dst, (u32)src, n, 0, 0);
}

static void draw_center_medium_text(u32 x, u32 y, u32 colour, const char* text) {
    game_call((u32)fn_draw_center_medium_text, x, y, colour, (u32)text, (u32)-1);
}

static s32 sys_open(const char* path, u32 flags, s32* fd) {
    return lv2(SYS_OPEN, (u32)path, flags, (u32)fd, 0);
}

static u32 sys_read(s32 fd, void* buf, u32 size) {
    G.io_bytes = 0;
    lv2(SYS_READ, fd, (u32)buf, size, (u32)&G.io_bytes);
    return (u32)G.io_bytes;
}

static void sys_write(s32 fd, const void* buf, u32 size) {
    lv2(SYS_WRITE, fd, (u32)buf, size, (u32)&G.io_bytes);
}

static void close_fd(s32* fd) {
    if (*fd > 0) lv2(SYS_CLOSE, *fd, 0, 0, 0);
    *fd = 0;
}

static u32 mftb(void) {
    u32 tb;
    // mftb r3, done by hand because the assembler emits mfspr 268 instead
    __asm__ volatile(".long 0x7C6C42E6\n\tmr %0, %%r3" : "=r"(tb) : : "r3");
    return tb;
}

static char* put_str(char* p, const char* s) {
    while (*s) *p++ = *s++;
    return p;
}

static char* put_dec(char* p, u32 v, u32 digits) {
    for (u32 i = digits; i; i--) {
        p[i - 1] = '0' + v % 10;
        v /= 10;
    }
    return p + digits;
}

static char* put_hex(char* p, u32 v) {
    for (int i = 7; i >= 0; i--) {
        p[i] = "0123456789ABCDEF"[v & 0xF];
        v >>= 4;
    }
    return p + 8;
}

static void message(const char* text, u32 id) {
    char* p = put_str(G.msg, text);
    if (id) p = put_hex(p, id);
    *p = 0;
    G.msg_frames = MSG_FRAMES;
}

static char* file_path(const char* name) {
    char* p = put_str(put_str(G.path, dir), name);
    *p = 0;
    return G.path;
}

static char* practice_path(u32 planet) {
    char* p = put_dec(put_str(put_str(G.path, dir), "ghost_"), planet, 2);
    put_str(p, ".rgh")[0] = 0;
    return G.path;
}

// has to match FilePlanetNames in GhostManagerForm or RacMAN can't find run files
static const char planet_names[] =
    "Veldin\0Novalis\0Aridia\0Kerwan\0Eudora\0Rilgar\0Blarg\0Umbris\0Batalia\0Gaspar\0"
    "Orxon\0Pokitaru\0Hoven\0Gemlik\0Oltanis\0Quartu\0Kalebo\0Fleet\0Veldin2\0";
#define PLANET_NAMES 19

static char* put_planet(char* p, u32 planet) {
    if (planet >= PLANET_NAMES) return put_dec(p, planet, 2);
    const char* s = planet_names;
    for (; planet; planet--)
        while (*s++) {}
    return put_str(p, s);
}

static char* run_path(u32 id, u32 seg, u32 planet) {
    char* p = put_hex(put_str(put_str(G.path, dir), "run_"), id);
    *p++ = '_';
    p = put_dec(p, seg, 3);
    *p++ = '_';
    put_str(put_planet(p, planet), ".rgh")[0] = 0;
    return G.path;
}

static u32 frame_index(const Frame* fr) {
    return fr->index & INDEX_MASK;
}

static u32 frame_class(const Frame* fr) {
    return fr->index >> CLASS_SHIFT;
}

static int ghost_alive(void) {
    Moby* g = G.ghost;
    return g && g->state < MOBY_STATE_GONE && g->uid == GHOST_UID;
}

static void remove_ghost(void) {
    Moby* g = G.ghost;
    // not delete_moby, this is just its first step where we hide it and free the slot
    if (ghost_alive()) {
        g->alpha = 0;
        g->state = MOBY_STATE_GONE;
        g->slot_time = time_since_reload + 2;
    }
    G.ghost = 0;
}

static Moby* spawn_ghost(Moby* src, u32 oclass) {
    Moby* g = spawn_moby(oclass);
    if (!g) return 0;

    if (src) {
        u64 slot_time = g->slot_time;
        u32 keep[(SPAWN_INIT_END - SPAWN_INIT_START) / 4];
        copy(keep, (u8*)g + SPAWN_INIT_START, sizeof(keep));
        copy(g, src, sizeof(Moby));
        g->slot_time = slot_time;
        copy((u8*)g + SPAWN_INIT_START, keep, sizeof(keep));
    }
    g->player_anim_layer = 0;  // copied from ratchet above. if not nulled the clone draws his live pose and flickers
    g->update_opd = 0;
    g->flags &= ~MOBY_HIDDEN;  // when not giant clank, its hidden, and the ghost inherits that. so we need to change when a ghost
    g->alpha = GHOST_ALPHA;
    g->uid = GHOST_UID;
    return g;
}

static u32 frame_ptr(MobyClass* cls, u32 seq, u32 frame) {
    // clank's sequence numbers fed to ratchet's class crashed the game, so check everything lmao
    if (seq >= cls->seq_count) return 0;
    AnimSeq* s = cls->seqs[seq];
    if (!s || (u32)s >= 0x80000000 || frame >= s->frame_count) return 0;
    return s->frames[frame];
}

static void apply_frame(Moby* g, const Frame* fr) {
    copy(&g->bsphere, &fr->bsphere, sizeof(Vec4));
    copy(&g->pos, &fr->pos, sizeof(Vec4));
    copy(&g->rot, &fr->rot, sizeof(Vec4));
    copy(g->matrix, fr->matrix, sizeof(g->matrix));

    MobyClass* cls = g->cls;
    if (!cls) return;
    u32 frame = fr->anim.frame, next = fr->anim.next_frame, seq = fr->anim.seq, target = fr->anim.target_seq;
    u32 cur, nxt;
    if (seq != 0xFF) {
        cur = frame_ptr(cls, seq, frame);
        nxt = frame_ptr(cls, seq, next);
        if (cur) G.last_frame_ptr = cur;
    } else {
        nxt = frame_ptr(cls, target, next);
        cur = G.last_frame_ptr ? G.last_frame_ptr : nxt;
        seq = target;
    }
    if (!cur || !nxt) return;

    g->anim.frame = frame;
    g->anim.next_frame = next;
    g->anim.seq = seq;
    g->anim.target_seq = target;
    copy(&g->anim.blend, &fr->anim.blend, sizeof(float));
    g->frame_cur = cur;
    g->frame_next = nxt;
}

static u32 read_last_run_id(void) {
    s32 fd = 0;
    u32 id = 0;
    if (sys_open(file_path("run_last.bin"), O_RDONLY, &fd)) return 0;
    sys_read(fd, &id, sizeof(id));
    close_fd(&fd);
    return id;
}

static void write_last_run_id(u32 id) {
    s32 fd = 0;
    if (sys_open(file_path("run_last.bin"), O_WRITE, &fd)) return;
    sys_write(fd, &id, sizeof(id));
    close_fd(&fd);
}

static void write_frames(s32 fd, const Frame* frames, u32 count) {
    if (fd && count) sys_write(fd, frames, count * sizeof(Frame));
}

static void flush_rec(void) {
    G.stage = STAGE_FLUSH;
    write_frames(G.rec_fd, G.scratch->rec, G.rec_count);
    G.rec_count = 0;
}

static int open_rec(s32* fd, u32 planet, u32 run_id, u32 seg, const char* name) {
    FileHeader h = { FILE_MAGIC, planet, sizeof(Frame) };
    char* path = run_id ? run_path(run_id, seg, planet) : file_path(name);
    G.err = sys_open(path, O_WRITE, fd);
    if (G.err) {
        *fd = 0;
        return 0;
    }
    sys_write(*fd, &h, sizeof(h));
    return 1;
}

static void end_segment(void) {
    u32 keep = G.rec_on && (G.rec_fd || G.rec_count) && G.pending_count < MAX_PENDING;
    if (keep) {
        Pending* p = &G.pending[G.pending_count++];
        p->fd = G.rec_fd;
        p->count = G.rec_count;
        p->planet = G.planet;
        p->run_id = G.seg_run_id;
        p->seg = G.seg_index;
        G.pending_frames += G.rec_count;
    }
    G.rec_fd = 0;
    G.rec_count = 0;
    G.rec_on = 0;
    G.rec_practice = 0;
}

static int rename_to(const char* from_name, u32 to_planet) {
    u32 from[sizeof(G.path) / 4];
    copy(from, file_path(from_name), sizeof(from));
    if (to_planet < MAX_PLANET) practice_path(to_planet);
    else file_path("ghost_prev.rgh");
    lv2(SYS_UNLINK, (u32)G.path, 0, 0, 0);
    return lv2(SYS_RENAME, (u32)from, (u32)G.path, 0, 0);
}

static void drain(void) {
    Frame* frames = G.scratch->rec;
    for (u32 i = 0; i < G.pending_count; i++) {
        Pending* p = &G.pending[i];
        u32 was_tmp = p->fd && !p->run_id;
        if (!p->fd) open_rec(&p->fd, p->planet, p->run_id, p->seg, "ghost_prev.rgh");
        write_frames(p->fd, frames, p->count);
        if (p->fd && !p->run_id) {
            G.prev_ok = 1;
            G.prev_planet = p->planet;
        }
        close_fd(&p->fd);
        if (was_tmp && rename_to("ghost_tmp.rgh", MAX_PLANET)) G.prev_ok = 0;
        frames += p->count;
    }
    if (G.pending_frames) copy(G.scratch->rec, frames, G.rec_count * sizeof(Frame));
    G.pending_count = G.pending_frames = 0;
    close_fd(&G.old_play_fd[0]);
    close_fd(&G.old_play_fd[1]);
}

static Ext* ext(void) {
    return G.scratch && G.scratch->ext.magic == EXT_MAGIC ? &G.scratch->ext : 0;
}

static float inv_sqrt(float f) {
    union { float f; u32 u; } c = { f };
    c.u = 0x5F3759DF - (c.u >> 1);
    float y = c.f;
    y = y * (1.5f - 0.5f * f * y * y);
    return y * (1.5f - 0.5f * f * y * y);
}

static void track(Track* t, const Vec4* pos, u32 index) {
    float dx = pos->x - t->pos.x, dy = pos->y - t->pos.y, d2 = dx * dx + dy * dy;
    u32 n = index - t->index;
    if (t->ok && n && n <= MAX_STEP_GAP && d2 < MAX_STEP * MAX_STEP * n * n)
        t->speed = d2 > 0.0f ? d2 * inv_sqrt(d2) * 60.0f / n : 0.0f;
    t->pos.x = pos->x;
    t->pos.y = pos->y;
    t->index = index;
    t->ok = 1;
}

static void reset_tracks(void) {
    Ext* x = ext();
    if (x) x->player.ok = x->ghost.ok = 0;
}

static u32 take_new_attempt(void) {
    Ext* x = ext();
    u32 v = x && x->new_attempt;
    if (x) x->new_attempt = 0;
    return v;
}

static void reset_ghost(void) {
    G.ghost = 0;
    G.spawns = 0;
    G.find_wait = 0;
    G.last_frame_ptr = 0;
}

// no syscalls in here, it can run mid-load
static void on_reload(void) {
    u32 fresh = take_new_attempt();
    reset_tracks();
    if (G.phase == PH_RUNNING && current_planet == G.planet && !fresh && (G.load_keep || (G.nosplit >> G.planet & 1))) {
        reset_ghost();
        G.resumed = G.load_keep ? RESUME_LOAD : RESUME_DEATH;
        G.load_keep = 0;
        return;
    }
    G.load_keep = 0;
    if (G.phase == PH_RUNNING) {
        end_segment();
        if (G.play_fd) G.old_play_fd[G.old_play_fd[0] ? 1 : 0] = G.play_fd;
        G.play_fd = 0;
        G.play_index = G.play_count = 0;
        if (G.prefetch_count && G.prefetch_planet == current_planet && G.prefetch_mode == G.mode) {
            copy(G.scratch->play, G.scratch->prefetch, G.prefetch_count * sizeof(Frame));
            G.play_fd = G.prefetch_fd;
            G.play_count = G.prefetch_count;
            G.race_seg = G.prefetch_seg;
            G.prefetch_fd = 0;
            G.prefetch_count = 0;
        }
    }
    reset_ghost();
    G.planet = current_planet;
    G.phase = PH_IDLE;
}

static u32 read_chunk(s32* fd, Frame* buf) {
    u32 count = sys_read(*fd, buf, PLAY_CHUNK * sizeof(Frame)) / sizeof(Frame);
    if (count < PLAY_CHUNK) close_fd(fd);
    return count;
}

static void fill_play(void) {
    G.stage = STAGE_FILL_PLAY;
    G.play_count = read_chunk(&G.play_fd, G.scratch->play);
    G.play_index = 0;
}

static int open_ghost(const char* path, u32 planet, s32* fd) {
    FileHeader h;
    if (sys_open(path, O_RDONLY, fd)) {
        *fd = 0;
        return 0;
    }
    if (sys_read(*fd, &h, sizeof(h)) != sizeof(h) || h.magic != FILE_MAGIC || h.planet != planet || h.frame_size != sizeof(Frame)) {
        close_fd(fd);
        return 0;
    }
    return 1;
}

static int open_race_segment(u32 planet, s32* fd, u32* seg) {
    u32 id = G.race_id ? G.race_id : G.race_last ? G.race_last : read_last_run_id();
    if (!id) return 0;
    for (u32 k = 0; k < RACE_LOOKAHEAD; k++) {
        if (open_ghost(run_path(id, *seg + k, planet), planet, fd)) {
            *seg += k + 1;
            return 1;
        }
    }
    return 0;
}

static int open_source(u32 planet, s32* fd, u32* seg) {
    if (!G.scratch || planet >= MAX_PLANET) return 0;
    G.play_name[sizeof(G.play_name) - 1] = 0;
    return G.mode == MODE_PRACTICE ? open_ghost(practice_path(planet), planet, fd)
         : G.mode == MODE_RACE ? open_race_segment(planet, fd, seg)
         : G.mode == MODE_FILE ? open_ghost(file_path(G.play_name), planet, fd) : 0;
}

static void open_playback(u32 planet) {
    G.stage = STAGE_OPEN_PLAY;
    G.play_index = G.play_count = 0;
    if (open_source(planet, &G.play_fd, &G.race_seg)) fill_play();
}

static void open_prefetch(u32 planet) {
    G.stage = STAGE_PREFETCH;
    close_fd(&G.prefetch_fd);
    G.prefetch_count = 0;
    u32 seg = G.race_seg;
    if (open_source(planet, &G.prefetch_fd, &seg)) {
        G.prefetch_count = read_chunk(&G.prefetch_fd, G.scratch->prefetch);
        G.prefetch_seg = seg;
        G.prefetch_planet = planet;
        G.prefetch_mode = G.mode;
    }
}

static void leave_segment(u32 next_planet) {
    G.stage = STAGE_LEAVE;
    take_new_attempt();
    end_segment();
    drain();
    close_fd(&G.play_fd);
    G.phase = PH_DONE;
    remove_ghost();  // loading with our clone still in the moby table crashes the load
    open_playback(next_planet);
    open_prefetch(next_planet);
    G.stage = STAGE_NONE;
}

static void snapshot_planet(void) {
    G.snap_ok = G.scratch && savedata_base;
    if (!G.snap_ok) return;
    game_memcpy(G.scratch->save, savedata_base + SAVE_IMAGE_OFFSET, SAVE_SIZE);
    SaveBlock* planet = (SaveBlock*)(G.scratch->save + SAVE_PLANET_BLOCK);
    // after a "load planet" from the ui the save still names the old planet, and restoring that hard-hangs the console
    if (planet->tag == 0 && planet->size == 4) planet->value = current_planet;
    else G.snap_ok = 0;
}

static void start_segment(void) {
    if (G.run_state == RUN_ARMED) {
        G.run_state = RUN_RECORDING;
        message("Recording run ", G.run_id);
    }
    G.seg_run_id = G.run_state == RUN_RECORDING ? G.run_id : 0;
    G.seg_index = G.seg_run_id ? G.run_seg++ : 0;
    G.seg_frames = 0;
    G.settled = 0;
    G.resumed = RESUME_NONE;
    G.load_keep = 0;
    G.io_hold = 0;
    G.rec_count = 0;
    G.rec_on = G.scratch != 0;
    G.rec_practice = 0;
    snapshot_planet();
    G.phase = PH_RUNNING;
}

static void save_prev(void) {
    G.save_prev_req = 0;
    if (!G.prev_ok) {
        message("No previous attempt to save", 0);
        return;
    }
    G.prev_ok = 0;
    G.err = rename_to("ghost_prev.rgh", G.prev_planet);
    message(G.err ? "Save failed" : "Previous attempt saved", 0);
    if (!G.err && G.prev_planet == G.planet) open_prefetch(G.planet);
}

static void settle(void) {
    G.stage = STAGE_SETTLE;
    drain();
    if (G.save_prev_req) save_prev();
    if (G.rec_on) {
        if (open_rec(&G.rec_fd, G.planet, G.seg_run_id, G.seg_index, "ghost_tmp.rgh")) {
            G.rec_practice = !G.seg_run_id;
        } else {
            G.rec_on = 0;
            G.rec_count = 0;
        }
    }
    if (!G.play_fd && G.play_index >= G.play_count) open_playback(G.planet);
    if (!G.prefetch_count) open_prefetch(G.planet);
    G.stage = STAGE_NONE;
}

static void keep_through_load(void) {
    if (G.load_keep) return;
    G.load_keep = 1;
    if (G.rec_fd && !G.pending_frames) flush_rec();
    remove_ghost();
}

static void restart_level(void) {
    leave_segment(G.planet);
    if (G.snap_ok) perform_load(G.scratch->save);
    destination_planet = G.planet;
    should_load = 1;
}

static void save_practice(u32 prev) {
    if (G.run_state == RUN_RECORDING) {
        message("Stop the run to save a practice ghost", 0);
        return;
    }
    if (prev || G.seg_frames < PREV_WINDOW) {
        if (G.seg_frames >= SETTLE_FRAMES) save_prev();
        else G.save_prev_req = 1;
        return;
    }
    if (!G.rec_practice) return;
    leave_segment(MAX_PLANET);
    G.err = rename_to("ghost_prev.rgh", G.planet);
    G.prev_ok = 0;
    message(G.err ? "Save failed" : "Practice ghost saved", 0);
    restart_level();
}

static void run_arm(void) {
    u64 sec = 0, nsec = 0;
    if (G.run_state != RUN_IDLE) return;
    G.stage = STAGE_RUN_ARM;
    G.race_last = read_last_run_id();
    G.race_seg = 0;
    lv2(SYS_TIME, (u32)&sec, (u32)&nsec, 0, 0);
    G.run_id = (u32)sec;
    G.run_seg = 0;
    write_last_run_id(G.run_id);
    G.run_state = RUN_ARMED;
    G.stage = STAGE_NONE;
    message("Run armed: starts on next load", 0);
}

static void run_stop(void) {
    if (G.run_state == RUN_ARMED) {
        write_last_run_id(G.race_last);
        G.run_state = RUN_IDLE;
        message("Run cancelled", 0);
    } else if (G.run_state == RUN_RECORDING) {
        end_segment();
        if (G.seg_frames >= SETTLE_FRAMES) drain();
        G.race_last = 0;
        G.run_state = RUN_IDLE;
        message("Run saved ", G.run_id);
    }
}

static void new_attempt(void) {
    Ext* x = ext();
    if (!x) return;
    x->new_attempt = 1;
    message("Next death or reload starts a new attempt", 0);
}

static void command(u32 cmd) {
    switch (cmd) {
    case CMD_SAVE:        save_practice(0); break;
    case CMD_SAVE_PREV:   save_practice(1); break;
    case CMD_RESTART:     restart_level(); break;
    case CMD_RUN_TOGGLE:  if (G.run_state == RUN_IDLE) run_arm(); else run_stop(); break;
    case CMD_RUN_ARM:     run_arm(); break;
    case CMD_RUN_STOP:    run_stop(); break;
    case CMD_NEW_ATTEMPT: new_attempt(); break;
    }
}

static u32 combo(void) {
    u32 held = down_buttons;
    u32 pressed = held & ~G.prev_buttons;
    G.prev_buttons = held;
    if ((held & (BTN_L3 | BTN_R3)) != (BTN_L3 | BTN_R3) || !(pressed & (BTN_L3 | BTN_R3))) return CMD_NONE;
    if (held & BTN_R1) return CMD_RUN_TOGGLE;
    if (held & BTN_L1) return CMD_RESTART;
    return CMD_SAVE;
}

static void record(Moby* player, u32 io) {
    if (G.pending_frames + G.rec_count >= REC_FRAMES) return;
    Frame* fr = &G.scratch->rec[G.pending_frames + G.rec_count++];
    fr->index = G.seg_frames | (u32)player->oclass << CLASS_SHIFT;
    copy(&fr->bsphere, &player->bsphere, sizeof(Vec4));
    copy(&fr->pos, &player->pos, sizeof(Vec4));
    copy(&fr->rot, &player->rot, sizeof(Vec4));
    copy(&fr->anim, &player->anim, sizeof(MobyAnim));
    copy(fr->matrix, player->matrix, sizeof(fr->matrix));
    if (!io || !G.rec_fd) return;
    if (G.seg_frames + 1 >= MAX_FRAMES) {
        flush_rec();
        close_fd(&G.rec_fd);
        G.rec_on = 0;
    } else if (G.rec_count >= FLUSH_FRAMES) {
        flush_rec();
    }
}

static Moby* find_moby(u32 oclass, Moby* player) {
    if (player->oclass == oclass) return player;
    Moby* last = moby_last;
    for (Moby* m = moby_first; m && m <= last; m++)
        if (m->state < MOBY_STATE_GONE && m->oclass == oclass && m->cls && m->uid != GHOST_UID) return m;
    return 0;
}

static void playback(Moby* player, u32 io) {
    Frame* fr = 0;
    for (;;) {
        if (G.play_index >= G.play_count) {
            if (!G.play_fd) {
                remove_ghost();
                return;
            }
            if (!io) break;
            fill_play();
            if (!G.play_count) {
                remove_ghost();
                return;
            }
        }
        Frame* next = &G.scratch->play[G.play_index];
        if (frame_index(next) > G.seg_frames) break;
        fr = next;
        G.play_index++;
    }
    if (!fr) return;

    u32 oclass = frame_class(fr);
    if (ghost_alive() && G.ghost_class != oclass) remove_ghost();
    if (!ghost_alive()) {
        G.ghost = 0;
        u32 same = G.ghost_class == oclass;
        if (same && G.spawns >= MAX_SPAWNS) return;
        if (G.find_wait) {
            G.find_wait--;
            return;
        }
        Moby* src = find_moby(oclass, player);
        if (!src && !class_table[class_index[oclass]]) {
            G.find_wait = FIND_RETRY;
            return;
        }
        G.last_frame_ptr = 0;
        G.ghost = spawn_ghost(src, oclass);
        if (!G.ghost) return;
        if (same) G.spawns++;
        G.ghost_class = oclass;
    }
    apply_frame(G.ghost, fr);
    Ext* x = ext();
    if (x) track(&x->ghost, &fr->pos, frame_index(fr));
}

static void init(void) {
    zero(&G, sizeof(G));
    G.magic = G_MAGIC;
    G.planet = current_planet;
    lv2(SYS_MEMINFO, (u32)&G.mem_total, 0, 0, 0);
    if (lv2(SYS_MEMALLOC, SCRATCH_SIZE, MEM_PAGE_1M, (u32)&G.scratch, 0)) G.scratch = 0;
}

void ghost_tick(void) {
    if (G.magic != G_MAGIC) init();
    if (G.scratch && G.scratch->ext.magic != EXT_MAGIC) {
        zero(&G.scratch->ext, sizeof(Ext));
        G.scratch->ext.magic = EXT_MAGIC;
    }

    u32 t = time_since_reload;
    if (t < G.last_reload_time || current_planet != G.planet) on_reload();
    G.last_reload_time = t;

    if (should_load) {
        if (G.phase != PH_RUNNING) return;
        Ext* x = ext();
        if (destination_planet == G.planet && (G.nosplit_loads >> G.planet & 1) && !(x && x->new_attempt)) keep_through_load();
        else leave_segment(destination_planet);
        return;
    }
    Moby* player = player_moby;
    if (!player || ui_screen != UI_NONE) return;

    u32 cmd = G.cmd;
    G.cmd = CMD_NONE;
    if (cmd == CMD_NONE && !(G.flags & FLAG_COMBOS_OFF)) cmd = combo();
    command(cmd);
    if (should_load) return;

    if (G.phase == PH_IDLE) start_segment();
    if (G.phase != PH_RUNNING) return;

    u32 tb = mftb();
    if (G.resumed) {
        u32 gap = (tb - G.last_tb) / TB_PER_FRAME;
        u32 cap = G.resumed == RESUME_LOAD ? MAX_LOAD_GAP : MAX_GAP;
        if (gap > 1) G.seg_frames += gap > cap ? cap : gap - 1;
        G.io_hold = G.resumed == RESUME_LOAD ? SETTLE_FRAMES : DEATH_HOLD;
        G.resumed = RESUME_NONE;
    }
    G.last_tb = tb;

    if (G.io_hold) G.io_hold--;
    u32 io = G.seg_frames >= SETTLE_FRAMES && !G.io_hold;
    if (io && !G.settled) {
        G.settled = 1;
        settle();
    }
    Ext* x = ext();
    if (x) track(&x->player, &player->pos, G.seg_frames);
    if (G.rec_on) record(player, io);
    if (G.scratch) playback(player, io);
    G.seg_frames++;
}

static u32 fbits(float f) {
    union { float f; u32 u; } c = { f };
    return c.u;
}

static int project(const Moby* m, Vec2* s) {
    const float* w = world_to_screen;
    float d[3] = {
        m->bsphere.x / 1024.0f - camera_pos.x,
        m->bsphere.y / 1024.0f - camera_pos.y,
        (m->bsphere.z + m->bsphere.w) / 1024.0f + LABEL_HEAD_Z - camera_pos.z,
    };
    float v[4];
    for (int j = 0; j < 4; j++) v[j] = d[0] * w[j] + d[1] * w[4 + j] + d[2] * w[8 + j] + w[12 + j];
    if (v[3] < W_NEAR) return 0;
    s->x = v[0] / v[3] - W2S_CENTER + HUD_W / 2;
    s->y = v[1] / v[3] - W2S_CENTER + HUD_H / 2;
    return s->x > 0.0f && s->x < HUD_W && s->y > 0.0f && s->y < HUD_H;
}

static void text(float x, float y, u32 colour, const char* str) {
    u32 n = 0;
    float width = 0.0f;
    for (const char* c = str; *c; c++) {
        const Glyph* gl = &glyphs[(u8)*c];
        if (gl->advance) {
            n++;
            width += gl->advance;
        }
    }
    CellGcmContextData* ctx = gcm_ctx;
    if (!n || ctx->current + 10 + n * 20 + GCM_HEADROOM_WORDS > ctx->end) return;
    rs_blend = 0x44;
    rs_filter = 0x4B;
    gfx_state(ctx, hud_textures + font_info[1] * HUD_TEXTURE_SIZE);
    volatile u32* p = ctx->current;
    *p++ = GCM_COLOR;
    *p++ = colour;
    *p++ = GCM_INVALIDATE_VTX;
    *p++ = 0;
    *p++ = 0;
    *p++ = 0;
    *p++ = GCM_BEGIN_END;
    *p++ = GCM_PRIM_QUADS;
    x -= width * TEXT_SCALE * 0.5f;
    for (const char* c = str; *c; c++) {
        const Glyph* gl = &glyphs[(u8)*c];
        if (!gl->advance) continue;
        u32 u0 = gl->u * 2, v0 = gl->v * 2, u1 = u0 + GLYPH_TEX, v1 = v0 + GLYPH_TEX;
        float x0 = x, x1 = x + GLYPH_CELL * TEXT_SCALE;
        float y0 = y + gl->y_offset * TEXT_SCALE, y1 = y0 + GLYPH_CELL * TEXT_SCALE;
        u32 tex[4] = { v0 << 16 | u0, v1 << 16 | u0, v1 << 16 | u1, v0 << 16 | u1 };
        float px[4] = { x0, x0, x1, x1 }, py[4] = { y0, y1, y1, y0 };
        for (int k = 0; k < 4; k++) {
            *p++ = GCM_TEXCOORD_2S;
            *p++ = tex[k];
            *p++ = GCM_POS_2F;
            *p++ = fbits(px[k]);
            *p++ = fbits(py[k]);
        }
        x += gl->advance * TEXT_SCALE;
    }
    *p++ = GCM_BEGIN_END;
    *p++ = 0;
    ctx->current = (u32*)p;
}

static void speed_label(Vec2 s, float speed, u32 colour) {
    char buf[12];
    u32 v = (u32)(speed * 100.0f + 0.5f);
    char* q = buf;
    u32 digits = 1;
    for (u32 t = v / 100; t >= 10; t /= 10) digits++;
    q = put_dec(q, v / 100, digits);
    *q++ = '.';
    q = put_dec(q, v % 100, 2);
    *q = 0;
    s.y -= GLYPH_CELL * TEXT_SCALE;
    text(s.x + SHADOW_OFFSET, s.y + SHADOW_OFFSET, COL_SHADOW, buf);
    text(s.x, s.y, colour, buf);
}

static void draw_speeds(void) {
    Ext* x = ext();
    Moby* player = player_moby;
    if (!x || !player || G.phase != PH_RUNNING || ui_screen != UI_NONE || should_load) return;
    Moby* g = ghost_alive() && x->ghost.ok ? G.ghost : 0;
    Vec2 ps, gs;
    u32 player_visible = x->player.ok && project(player, &ps), ghost_visible = g && project(g, &gs);
    u32 colour = COL_TEXT;
    if (g) {
        float d = x->player.speed - x->ghost.speed;
        colour = d > SPEED_TIE ? COL_FASTER : d < -SPEED_TIE ? COL_SLOWER : COL_TEXT;
    }
    if (player_visible && ghost_visible) {
        float dx = gs.x - ps.x, dy = gs.y - ps.y;
        if (dx > -LABEL_OVERLAP_X && dx < LABEL_OVERLAP_X && dy > -LABEL_OVERLAP_Y && dy < LABEL_OVERLAP_Y) gs.y = ps.y - LABEL_OVERLAP_Y;
    }
    if (player_visible) speed_label(ps, x->player.speed, colour);
    if (ghost_visible) speed_label(gs, x->ghost.speed, COL_GHOST);
}

void ghost_draw_hook(void) {
    game_call((u32)fn_hud_debug_panel, 0, 0, 0, 0, 0);
    if (G.magic != G_MAGIC) return;
    if (G.flags & FLAG_SPEED) draw_speeds();
    if (!G.msg_frames) return;
    G.msg_frames--;
    draw_center_medium_text(MSG_X, MSG_Y, MSG_COLOR, G.msg);
}
