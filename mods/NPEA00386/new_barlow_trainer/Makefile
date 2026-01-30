export BINARY_NAME := rack
export CODE_CAVE_START := 0x10c955c
export CODE_CAVE_END := 0x10cc8e8
export GLOBALS_START := 0x1BE0000

CC = powerpc64-linux-gnu-gcc
AS = powerpc64-linux-gnu-as
LD = powerpc64-linux-gnu-ld
OBJCOPY = powerpc64-linux-gnu-objcopy

CFLAGS = -mcpu=cell -mbig -m32 -nostdlib -O1 -ffunction-sections -fdata-sections -fno-builtin -fno-common -Isrc/include -fsingle-precision-constant
ASFLAGS = -mregnames -mcell -be -a32

OBJS := $(patsubst src/c/%.c,obj/%.o,$(wildcard src/c/*.c)) \
        $(patsubst src/asm/%.s,obj/%.o,$(wildcard src/asm/*.s))

all: bin/$(BINARY_NAME).bin patch.txt usage patch_eboot

obj/%.o: src/c/%.c
	@mkdir -p $(@D) && $(CC) $(CFLAGS) -c $< -o $@

obj/%.o: src/asm/%.s
	@mkdir -p $(@D) && $(AS) $(ASFLAGS) -o $@ $<

obj/$(BINARY_NAME).ld: src/linker_template.ld src/symbols.ld
	@mkdir -p $(@D) && ./scripts/linker.sh

obj/$(BINARY_NAME).elf: $(OBJS) obj/$(BINARY_NAME).ld
	@$(LD) -m elf32ppc -T obj/$(BINARY_NAME).ld -o $@ $(OBJS) -Map=$(@:.elf=.map)

bin/$(BINARY_NAME).bin: obj/$(BINARY_NAME).elf
	@mkdir -p $(@D) && $(OBJCOPY) -O binary $< $@

patch.txt: obj/$(BINARY_NAME).elf src/patch.txt
	@./scripts/patch.sh
	
usage:
	@echo "Usage: $$(stat -c%s bin/$(BINARY_NAME).bin) / $$(($(CODE_CAVE_END) - $(CODE_CAVE_START))) bytes"
	
patch_eboot:
	@python3 -W ignore ./scripts/eboot.py

clean:
	@rm -rf obj bin patch.txt EBOOT.BIN