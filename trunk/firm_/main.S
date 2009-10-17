#include <avr/io.h>
#include "usbdrv.h"

/**
 * @section FLASH上に配置される定数
 */
	.section .progmem.data

#define	BLINFO_SIZE	(blInfo_End - blInfo)

/**
 * @brief Bootloader用のReportDescriptor
 */
hidReportDescriptor:
	.byte 0x06, 0x00, 0xff				//	USAGE_PAGE (Vendor Defined)
	.byte 0x09, 0x01							//	USAGE (Vendor Usage 1)
	.byte 0xa1, 0x02							//	COLLECTION (Logical)
	.byte	0x85, 0x01							//		REPORT_ID	(0x01)
	.byte	0x75, 0x08						//		REPORT_SIZE (8)
	.byte	0x95, 6								//		REPORT_COUNT (BLINFO)
	.byte	0x09, 0x00						//		USAGE (Undefined)
	.byte	0xb2, 0x02, 0x01			//		FEATURE (Data,Var,Abs,Buf)
	
	.byte	0x85, 0x02						//		REPORT_ID	(0x02)
	.byte	0x75, 0x08						//		REPORT_SIZE (8)
	.byte	0x95, 66							//		REPORT_COUNT (BLPAGE)
	.byte	0x09, 0x00						//		USAGE (Undefined)
	.byte	0xb2, 0x02, 0x01			//		FEATURE (Data,Var,Abs,Buf)
	.byte	0xc0										//	END_COLLECTION
hidReportDescriptor_End:

#define	REPORTDESC_SIZE	(hidReportDescriptor_End - hidReportDescriptor)

/**
 * @brief Bootloader用のデバイスディスクリプタ
 */
deviceDescriptor:
	.byte	0x12								// 0x12 bytes
	.byte	USBDESCR_DEVICE			// Device Descriptor
	.byte	0x01, 0x01					// USB Specification Version = Version 1,1
	.byte	0x00, 0x00, 0x00		// Class 0, Subclass 0, Protocol 0
	.byte	0x08								// Max packet size = 8 bytes
	.byte	USB_CFG_VENDOR_ID		// Vendor ID
	.byte	USB_CFG_DEVICE_ID		// Product ID
	.byte	0x00, 0x01					// Device release number = 0x100
	.byte	0x01								// Index of string descriptor which contains manufacturer's name
	.byte	0x02								// Index of string descriptor which contains product's name
	.byte	0x00								// Index of string descriptor which contains serial number
	.byte	0x01								// Number of configurations = 1
deviceDescriptor_End:

#define	DEVICEDESC_SIZE	(deviceDescriptor_End - deviceDescriptor)

/**
 * @brief Bootloader用のコンフィギュレーションデスクリプタ
 */
configurationDescriptor:
	.byte	0x09										// 0x09 bytes
	.byte	USBDESCR_CONFIG					// Configuration Descriptor
	.byte	41, 00									// Total length of descriptor
	.byte	0x01										// Number of interfaces = 1
	.byte	0x01										// Configuration index = 1
	.byte	0x00										// Description of this configuration = none
	.byte	USBATTR_BUSPOWER				// Bus powered device
	.byte	USB_CFG_MAX_BUS_POWER/2	// Maximum power
		//**** begining of inlined Interface Descriptor ****
	.byte	0x09										// 0x09 bytes
	.byte	USBDESCR_INTERFACE			// Interface Descriptor
	.byte	0x00										// Interface #0
	.byte	0x00										// Alternate setting
	.byte	0x01										// Number of endpoints = 1
	.byte	0x03, 0x00, 0x00				// Class3 (HID), Subclass0 (noclass), Protocol0 (noprotocol)
	.byte	0x00										// Description of this interface = none
		//**** begining of inlined HID Descriptor ****
	.byte	0x09										// 0x09 bytes
	.byte	USBDESCR_HID						// HID Descriptor
	.byte	0x01, 0x01							// HID Specification Version = Version 1.1
	.byte	0x00										// Country code = 0 (none)
	.byte	0x01										// Number of HID class descriptors = 1
	.byte	USBDESCR_HID_REPORT		// Report Descriptor
	.byte	REPORTDESC_SIZE & 0xff,	REPORTDESC_SIZE >> 8 // 
		//**** begining of inlined Endpoint Descriptor ****
	.byte	0x07										// 0x07 bytes
	.byte	USBDESCR_ENDPOINT				// Endpoint Descriptor
	.byte	0x81,	0x03							// Interrupt-IN1 Endpoint
	.byte	0x08, 0x00							// Maximum packed size = 0x08 bytes
	.byte	0x0a										// Polling interval = 0x0a ms
		//**** begining of inlined Endpoint Descriptor ****
	.byte	0x07										// 0x07 bytes
	.byte	USBDESCR_ENDPOINT				// Endpoint Descriptor
	.byte	0x01,	0x03							// Interrupt-OUT1 Endpoint
	.byte	0x08,	0x00							// Maximum packed size = 0x08 bytes
	.byte	0x0a										// Polling interval = 0x0a ms
configurationDescriptor_End:

#define	CONFIGDESC_SIZE	(configurationDescriptor_End - configurationDescriptor)

/**
 * @section 変数
 */
	.section	.data
	
	/**
	 * @brief Bootloaderの情報
	 */
	blInfo:
		.byte	0x1e, 0x93, 0x08
		.byte	SPM_PAGESIZE
		.word	FLASHEND
	blInfo_End:

	/**
	 * @brief USBドライバから呼び出される関数のテーブル
	 */
functionTable:
	.word	bootUsbDescriptor
	.word	bootUsbSetup
	.word	bootUsbRead
	.word	bootUsbWrite
functionTable_End:


	.section .bss
exitBootloader:	.byte	0
currentAddress:	.byte	0
bytesRemaining:	.byte	0
buffer:					.space SPM_PAGESIZE + 2 + 1

/**
 * @section コード
 */
	.section	.text
	
	// uint8_t			bootUsbDescriptor(usbRequest_t *rq);
	/**
	 * @brief 要求されたディスクリプタを返す
	 */
	.func bootUsbDescriptor
bootUsbDescriptor:
	movw	ZL, r24
	ldd		r24, Z+3
	clr		r25
	ldi		ZL, lo8(usbMsgPtr)
	ldi		ZH, hi8(usbMsgPtr)
	cpi		r24, USBDESCR_DEVICE
	brne	1f
	// USBDESCR_DEVICE
	ldi		r24, lo8(deviceDescriptor)	// usbMsgPtr = deviceDescriptor;
	st		Z+, r24											//
	ldi		r24, hi8(deviceDescriptor)	//
	st		Z, r24											// /
	ldi		r24, DEVICEDESC_SIZE				// return sizeof(deviceDescriptor);
	ret																// /
1:
	cpi		r24, USBDESCR_CONFIG
	brne	2f
	// USBDESCR_CONFIG
	ldi		r24, lo8(configurationDescriptor)
	st		Z+, r24
	ldi		r24, hi8(configurationDescriptor)
	st		Z, r24
	ldi		r24, CONFIGDESC_SIZE
	ret
2:
	cpi		r24, USBDESCR_CONFIG
	brne	3f
	// USBDESCR_HID_REPORT
	ldi		r24, lo8(hidReportDescriptor)
	st		Z+, r24
	ldi		r24, hi8(hidReportDescriptor)
	st		Z, r24
	ldi		r24, REPORTDESC_SIZE
	ret
3:
	clr		r24
	ret
	.endfunc
	
	// usbMsgLen_t	bootUsbSetup(uint8_t data[8]);
	.func bootUsbSetup
bootUsbSetup:
	movw	ZL, r24
	ldd		r24, Z+1
	andi	r24, USBRQ_TYPE_MASK
	cpi		r24, USBRQ_TYPE_CLASS
	brne	2f
	ld		r24, Z
	cpi		r24, USBRQ_HID_GET_REPORT
	brne	1f
	// USBRQ_HID_GET_REPORT
	ldi		r30, lo8(usbMsgPtr)
	ldi		r31, hi8(usbMsgPtr)
	ldi		r24, lo8(blInfo)
	ldi		r25, hi8(blInfo)
	std		Z+1, r24
	std		Z+2, r25
	clr		r25
	ldi		r24, BLINFO_SIZE
	ret
1:
	cpi		r24, USBRQ_HID_SET_REPORT
	brne	2f
	// USBRQ_HID_SET_REPORT
	ldd		r24, Z+6
	sts		bytesRemaining, r24
	clr		r24
	sts		currentAddress, r24
	clr		r25
	ldi		r24, 0xff	// USB_NO_MSG = (usbMsgLen_t) - 1
	ret
2:
	clr		r25
	mov		r24, r25
	ret
	.endfunc

	.func bootUsbRead
bootUsbRead:
	clr		r25
	ldi		r24, 1
	ret
	.endfunc
	
	// uint8_t   bootUsbWrite(uint8_t *data, uint8_t len)
	.func	bootUsbWrite
bootUsbWrite:
	push	YL
	push	YH
	lds		r20, bytesRemaining
	cpi		r20, 0
	brne	1f
	ldi		r24, 1
	rjmp	9f
1:
	cp		r20, r22
	brsh	2f
	mov		r22, r20
2:
	//	if( currentAddress == 0 && data[0] == 1 )
	//	{
	//		exitBootloader = 1;
	//		return 0;
	//	}
	lds		r21, currentAddress
	cpi		r21, 0
	brne	3f
	movw	ZL, r24	// Z <- data
	ld		r19, Z
	cpi		r19, 1
	brne	3f
	sts		exitBootloader, r19
	clr		r24
	clr		r25
	rjmp	9f
3:
	// memcpy(buffer + currentAddress, data, len);
	ldi		YL, lo8(buffer)			// Y <- buffer + currentAddress
	ldi		YH, hi8(buffer)			// 
	add		YL, r21							//
	adc		YH, r1							// /
	sub		r21, r22						// currentAddress -= len
	sts		currentAddress, r21	// /
	sub		r20, r22						// bytesRemaining -= len
	sts		bytesRemaining, r20	// /
4:
	ld		r21, Z+		// while(len--) *(Z++) = *(Y++);
	st		Y+, r21		//
	dec		r22				//
	brne	4b				// /
	
	cpi		r20, 0
	brne	9f
	
	ldi		YL, lo8(buffer + 1)
	ldi		YH, lo8(buffer + 1)
	ldd		ZL, Y+0
	ldd		ZH, Y+1
	ldi		r20, 0b00000011
	cli
	sts		SPMCSR, r20
	spm
	sei
5:
	lds		r20, SPMCSR
	andi	r20, _BV(SPMEN)
	brne	5b
	
	ldi		r20, 0b00000001
	sts		SPMCSR, r20
	ldi		r20, SPM_PAGESIZE
	adiw	YL, 2			// Y <- buffer + 3
6:
	ld		r0, Y+
	ld		r1, Y+
	spm
	adiw	ZL, 1
	dec		r20
	brne	6b
	
	clr		r1
	subi	ZL, SPM_PAGESIZE
	sbc		ZH, r1
	ldi		r20, 0b00000101
	cli
	sts		SPMCSR, r20
	spm
	sei
9:
	pop		YH
	pop		YL
	.endfunc

	.global usbFunctionDescriptor
usbFunctionDescriptor:
	lds		ZL, functionTable + 0
	lds		ZH, functionTable + 1
	ijmp
.global usbFunctionSetup
usbFunctionSetup:
	lds		ZL, functionTable + 2
	lds		ZH, functionTable + 3
	ijmp
.global usbFunctionRead
usbFunctionRead:
	lds		ZL, functionTable + 4
	lds		ZH, functionTable + 5
	ijmp
.global usbFunctionWrite
usbFunctionWrite:
	lds		ZL, functionTable + 6
	lds		ZH, functionTable + 7
	ijmp

	.global	main
	.func		main
main:
	ldi		r25, _BV(IVCE)
	out		_SFR_IO_ADDR(MCUCR), r25
	ldi		r25, _BV(IVSEL)
	out		_SFR_IO_ADDR(MCUCR), r25
	//	Enable Watchdog Timer
	ldi		r25, 0b00001110
	sts		WDTCSR, r25
	//	Initialize USB Driver
	rcall	usbInit
	//	Force re-enumeration
	
	sbi		_SFR_IO_ADDR(USBDDR), USBMINUS	// Disconnect
	rcall	wait
	cbi		_SFR_IO_ADDR(USBDDR), USBMINUS	// Connect


	//	Main event loop
	sei
2:
	lds		r25, exitBootloader
	cpi		r25, 0
	brne	4f
	clr		r25
3:
	wdr
	push	r25
	rcall	usbPoll
	pop		r25
	dec		r25
	brne	3b
	rjmp	2b

	// Exit from bootloader
4:
	cli
	sbi		_SFR_IO_ADDR(USBDDR), USBMINUS	// Disconnect
	sts		WDTCSR, r1
	rcall	wait
	// Enable RWW Section
	ldi		r25, 0b00010001
	sts		SPMCSR, r25
	spm
	// Move interrupt vector to beginning of flash
	ldi		r25, _BV(IVCE)
	out		_SFR_IO_ADDR(MCUCR), r25
	out		_SFR_IO_ADDR(MCUCR), r1
	// Reset
	jmp		0

	.endfunc

	.func	wait
wait:
	ldi		r26, 255
1:
	ldi		r25, 255
2:
	dec		r25
	brne	2b

	dec		r26
	brne	1b
	ret
	.endfunc
