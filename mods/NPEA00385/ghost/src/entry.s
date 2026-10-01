# Must be first in the image: patch.txt branches to these two fixed slots.
#   0x57DD40  0x166F98 (tick epilogue `addi r1,r1,0x40`) -> ba
#   0x57DD44  0x6C2B4 (HUD pass `bl 0x7C978`) -> bla
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
    b       tick_hook
    b       draw_hook

tick_hook:
    addi    %r1, %r1, 0x40
    lis     %r12, ghost_tick@ha
    addi    %r12, %r12, ghost_tick@l
    b       call_saved

draw_hook:
    lis     %r12, ghost_draw_hook@ha
    addi    %r12, %r12, ghost_draw_hook@l

call_saved:
    SAVE_REGS
    mtctr   %r12
    bctrl
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
    clrldi  %r3, %r4, 32
    clrldi  %r4, %r5, 32
    clrldi  %r5, %r6, 32
    clrldi  %r6, %r7, 32
    clrldi  %r7, %r8, 32
    bctrl
    ld      %r2, 0xE8(%r1)
    ld      %r0, 0xF0(%r1)
    mtlr    %r0
    addi    %r1, %r1, 0x100
    blr

    .global lv2
lv2:
    stdu    %r1, -0x40(%r1)
    mflr    %r0
    std     %r0, 0x30(%r1)
    clrldi  %r11, %r3, 32
    clrldi  %r3, %r4, 32
    clrldi  %r4, %r5, 32
    clrldi  %r5, %r6, 32
    clrldi  %r6, %r7, 32
    li      %r7, 0
    li      %r8, 0
    sc
    ld      %r0, 0x30(%r1)
    mtlr    %r0
    addi    %r1, %r1, 0x40
    blr
