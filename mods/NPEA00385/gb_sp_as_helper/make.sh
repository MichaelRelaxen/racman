#!/bin/bash
s() {
    powerpc64-linux-gnu-as -o $1.o -mregnames -mcell -be $1.s
    powerpc64-linux-gnu-ld --oformat=binary -o $1.bin $1.o
    rm $1.o
}
c() {
    powerpc64-linux-gnu-gcc -mcpu=cell -mbig -m32 -Wl,--oformat=binary,-Ttext=$2 -nostdlib -O2 -o $1.bin $1.c
}
	
c gold_bolt 708ec8
c skillpoint 4f5cac
c item 4F5D10
c infobots 4F5D74