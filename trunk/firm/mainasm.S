#include <avr/io.h>
#include "usbdrv.h"
	.section	.text

#define	BLINFO_SIZE	6
#define	REPORTDESC_SIZE	30
#define	DEVICEDESC_SIZE	16
#define	CONFIGDESC_SIZE 41

#ifdef	USE_ASM_DESCRIPTOR
// uint8_t			bootUsbDescriptor(usbRequest_t *rq);
	/**
	 * @brief 要求されたディスクリプタを返す
	 */
	 .global bootUsbDescriptor
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
	ldi		r24, lo8(_deviceDescriptor)	// usbMsgPtr = deviceDescriptor;
	std		Z+0, r24										//
	ldi		r24, hi8(_deviceDescriptor)	//
	std		Z+1, r24										// /
	ldi		r24, DEVICEDESC_SIZE				// return sizeof(deviceDescriptor);
	ret																// /
1:
	cpi		r24, USBDESCR_CONFIG
	brne	2f
	// USBDESCR_CONFIG
	ldi		r24, lo8(_configurationDescriptor)
	std		Z+0, r24
	ldi		r24, hi8(_configurationDescriptor)
	std		Z+1, r24
	ldi		r24, CONFIGDESC_SIZE
	ret
2:
	cpi		r24, USBDESCR_CONFIG
	brne	3f
	// USBDESCR_HID_REPORT
	ldi		r24, lo8(_bootHidReportDescriptor)
	std		Z+0, r24
	ldi		r24, hi8(_bootHidReportDescriptor)
	std		Z+1, r24
	ldi		r24, REPORTDESC_SIZE
	ret
3:
	clr		r24
	ret
	.endfunc
#endif	//USE_ASM_DESCRIPTOR

#ifdef	USE_ASM_SETUP
	.global bootUsbSetup
	.func bootUsbSetup
bootUsbSetup:
	mov		r30, r24
	mov		r31, r25
	adiw	ZL, 1
	clr		r25
	ld		r24, Z
	andi	r24, USBRQ_TYPE_MASK
	cpi		r24, USBRQ_TYPE_CLASS
	brne	2f
	ld		r24, -Z
	cpi		r24, USBRQ_HID_GET_REPORT
	brne	1f
	// USBRQ_HID_GET_REPORT
	ldi		r24, lo8(_blinfo)
	sts		usbMsgPtr + 0, r24
	ldi		r24, hi8(_blinfo)
	sts		usbMsgPtr + 1, r24
	ldi		r24, BLINFO_SIZE
	rjmp	2f
1:
	cpi		r24, USBRQ_HID_SET_REPORT
	brne	2f
	// USBRQ_HID_SET_REPORT
	ldd		r24, Z+6
	sts		_bytesRemaining, r24
	clr		r24
	sts		_currentAddress, r24
	ldi		r24, 0xff	// USB_NO_MSG = (usbMsgLen_t) - 1
2:
	clr		r24
	ret
	.endfunc

#endif

#ifdef	USE_ASM_MAIN
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
	lds		r25, _exitBootloader
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
	clr		r30
	mov		r31, r30
	ijmp

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
#endif
