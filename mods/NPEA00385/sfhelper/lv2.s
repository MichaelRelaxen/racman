entry:
    addi    r1, r1, -0x10
    mflr    r11
    std     r11, 0x00(r1)
    mr      r11, r3
    mr      r3, r4
    mr      r4, r5
    mr      r5, r6
    mr      r6, r7
    mr      r7, r8
    mr      r8, r9
    mr      r9, r10
    sc
    ld      r11, 0x00(r1)
    mtlr    r11
    addi    r1, r1, 0x10
    blr
