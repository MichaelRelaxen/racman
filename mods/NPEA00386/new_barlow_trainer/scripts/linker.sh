#!/bin/bash
set -e

HOOKS_ASM="src/asm/hooks.s"
OUTPUT="obj/${BINARY_NAME}.ld"

cp src/linker_template.ld "$OUTPUT"

sed -i "1a CODE_CAVE_START = $CODE_CAVE_START;\nCODE_CAVE_END = $CODE_CAVE_END;" "$OUTPUT"

sed -i "1a GLOBALS_START = $GLOBALS_START;" "$OUTPUT"

sed -i '/\/\* SYMBOLS_GO_HERE \*\//r src/symbols.ld' "$OUTPUT"
sed -i '/\/\* SYMBOLS_GO_HERE \*\//d' "$OUTPUT"

HOOKS=$(mktemp)
grep '^HOOK ' "$HOOKS_ASM" | \
    awk '{addr=$2; name=$3; gsub(/,/, "", addr); gsub(/,/, "", name); print "    .hook." name " " addr " : { *(.hook." name ") }"}' > "$HOOKS"
sed -i '/\/\* HOOKS_GO_HERE \*\//r '"$HOOKS" "$OUTPUT"
sed -i '/\/\* HOOKS_GO_HERE \*\//d' "$OUTPUT"
rm "$HOOKS"