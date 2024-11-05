# -0xF8 = branch target
# -0xF0 = LR
# -0xE8 = CTR
# -0xE0 = GPR[3..10]

entry:
    addi    r1, r1, -0x90
    std     r0, 0x78(r1)
    mflr    r0
    std     r0, 0x70(r1)
    mfctr   r0
    std     r0, 0x68(r1)
    std     r3, 0x60(r1)
    std     r4, 0x58(r1)
    std     r5, 0x50(r1)
    std     r6, 0x48(r1)
    std     r7, 0x40(r1)
    std     r8, 0x38(r1)
    std     r9, 0x30(r1)
    std     r10, 0x28(r1)
    ld      r0, 0x78(r1)
    mtctr   r0
    bctrl
    ld      r10, 0x28(r1)
    ld      r9, 0x30(r1)
    ld      r8, 0x38(r1)
    ld      r7, 0x40(r1)
    ld      r6, 0x48(r1)
    ld      r5, 0x50(r1)
    ld      r4, 0x58(r1)
    ld      r3, 0x60(r1)
    ld      r0, 0x68(r1)
    mtctr   r0
    ld      r0, 0x70(r1)
    mtlr    r0
    addi    r1, r1, 0x90
    blr
