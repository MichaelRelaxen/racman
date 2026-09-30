OUTPUT_FORMAT("elf32-powerpc")
ENTRY(_start)

SECTIONS {
    . = 0x628104;
    .text : {
        KEEP(*(.text.entry))
        *(.text .text.*)
    }
    .rodata : { *(.rodata .rodata.*) }

    .data : { *(.data .data.* .sdata .sdata.* .bss .bss.* .sbss .sbss.* COMMON) }
    ASSERT(SIZEOF(.data) == 0, "no writable data allowed, use the Globals block")
    ASSERT(. <= 0x62958C, "code cave overflow (0x628104-0x62958C)")

    /DISCARD/ : { *(.comment) *(.note*) *(.eh_frame*) }
}
