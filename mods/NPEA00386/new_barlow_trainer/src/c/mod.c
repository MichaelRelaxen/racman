#include "types.h"
#include "game.h"

#define GREEN 0xAA00FF00
#define WHITE 0xAAFFFFFF
#define RED   0xAA0000FF
#define BLACK 0xAA000000

char formatted_race_string[32];
Moby* pBike;

Moby* get_moby_with_oclass(ushort oclass) {
    reserve_stack(256);
    for (Moby* moby = moby_instance_table; moby <= moby_instance_perm_end; ++moby) {
        if (moby->state > 0 && moby->oClass == oclass && moby->UID != 0) {
            return moby;
        }
    }
    return 0;
}

void text_with_shadow(int x, int y, unsigned int color, char* str, int len) {
    reserve_stack(256);
    text_medium_right(x + 2, y + 2, BLACK, str, len);
    text_medium_right(x, y, color, str, len);
}

void main_loop() {
    reserve_stack(256);
    if (gamestate != INGAME) return;
    if (current_planet != BARLOW && current_planet != JOBA) return;

    Moby* pBike = get_moby_with_oclass(3004);
    if (pBike == (Moby*)0x0) {
        sprintf(formatted_race_string, "No bike!");
        text_medium_left(200 , 200, WHITE, formatted_race_string, -1);
    } else {
        char* pVarBase = pBike->pVar;
        char lapFlag = *(pVarBase + 0x31F);
        sprintf(formatted_race_string, "Lap Value: %d", lapFlag);
        text_with_shadow(10, 200, lapFlag ? GREEN : RED, formatted_race_string, -1);
    }
}