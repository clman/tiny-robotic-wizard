#include <avr/io.h>
/*測距センサ*/
int DistanceSensor;

void update_DistanceSensor(void){
	static uint8_t data[4] = {0, 0, 0, 0}, counter = 0;
	uint8_t i, max;
	// AD変換
	ADCSRA |= _BV(ADSC);							/* AD変換開始 */
	while(ADCSRA & _BV(ADSC));						/* AD変換完了待ち */
	
	// AD変換値に応じて返す数値を付ける
	if(110 < ADCH){
		data[1]++;									/* 近い */
	}else if(55 < ADCH){
		data[2]++;									/* 中くらい */
	}else if(40 < ADCH){
		data[3]++;									/* 遠い */
	}else{
		data[0]++;									/* 見あたらない */
	}
	if(++counter == 10){
		counter = 0;
		max = 0;
		for(i = 0; i <= 4 - 1; i++){
			if( data[max] < data[i]){
				max = i;
			}
		}
		DistanceSensor = max;
		for(i = 0; i <= 4 - 1; i++){
			data[i] = 0;
		}
	}
	return;
}

void initialize_DistanceSensor(void){
	// ADC初期化
	ADMUX &= ~_BV(REFS1), ADMUX |= _BV(REFS0);		/* AVCCを基準電圧にする */
	ADMUX |= _BV(ADLAR);							/* 変換結果は左詰 */
	ADMUX &= 0xF0, ADMUX |= 0x00;					/* チャンネル選択（この場合はADC0） */

	ADCSRA |= _BV(ADEN);							/* ADCを許可 */
	ADCSRA &= ~_BV(ADATE);							/* ADCの自動起動禁止 */

	DIDR0 |= _BV(ADC0D);							/* ADC0のデジタル入力禁止 */

	ADCSRA |= _BV(ADPS2) | _BV(ADPS1) | _BV(ADPS0);	/* プリスケーラ設定 */

	DistanceSensor = 0;

	return;
}

/*ラインセンサ*/
#define LSDDR DDRC
#define LSPORT PINC
#define LS0 PC4
#define LS1 PC5

int LineSensor;

void update_LineSensor(void){
	LineSensor = ((LSPORT&_BV(LS1))?2:0) | ((LSPORT&_BV(LS0))?1:0);
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
void (*action[4][4][2])(void);
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
action[3][0][0] = LED[0];
action[3][0][1] = Ambulation[0];
action[0][1][0] = LED[0];
action[0][1][1] = Ambulation[0];
action[1][1][0] = LED[0];
action[1][1][1] = Ambulation[0];
action[2][1][0] = LED[0];
action[2][1][1] = Ambulation[0];
action[3][1][0] = LED[0];
action[3][1][1] = Ambulation[0];
action[0][2][0] = LED[0];
action[0][2][1] = Ambulation[0];
action[1][2][0] = LED[0];
action[1][2][1] = Ambulation[0];
action[2][2][0] = LED[0];
action[2][2][1] = Ambulation[0];
action[3][2][0] = LED[0];
action[3][2][1] = Ambulation[0];
action[0][3][0] = LED[0];
action[0][3][1] = Ambulation[0];
action[1][3][0] = LED[0];
action[1][3][1] = Ambulation[0];
action[2][3][0] = LED[0];
action[2][3][1] = Ambulation[0];
action[3][3][0] = LED[0];
action[3][3][1] = Ambulation[0];
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
