#include <stdio.h>

struct x {
    int frames;
    int x;
    char buf[0x14];
};

int new_function(struct x* arg) {
    int hours = arg->frames / (3600 * 60);
    int minutes = arg->frames % (3600 * 60) / (60 * 60);
    int seconds = arg->frames % (60 * 60) / 60;
    int cs = (arg->frames % 60) * (100.f / 60.f);

    if (minutes == 0) {
        sprintf(arg->buf, "%d.%.2d", seconds, cs);
    } else {
        sprintf(arg->buf, "%d:%.2d.%.2d", minutes, seconds, cs);
    }
}

int main() {
    struct x x;
    x.frames = 3630;
    new_function(&x);
    return 0;
}