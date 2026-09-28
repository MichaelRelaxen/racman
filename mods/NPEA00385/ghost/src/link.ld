OUTPUT_FORMAT("elf32-powerpc")
ENTRY(_start)

SECTIONS {
    . = 0x57DD40;
    .text : {
        KEEP(*(.text.entry))
        *(.text .text.*)
    }
    .rodata : { *(.rodata .rodata.*) }

    .data : { *(.data .data.* .sdata .sdata.* .bss .bss.* .sbss .sbss.* COMMON) }
    ASSERT(SIZEOF(.data) == 0, "no writable data allowed, use the Globals block")
    ASSERT(. <= 0x57F854, "code cave overflow (0x57DD40-0x57F854)")

    /DISCARD/ : { *(.comment) *(.note*) *(.eh_frame*) }
}
