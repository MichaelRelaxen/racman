# Build from WSL: make -C src   (needs clang, lld, llvm)
CC      = clang
LD      = ld.lld
OBJCOPY = llvm-objcopy

TARGET  = --target=powerpc-unknown-none -mcpu=ppc
CFLAGS  = $(TARGET) -Oz -ffreestanding -fno-builtin -nostdlib -fno-pic -fno-stack-protector -Wall -Werror
ASFLAGS = --target=powerpc-unknown-none -mcpu=ppc64

OUT = ../ghost.bin

all: $(OUT)

obj/%.o: %.c
	@mkdir -p obj && $(CC) $(CFLAGS) -c $< -o $@

obj/%.o: %.s
	@mkdir -p obj && $(CC) $(ASFLAGS) -c $< -o $@

obj/ghost.elf: obj/entry.o obj/ghost.o link.ld
	$(LD) -T link.ld -o $@ obj/entry.o obj/ghost.o -Map=obj/ghost.map

$(OUT): obj/ghost.elf
	$(OBJCOPY) -O binary $< $@
	@echo "size: $$(stat -c%s $@) / $$((0x57F854 - 0x57DD40)) bytes"

clean:
	rm -rf obj $(OUT)

.PHONY: all clean
