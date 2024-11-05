entry:
    addi    r1, r1, -0x08
    mflr    r0
    std     r0, -0x08(r1)
    lis     r28, 0x4F
    ori     r0, r28, 0x8000
    bla     0x4F63C0
    ld      r0, -0x08(r1)
    mtlr    r0
    addi    r1, r1, 0x08
    ld      r0, 0xA0(r1)
    blr
