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
