#include <avr/io.h>
#include <avr/interrupt.h>

#include "queue.h"

#define NULL	((void*)0)

/***
 * 指定したキューを初期化する
 */
void queueClear(Queue_t* q)
{
	q->rd = q->wr = 0;
}
/***
 * 指定したキューから1バイト読む
 */
uint8_t queueRead(Queue_t* q)
{
	while(q->rd == q->wr);
	uint8_t sreg = SREG;
	cli();
	uint8_t data = q->buf[q->rd];
	q->rd = (q->rd + 1) & q->mask;
	if( sreg & _BV(SREG_I) ) sei();
	return data;
}
/***
 * 指定したキューの先頭1バイトを取得する．
 * 読み出しポインタは先に進まない．
 */
uint8_t queuePeek(Queue_t* q)
{
	while(queueIsEmpty(q));
	return q->buf[q->rd];
}
/***
 * 指定したキューの末尾に1バイト追加する．
 * キューがいっぱいで追加できなかったらfalse
 * 追加できたらtrueを返す
 */
int8_t queueWrite(Queue_t* q, uint8_t c)
{
	uint8_t next = (q->wr + 1) & q->mask;
	if( next == q->rd ) return 0;
	
	uint8_t sreg = SREG;
	cli();
	q->buf[q->wr] = c;
	q->wr = next;
	if( sreg & _BV(SREG_I) ) sei();
	
	return 1;
}
#if 0
/***
 * 指定したキューからバッファに読み込む
 */
uint8_t queueReadBuffer(Queue_t* q, uint8_t* buf, uint8_t len)
{
	uint8_t bytesRead = 0;
	while(!queueIsEmpty(q) && len--)
	{
		*(buf++) = queueRead(q);
		bytesRead++;
	}
	return bytesRead;
}

/***
 * 指定したキューから指定した分だけデータを破棄する
 */
void queueRemove(Queue_t* q, uint8_t len)
{
	// 残りがlenより少ないかどうかのチェックは省略
	q->rd = (q->rd + len) & q->mask;
}
#endif
