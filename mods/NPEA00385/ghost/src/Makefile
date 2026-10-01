# Build from WSL: make -C src   (needs clang, lld, llvm)
CC      = clang
LD      = ld.lld
OBJCOPY = llvm-objcopy

TARGET  = --target=powerpc-unknown-none -mcpu=ppc
CFLAGS  = $(TARGET) -Oz -ffunction-sections -ffreestanding -fno-builtin -nostdlib -fno-pic -fno-stack-protector -Wall -Werror
ASFLAGS = --target=powerpc-unknown-none -mcpu=ppc64

OUT = ../ghost.bin
OUT2 = ../ghost2.bin

all: $(OUT)

obj/%.o: %.c
	@mkdir -p obj && $(CC) $(CFLAGS) -c $< -o $@

obj/%.o: %.s
	@mkdir -p obj && $(CC) $(ASFLAGS) -c $< -o $@

obj/ghost.elf: obj/entry.o obj/ghost.o link.ld
	$(LD) -T link.ld -o $@ obj/entry.o obj/ghost.o -Map=obj/ghost.map

$(OUT): obj/ghost.elf
	$(OBJCOPY) -O binary -j .text -j .rodata $< $@
	$(OBJCOPY) -O binary -j .text2 $< $(OUT2)
	@echo "cave 1: $$(stat -c%s $@) / $$((0x57F854 - 0x57DD40)) bytes, cave 2: $$(stat -c%s $(OUT2)) / $$((0x4C29C0 - 0x4C0FD8)) bytes"

clean:
	rm -rf obj $(OUT) $(OUT2)

.PHONY: all clean
