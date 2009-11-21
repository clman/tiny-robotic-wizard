#ifndef	__QUEUE_H__
#define	__QUEUE_H__

typedef struct Queue
{
	uint8_t*	buf;
	uint8_t		rd;
	uint8_t		wr;
	uint8_t		len;
	uint8_t		mask;
} Queue_t;

#define INIT_QUEUE(buf, len)	{(buf), 0, 0, (len), (len)-1}

extern void queueClear(Queue_t* q);
extern uint8_t queuePeek(Queue_t* q);
extern uint8_t queueRead(Queue_t* q);
extern int8_t queueWrite(Queue_t* q, uint8_t c);
//extern void queueRemove(Queue_t* q, uint8_t len);
//extern uint8_t queueReadBuffer(Queue_t* q, uint8_t* buf, uint8_t len);

/***
 * 指定したキューが空かどうかを返す
 */
static inline int8_t	queueIsEmpty(Queue_t* q)
{
	return q->rd == q->wr;
}
/***
 * 指定したキューがいっぱいかどうかを返す．
 */
static inline int8_t	queueIsFull(Queue_t* q)
{
	return ((q->wr + 1) & q->mask) == q->wr;
}
/***
 * 指定したキューにあるデータのバイト数を返す
 */
static inline uint8_t queueGetCount(Queue_t* q)
{
	return q->wr >= q->rd ? q->wr - q->rd : q->len + q->wr - q->rd;
}
/***
 * 指定したキューにあるデータの連続したバイト数を返す
 */
static inline uint8_t queueGetContinuousCount(Queue_t* q)
{
	return q->wr >= q->rd ? q->wr - q->rd : q->len  - q->rd;
}
/***
 * 指定したキューの空き容量を返す
 */
static inline uint8_t queueGetFree(Queue_t* q)
{
	return q->len - queueGetCount(q);
}

#endif	//__QUEUE_H__
