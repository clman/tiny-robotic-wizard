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
