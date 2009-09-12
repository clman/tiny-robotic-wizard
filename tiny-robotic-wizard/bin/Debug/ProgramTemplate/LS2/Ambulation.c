void (*Amblation[6])(void);

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
	Amblation[0] = nop;
	Amblation[1] = forward;
	Amblation[2] = back;
	Amblation[3] = cw;
	Amblation[4] = ccw;
	Amblation[5] = stop;
	return;
}
