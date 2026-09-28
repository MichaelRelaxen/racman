// RAC1 checkpoint overlay
// draws the trigger cuboid of every checkpoint in the level as a 3D wireframe on the HUD, 
// plus a cross on its respawn spot, the active checkpoint gets its own colour
// mode 1 draws every cuboid in the level instead, with checkpoints coloured.

typedef unsigned char u8;
typedef unsigned short u16;
typedef unsigned int u32;
typedef short s16;
typedef int s32;

#define R8(a)  (*(volatile u8*)(a))
#define R32(a) (*(volatile u32*)(a))
#define F32(a) (*(volatile float*)(a))

#define CUBOID_PTR   R32(0xA51BD8)
#define CUBOID_COUNT R32(0xA51BDC)
#define CAM_POS      0x951500
#define W2S_MTX      0x951440
#define PLAYER_POS   0x969D60
#define MOBY_FIRST   R32(0xA390A0)
#define MOBY_END     R32(0xA390A8)
#define CP_SET       R32(0xA54170)
#define CP_POS       0xA54180
#define CP_UPDATE    0x465D10
#define GCM_CTX      R32(0x8FA43C)
#define HUD_TEX_BASE 0xA15F4C

#define FN_DRAW_CENTER_SMALL 0x70834

#define M_COLOUR  0x00041948
#define M_INVAL   0x400C1714
#define M_BEGIN   0x00041808
#define M_TEX     0x00041904
#define M_POS     0x00041900
#define PRIM_LINES 2
#define PRIM_QUADS 8

#define G_MAGIC   0x43554231
#define MAX_HI    16
#define MAX_CP    48
#define SCR_W     512.0f
#define SCR_H     416.0f
#define GUARD     32.0f
#define MAX_VERTS 48
#define W_NEAR    0.01f

typedef struct {
    u32 magic;
    u8 enable;
    u8 labels;
    u8 lines;
    u8 marker;
    u8 mode;
    u8 pad[3];
    float max_dist;
    float sx, sy, ox, oy;
    float width;
    u32 colour;
    u32 hi_colour;
    u32 cp_colour;
    u32 active_colour;
    u32 label_colour;
    s16 hi[MAX_HI];
    u32 frames;
    u32 drawn;
    u32 count;
    u32 skipped;
    u32 cps;
} Globals;

#define G (*(Globals*)0x7142A0)
_Static_assert(sizeof(Globals) <= 0x714378 - 0x7142A0, "Globals overflow the dead OPD block");

typedef struct { float x, y, z, w; } V4;
typedef struct { float x, y; } V2;

extern s32 game_call(u32 fn, u32 a0, u32 a1, u32 a2, u32 a3, u32 a4);
extern void gfx_state(u32 ctx, u32 tex);

typedef struct { u32 n; u32 v[MAX_VERTS * 2]; } Batch;
typedef struct { s32 idx; u32 moby; u32 active; } Checkpoint;

static void init(void) {
    G.magic = G_MAGIC;
    G.enable = 1;
    G.labels = 1;
    G.lines = 0;
    G.marker = 0;
    G.mode = 0;
    G.max_dist = 200.0f;
    G.sx = 1.0f;
    G.sy = 1.0f;
    G.ox = 256.0f;
    G.oy = 208.0f;
    G.width = 1.0f;
    G.colour = 0x60FFFF00;
    G.hi_colour = 0x80FF00FF;
    G.cp_colour = 0x8000FF00;
    G.active_colour = 0x800080FF;
    G.label_colour = 0x80F0F0F0;
    for (int i = 0; i < MAX_HI; i++) G.hi[i] = -1;
    G.frames = G.drawn = G.count = G.skipped = G.cps = 0;
}

static V4 xform(const float* p) {
    const volatile float* m = (const volatile float*)W2S_MTX;
    float dx = p[0] - F32(CAM_POS), dy = p[1] - F32(CAM_POS + 4), dz = p[2] - F32(CAM_POS + 8);
    V4 v;
    v.x = dx * m[0] + dy * m[4] + dz * m[8] + m[12];
    v.y = dx * m[1] + dy * m[5] + dz * m[9] + m[13];
    v.z = dx * m[2] + dy * m[6] + dz * m[10] + m[14];
    v.w = dx * m[3] + dy * m[7] + dz * m[11] + m[15];
    return v;
}

static V2 to_hud(V4 v) {
    V2 s;
    s.x = (v.x / v.w - 2048.0f) * G.sx + G.ox;
    s.y = (v.y / v.w - 2048.0f) * G.sy + G.oy;
    return s;
}

static u32 pack(float x, float y) {
    return ((u32)(u16)(s16)(s32)y << 16) | (u16)(s16)(s32)x;
}

static int clip_edge(float p, float q, float* t0, float* t1) {
    if (p == 0.0f) return q >= 0.0f;
    float r = q / p;
    if (p < 0.0f) {
        if (r > *t1) return 0;
        if (r > *t0) *t0 = r;
    } else {
        if (r < *t0) return 0;
        if (r < *t1) *t1 = r;
    }
    return 1;
}

static int clip2d(V2* p, V2* q) {
    float t0 = 0.0f, t1 = 1.0f, dx = q->x - p->x, dy = q->y - p->y;
    if (!clip_edge(-dx, p->x + GUARD, &t0, &t1)) return 0;
    if (!clip_edge(dx, SCR_W + GUARD - p->x, &t0, &t1)) return 0;
    if (!clip_edge(-dy, p->y + GUARD, &t0, &t1)) return 0;
    if (!clip_edge(dy, SCR_H + GUARD - p->y, &t0, &t1)) return 0;
    V2 a = { p->x + dx * t0, p->y + dy * t0 }, b = { p->x + dx * t1, p->y + dy * t1 };
    *p = a;
    *q = b;
    return 1;
}

static u32 outcode(V4 v) {
    if (v.w < W_NEAR) return 16;
    V2 s = to_hud(v);
    return (s.x < 0.0f) | (s.x > SCR_W) << 1 | (s.y < 0.0f) << 2 | (s.y > SCR_H) << 3;
}

static float inv_sqrt(float f) {
    union { float f; u32 u; } c = { f };
    c.u = 0x5F3759DF - (c.u >> 1);
    float y = c.f;
    y = y * (1.5f - 0.5f * f * y * y);
    return y * (1.5f - 0.5f * f * y * y);
}

static void put(Batch* bt, u32 tex, u32 pos) {
    bt->v[bt->n * 2] = tex;
    bt->v[bt->n * 2 + 1] = pos;
    bt->n++;
}

static void flush(Batch* bt, u32 colour) {
    if (!bt->n) return;
    u32 ctx = GCM_CTX;
    u32 words = 10 + bt->n * 4;
    if (R32(ctx + 8) + words * 4 + 0x1000 > R32(ctx + 4)) {
        G.skipped++;
        bt->n = 0;
        return;
    }
    u32 tex = R32(HUD_TEX_BASE + 0x1C) + R32(R32(HUD_TEX_BASE + 0x4854) + 0x58) * 0x24;
    gfx_state(ctx, tex);
    volatile u32* p = (volatile u32*)R32(ctx + 8);
    *p++ = M_COLOUR;
    *p++ = colour;
    *p++ = M_INVAL;
    *p++ = 0;
    *p++ = 0;
    *p++ = 0;
    *p++ = M_BEGIN;
    *p++ = G.lines ? PRIM_LINES : PRIM_QUADS;
    for (u32 i = 0; i < bt->n; i++) {
        *p++ = M_TEX;
        *p++ = bt->v[i * 2];
        *p++ = M_POS;
        *p++ = bt->v[i * 2 + 1];
    }
    *p++ = M_BEGIN;
    *p++ = 0;
    R32(ctx + 8) = (u32)p;
    bt->n = 0;
}

static void segment(Batch* bt, V4 a, V4 b, u32 colour) {
    if (a.w < W_NEAR && b.w < W_NEAR) return;
    if (a.w < W_NEAR || b.w < W_NEAR) {
        float t = (W_NEAR - a.w) / (b.w - a.w);
        V4 c = { a.x + (b.x - a.x) * t, a.y + (b.y - a.y) * t, a.z + (b.z - a.z) * t, W_NEAR };
        if (a.w < W_NEAR) a = c; else b = c;
    }
    V2 p = to_hud(a), q = to_hud(b);
    if (!clip2d(&p, &q)) return;
    if (bt->n + 4 > MAX_VERTS) flush(bt, colour);
    if (G.lines) {
        put(bt, 0x00100010, pack(p.x, p.y));
        put(bt, 0x00100010, pack(q.x, q.y));
        return;
    }
    float dx = q.x - p.x, dy = q.y - p.y, l2 = dx * dx + dy * dy;
    if (l2 < 0.25f) return;
    float k = inv_sqrt(l2) * G.width * 0.5f;
    float nx = -dy * k, ny = dx * k;
    put(bt, 0x00000000, pack(p.x + nx, p.y + ny));
    put(bt, 0x00200000, pack(p.x - nx, p.y - ny));
    put(bt, 0x00200020, pack(q.x - nx, q.y - ny));
    put(bt, 0x00000020, pack(q.x + nx, q.y + ny));
}

static int highlighted(u32 idx) {
    for (int i = 0; i < MAX_HI; i++)
        if (G.hi[i] == (s16)idx) return 1;
    return 0;
}

static u32 find_checkpoints(Checkpoint* cp, u32 n) {
    u32 first = MOBY_FIRST, end = MOBY_END, found = 0;
    if (!first || end <= first || end - first > 0x100 * 4096) return 0;
    for (u32 m = first; m < end && found < MAX_CP; m += 0x100) {
        if (R8(m + 0x20) >= 0xFE) continue;
        u32 upd = R32(m + 0x74);
        if (upd < 0x700000 || upd >= 0x730000 || R32(upd) != CP_UPDATE) continue;
        u32 pv = R32(m + 0x78);
        if (!pv) continue;
        s32 idx = (s32)R32(pv);
        if (idx < 0 || (u32)idx >= n) continue;
        float dx = F32(m + 0x10) - F32(CP_POS), dy = F32(m + 0x14) - F32(CP_POS + 4);
        cp[found].idx = idx;
        cp[found].moby = m;
        cp[found].active = CP_SET && dx * dx + dy * dy < 0.25f;
        found++;
    }
    return found;
}

static void label(V4 c, u32 idx) {
    if (c.w < W_NEAR) return;
    V2 s = to_hud(c);
    if (s.x < 0 || s.x > 512 || s.y < 0 || s.y > 416) return;
    char buf[8];
    int n = 0;
    char tmp[8];
    do { tmp[n++] = '0' + idx % 10; idx /= 10; } while (idx && n < 7);
    for (int i = 0; i < n; i++) buf[i] = tmp[n - 1 - i];
    buf[n] = 0;
    game_call(FN_DRAW_CENTER_SMALL, (u32)(s32)s.x, (u32)(s32)s.y, G.label_colour, (u32)buf, (u32)-1);
}

static void draw_cuboid(Batch* bt, u32 base, u32 idx, u32 colour) {
    const volatile float* m = (const volatile float*)base;
    float c[3] = { m[12], m[13], m[14] };
    float dx = c[0] - F32(CAM_POS), dy = c[1] - F32(CAM_POS + 4), dz = c[2] - F32(CAM_POS + 8);
    if (dx * dx + dy * dy + dz * dz > G.max_dist * G.max_dist) return;
    V4 v[8];
    for (int k = 0; k < 8; k++) {
        float sx = (k & 1) ? 1.0f : -1.0f, sy = (k & 2) ? 1.0f : -1.0f, sz = (k & 4) ? 1.0f : -1.0f;
        float p[3];
        for (int j = 0; j < 3; j++) p[j] = c[j] + sx * m[j] + sy * m[4 + j] + sz * m[8 + j];
        v[k] = xform(p);
    }
    u32 all = 31;
    for (int k = 0; k < 8; k++) all &= outcode(v[k]);
    if (all) return;
    for (int k = 0; k < 8; k++)
        for (int b = 1; b < 8; b <<= 1)
            if (!(k & b)) segment(bt, v[k], v[k | b], colour);
    flush(bt, colour);
    G.drawn++;
    if (G.labels) label(xform(c), idx);
}

static void draw_cross(Batch* bt, u32 pos, float size, u32 colour) {
    const volatile float* pp = (const volatile float*)pos;
    float c[3] = { pp[0], pp[1], pp[2] };
    for (int j = 0; j < 3; j++) {
        float a[3] = { c[0], c[1], c[2] }, b[3] = { c[0], c[1], c[2] };
        a[j] -= size;
        b[j] += size;
        segment(bt, xform(a), xform(b), colour);
    }
    flush(bt, colour);
}

void overlay_draw(void) {
    if (G.magic != G_MAGIC) init();
    G.frames++;
    if (!G.enable) return;
    u32 base = CUBOID_PTR, n = CUBOID_COUNT;
    G.count = n;
    G.drawn = 0;
    Batch bt;
    bt.n = 0;
    if (G.marker) draw_cross(&bt, PLAYER_POS, 0.5f, 0x80FF00FF);
    if (!base || n > 4096) return;
    Checkpoint cp[MAX_CP];
    u32 ncp = find_checkpoints(cp, n);
    G.cps = ncp;
    for (u32 i = 0; i < ncp; i++) {
        u32 colour = cp[i].active ? G.active_colour : G.cp_colour;
        draw_cuboid(&bt, base + cp[i].idx * 0x80, cp[i].idx, colour);
        draw_cross(&bt, cp[i].moby + 0x10, 0.75f, colour);
    }
    if (G.mode != 1) return;
    for (u32 i = 0; i < n; i++) {
        u32 is_cp = 0;
        for (u32 j = 0; j < ncp; j++)
            if (cp[j].idx == (s32)i) is_cp = 1;
        if (!is_cp) draw_cuboid(&bt, base + i * 0x80, i, highlighted(i) ? G.hi_colour : G.colour);
    }
}
