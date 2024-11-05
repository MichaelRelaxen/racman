entry:
    lis     r3, 0xB0
    lwz     r4, -0x08(r3)   # read custom frame timer
    lwz     r5, 0x28(r3)    # read render barrier frame id
    lbz     r3, 0x23(r3)    # read tas state
    cmpw    r5, r4
    blt     .render
    cmpwi   r3, 2           # TAS_WORK_P
    bne     .render
    blr
.render:
    ba      0x6BB78
