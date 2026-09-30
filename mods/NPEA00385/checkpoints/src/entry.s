# Must be first in the image: patch.txt turns 0x6C2AC (HUD pass `bl 0xD5C20`) into `bla 0x628104`.
# Our C is -m32 and only preserves the low word of r14-r31, so the full 64-bit regs are saved here.

.macro SAVE_REGS
    stdu    %r1, -0x200(%r1)
    mflr    %r0
    std     %r0, 0x1F0(%r1)
    mfcr    %r0
    std     %r0, 0x1E8(%r1)
    std     %r2, 0x1E0(%r1)
    std     %r3, 0x1D8(%r1)
    std     %r13, 0x1D0(%r1)
    std     %r14, 0x1C8(%r1)
    std     %r15, 0x1C0(%r1)
    std     %r16, 0x1B8(%r1)
    std     %r17, 0x1B0(%r1)
    std     %r18, 0x1A8(%r1)
    std     %r19, 0x1A0(%r1)
    std     %r20, 0x198(%r1)
    std     %r21, 0x190(%r1)
    std     %r22, 0x188(%r1)
    std     %r23, 0x180(%r1)
    std     %r24, 0x178(%r1)
    std     %r25, 0x170(%r1)
    std     %r26, 0x168(%r1)
    std     %r27, 0x160(%r1)
    std     %r28, 0x158(%r1)
    std     %r29, 0x150(%r1)
    std     %r30, 0x148(%r1)
    std     %r31, 0x140(%r1)
.endm

.macro RESTORE_REGS
    ld      %r31, 0x140(%r1)
    ld      %r30, 0x148(%r1)
    ld      %r29, 0x150(%r1)
    ld      %r28, 0x158(%r1)
    ld      %r27, 0x160(%r1)
    ld      %r26, 0x168(%r1)
    ld      %r25, 0x170(%r1)
    ld      %r24, 0x178(%r1)
    ld      %r23, 0x180(%r1)
    ld      %r22, 0x188(%r1)
    ld      %r21, 0x190(%r1)
    ld      %r20, 0x198(%r1)
    ld      %r19, 0x1A0(%r1)
    ld      %r18, 0x1A8(%r1)
    ld      %r17, 0x1B0(%r1)
    ld      %r16, 0x1B8(%r1)
    ld      %r15, 0x1C0(%r1)
    ld      %r14, 0x1C8(%r1)
    ld      %r13, 0x1D0(%r1)
    ld      %r3, 0x1D8(%r1)
    ld      %r2, 0x1E0(%r1)
    ld      %r0, 0x1E8(%r1)
    mtcr    %r0
    ld      %r0, 0x1F0(%r1)
    mtlr    %r0
    addi    %r1, %r1, 0x200
.endm

    .section .text.entry, "ax"
    .global _start
_start:
    SAVE_REGS
    ld      %r3, 0x1D8(%r1)
    lis     %r12, 0xD
    ori     %r12, %r12, 0x5C20
    mtctr   %r12
    bctrl
    std     %r3, 0x1D8(%r1)
    bl      overlay_draw
    RESTORE_REGS
    blr

    .text
    .global game_call
game_call:
    stdu    %r1, -0x100(%r1)
    mflr    %r0
    std     %r0, 0xF0(%r1)
    std     %r2, 0xE8(%r1)
    mtctr   %r3
    mr      %r3, %r4
    mr      %r4, %r5
    mr      %r5, %r6
    mr      %r6, %r7
    mr      %r7, %r8
    bctrl
    ld      %r2, 0xE8(%r1)
    ld      %r0, 0xF0(%r1)
    mtlr    %r0
    addi    %r1, %r1, 0x100
    blr

    .global gfx_state
gfx_state:
    stdu    %r1, -0x100(%r1)
    mflr    %r0
    std     %r0, 0xF0(%r1)
    std     %r2, 0xE8(%r1)
    std     %r31, 0xE0(%r1)
    clrldi  %r31, %r3, 32
    clrldi  %r5, %r4, 32
    mr      %r3, %r31
    li      %r4, 0
    lis     %r6, 0x10F
    ld      %r6, -0x1D20(%r6)
    lis     %r12, 0x4D
    ori     %r12, %r12, 0x6E60
    mtctr   %r12
    bctrl
    mr      %r3, %r31
    lis     %r12, 0x4D
    ori     %r12, %r12, 0x6948
    mtctr   %r12
    bctrl
    ld      %r31, 0xE0(%r1)
    ld      %r2, 0xE8(%r1)
    ld      %r0, 0xF0(%r1)
    mtlr    %r0
    addi    %r1, %r1, 0x100
    blr
