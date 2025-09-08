new_function:
    mflr %r0
    stdu %r1, -128(%r1)
    std %r0, 144(%r1)
    lwz %r4, 0(%r3)
    lis %r7, -30584
    lis %r5, -25768
    ori %r7, %r7, 34953
    ori %r5, %r5, 14137
    mulhw %r7, %r4, %r7
    mulhw %r5, %r4, %r5
    add %r7, %r7, %r4
    lis %r6, -28254
    srwi %r10, %r7, 31
    srawi %r7, %r7, 5
    add %r7, %r7, %r10
    ori %r6, %r6, 46021
    mulli %r7, %r7, 60
    add %r5, %r5, %r4
    sub     %r7, %r4, %r7
    mulhw %r8, %r4, %r6
    srwi %r9, %r5, 31
    srawi %r5, %r5, 17
    extsw %r7, %r7
    add %r5, %r5, %r9
    std %r7, 112(%r1)
    add %r8, %r8, %r4
    mulli %r9, %r5, 3375
    srwi %r10, %r8, 31
    lfd %f0, 112(%r1)
    srawi %r8, %r8, 11
    add %r8, %r8, %r10
    fcfid %f0, %f0
    slwi %r7, %r9, 6
    lis %r9, 0x3e # multiplier hi
    frsp %f0, %f0
    mulli %r8, %r8, 3600
    lfs %f1, -0x7a24(%r9) # multiplier lo
    sub     %r8, %r4, %r8
    sub     %r4, %r4, %r7
    mulhw %r6, %r4, %r6
    fmuls %f0, %f0, %f1
    mulli %r7, %r8, -30583
    fctiwz %f0, %f0
    add %r4, %r6, %r4
    srwi %r6, %r7, 16
    addi %r7, %r1, 124
    add %r6, %r6, %r8
    stfiwx %f0, 0, %r7
    srwi %r7, %r4, 31
    srawi %r4, %r4, 11
    rlwinm %r9, %r6, 17, 31, 31
    extsh %r6, %r6
    lwa %r8, 124(%r1)
    add %r4, %r4, %r7
    srawi %r6, %r6, 5
    add %r7, %r6, %r9
    extsw %r6, %r4
    lis %r4, 0x3e # format hi
    addi %r4, %r4, -0x7a20 # format lo
    addi %r3, %r3, 8
    extsw %r7, %r7
    extsw %r5, %r5
    bl 0x41547c # sprintf
    nop
    li %r3, 0
    addi %r1, %r1, 128
    ld %r0, 16(%r1)
    mtlr %r0
    blr
.MULTIPLIER:
    .long   0x3fd55555
.FORMAT:
    .asciz  "%d:%.2d:%.2d.%.2d"
