#include "npea00387.h"

// binds
#define BTN_ACTION (BTN_L2 | BTN_R2)
#define BTN_ALT BTN_L3

#define BTN_RELOAD BTN_R3
#define BTN_SAVE BTN_L1
#define BTN_LOAD BTN_R1

// variables
#define i (*((int*)0xD9FEFC))

#define api_mod (*(char*)0xD9FF00)
#define api_load (*(char*)0xD9FF01)
#define api_setaside (*(char*)0xD9FF02)

#define save_save ((void*)0x1100000)

#define fastload1 (*(unsigned int*)0x134EBD4)
#define fastload2 (*(short*)0x134EE70)



void entry() {
	api_mod = 1;
	// Set aside save file
	if(api_setaside) {
		api_setaside = 0;
		
		for(i = 0; i < 0x200000; i += 0x8000) {
			memcpy((void*)((int)save_save + i), (void*)((int)savedata_buf + i), 0x8000);
		}
		
	}
	
	if(api_load == 1) {
		
		// Check if set aside savadata buffer is empty and set aside file
		if(*(int*)save_save == 0) {
			for(i = 0; i < 0x200000; i += 0x8000) {
				memcpy((void*)((int)save_save + i), (void*)((int)savedata_buf + i), 0x8000);
			}
		}
		
		// Load set aside savedata buffer
		perform_load(0, save_save);
		api_load = 2;
		
		planet_timer = 0;
	}
	else if(api_load == 2)
	{
		if(planet_timer > 30)
		{
			fastload1 = 3;
			fastload2 = 0x0101;
			
			api_load = 0;
		}
	}
}