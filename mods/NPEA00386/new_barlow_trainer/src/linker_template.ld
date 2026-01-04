OUTPUT_FORMAT("elf32-powerpc");

/* SYMBOLS_GO_HERE */

SECTIONS {
    . = CODE_CAVE_START;
    
    .text.start : {
        KEEP(*entry.o(.text))
        *entry.o(.text.*)
    }
    
    .text : {
        *(.text)
        *(.text.*)
        *(.gnu.linkonce.t*)
    }
    
    .rodata ALIGN(8) : { *(.rodata*) }
	
	. = GLOBALS_START;
	
    .data ALIGN(8) : { *(.data*) }
    .sdata ALIGN(8) : { *(.sdata*) }
    .bss ALIGN(8) : { *(.bss*) *(COMMON) }
    
    /* HOOKS_GO_HERE */
    
    /DISCARD/ : {
        *(.comment)
        *(.note*)
        *(.eh_frame)
    }
}
