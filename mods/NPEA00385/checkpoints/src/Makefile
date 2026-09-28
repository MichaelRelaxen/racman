CC      = clang
LD      = ld.lld
OBJCOPY = llvm-objcopy

TARGET  = --target=powerpc-unknown-none -mcpu=ppc
CFLAGS  = $(TARGET) -Oz -ffreestanding -fno-builtin -nostdlib -fno-pic -fno-stack-protector -Wall -Werror
ASFLAGS = --target=powerpc-unknown-none -mcpu=ppc64

OUT = ../checkpoints.bin

all: $(OUT)

obj/%.o: %.c
	@mkdir -p obj && $(CC) $(CFLAGS) -c $< -o $@

obj/%.o: %.s
	@mkdir -p obj && $(CC) $(ASFLAGS) -c $< -o $@

obj/checkpoints.elf: obj/entry.o obj/checkpoints.o link.ld
	$(LD) -T link.ld -o $@ obj/entry.o obj/checkpoints.o -Map=obj/checkpoints.map

$(OUT): obj/checkpoints.elf
	$(OBJCOPY) -O binary $< $@
	@echo "size: $$(stat -c%s $@) / $$((0x62958C - 0x628104)) bytes"

clean:
	rm -rf obj $(OUT)

.PHONY: all clean
