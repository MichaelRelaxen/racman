entry:
    addi    r1, r1, -0x08
    mflr    r0
    std     r0, -0x08(r1)
    lis     r26, 0x98
    ori     r0, r26, 0x1000
    bla     0x980500
    ld      r0, -0x08(r1)
    mtlr    r0
    addi    r1, r1, 0x08
    ld      r0, 0x150(r1)
    blr
