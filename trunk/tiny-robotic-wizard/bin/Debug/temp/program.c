#include <avr/io.h>
/*測距センサ*/
int DistanceSensor;

void update_DistanceSensor(void){
	return;
}

void initialize_DistanceSensor(void){
	update_DistanceSensor();
	return;
}

/*ラインセンサ*/
int LineSensor;

void update_LineSensor(void){
	return;
}

void initialize_LineSensor(void){
	update_LineSensor();
	return;
}

/*LED*/
void (*LED[8])(void);

void black(void){
	return;
}

void blue(void){
	return;
}

void green(void){
	return;
}

void cyan(void){
	return;
}

void red(void){
	return;
}

void magenta(void){
	return;
}

void yellow(void){
	return;
}

void white(void){
	return;
}

void initialize_LED(void){
	LED[0] = black;
	LED[1] = blue;
	LED[2] = green;
	LED[3] = cyan;
	LED[4] = red;
	LED[5] = magenta;
	LED[6] = yellow;
	LED[7] = white;
	return;
}

/*移動*/
void (*Ambulation[6])(void);

void nop(void){
	return;
}

void forward(void){
	return;
}

void back(void){
	return;
}

void cw(void){
	return;
}

void ccw(void){
	return;
}

void stop(void){
	return;
}

void initialize_Ambulation(void){
	Ambulation[0] = nop;
	Ambulation[1] = forward;
	Ambulation[2] = back;
	Ambulation[3] = cw;
	Ambulation[4] = ccw;
	Ambulation[5] = stop;
	return;
}

int main(void){
int w = 0;
void (*action[3][2][2])(void);
/* statusの初期化 */
initialize_DistanceSensor();
initialize_LineSensor();
/* actionの初期化 */
initialize_LED();
initialize_Ambulation();
/* actionの関数ポインタの初期化 */
action[0][0][0] = LED[0];
action[0][0][1] = Ambulation[0];
action[1][0][0] = LED[0];
action[1][0][1] = Ambulation[0];
action[2][0][0] = LED[0];
action[2][0][1] = Ambulation[0];
action[0][1][0] = LED[0];
action[0][1][1] = Ambulation[0];
action[1][1][0] = LED[0];
action[1][1][1] = Ambulation[0];
action[2][1][0] = LED[0];
action[2][1][1] = Ambulation[0];
while(1){
/* statusの初期化 */
update_DistanceSensor();
update_LineSensor();
for(w = 0; w <= 1; w++){
(*action[DistanceSensor][LineSensor][w])();
}
}
return 0;
}
