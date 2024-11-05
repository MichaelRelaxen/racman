// Code originally written by doesthisusername for Rackets.
#include "npea00385.h"

#define api_mod (*(char*)0xB00070)
#define api_load (*(char*)0xB00071)
#define api_setaside (*(char*)0xB00072)
#define api_savemode (*(char*)0xB00073)

#define i (*((int*)0x00FFFFFC))
#define saved_save ((void*)0x01000000)
#define saved_save_planet (*((int*)0x01000018))

void entry() {
	    api_mod = 1;

		if (api_setaside == 1) {

			// set aside file
			api_setaside = 0;
            for(i = 0; i < 0xB0000; i += 0x8000) {
                memcpy((void*)((int)saved_save + i), (void*)((int)savedata_buf + i), 0x8000);
			}
		}

		// Load the set aside file, 
		if (api_load == 1) {
			if(*(int*)saved_save == 0) { // If client didn't set aside file first
				for(i = 0; i < 0xB0000; i += 0x8000) {
					memcpy((void*)((int)saved_save + i), (void*)((int)savedata_buf + i), 0x8000);
				}
			}
			perform_load(0, saved_save);
			destination_planet = saved_save_planet;
			should_load = 1;
			api_load = 0;
		}
		
		if(api_savemode != 0) {
			if(api_savemode == 2) // lmao xd
				save_handler(0);
			
			save_handler(api_savemode); 
			api_savemode = 0;
		}
}