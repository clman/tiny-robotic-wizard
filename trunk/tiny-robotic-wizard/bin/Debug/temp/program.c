#include <avr/io.h>
/*ラインセンサ*/
#define LSDDR DDRC
#define LSPORT PINC
#define LS0 PC4
#define LS1 PC5

int LineSensor;

void update_LineSensor(void){
	LineSensor = ((LSPORT&_BV(LS0))?1:0);
	return;
}

void initialize_LineSensor(void){
	LSDDR &= ~_BV(LS1) & ~_BV(LS0);

	update_LineSensor();

	return;
}

/*LED*/
#define LEDDDR DDRB
#define LEDPORT PORTB
#define GREEN _BV(PB5)
#define BLUE _BV(PB4)
#define RED _BV(PB2)

void (*LED[8])(void);

void black(void){
	LEDPORT &= ~RED & ~GREEN & ~BLUE;
	return;
}

void blue(void){
	LEDPORT &= ~RED & ~GREEN;
	LEDPORT |= BLUE;
	return;
}

void green(void){
	LEDPORT &= ~RED & ~BLUE;
	LEDPORT |= GREEN;
	return;
}

void cyan(void){
	LEDPORT &= ~RED;
	LEDPORT |= GREEN | BLUE;
	return;
}

void red(void){
	LEDPORT &= ~GREEN & ~BLUE;
	LEDPORT |= RED;
	return;
}

void magenta(void){
	LEDPORT &= ~GREEN;
	LEDPORT |= RED | BLUE;
	return;
}

void yellow(void){
	LEDPORT &= ~BLUE;
	LEDPORT |= RED | GREEN;
	return;
}

void white(void){
	LEDPORT |= RED | GREEN | BLUE;
	return;
}

void initialize_LED(void){
	LEDDDR |= RED | GREEN | BLUE;

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
#define LF() (void)(PORTD &= ~_BV(PD6), PORTD |= _BV(PD5))
#define LB() (void)(PORTD |= _BV(PD6), PORTD &= ~_BV(PD5))
#define RF() (void)(PORTD &= ~_BV(PD7), PORTB |= _BV(PB0))
#define RB() (void)(PORTD |= _BV(PD7), PORTB &= ~_BV(PB0))

void (*Ambulation[6])(void);

void nop(void){
	return;
}

void forward(void){
	LF();
	RF();

	return;
}

void back(void){
	LB();
	RB();

	return;
}

void cw(void){
	LF();
	RB();

	return;
}

void ccw(void){
	LB();
	RF();

	return;
}

void stop(void){
	PORTD &= ~_BV(PD6), PORTD &= ~_BV(PD5);
	PORTB &= ~_BV(PD7), PORTB &= ~_BV(PB0);
	return;
}

void initialize_Ambulation(void){
	DDRD |= _BV(PD7) | _BV(PD6) | _BV(PD5);
	DDRB |= _BV(PB0);

	stop();

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
void (*action[2][2])(void);
/* statusの初期化 */
initialize_LineSensor();
/* actionの初期化 */
initialize_LED();
initialize_Ambulation();
/* actionの関数ポインタの初期化 */
action[0][0] = LED[1];
action[0][1] = Ambulation[2];
action[1][0] = LED[4];
action[1][1] = Ambulation[0];
while(1){
/* statusの初期化 */
update_LineSensor();
for(w = 0; w <= 1; w++){
(*action[LineSensor][w])();
}
}
return 0;
}
