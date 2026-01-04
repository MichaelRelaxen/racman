#!/bin/bash
set -e

OBJDUMP=powerpc64-linux-gnu-objdump
HOOKS_ASM="src/asm/hooks.s"
OUTPUT="patch.txt"
ELF="obj/${BINARY_NAME}.elf"

head -3 src/patch.txt > "$OUTPUT"
echo "" >> "$OUTPUT"
echo "$CODE_CAVE_START: bin/$BINARY_NAME.bin" >> "$OUTPUT"
echo "" >> "$OUTPUT"
echo "#### Hooks ####" >> "$OUTPUT"

grep '^HOOK ' "$HOOKS_ASM" | \
while read _ addr name rest; do
    addr=${addr%,}
    name=${name%,}
    INSN=$($OBJDUMP -s -j ".hook.$name" "$ELF" 2>/dev/null | grep -A1 "Contents of section" | tail -1 | awk '{print $2}')
    
    if [ -n "$INSN" ]; then
        echo "$addr: 0x$INSN" >> "$OUTPUT"
    fi
done

tail -n +5 src/patch.txt >> "$OUTPUT"