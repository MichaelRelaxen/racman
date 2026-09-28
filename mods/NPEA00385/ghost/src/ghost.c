// RAC1 (NPEA00385) ghost: records Ratchet every gameplay frame to a file on the HDD
// and plays a saved recording back as a clone of Ratchet
//
// Controls (in game, or the same commands from RacMAN via the cmd byte)
//   L3+R3     save this attempt as the planet's practice ghost and restart the level
//   L1+L3+R3  restart without saving
//   R1+L3+R3  arm a run (starts on the next load) / cancel / stop the run
//
// Files in USRDIR: ghost_PP.rgh (practice ghost of planet PP), ghost_tmp.rgh (current attempt),
// run_<ID>_<NNN>_<Planet>.rgh (run segments), run_last.bin (ID of the last run)

typedef unsigned char u8;
typedef unsigned short u16;
typedef unsigned int u32;
typedef int s32;
typedef unsigned long long u64;

#define R8(a)  (*(volatile u8*)(a))
#define R16(a) (*(volatile u16*)(a))
#define R32(a) (*(volatile u32*)(a))

#define player_moby        R32(0x96BD60)
#define down_buttons       R32(0x964AE0)
#define current_planet     R32(0x969C70)
#define should_load        R32(0xA10700)
#define destination_planet R32(0xA10704)
#define ui_screen          R32(0xA10708)
#define time_since_reload  R32(0xA10710)
#define savedata_base      R32(0xA10928)

#define FN_SPAWN_MOBY         0xEFA28
#define FN_PERFORM_LOAD       0xE8CA0
#define FN_MEMCPY             0x5C5AD0
#define FN_DRAW_CENTER_MEDIUM 0x70514
#define FN_HUD_DEBUG_DRAW     0x7C978

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

#define GHOST_ALPHA   0x40
#define GHOST_UID     0x7FF0
#define MAX_FRAMES    (60 * 60 * 30)
#define MAX_SPAWNS    4
#define RACE_LOOKAHEAD 16
#define MSG_FRAMES    180
#define MSG_COLOR     0x80F0F0F0
#define SAVE_SIZE     0xB0000
#define SCRATCH_SIZE  0x100000
#define BUF_SIZE      0x20000
#define BUF_FRAMES    (BUF_SIZE / sizeof(Frame))
#define FLUSH_FRAMES  64
#define PLAY_CHUNK    256
#define SETTLE_FRAMES 120
#define MAX_PLANET    32

#define FILE_MAGIC 0x52474831
#define G_MAGIC    0x47485335

enum { PH_IDLE, PH_RUNNING, PH_DONE };
enum { MODE_PRACTICE, MODE_RACE, MODE_OFF, MODE_FILE };
enum { RUN_IDLE, RUN_ARMED, RUN_RECORDING };
enum { CMD_NONE, CMD_SAVE, CMD_RESTART, CMD_RUN_TOGGLE, CMD_RUN_ARM, CMD_RUN_STOP };

typedef struct {
    u32 frame;
    u8 bsphere[16];
    u8 pos[16];
    u8 rot[16];
    u8 anim[8];
    u8 mtx[48];
} Frame;

typedef struct {
    u32 magic;
    u32 planet;
    u32 frame_size;
} FileHeader;

typedef struct {
    u32 magic;
    u8 cmd;
    u8 mode;
    u8 run_state;
    u8 pad;
    u32 race_id;
    u32 run_id;
    u32 run_seg;
    u32 race_seg;
    u32 mem_total;
    u32 mem_avail;
    char play_name[32];

    u32 scratch;
    u32 snap_ok;
    u32 race_last;
    u32 rec_practice;
    u32 phase;
    u32 last_reload_time;
    u32 planet;
    s32 rec_fd;
    s32 play_fd;
    s32 old_rec_fd;
    s32 old_play_fd;
    u32 rec_on;
    u32 rec_n;
    u32 old_n;
    u32 seg_frames;
    u32 play_i;
    u32 play_n;
    u32 play_pos;
    u32 ghost;
    u32 spawns;
    u32 last_cur_ptr;
    u32 prev_buttons;
    u32 msg_frames;
    s32 err;
    u32 stage;
    u64 nio;
    char path[64];
    char msg[48];
} Globals;

#define G (*(Globals*)0x717290)
_Static_assert(sizeof(Globals) <= 0x717450 - 0x717290, "Globals overflow the dead OPD block");
_Static_assert(SAVE_SIZE + 2 * BUF_SIZE <= SCRATCH_SIZE, "scratch overflow");
_Static_assert(PLAY_CHUNK > SETTLE_FRAMES && PLAY_CHUNK <= BUF_FRAMES, "prefetch must outlast the settle delay");

#define REC_BUF  (G.scratch + SAVE_SIZE)
#define PLAY_BUF (REC_BUF + BUF_SIZE)

extern s32 game_call(u32 fn, u32 a0, u32 a1, u32 a2, u32 a3, u32 a4);
extern s32 lv2(u32 num, u32 a0, u32 a1, u32 a2, u32 a3);

static const char dir[] = "/dev_hdd0/game/NPEA00385/USRDIR/";

static void __attribute__((noinline)) copy(void* dst, const void* src, u32 n) {
    volatile u32* d = dst;
    const volatile u32* s = src;
    for (n /= 4; n; n--) *d++ = *s++;
}

static void __attribute__((noinline)) zero(void* dst, u32 n) {
    volatile u32* d = dst;
    for (n /= 4; n; n--) *d++ = 0;
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

static void close_fd(s32* fd) {
    if (*fd > 0) lv2(SYS_CLOSE, *fd, 0, 0, 0);
    *fd = 0;
}

static int ghost_alive(void) {
    u32 g = G.ghost;
    return g && R8(g + 0x20) < 0xFE && R16(g + 0xB2) == GHOST_UID;
}

static void remove_ghost(void) {
    u32 g = G.ghost;
    if (ghost_alive()) {
        R8(g + 0x23) = 0;
        R8(g + 0x20) = 0xFE;
        R32(g + 0x38) = 0;
        R32(g + 0x3C) = time_since_reload + 2;
    }
    G.ghost = 0;
}

static u32 spawn_ghost(u32 ratchet) {
    u32 g = game_call(FN_SPAWN_MOBY, 0, 0, 0, 0, 0);
    if (!g) return 0;

    u32 t0 = R32(g + 0x38), t1 = R32(g + 0x3C);
    u32 keep[(0xC0 - 0x70) / 4];
    copy(keep, (void*)(g + 0x70), sizeof(keep));

    copy((void*)g, (void*)ratchet, 0x100);

    R32(g + 0x38) = t0; R32(g + 0x3C) = t1;
    copy((void*)(g + 0x70), keep, sizeof(keep));
    R32(g + 0x64) = 0;
    R32(g + 0x74) = 0;
    R8(g + 0x23) = GHOST_ALPHA;
    R16(g + 0xB2) = GHOST_UID;
    return g;
}

static u32 frame_ptr(u32 cls, u32 seq, u32 f) {
    u32 sp = R32(cls + 0x48 + seq * 4);
    if (!sp || sp >= 0x80000000 || f >= R8(sp + 0x10)) return 0;
    return R32(sp + 0x1C + f * 4);
}

static void apply_frame(u32 g, Frame* fr) {
    copy((void*)(g + 0x00), fr->bsphere, 16);
    copy((void*)(g + 0x10), fr->pos, 16);
    copy((void*)(g + 0x40), fr->rot, 16);
    copy((void*)(g + 0xC0), fr->mtx, 48);

    u32 cls = R32(g + 0x24);
    if (!cls) return;
    u32 f = fr->anim[0], nf = fr->anim[1], seq = fr->anim[2], tseq = fr->anim[3];
    u32 cur, nxt;
    if (seq != 0xFF) {
        cur = frame_ptr(cls, seq, f);
        nxt = frame_ptr(cls, seq, nf);
        if (cur) G.last_cur_ptr = cur;
    } else {
        nxt = frame_ptr(cls, tseq, nf);
        cur = G.last_cur_ptr ? G.last_cur_ptr : nxt;
        seq = tseq;
    }
    if (!cur || !nxt) return;

    R8(g + 0x50) = f;
    R8(g + 0x51) = nf;
    R8(g + 0x52) = seq;
    R8(g + 0x53) = tseq;
    copy((void*)(g + 0x54), &fr->anim[4], 4);
    R32(g + 0x68) = cur;
    R32(g + 0x6C) = nxt;
}

static u32 read_last_run_id(void) {
    s32 fd = 0;
    u32 id = 0;
    if (lv2(SYS_OPEN, (u32)file_path("run_last.bin"), O_RDONLY, (u32)&fd, 0)) return 0;
    lv2(SYS_READ, fd, (u32)&id, 4, (u32)&G.nio);
    close_fd(&fd);
    return id;
}

static void write_last_run_id(u32 id) {
    s32 fd = 0;
    if (lv2(SYS_OPEN, (u32)file_path("run_last.bin"), O_WRITE, (u32)&fd, 0)) return;
    lv2(SYS_WRITE, fd, (u32)&id, 4, (u32)&G.nio);
    close_fd(&fd);
}

static void write_frames(s32 fd, u32 buf, u32 n) {
    if (fd && n) lv2(SYS_WRITE, fd, buf, n * sizeof(Frame), (u32)&G.nio);
}

static void flush_rec(void) {
    G.stage = 5;
    write_frames(G.rec_fd, REC_BUF, G.rec_n);
    G.rec_n = 0;
}

static void finish_old(void) {
    write_frames(G.old_rec_fd, REC_BUF, G.old_n);
    close_fd(&G.old_rec_fd);
    close_fd(&G.old_play_fd);
    if (G.old_n) {
        G.rec_n -= G.old_n;
        copy((void*)REC_BUF, (void*)(REC_BUF + G.old_n * sizeof(Frame)), G.rec_n * sizeof(Frame));
        G.old_n = 0;
    }
}

static void on_reload(void) {
    if (G.phase == PH_RUNNING && !G.old_rec_fd && !G.old_play_fd) {
        G.old_rec_fd = G.rec_fd;
        G.old_play_fd = G.play_fd;
        G.old_n = G.rec_fd ? G.rec_n : 0;
        G.rec_n = G.old_n;
        G.rec_fd = G.play_fd = 0;
        G.play_i = G.play_n = 0;
    }
    G.ghost = 0;
    G.spawns = 0;
    G.last_cur_ptr = 0;
    G.planet = current_planet;
    G.phase = PH_IDLE;
}

static void fill_play(void) {
    G.stage = 6;
    G.nio = 0;
    lv2(SYS_READ, G.play_fd, PLAY_BUF, PLAY_CHUNK * sizeof(Frame), (u32)&G.nio);
    G.play_n = (u32)G.nio / sizeof(Frame);
    G.play_i = 0;
    if (G.play_n < PLAY_CHUNK) close_fd(&G.play_fd);
}

static int open_ghost(const char* path, u32 planet) {
    FileHeader h;
    if (lv2(SYS_OPEN, (u32)path, O_RDONLY, (u32)&G.play_fd, 0)) {
        G.play_fd = 0;
        return 0;
    }
    G.nio = 0;
    lv2(SYS_READ, G.play_fd, (u32)&h, sizeof(h), (u32)&G.nio);
    if (G.nio != sizeof(h) || h.magic != FILE_MAGIC || h.planet != planet || h.frame_size != sizeof(Frame)) {
        close_fd(&G.play_fd);
        return 0;
    }
    return 1;
}

static int open_race_segment(u32 planet) {
    u32 id = G.race_id ? G.race_id : G.race_last ? G.race_last : read_last_run_id();
    if (!id) return 0;
    for (u32 k = 0; k < RACE_LOOKAHEAD; k++) {
        if (open_ghost(run_path(id, G.race_seg + k, planet), planet)) {
            G.race_seg += k + 1;
            return 1;
        }
    }
    return 0;
}

static void open_playback(u32 planet) {
    G.stage = 3;
    G.play_i = G.play_n = G.play_pos = 0;
    if (!G.scratch || planet >= MAX_PLANET) return;
    G.play_name[sizeof(G.play_name) - 1] = 0;
    int ok = G.mode == MODE_PRACTICE ? open_ghost(practice_path(planet), planet)
           : G.mode == MODE_RACE ? open_race_segment(planet)
           : G.mode == MODE_FILE ? open_ghost(file_path(G.play_name), planet) : 0;
    if (ok) fill_play();
}

static void leave_segment(u32 next_planet) {
    G.stage = 2;
    finish_old();
    flush_rec();
    close_fd(&G.rec_fd);
    close_fd(&G.play_fd);
    G.rec_on = 0;
    G.rec_practice = 0;
    G.phase = PH_DONE;
    remove_ghost();
    open_playback(next_planet);
    G.stage = 0;
}

static void snapshot_planet(void) {
    G.snap_ok = G.scratch && savedata_base;
    if (G.snap_ok) game_call(FN_MEMCPY, G.scratch, savedata_base + 0x100000, SAVE_SIZE, 0, 0);
}

static void start_segment(void) {
    if (G.run_state == RUN_ARMED) {
        G.run_state = RUN_RECORDING;
        message("Recording run ", G.run_id);
    }
    G.seg_frames = 0;
    G.rec_on = G.scratch != 0;
    G.rec_practice = 0;
    snapshot_planet();
    G.phase = PH_RUNNING;
}

static void settle(void) {
    G.stage = 4;
    finish_old();
    if (G.rec_on) {
        FileHeader h = { FILE_MAGIC, G.planet, sizeof(Frame) };
        char* path = G.run_state == RUN_RECORDING ? run_path(G.run_id, G.run_seg++, G.planet) : file_path("ghost_tmp.rgh");
        G.err = lv2(SYS_OPEN, (u32)path, O_WRITE, (u32)&G.rec_fd, 0);
        if (G.err) {
            G.rec_fd = 0;
            G.rec_on = 0;
            G.rec_n = 0;
        } else {
            lv2(SYS_WRITE, G.rec_fd, (u32)&h, sizeof(h), (u32)&G.nio);
        }
        G.rec_practice = G.rec_fd && G.run_state != RUN_RECORDING;
    }
    if (!G.play_fd && G.play_i >= G.play_n) open_playback(G.planet);
    G.stage = 0;
}

static void restart_level(void) {
    leave_segment(G.planet);
    if (G.snap_ok) game_call(FN_PERFORM_LOAD, 0, G.scratch, 0, 0, 0);
    destination_planet = G.planet;
    should_load = 1;
}

static void save_practice(void) {
    if (G.run_state == RUN_RECORDING) {
        message("Stop the run to save a practice ghost", 0);
        return;
    }
    if (!G.rec_practice) return;
    leave_segment(MAX_PLANET);

    u32 from[sizeof(G.path) / 4];
    copy(from, file_path("ghost_tmp.rgh"), sizeof(from));
    lv2(SYS_UNLINK, (u32)practice_path(G.planet), 0, 0, 0);
    G.err = lv2(SYS_RENAME, (u32)from, (u32)G.path, 0, 0);
    message(G.err ? "Save failed" : "Practice ghost saved", 0);
    restart_level();
}

static void run_arm(void) {
    u64 sec = 0, nsec = 0;
    if (G.run_state != RUN_IDLE) return;
    G.stage = 1;
    G.race_last = read_last_run_id();
    G.race_seg = 0;
    lv2(SYS_TIME, (u32)&sec, (u32)&nsec, 0, 0);
    G.run_id = (u32)sec;
    G.run_seg = 0;
    write_last_run_id(G.run_id);
    G.run_state = RUN_ARMED;
    G.stage = 0;
    message("Run armed: starts on next load", 0);
}

static void run_stop(void) {
    if (G.run_state == RUN_ARMED) {
        write_last_run_id(G.race_last);
        G.run_state = RUN_IDLE;
        message("Run cancelled", 0);
    } else if (G.run_state == RUN_RECORDING) {
        flush_rec();
        close_fd(&G.rec_fd);
        G.rec_on = 0;
        G.race_last = 0;
        G.run_state = RUN_IDLE;
        message("Run saved ", G.run_id);
    }
}

static void command(u32 cmd) {
    switch (cmd) {
    case CMD_SAVE:       save_practice(); break;
    case CMD_RESTART:    restart_level(); break;
    case CMD_RUN_TOGGLE: if (G.run_state == RUN_IDLE) run_arm(); else run_stop(); break;
    case CMD_RUN_ARM:    run_arm(); break;
    case CMD_RUN_STOP:   run_stop(); break;
    }
}

static u32 combo(void) {
    u32 btn = down_buttons;
    u32 pressed = btn & ~G.prev_buttons;
    G.prev_buttons = btn;
    if ((btn & (BTN_L3 | BTN_R3)) != (BTN_L3 | BTN_R3) || !(pressed & (BTN_L3 | BTN_R3))) return CMD_NONE;
    if (btn & BTN_R1) return CMD_RUN_TOGGLE;
    if (btn & BTN_L1) return CMD_RESTART;
    return CMD_SAVE;
}

static void record(u32 r, u32 io) {
    if (G.rec_n >= BUF_FRAMES) return;
    Frame* fr = (Frame*)(REC_BUF + G.rec_n++ * sizeof(Frame));
    fr->frame = G.seg_frames;
    copy(fr->bsphere, (void*)(r + 0x00), 16);
    copy(fr->pos, (void*)(r + 0x10), 16);
    copy(fr->rot, (void*)(r + 0x40), 16);
    copy(fr->anim, (void*)(r + 0x50), 8);
    copy(fr->mtx, (void*)(r + 0xC0), 48);
    if (!io || !G.rec_fd) return;
    if (G.seg_frames + 1 >= MAX_FRAMES) {
        flush_rec();
        close_fd(&G.rec_fd);
        G.rec_on = 0;
    } else if (G.rec_n >= FLUSH_FRAMES) {
        flush_rec();
    }
}

static void playback(u32 r, u32 io) {
    Frame* fr = 0;
    while (G.play_pos <= G.seg_frames) {
        if (G.play_i >= G.play_n) {
            if (!G.play_fd) {
                remove_ghost();
                return;
            }
            if (!io) return;
            fill_play();
            if (!G.play_n) {
                remove_ghost();
                return;
            }
        }
        fr = (Frame*)(PLAY_BUF + G.play_i++ * sizeof(Frame));
        G.play_pos++;
    }
    if (!fr) return;

    if (!ghost_alive()) {
        G.ghost = 0;
        if (G.spawns >= MAX_SPAWNS) return;
        G.spawns++;
        G.ghost = spawn_ghost(r);
        if (!G.ghost) return;
    }
    apply_frame(G.ghost, fr);
}

void ghost_tick(void) {
    if (G.magic != G_MAGIC) {
        zero(&G, sizeof(G));
        G.magic = G_MAGIC;
        G.planet = current_planet;
        lv2(SYS_MEMINFO, (u32)&G.mem_total, 0, 0, 0);
        if (lv2(SYS_MEMALLOC, SCRATCH_SIZE, MEM_PAGE_1M, (u32)&G.scratch, 0)) G.scratch = 0;
    }

    u32 t = time_since_reload;
    if (t < G.last_reload_time || current_planet != G.planet) on_reload();
    G.last_reload_time = t;

    if (should_load) {
        if (G.phase == PH_RUNNING) leave_segment(destination_planet);
        return;
    }
    u32 r = player_moby;
    if (!r || ui_screen != UI_NONE) return;

    u32 cmd = G.cmd;
    G.cmd = CMD_NONE;
    if (cmd == CMD_NONE) cmd = combo();
    command(cmd);
    if (should_load) return;

    if (G.phase == PH_IDLE) start_segment();
    if (G.phase != PH_RUNNING) return;

    u32 io = G.seg_frames >= SETTLE_FRAMES;
    if (G.seg_frames == SETTLE_FRAMES) settle();
    if (G.rec_on) record(r, io);
    if (G.scratch) playback(r, io);
    G.seg_frames++;
}

void ghost_draw_hook(void) {
    game_call(FN_HUD_DEBUG_DRAW, 0, 0, 0, 0, 0);
    if (G.magic != G_MAGIC || !G.msg_frames) return;
    G.msg_frames--;
    game_call(FN_DRAW_CENTER_MEDIUM, 256, 80, MSG_COLOR, (u32)G.msg, (u32)-1);
}
