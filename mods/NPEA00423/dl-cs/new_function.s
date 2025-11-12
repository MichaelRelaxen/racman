	.file	"new_function.c"
	.machine power7
	.machine altivec
	.section	".text"
	.section	.rodata
	.align 3
.LC1:
	.string	"%d.%.2d"
	.align 3
.LC2:
	.string	"%d:%.2d.%.2d"
	.section	".text"
	.align 2
	.globl new_function
	.section	".opd","aw"
	.align 3
new_function:
	.quad	.L.new_function,.TOC.@tocbase,0
	.previous
	.type	new_function, @function
.L.new_function:
.LFB0:
	.cfi_startproc
	mflr 0
	std 0,16(1)
	std 31,-8(1)
	stdu 1,-160(1)
	.cfi_def_cfa_offset 160
	.cfi_offset 65, 16
	.cfi_offset 31, -8
	mr 31,1
	.cfi_def_cfa_register 31
	std 3,208(31)
	ld 9,208(31)
	lwz 9,0(9)
	lis 10,0x9b58
	ori 10,10,0x3739
	mulhw 10,9,10
	add 10,10,9
	srawi 10,10,17
	srawi 9,9,31
	subf 9,9,10
	stw 9,128(31)
	ld 9,208(31)
	lwz 10,0(9)
	lis 9,0x9b58
	ori 9,9,0x3739
	mulhw 9,10,9
	add 9,9,10
	srawi 8,9,17
	srawi 9,10,31
	subf 9,9,8
	lis 8,0x3
	ori 8,8,0x4bc0
	mullw 9,9,8
	subf 9,9,10
	lis 10,0x91a2
	ori 10,10,0xb3c5
	mulhw 10,9,10
	add 10,10,9
	srawi 10,10,11
	srawi 9,9,31
	subf 9,9,10
	stw 9,132(31)
	ld 9,208(31)
	lwz 10,0(9)
	lis 9,0x91a2
	ori 9,9,0xb3c5
	mulhw 9,10,9
	add 9,9,10
	srawi 8,9,11
	srawi 9,10,31
	subf 9,9,8
	mulli 9,9,3600
	subf 9,9,10
	lis 10,0x8888
	ori 10,10,0x8889
	mulhw 10,9,10
	add 10,10,9
	srawi 10,10,5
	srawi 9,9,31
	subf 9,9,10
	stw 9,136(31)
	ld 9,208(31)
	lwz 9,0(9)
	lis 10,0x8888
	ori 10,10,0x8889
	mulhw 10,9,10
	add 10,10,9
	srawi 8,10,5
	srawi 10,9,31
	subf 10,10,8
	mulli 10,10,60
	subf 10,10,9
	addi 9,31,124
	stw 10,0(9)
	lfiwax 0,0,9
	fcfids 12,0
	addis 9,2,.LC0@toc@ha
	addi 9,9,.LC0@toc@l
	lfs 0,0(9)
	fmuls 0,12,0
	fctiwz 0,0
	addi 9,31,120
	stfiwx 0,0,9
	lwz 9,0(9)
	stw 9,140(31)
	lwz 9,132(31)
	cmpwi 0,9,0
	bne 0,.L2
	ld 9,208(31)
	addi 9,9,8
	lwa 8,140(31)
	lwa 10,136(31)
	mr 6,8
	mr 5,10
	addis 4,2,.LC1@toc@ha
	addi 4,4,.LC1@toc@l
	mr 3,9
	bl sprintf
	nop
	b .L3
.L2:
	ld 9,208(31)
	addi 9,9,8
	lwa 7,140(31)
	lwa 8,136(31)
	lwa 10,132(31)
	mr 6,8
	mr 5,10
	addis 4,2,.LC2@toc@ha
	addi 4,4,.LC2@toc@l
	mr 3,9
	bl sprintf
	nop
.L3:
	nop
	mr 3,9
	addi 1,31,160
	.cfi_def_cfa 1, 0
	ld 0,16(1)
	mtlr 0
	ld 31,-8(1)
	blr
	.long 0
	.byte 0,0,0,1,128,1,0,1
	.cfi_endproc
.LFE0:
	.size	new_function,.-.L.new_function
	.align 2
	.globl main
	.section	".opd","aw"
	.align 3
main:
	.quad	.L.main,.TOC.@tocbase,0
	.previous
	.type	main, @function
.L.main:
.LFB1:
	.cfi_startproc
	mflr 0
	std 0,16(1)
	std 31,-8(1)
	stdu 1,-176(1)
	.cfi_def_cfa_offset 176
	.cfi_offset 65, 16
	.cfi_offset 31, -8
	mr 31,1
	.cfi_def_cfa_register 31
	ld 9,-28688(13)
	std 9,152(31)
	li 9,0
	li 9,3630
	stw 9,124(31)
	addi 9,31,124
	mr 3,9
	bl new_function
	li 9,0
	extsw 9,9
	ld 8,152(31)
	ld 10,-28688(13)
	xor. 8,8,10
	li 10,0
	beq 0,.L6
	bl __stack_chk_fail
	nop
.L6:
	mr 3,9
	addi 1,31,176
	.cfi_def_cfa 1, 0
	ld 0,16(1)
	mtlr 0
	ld 31,-8(1)
	blr
	.long 0
	.byte 0,0,0,1,128,1,0,1
	.cfi_endproc
.LFE1:
	.size	main,.-.L.main
	.section	.rodata
	.align 2
.LC0:
	.long	1070945621
	.ident	"GCC: (Ubuntu 13.3.0-6ubuntu2~24.04) 13.3.0"
