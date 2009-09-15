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
