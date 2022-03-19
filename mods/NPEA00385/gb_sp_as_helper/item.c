#define n_items (*((int*)0x00aff020))

void _start() {	
	n_items += 1;
}