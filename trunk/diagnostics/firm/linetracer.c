#include <avr/io.h>
#include <avr/wdt.h>
#include <avr/interrupt.h>
#include <util/delay.h>
#include <avr/pgmspace.h>
#include <avr/boot.h>
#include <string.h>

#include "usbdrv.h"
#include "map.h"

/**
 * @brief Bootloader用のReportDescriptor
 */
static PROGMEM uint8_t _bootHidReportDescriptor[30] = 
	{
		0x06, 0x00, 0xff,						//	USAGE_PAGE (Vendor Defined)
		0x09, 0x01,									//	USAGE (Vendor Usage 1)
		0xa1, 0x02,									//	COLLECTION (Logical)
			0x85, 0x01,								//		REPORT_ID	(0x01)
			0x75, 0x08,								//		REPORT_SIZE (8)
			0x95, 2,							//		REPORT_COUNT (SENSOR)
			0x09, 0x00,								//		USAGE (Undefined)
			0xb2, 0x02, 0x01,					//		FEATURE (Data,Var,Abs,Buf)

			0x85, 0x02,								//		REPORT_ID	(0x02)
			0x75, 0x08,								//		REPORT_SIZE (8)
			0x95, 3,									//		REPORT_COUNT (3)
			0x09, 0x00,								//		USAGE (Undefined)
			0xb2, 0x02, 0x01,					//		FEATURE (Data,Var,Abs,Buf)
		0xc0,												//	END_COLLECTION
	};

/**
 * @brief Bootloader用のデバイスディスクリプタ
 */
static PROGMEM uint8_t _deviceDescriptor[0x12] = 
	{
		0x12,								// 0x12 bytes
		USBDESCR_DEVICE,		// Device Descriptor
		0x01, 0x01,					// USB Specification Version = Version 1,1
		0x00, 0x00, 0x00,		// Class 0, Subclass 0, Protocol 0
		0x08,								// Max packet size = 8 bytes
		USB_CFG_VENDOR_ID,	// Vendor ID
		USB_CFG_DEVICE_ID,	// Product ID
		0x00, 0x01,					// Device release number = 0x100
		0x01,								// Index of string descriptor which contains manufacturer's name
		0x02,								// Index of string descriptor which contains product's name
		0x00,								// Index of string descriptor which contains serial number
		0x01,								// Number of configurations = 1
	};
/**
 * @brief Bootloader用のコンフィギュレーションデスクリプタ
 */
static PROGMEM uint8_t _configurationDescriptor[41] = 
	{
		0x09,										// 0x09 bytes
		USBDESCR_CONFIG,				// Configuration Descriptor
		41, 00,									// Total length of descriptor
		0x01,										// Number of interfaces = 1
		0x01,										// Configuration index = 1
		0x00,										// Description of this configuration = none
		USBATTR_BUSPOWER,				// Bus powered device
		USB_CFG_MAX_BUS_POWER/2,// Maximum power
		//**** begining of inlined Interface Descriptor ****
		0x09,										// 0x09 bytes
		USBDESCR_INTERFACE,			// Interface Descriptor
		0x00,										// Interface #0
		0x00,										// Alternate setting
		0x01,										// Number of endpoints = 1
		0x03, 0x00, 0x00,				// Class3 (HID), Subclass0 (noclass), Protocol0 (noprotocol)
		0x00,										// Description of this interface = none
		//**** begining of inlined HID Descriptor ****
		0x09,										// 0x09 bytes
		USBDESCR_HID,						// HID Descriptor
		0x01, 0x01,							// HID Specification Version = Version 1.1
		0x00,										// Country code = 0 (none)
		0x01,										// Number of HID class descriptors = 1
		USBDESCR_HID_REPORT,		// Report Descriptor
		sizeof(_bootHidReportDescriptor) & 0xff,	sizeof(_bootHidReportDescriptor) >> 8, // 
		//**** begining of inlined Endpoint Descriptor ****
		0x07,										// 0x07 bytes
		USBDESCR_ENDPOINT,			// Endpoint Descriptor
		0x81,	0x03,							// Interrupt-IN1 Endpoint
		0x08, 0x00,							// Maximum packed size = 0x08 bytes
		0x0a,										// Polling interval = 0x0a ms
		//**** begining of inlined Endpoint Descriptor ****
		0x07,										// 0x07 bytes
		USBDESCR_ENDPOINT,			// Endpoint Descriptor
		0x01,	0x03,							// Interrupt-OUT1 Endpoint
		0x08,	0x00,							// Maximum packed size = 0x08 bytes
		0x0a,										// Polling interval = 0x0a ms
	};

typedef struct __COMMAND
{
	uint8_t		led;	// LEDの出力．LSBからRGB
	uint8_t		motorL;	// 左モータのモード．下位2ビットがLSBからTA8428のIN1,IN2に設定される．
	uint8_t		motorR;	// 右モータのモード．詳細は左モータを参照
} COMMAND, *PCOMMAND;

typedef struct __STATUS
{
	uint8_t		line;		// ラインセンサの状態．LSBから左，右
	uint8_t		distance;	// 距離センサの値．8bitADCの値そのまま．
} STATUS, *PSTATUS;

//static	uint8_t _reportId;				//< @brief ReportのID
uint8_t	volatile _currentAddress;	//< @brief 現在転送中のアドレス
uint8_t	volatile _bytesRemaining;	//< @brief 残転送量
uint8_t	volatile _buffer[8 + 1];	//< @brief コントロール転送用バッファ
STATUS volatile _status;			//< @brief 現在のステータス


uint8_t			bootUsbDescriptor(usbRequest_t *rq);
uint8_t   	bootUsbRead(uint8_t *data, uint8_t len);
uint8_t   	bootUsbWrite(uint8_t *data, uint8_t len);
usbMsgLen_t	bootUsbSetup(uint8_t data[8]);

typedef	uint8_t	(*USBFUNCTIONDESCRIPTOR)(usbRequest_t*);
typedef	usbMsgLen_t	(*USBFUNCTIONSETUP)(uint8_t data[8]);
typedef	uint8_t	(*USBFUNCTIONREAD)(uint8_t*, uint8_t);
typedef	uint8_t	(*USBFUNCTIONWRITE)(uint8_t*, uint8_t);

#define	FUNCTION_DESCRIPTOR	0
#define	FUNCTION_SETUP			1
#define	FUNCTION_READ				2
#define	FUNCTION_WRITE			3

void* volatile _functionTable[] = 
	{
		bootUsbDescriptor,
		bootUsbSetup,
		bootUsbRead,
		bootUsbWrite,
	};

uint8_t usbFunctionDescriptor(usbRequest_t *rq)
{
	return ((USBFUNCTIONDESCRIPTOR)_functionTable[FUNCTION_DESCRIPTOR])(rq);
}
usbMsgLen_t usbFunctionSetup(uint8_t* data)
{
	return ((USBFUNCTIONSETUP)_functionTable[FUNCTION_SETUP])(data);
}
uint8_t usbFunctionRead(uint8_t* data, uint8_t len)
{
	return ((USBFUNCTIONREAD)_functionTable[FUNCTION_READ])(data, len);
}
uint8_t usbFunctionWrite(uint8_t* data, uint8_t len)
{
	return ((USBFUNCTIONWRITE)_functionTable[FUNCTION_WRITE])(data, len);
}

/**
 * @brief 要求されたディスクリプタを設定する
 */
uint8_t	bootUsbDescriptor(usbRequest_t *rq)
{
	if( rq->wValue.bytes[1] == USBDESCR_DEVICE )
	{
		usbMsgPtr = _deviceDescriptor;
		return sizeof(_deviceDescriptor);
	}
	else if( rq->wValue.bytes[1] == USBDESCR_CONFIG )
	{
		usbMsgPtr = _configurationDescriptor;
		return sizeof(_configurationDescriptor);
	}
	else if( rq->wValue.bytes[1] == USBDESCR_HID_REPORT )
	{
		usbMsgPtr = _bootHidReportDescriptor;;
		return sizeof(_bootHidReportDescriptor);
	}

	return 0;
}



/**
 * @brief Control-In 転送のデータ転送処理を行う
 */
uint8_t   bootUsbRead(uint8_t *data, uint8_t len)
{
	return 1;
}

/**
 * @brief Control-Out 転送のデータ転送処理を行う
 */
uint8_t   bootUsbWrite(uint8_t *data, uint8_t len)
{
	if(_bytesRemaining == 0)	// もう残っていない．転送終了．
		return 1;
	if(len > _bytesRemaining)
		len = _bytesRemaining;

	memcpy(_buffer + _currentAddress, data, len);

	_currentAddress += len;
	_bytesRemaining -= len;
	
	if( _bytesRemaining == 0 )
	{
		// 転送終了．
		COMMAND* pc = (COMMAND*)(_buffer+1);
		if(pc->led & 1) PORT_LED_RED |= _BV(BIT_LED_RED); else PORT_LED_RED &= ~_BV(BIT_LED_RED);
		if(pc->led & 2) PORT_LED_GREEN |= _BV(BIT_LED_GREEN); else PORT_LED_GREEN &= ~_BV(BIT_LED_GREEN);
		if(pc->led & 4) PORT_LED_BLUE |= _BV(BIT_LED_BLUE); else PORT_LED_BLUE &= ~_BV(BIT_LED_BLUE);

		if(pc->motorL & 1) PORT_MOTOR_L_IN1 |= _BV(BIT_MOTOR_L_IN1); else PORT_MOTOR_L_IN1 &= ~_BV(BIT_MOTOR_L_IN1);
		if(pc->motorL & 2) PORT_MOTOR_L_IN2 |= _BV(BIT_MOTOR_L_IN2); else PORT_MOTOR_L_IN2 &= ~_BV(BIT_MOTOR_L_IN2);
		if(pc->motorR & 1) PORT_MOTOR_R_IN1 |= _BV(BIT_MOTOR_R_IN1); else PORT_MOTOR_R_IN1 &= ~_BV(BIT_MOTOR_R_IN1);
		if(pc->motorR & 2) PORT_MOTOR_R_IN2 |= _BV(BIT_MOTOR_R_IN2); else PORT_MOTOR_R_IN2 &= ~_BV(BIT_MOTOR_R_IN2);

		return 1;
	}
	
	return 0;
}

/**
 * @brief ホストからのリクエストに対する処理を行う．
 */
usbMsgLen_t bootUsbSetup(uint8_t data[8])
{
	usbRequest_t    *rq = (void *)data;

	if((rq->bmRequestType & USBRQ_TYPE_MASK) == USBRQ_TYPE_CLASS)	// HID Class Request
	{
		if(rq->bRequest == USBRQ_HID_GET_REPORT)
		{
			// 読み出しはReportID = 1のレポートだけ．
			// 長さが8バイト以下なので，ここで転送する．
			usbMsgPtr = (uint8_t*)&_status;
			return sizeof(_status);
		}
		else if(rq->bRequest == USBRQ_HID_SET_REPORT)
		{
			// 書き込みはReportID = 2のレポートだけ．
			_bytesRemaining = rq->wLength.bytes[0];
			_currentAddress = 0;
			return USB_NO_MSG;
		}
	}
	return 0;
}

/**
 * @brief メインです
 */
int main(void)
{
	wdt_enable(WDTO_1S);
	
	// ADCを初期化する
	ADMUX = _BV(REFS0) | _BV(ADLAR);
	DIDR0 = 0b00000001;
	ADCSRB = 0;
	ADCSRA = _BV(ADEN)|  _BV(ADIF) | 0b111;
	ADCSRA |= _BV(ADSC);
	
	// LEDのポートを初期化する
	DDR_LED_RED |= _BV(BIT_LED_RED);
	DDR_LED_GREEN |= _BV(BIT_LED_GREEN);
	DDR_LED_BLUE |= _BV(BIT_LED_BLUE);
	
	// モータードライバのポートを初期化する
	DDR_MOTOR_L_IN1 |= _BV(BIT_MOTOR_L_IN1);	
	DDR_MOTOR_L_IN2 |= _BV(BIT_MOTOR_L_IN2);
	DDR_MOTOR_R_IN1 |= _BV(BIT_MOTOR_R_IN1);	
	DDR_MOTOR_R_IN2 |= _BV(BIT_MOTOR_R_IN2);
	//
	usbInit();
	
	usbDeviceDisconnect();
	uint8_t i = 0;
	while(--i > 0)
	{
		wdt_reset();
		_delay_ms(1);
	}
	usbDeviceConnect();
	
	sei();
	
	while(1)
	{
		wdt_reset();
		
		usbPoll();
		
		if( PIN_LINE_L & _BV(BIT_LINE_L) ) _status.line |= 1; else _status.line &= ~1;
		if( PIN_LINE_R & _BV(BIT_LINE_R) ) _status.line |= 2; else _status.line &= ~2;

		if( ADCSRA & _BV(ADIF) )
		{
			_status.distance = ADCH;
			
			ADCSRA |= _BV(ADIF);
			ADCSRA |= _BV(ADSC);
		}
	}
}

