OUTPUT_FORMAT("elf32-powerpc")
ENTRY(_start)

SECTIONS {
    . = 0x57DD40;
    .text : {
        KEEP(*(.text.entry))
        obj/entry.o(.text)
        *(.text.ghost_tick .text.playback .text.apply_frame .text.record .text.spawn_ghost .text.frame_ptr)
        *(.text.remove_ghost .text.ghost_alive .text.ghost_draw_hook .text.combo .text.copy .text.zero)
        *(.text.on_reload .text.end_segment .text.start_segment .text.message .text.put_str .text.put_hex)
    }
    .rodata : { *(.rodata .rodata.*) }
    ASSERT(. <= 0x57F854, "code cave overflow (0x57DD40-0x57F854)")

    . = 0x4C0FD8;
    .text2 : { *(.text .text.*) }
    ASSERT(. <= 0x4C29C0, "code cave 2 overflow (0x4C0FD8-0x4C29C0)")

    .data : { *(.data .data.* .sdata .sdata.* .bss .bss.* .sbss .sbss.* COMMON) }
    ASSERT(SIZEOF(.data) == 0, "no writable data allowed, use the Globals block")

    /DISCARD/ : { *(.comment) *(.note*) *(.eh_frame*) }
}
