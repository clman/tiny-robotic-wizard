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
