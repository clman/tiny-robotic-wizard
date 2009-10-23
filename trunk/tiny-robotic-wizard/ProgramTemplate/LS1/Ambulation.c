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
	PORTD &= ~_BV(PD7), PORTB &= ~_BV(PB0);
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
