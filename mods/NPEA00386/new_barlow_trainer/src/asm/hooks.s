# Macro for creating raw patches.
.macro HOOK addr, name, instr:vararg
	.section .hook.\name 
	\instr
.endm 

# --- Entrypoints ---
# Runs our custom function called 'draw', hooked to some point in draw loop.
HOOK 0x10c18ec, draw_hook, bl main_loop

HOOK 0x10c3498, return_early, blr