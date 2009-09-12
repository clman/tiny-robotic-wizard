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
