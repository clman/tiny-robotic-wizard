#ifndef __MAP_H__
/**
 * @file map.h
 * 
 * ���̃t�@�C����gpiomap.py�ɂ�莩���I�ɐ�������܂����D
 * �|�[�g�̊��蓖�Ă̐ݒ���s���܂��D
 */

#define	MAP_CC(a, b)	a ## b
#define	MAP_OUT(name)	MAP_CC(PORT, name)
#define	MAP_IN(name)	MAP_CC(PIN, name)
#define	MAP_DIR(name)	MAP_CC(DDR, name)

//definition for BOOTMODE
#define	PORTNAME_BOOTMODE	D
#define	BIT_BOOTMODE	0
#define	PORT_BOOTMODE	MAP_OUT(PORTNAME_BOOTMODE)
#define	PIN_BOOTMODE	MAP_IN(PORTNAME_BOOTMODE)
#define	DDR_BOOTMODE	MAP_DIR(PORTNAME_BOOTMODE)

//definition for MOTOR_L_IN1
#define	PORTNAME_MOTOR_L_IN1	D
#define	BIT_MOTOR_L_IN1	5
#define	PORT_MOTOR_L_IN1	MAP_OUT(PORTNAME_MOTOR_L_IN1)
#define	PIN_MOTOR_L_IN1	MAP_IN(PORTNAME_MOTOR_L_IN1)
#define	DDR_MOTOR_L_IN1	MAP_DIR(PORTNAME_MOTOR_L_IN1)

//definition for MOTOR_L_IN2
#define	PORTNAME_MOTOR_L_IN2	D
#define	BIT_MOTOR_L_IN2	6
#define	PORT_MOTOR_L_IN2	MAP_OUT(PORTNAME_MOTOR_L_IN2)
#define	PIN_MOTOR_L_IN2	MAP_IN(PORTNAME_MOTOR_L_IN2)
#define	DDR_MOTOR_L_IN2	MAP_DIR(PORTNAME_MOTOR_L_IN2)

//definition for MOTOR_R_IN2
#define	PORTNAME_MOTOR_R_IN2	D
#define	BIT_MOTOR_R_IN2	7
#define	PORT_MOTOR_R_IN2	MAP_OUT(PORTNAME_MOTOR_R_IN2)
#define	PIN_MOTOR_R_IN2	MAP_IN(PORTNAME_MOTOR_R_IN2)
#define	DDR_MOTOR_R_IN2	MAP_DIR(PORTNAME_MOTOR_R_IN2)

//definition for MOTOR_R_IN1
#define	PORTNAME_MOTOR_R_IN1	B
#define	BIT_MOTOR_R_IN1	0
#define	PORT_MOTOR_R_IN1	MAP_OUT(PORTNAME_MOTOR_R_IN1)
#define	PIN_MOTOR_R_IN1	MAP_IN(PORTNAME_MOTOR_R_IN1)
#define	DDR_MOTOR_R_IN1	MAP_DIR(PORTNAME_MOTOR_R_IN1)

//definition for LED_RED
#define	PORTNAME_LED_RED	B
#define	BIT_LED_RED	2
#define	PORT_LED_RED	MAP_OUT(PORTNAME_LED_RED)
#define	PIN_LED_RED	MAP_IN(PORTNAME_LED_RED)
#define	DDR_LED_RED	MAP_DIR(PORTNAME_LED_RED)

//definition for LED_BLUE
#define	PORTNAME_LED_BLUE	B
#define	BIT_LED_BLUE	4
#define	PORT_LED_BLUE	MAP_OUT(PORTNAME_LED_BLUE)
#define	PIN_LED_BLUE	MAP_IN(PORTNAME_LED_BLUE)
#define	DDR_LED_BLUE	MAP_DIR(PORTNAME_LED_BLUE)

//definition for LED_GREEN
#define	PORTNAME_LED_GREEN	B
#define	BIT_LED_GREEN	5
#define	PORT_LED_GREEN	MAP_OUT(PORTNAME_LED_GREEN)
#define	PIN_LED_GREEN	MAP_IN(PORTNAME_LED_GREEN)
#define	DDR_LED_GREEN	MAP_DIR(PORTNAME_LED_GREEN)

//definition for DISTANCE
#define	PORTNAME_DISTANCE	C
#define	BIT_DISTANCE	0
#define	PORT_DISTANCE	MAP_OUT(PORTNAME_DISTANCE)
#define	PIN_DISTANCE	MAP_IN(PORTNAME_DISTANCE)
#define	DDR_DISTANCE	MAP_DIR(PORTNAME_DISTANCE)

//definition for LINE_L
#define	PORTNAME_LINE_L	C
#define	BIT_LINE_L	4
#define	PORT_LINE_L	MAP_OUT(PORTNAME_LINE_L)
#define	PIN_LINE_L	MAP_IN(PORTNAME_LINE_L)
#define	DDR_LINE_L	MAP_DIR(PORTNAME_LINE_L)

//definition for LINE_R
#define	PORTNAME_LINE_R	C
#define	BIT_LINE_R	5
#define	PORT_LINE_R	MAP_OUT(PORTNAME_LINE_R)
#define	PIN_LINE_R	MAP_IN(PORTNAME_LINE_R)
#define	DDR_LINE_R	MAP_DIR(PORTNAME_LINE_R)


#endif	//__MAP_H__