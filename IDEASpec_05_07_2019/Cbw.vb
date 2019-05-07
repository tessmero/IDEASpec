Option Strict Off
Option Explicit On
Module Module2
	'*********************************************************************
	'
	'  File: CBW.BAS
	'
	'  (c) Copyright 1996 - 2004 by Measurement Computing Corp.
	'      All Rights Reserved
	'
	' This file contains the Visual BASIC declarations for all Measurement
	' Computing library commands.   This file should be included in the
	' project as a Global Module
	'
	'***********************************************************************
	
	' Current Revision Number
	Public Const CURRENTREVNUM As Double = 5.51
	' Error Codes
	Public Const NOERRORS As Short = 0
	Public Const BADBOARD As Short = 1
	Public Const DEADDIGITALDEV As Short = 2
	Public Const DEADCOUNTERDEV As Short = 3
	Public Const DEADDADEV As Short = 4
	Public Const DEADADDEV As Short = 5
	Public Const NOTDIGITALCONF As Short = 6
	Public Const NOTCOUNTERCONF As Short = 7
	Public Const NOTDACONF As Short = 8
	Public Const NOTADCONF As Short = 9
	Public Const NOTMUXCONF As Short = 10
	Public Const BADPORTNUM As Short = 11
	Public Const BADCOUNTERDEVNUM As Short = 12
	Public Const BADDADEVNUM As Short = 13
	Public Const BADSAMPLEMODE As Short = 14
	Public Const BADINT As Short = 15
	Public Const BADADCHAN As Short = 16
	Public Const BADCOUNT As Short = 17
	Public Const BADCNTRCONFIG As Short = 18
	Public Const BADDAVAL As Short = 19
	Public Const BADDACHAN As Short = 20
	Public Const ALREADYACTIVE As Short = 22
	Public Const PAGEOVERRUN As Short = 23
	Public Const BADRATE As Short = 24
	Public Const COMPATMODE As Short = 25
	Public Const TRIGSTATE As Short = 26
	Public Const ADSTATUSHUNG As Short = 27
	Public Const TOOFEW As Short = 28
	Public Const OVERRUN As Short = 29
	Public Const BADRANGE As Short = 30
	Public Const NOPROGGAIN As Short = 31
	Public Const BADFILENAME As Short = 32
	Public Const DISKISFULL As Short = 33
	Public Const COMPATWARN As Short = 34
	Public Const BADPOINTER As Short = 35
	Public Const TOOMANYGAINS As Short = 36
	Public Const RATEWARNING As Short = 37
	Public Const CONVERTDMA As Short = 38
	Public Const DTCONNECTERR As Short = 39
	Public Const FORECONTINUOUS As Short = 40
	Public Const BADBOARDTYPE As Short = 41
	Public Const WRONGDIGCONFIG As Short = 42
	Public Const NOTCONFIGURABLE As Short = 43
	Public Const BADPORTCONFIG As Short = 44
	Public Const BADFIRSTPOINT As Short = 45
	Public Const ENDOFFILE As Short = 46
	Public Const NOT8254CTR As Short = 47
	Public Const NOT9513CTR As Short = 48
	Public Const BADTRIGTYPE As Short = 49
	Public Const BADTRIGVALUE As Short = 50
	Public Const BADOPTION As Short = 52
	Public Const BADPRETRIGCOUNT As Short = 53
	Public Const BADDIVIDER As Short = 55
	Public Const BADSOURCE As Short = 56
	Public Const BADCOMPARE As Short = 57
	Public Const BADTIMEOFDAY As Short = 58
	Public Const BADGATEINTERVAL As Short = 59
	Public Const BADGATECNTRL As Short = 60
	Public Const BADCOUNTEREDGE As Short = 61
	Public Const BADSPCLGATE As Short = 62
	Public Const BADRELOAD As Short = 63
	Public Const BADRECYCLEFLAG As Short = 64
	Public Const BADBCDFLAG As Short = 65
	Public Const BADDIRECTION As Short = 66
	Public Const BADOUTCONTROL As Short = 67
	Public Const BADBITNUMBER As Short = 68
	Public Const NONEENABLED As Short = 69
	Public Const BADCTRCONTROL As Short = 70
	Public Const BADEXPCHAN As Short = 71
	Public Const WRONGADRANGE As Short = 72
	Public Const OUTOFRANGE As Short = 73
	Public Const BADTEMPSCALE As Short = 74
	Public Const BADERRCODE As Short = 75
	Public Const NOQUEUE As Short = 76
	Public Const CONTINUOUSCOUNT As Short = 77
	Public Const UNDERRUN As Short = 78
	Public Const BADMEMMODE As Short = 79
	Public Const FREQOVERRUN As Short = 80
	Public Const NOCJCCHAN As Short = 81
	Public Const BADCHIPNUM As Short = 82
	Public Const DIGNOTENABLED As Short = 83
	Public Const CONVERT16BITS As Short = 84
	Public Const NOMEMBOARD As Short = 85
	Public Const DTACTIVE As Short = 86
	Public Const NOTMEMCONF As Short = 87
	Public Const ODDCHAN As Short = 88
	Public Const CTRNOINIT As Short = 89
	Public Const NOT8536CTR As Short = 90
	Public Const FREERUNNING As Short = 91
	Public Const INTERRUPTED As Short = 92
	Public Const NOSELECTORS As Short = 93
	Public Const NOBURSTMODE As Short = 94
	Public Const NOTWINDOWSFUNC As Short = 95
	Public Const NOTSIMULCONF As Short = 96
	Public Const EVENODDMISMATCH As Short = 97
	Public Const M1RATEWARNING As Short = 98
	Public Const NOTRS485 As Short = 99
	Public Const NOTDOSFUNC As Short = 100
	Public Const RANGEMISMATCH As Short = 101
	Public Const CLOCKTOOSLOW As Short = 102
	Public Const BADCALFACTORS As Short = 103
	Public Const BADCONFIGTYPE As Short = 104
	Public Const BADCONFIGITEM As Short = 105
	Public Const NOPCMCIABOARD As Short = 106
	Public Const NOBACKGROUND As Short = 107
	Public Const STRINGTOOSHORT As Short = 108
	Public Const CONVERTEXTMEM As Short = 109
	Public Const BADEUADD As Short = 110
	Public Const DAS16JRRATEWARNING As Short = 111
	Public Const DAS08TOOLOWRATE As Short = 112
	Public Const AMBIGSENSORTYPE As Short = 114 ' more than one sensor type defined for EXP-GP (obsolete)
	Public Const AMBIGSENSORONGP As Short = 114 ' more than one sensor type defined for EXP-GP
	Public Const NOSENSORTYPEONGP As Short = 115 ' no sensor type defined for EXP-GP
	Public Const NOCONVERSIONNEEDED As Short = 116 ' 12 bit board without chan tags - converted in ISR
	Public Const NOEXTCONTINUOUS As Short = 117
	Public Const INVALIDPRETRIGCONVERT As Short = 118 ' cbConvertPretirg called after cbPretrigScan failed
	Public Const BADCTRREG As Short = 119 ' Bad arg to CLoad for 9513 }
	Public Const BADTRIGTHRESHOLD As Short = 120 ' Invalid trigger threshold specified in cbSetTrigger }
	Public Const BADPCMSLOTREF As Short = 121 ' Bad PCM Card slot reference
	Public Const AMBIGPCMSLOTREF As Short = 122 ' Ambiguous PCM Card slot reference
	Public Const BADSENSORTYPE As Short = 123 ' Bad sensor type selected in Instacal
	Public Const DELBOARDNOTEXIST As Short = 124 ' tried to delete board number which doesn't exist
	Public Const NOBOARDNAMEFILE As Short = 125 ' board name file not found
	Public Const CFGFILENOTFOUND As Short = 126 ' configuration file not found
	Public Const NOVDDINSTALLED As Short = 127 ' CBUL.386 device driver not installed
	Public Const NOWINDOWSMEMORY As Short = 128 ' No Windows memory available
	Public Const OUTOFDOSMEMORY As Short = 129 ' No DOS memory available
	Public Const OBSOLETEOPTION As Short = 130 ' Option on longer supporeted in cbGetConfig/cbSetConfig
	Public Const NOPCMREGKEY As Short = 131 ' No registry entry for this PCMCIA board
	Public Const NOCBUL32SYS As Short = 132 ' CBUL32.SYS device driver not installed
	Public Const NODMAMEMEMORY As Short = 133 ' No memory for device driver's DMA buffer
	Public Const IRQNOTAVAILABLE As Short = 134 ' IRQ in use by another device
	Public Const NOT7266CTR As Short = 135 ' This board does not have an LS7266 counter /
	Public Const BADQUADRATURE As Short = 136 ' Invalid quadrature specified
	Public Const BADCOUNTMODE As Short = 137 ' Invalid counting mode specified
	Public Const BADENCODING As Short = 138 ' Invalid data encoding specified
	Public Const BADINDEXMODE As Short = 139 ' Invalid index mode specified
	Public Const BADINVERTINDEX As Short = 140 ' Invalid invert index specified
	Public Const BADFLAGPINS As Short = 141 ' Invalid flag pins specified
	Public Const NOCTRSTATUS As Short = 142 ' This board does not support cbCStatus()
	Public Const NOGATEALLOWED As Short = 143 ' Gating and indexing not allowed simultaneously
	Public Const NOINDEXALLOWED As Short = 144 ' Indexing not allowed in non-quadratue mode
	Public Const OPENCONNECTION As Short = 145 ' Temperature input has open connection
	Public Const BMCONTINUOUSCOUNT As Short = 146 ' Count must be integer multiple of packetsize for recycle mode.
	Public Const BADCALLBACKFUNC As Short = 147 ' Invalid pointer to callback function passed as arg
	Public Const MBUSINUSE As Short = 148 ' Metrabus in use
	Public Const MBUSNOCTLR As Short = 149 ' MetraBus I/O card has no configured controller card
	Public Const BADEVENTTYPE As Short = 150 ' Invalid event type specified for this board.
	Public Const ALREADYENABLED As Short = 151 ' An event handler has already been enabled for this event type
	Public Const BADEVENTSIZE As Short = 152 ' Invalid event count specified.
	Public Const CANTINSTALLEVENT As Short = 153 ' Unable to install event handler
	Public Const BADBUFFERSIZE As Short = 154 ' Buffer is too small for operation
	Public Const BADAIMODE As Short = 155 ' Invalid Analog Input Mode (RSE, NRSE, DIFF)
	Public Const BADSIGNAL As Short = 156 ' Invalid signal type specified.
	Public Const BADCONNECTION As Short = 157 ' Invalid connection specified.
	Public Const BADINDEX As Short = 158 ' Invalid index specified, or reached end of internal connection list.
	Public Const NOCONNECTION As Short = 159 ' No connection is assigned to specified signal.
	Public Const BADBURSTIOCOUNT As Short = 160 ' Count cannot be greater than the FIFO size for BURSTIO mode
	Public Const DEADDEV As Short = 161 ' Device has stopped responding. Please check connections.
	
	Public Const INTERNALERR As Short = 200 ' 200-299 = 16 bit library internal errors
	Public Const CANT_LOCK_DMA_BUF As Short = 201 ' DMA buffer could not be locked
	Public Const DMA_IN_USE As Short = 202 ' DMA already controlled by another device
	Public Const BAD_MEM_HANDLE As Short = 203 ' Invalid Windows memory handle
	
	Public Const INTERNALERR32 As Short = 300 ' 300-399 = 32 bit library internal errors
	Public Const CFG_FILE_READ_FAILURE As Short = 304 ' Error reading from configuration file
	Public Const CFG_FILE_WRITE_FAILURE As Short = 305 ' Error writing to configuration file
	Public Const CFGFILE_CANT_OPEN As Short = 308 ' Cannot open configuration file
	Public Const BAD_RTD_CONVERSION As Short = 325 ' Overflow of RTD conversion
	Public Const NO_PCI_BIOS As Short = 326 ' PCI BIOS not present on the PC
	Public Const BAD_PCI_INDEX As Short = 327 ' Specified PCI board not detected
	Public Const NO_PCI_BOARD As Short = 328 ' Specified PCI board not detected
	Public Const CANT_INSTALL_INT As Short = 334 ' Cannot install interrupt handler. IRQ already in use
	
	Public Const PCMCIAERRS As Short = 400 ' 400-499 = PCMCIA errors
	
	' These are the most commonly occurring remapped DOS error codes
	Public Const DOSBADFUNC As Short = 501
	Public Const DOSFILENOTFOUND As Short = 502
	Public Const DOSPATHNOTFOUND As Short = 503
	Public Const DOSNOHANDLES As Short = 504
	Public Const DOSACCESSDENIED As Short = 505
	Public Const DOSINVALIDHANDLE As Short = 506
	Public Const DOSNOMEMORY As Short = 507
	Public Const DOSBADDRIVE As Short = 515
	Public Const DOSTOOMANYFILES As Short = 518
	Public Const DOSWRITEPROTECT As Short = 519
	Public Const DOSDRIVENOTREADY As Short = 521
	Public Const DOSSEEKERROR As Short = 525
	Public Const DOSWRITEFAULT As Short = 529
	Public Const DOSREADFAULT As Short = 530
	Public Const DOSGENERALFAULT As Short = 531
	
	Public Const WIN_CANNOT_ENABLE_INT As Short = 603 ' Cannot enable interrupt. IRQ already in use
	Public Const WIN_CANNOT_DISABLE_INT As Short = 605 ' Cannot disable interrupts
	Public Const WIN_CANT_PAGE_LOCK_BUFFER As Short = 606 ' Insufficient memory to page lock data buffer
	Public Const NO_PCM_CARD As Short = 630 ' PCM card not detected
	
	
	' Types of operations or functions
	Public Const AIFUNCTION As Short = 1 ' Analog Input Function
	Public Const AOFUNCTION As Short = 2 ' Analog Output Function
	Public Const DIFUNCTION As Short = 3 ' Digital Input Function
	Public Const DOFUNCTION As Short = 4 ' Digital Output Function
	Public Const CTRFUNCTION As Short = 5 ' Counter Function
	
	
	Public Const NotUsed As Short = -1
	
	' Maximum length of error string
	Public Const ERRSTRLEN As Short = 256
	
	' Maximum length of board name string
	Public Const BOARDNAMELEN As Short = 25
	
	
	' Status values
	Public Const IDLE As Short = 0
	Public Const RUNNING As Short = 1
	
	
	Public Const CBENABLED As Short = 1
	Public Const CBDISABLED As Short = 0
	
	Public Const UPDATEIMMEDIATE As Short = 0
	Public Const UPDATEONCOMMAND As Short = 1
	
	' Types of error reporting
	Public Const DONTPRINT As Short = 0
	Public Const PRINTWARNINGS As Short = 1
	Public Const PRINTFATAL As Short = 2
	Public Const PRINTALL As Short = 3
	
	' Types of error handling
	Public Const DONTSTOP As Short = 0
	Public Const STOPFATAL As Short = 1
	Public Const STOPALL As Short = 2
	
	' Types of digital input ports
	Public Const DIGITALOUT As Short = 1
	Public Const DIGITALIN As Short = 2
	
	' DT Modes for cbSetDTMode ()
	Public Const DTIN As Short = 0
	Public Const DTOUT As Short = 2
	
	Public Const FROMHERE As Short = -1
	Public Const GETFIRST As Short = -2
	Public Const GETNEXT As Short = -3
	
	'  Temperature scales
	Public Const CELSIUS As Short = 0
	Public Const FAHRENHEIT As Short = 1
	Public Const KELVIN As Short = 2
	Public Const VOLTS As Short = 4
	
	' Types of digital I/O Ports
	Public Const AUXPORT As Short = 1
	Public Const FIRSTPORTA As Short = 10
	Public Const FIRSTPORTB As Short = 11
	Public Const FIRSTPORTCL As Short = 12
	Public Const FIRSTPORTCH As Short = 13
	Public Const SECONDPORTA As Short = 14
	Public Const SECONDPORTB As Short = 15
	Public Const SECONDPORTCL As Short = 16
	Public Const SECONDPORTCH As Short = 17
	Public Const THIRDPORTA As Short = 18
	Public Const THIRDPORTB As Short = 19
	Public Const THIRDPORTCL As Short = 20
	Public Const THIRDPORTCH As Short = 21
	Public Const FOURTHPORTA As Short = 22
	Public Const FOURTHPORTB As Short = 23
	Public Const FOURTHPORTCL As Short = 24
	Public Const FOURTHPORTCH As Short = 25
	Public Const FIFTHPORTA As Short = 26
	Public Const FIFTHPORTB As Short = 27
	Public Const FIFTHPORTCL As Short = 28
	Public Const FIFTHPORTCH As Short = 29
	Public Const SIXTHPORTA As Short = 30
	Public Const SIXTHPORTB As Short = 31
	Public Const SIXTHPORTCL As Short = 32
	Public Const SIXTHPORTCH As Short = 33
	Public Const SEVENTHPORTA As Short = 34
	Public Const SEVENTHPORTB As Short = 35
	Public Const SEVENTHPORTCL As Short = 36
	Public Const SEVENTHPORTCH As Short = 37
	Public Const EIGHTHPORTA As Short = 38
	Public Const EIGHTHPORTB As Short = 39
	Public Const EIGHTHPORTCL As Short = 40
	Public Const EIGHTHPORTCH As Short = 41
	
	
	' Selectable A/D Ranges codes
	Public Const BIP20VOLTS As Short = 15 ' Bipolar Ranges (-20 to +20 Volts)
	Public Const BIP10VOLTS As Short = 1 ' -10 to +10 Volts
	Public Const BIP5VOLTS As Short = 0 ' -5 to +5 Volts
	Public Const BIP4VOLTS As Short = 16 ' -4 to +4 Volts
	Public Const BIP2PT5VOLTS As Short = 2 ' -2.5 to +2.5 Volts
	Public Const BIP2VOLTS As Short = 14 ' -2 to +2 Volts
	Public Const BIP1PT25VOLTS As Short = 3 ' -1.25 to +1.25 Volts
	Public Const BIP1VOLTS As Short = 4 ' -1 to +1 Volt
	Public Const BIPPT625VOLTS As Short = 5 ' -0.625 to + 0.625 Volt
	Public Const BIPPT5VOLTS As Short = 6 ' -0.5 to +0.5 Volt
	Public Const BIPPT25VOLTS As Short = 12 ' -0.25 to +0.25 Volt
	Public Const BIPPT2VOLTS As Short = 13 ' -0.2 to +0.2 Volt
	Public Const BIPPT1VOLTS As Short = 7 ' -0.1 to +0.1 Volt
	Public Const BIPPT05VOLTS As Short = 8 ' -0.05 to +0.05 Volt
	Public Const BIPPT01VOLTS As Short = 9 ' -0.01 to +0.01 Volt
	Public Const BIPPT005VOLTS As Short = 10 ' -0.005 to +0.005 Volt
	Public Const BIP1PT67VOLTS As Short = 11 ' -1.67 to +1.67 Volts
	
	Public Const UNI10VOLTS As Short = 100 ' Unipolar Ranges (0 to 10 Volts)
	Public Const UNI5VOLTS As Short = 101 ' 0 to 5 Volts
	Public Const UNI2PT5VOLTS As Short = 102 ' 0 to 2.5 Volts
	Public Const UNI2VOLTS As Short = 103 ' 0 to 2 Volts
	Public Const UNI1PT67VOLTS As Short = 109 ' 0 to 1.67 Volts
	Public Const UNI1PT25VOLTS As Short = 104 ' 0 to 1.25 Volts
	Public Const UNI1VOLTS As Short = 105 ' 0 to 1 Volt
	Public Const UNIPT5VOLTS As Short = 110 ' 0 to 0.5 Volt
	Public Const UNIPT25VOLTS As Short = 111 ' 0 to 0.25 Volt
	Public Const UNIPT2VOLTS As Short = 112 ' 0 to 0.2 Volt
	Public Const UNIPT1VOLTS As Short = 106 ' 0 to 0.1 Volt
	Public Const UNIPT05VOLTS As Short = 113 ' 0 to 0.05 Volt
	Public Const UNIPT02VOLTS As Short = 108 ' 0 to 0.02 Volt
	Public Const UNIPT01VOLTS As Short = 107 ' 0 to 0.01 Volt
	
	
	
	Public Const MA4TO20 As Short = 200 ' Current Ranges (4 to 20 mA )
	Public Const MA2to10 As Short = 201 ' 2 to 10 mA
	Public Const MA1TO5 As Short = 202 ' 1 to 5 mA
	Public Const MAPT5TO2PT5 As Short = 203 ' 0.5 to 2.5 mA
	Public Const MA0TO20 As Short = 204 ' 0 to 20 mA
	
	Public Const UNIPOLAR As Short = 300 ' Unipolar range
	Public Const BIPOLAR As Short = 301 ' Bipolar range
	
	' Types of D/A
	Public Const ADDA1 As Short = 0
	Public Const ADDA2 As Short = 1
	
	' 8536 counter output 1 control
	Public Const NOTLINKED As Short = 0
	Public Const GATECTR2 As Short = 1
	Public Const TRIGCTR2 As Short = 2
	Public Const INCTR2 As Short = 3
	
	' Types of 8254 Counter configurations
	Public Const HIGHONLASTCOUNT As Short = 0
	Public Const ONESHOT As Short = 1
	Public Const RATEGENERATOR As Short = 2
	Public Const SQUAREWAVE As Short = 3
	Public Const SOFTWARESTROBE As Short = 4
	Public Const HARDWARESTROBE As Short = 5
	
	' Where to reload from for 9513 counters
	Public Const LOADREG As Short = 0
	Public Const LOADANDHOLDREG As Short = 1
	
	' Counter recycle modes
	Public Const ONETIME As Short = 0
	Public Const RECYCLE As Short = 1
	
	' Direction of counting for 9513 counters
	Public Const COUNTDOWN As Short = 0
	Public Const COUNTUP As Short = 1
	
	' Types of count detection for 9513 counters
	Public Const POSITIVEEDGE As Short = 0
	Public Const NEGATIVEEDGE As Short = 1
	
	' Counter output control
	Public Const ALWAYSLOW As Short = 0
	Public Const HIGHPULSEONTC As Short = 1
	Public Const TOGGLEONTC As Short = 2
	Public Const DISCONNECTED As Short = 4
	Public Const LOWPULSEONTC As Short = 5
	Public Const HIGHUNTILTC As Short = 6
	
	' Counter input sources
	Public Const TCPREVCTR As Short = 0
	Public Const CTRINPUT1 As Short = 1
	Public Const CTRINPUT2 As Short = 2
	Public Const CTRINPUT3 As Short = 3
	Public Const CTRINPUT4 As Short = 4
	Public Const CTRINPUT5 As Short = 5
	Public Const GATE1 As Short = 6
	Public Const GATE2 As Short = 7
	Public Const GATE3 As Short = 8
	Public Const GATE4 As Short = 9
	Public Const GATE5 As Short = 10
	Public Const FREQ1 As Short = 11
	Public Const FREQ2 As Short = 12
	Public Const FREQ3 As Short = 13
	Public Const FREQ4 As Short = 14
	Public Const FREQ5 As Short = 15
	Public Const CTRINPUT6 As Short = 101
	Public Const CTRINPUT7 As Short = 102
	Public Const CTRINPUT8 As Short = 103
	Public Const CTRINPUT9 As Short = 104
	Public Const CTRINPUT10 As Short = 105
	Public Const GATE6 As Short = 106
	Public Const GATE7 As Short = 107
	Public Const GATE8 As Short = 108
	Public Const GATE9 As Short = 109
	Public Const GATE10 As Short = 110
	Public Const FREQ6 As Short = 111
	Public Const FREQ7 As Short = 112
	Public Const FREQ8 As Short = 113
	Public Const FREQ9 As Short = 114
	Public Const FREQ10 As Short = 115
	
	Public Const CTRINPUT11 As Short = 201
	Public Const CTRINPUT12 As Short = 202
	Public Const CTRINPUT13 As Short = 203
	Public Const CTRINPUT14 As Short = 204
	Public Const CTRINPUT15 As Short = 205
	Public Const GATE11 As Short = 206
	Public Const GATE12 As Short = 207
	Public Const GATE13 As Short = 208
	Public Const GATE14 As Short = 209
	Public Const GATE15 As Short = 210
	Public Const FREQ11 As Short = 211
	Public Const FREQ12 As Short = 212
	Public Const FREQ13 As Short = 213
	Public Const FREQ14 As Short = 214
	Public Const FREQ15 As Short = 215
	Public Const CTRINPUT16 As Short = 301
	Public Const CTRINPUT17 As Short = 302
	Public Const CTRINPUT18 As Short = 303
	Public Const CTRINPUT19 As Short = 304
	Public Const CTRINPUT20 As Short = 305
	Public Const GATE16 As Short = 306
	Public Const GATE17 As Short = 307
	Public Const GATE18 As Short = 308
	Public Const GATE19 As Short = 309
	Public Const GATE20 As Short = 310
	Public Const FREQ16 As Short = 311
	Public Const FREQ17 As Short = 312
	Public Const FREQ18 As Short = 313
	Public Const FREQ19 As Short = 314
	Public Const FREQ20 As Short = 315
	
	' 9513 Counter registers
	Public Const LOADREG1 As Short = 1
	Public Const LOADREG2 As Short = 2
	Public Const LOADREG3 As Short = 3
	Public Const LOADREG4 As Short = 4
	Public Const LOADREG5 As Short = 5
	Public Const LOADREG6 As Short = 6
	Public Const LOADREG7 As Short = 7
	Public Const LOADREG8 As Short = 8
	Public Const LOADREG9 As Short = 9
	Public Const LOADREG10 As Short = 10
	
	Public Const LOADREG11 As Short = 11
	Public Const LOADREG12 As Short = 12
	Public Const LOADREG13 As Short = 13
	Public Const LOADREG14 As Short = 14
	Public Const LOADREG15 As Short = 15
	Public Const LOADREG16 As Short = 16
	Public Const LOADREG17 As Short = 17
	Public Const LOADREG18 As Short = 18
	Public Const LOADREG19 As Short = 19
	Public Const LOADREG20 As Short = 20
	
	Public Const HOLDREG1 As Short = 101
	Public Const HOLDREG2 As Short = 102
	Public Const HOLDREG3 As Short = 103
	Public Const HOLDREG4 As Short = 104
	Public Const HOLDREG5 As Short = 105
	Public Const HOLDREG6 As Short = 106
	Public Const HOLDREG7 As Short = 107
	Public Const HOLDREG8 As Short = 108
	Public Const HOLDREG9 As Short = 109
	Public Const HOLDREG10 As Short = 110
	
	Public Const HOLDREG11 As Short = 111
	Public Const HOLDREG12 As Short = 112
	Public Const HOLDREG13 As Short = 113
	Public Const HOLDREG14 As Short = 114
	Public Const HOLDREG15 As Short = 115
	Public Const HOLDREG16 As Short = 116
	Public Const HOLDREG17 As Short = 117
	Public Const HOLDREG18 As Short = 118
	Public Const HOLDREG19 As Short = 119
	Public Const HOLDREG20 As Short = 120
	
	Public Const ALARM1CHIP1 As Short = 201
	Public Const ALARM2CHIP1 As Short = 202
	Public Const ALARM1CHIP2 As Short = 301
	Public Const ALARM2CHIP2 As Short = 302
	Public Const ALARM1CHIP3 As Short = 401
	Public Const ALARM2CHIP3 As Short = 402
	Public Const ALARM1CHIP4 As Short = 501
	Public Const ALARM2CHIP4 As Short = 502
	
	' LS7266 Counter registers
	Public Const COUNT1 As Short = 601
	Public Const COUNT2 As Short = 602
	Public Const COUNT3 As Short = 603
	Public Const COUNT4 As Short = 604
	
	Public Const PRESET1 As Short = 701
	Public Const PRESET2 As Short = 702
	Public Const PRESET3 As Short = 703
	Public Const PRESET4 As Short = 704
	
	Public Const PRESCALER1 As Short = 801
	Public Const PRESCALER2 As Short = 802
	Public Const PRESCALER3 As Short = 803
	Public Const PRESCALER4 As Short = 804
	
	
	'  Counter Gate Control
	Public Const NOGATE As Short = 0
	Public Const AHLTCPREVCTR As Short = 1
	Public Const AHLNEXTGATE As Short = 2
	Public Const AHLPREVGATE As Short = 3
	Public Const AHLGATE As Short = 4
	Public Const ALLGATE As Short = 5
	Public Const AHEGATE As Short = 6
	Public Const ALEGATE As Short = 7
	
	' 7266 Counter Quadrature values
	Public Const NO_QUAD As Short = 0
	Public Const X1_QUAD As Short = 1
	Public Const X2_QUAD As Short = 2
	Public Const X4_QUAD As Short = 4
	
	' 7266 Counter Counting Modes
	Public Const NORMAL_MODE As Short = 0
	Public Const RANGE_LIMIT As Short = 1
	Public Const NO_RECYCLE As Short = 2
	Public Const MODULO_N As Short = 3
	
	' 7266 Counter encodings
	Public Const BCD_ENCODING As Short = 1
	Public Const BINARY_ENCODING As Short = 2
	
	' 7266 Counter Index Modes
	Public Const INDEX_DISABLED As Short = 0
	Public Const LOAD_CTR As Short = 1
	Public Const LOAD_OUT_LATCH As Short = 2
	Public Const RESET_CTR As Short = 3
	
	' 7266 Counter Flag Pins
	Public Const CARRY_BORROW As Short = 1
	Public Const COMPARE_BORROW As Short = 2
	Public Const CARRYBORROW_UPDOWN As Short = 3
	Public Const INDEX_ERROR As Short = 4
	
	' Counter status bits
	Public Const C_UNDERFLOW As Short = &H1s
	Public Const C_OVERFLOW As Short = &H2s
	Public Const C_COMPARE As Short = &H4s
	Public Const C_SIGN As Short = &H8s
	Public Const C_ERROR As Short = &H10s
	Public Const C_UP_DOWN As Short = &H20s
	Public Const C_INDEX As Short = &H40s
	
	
	' Types of triggers
	Public Const TRIGABOVE As Short = 0
	Public Const TRIGBELOW As Short = 1
	Public Const GATENEGHYS As Integer = (TRIGBELOW + 1)
	Public Const GATEPOSHYS As Integer = (TRIGBELOW + 2)
	Public Const GATEABOVE As Integer = (TRIGBELOW + 3)
	Public Const GATEBELOW As Integer = (TRIGBELOW + 4)
	Public Const GATEINWINDOW As Integer = (TRIGBELOW + 5)
	Public Const GATEOUTWINDOW As Integer = (TRIGBELOW + 6)
	Public Const GATEHIGH As Integer = (TRIGBELOW + 7)
	Public Const GATELOW As Integer = (TRIGBELOW + 8)
	Public Const TRIGHIGH As Integer = (TRIGBELOW + 9)
	Public Const TRIGLOW As Integer = (TRIGBELOW + 10)
	Public Const TRIGPOSEDGE As Integer = (TRIGBELOW + 11)
	Public Const TRIGNEGEDGE As Integer = (TRIGBELOW + 12)
	
	
	' Signal I/O Configuration Parameters
	' --Connections
	Public Const AUXIN0 As Integer = &H1s
	Public Const AUXIN1 As Integer = &H2s
	Public Const AUXIN2 As Integer = &H4s
	Public Const AUXIN3 As Integer = &H8s
	Public Const AUXIN4 As Integer = &H10s
	Public Const AUXIN5 As Integer = &H20s
	
	Public Const AUXOUT0 As Integer = &H100s
	Public Const AUXOUT1 As Integer = &H200s
	Public Const AUXOUT2 As Integer = &H400s
	
	Public Const DS_CONNECTOR As Integer = &H1000s
	
	Public Const MAX_CONNECTIONS As Integer = 4 'maximum  number connections per output signal
	
	' --Signal Types
	Public Const ADC_CONVERT As Integer = &H1s
	Public Const ADC_GATE As Integer = &H2s
	Public Const ADC_START_TRIG As Integer = &H4s
	Public Const ADC_STOP_TRIG As Integer = &H8s
	Public Const ADC_TB_SRC As Integer = &H10s
	Public Const ADC_SCANCLK As Integer = &H20
	Public Const ADC_SSH As Integer = &H40
	Public Const ADC_STARTSCAN As Integer = &H80
	Public Const ADC_SCAN_STOP As Integer = &H100
	
	Public Const DAC_UPDATE As Integer = &H200
	Public Const DAC_TB_SRC As Integer = &H400
	Public Const DAC_START_TRIG As Integer = &H800
	
	Public Const SYNC_CLK As Integer = &H1000
	
	Public Const CTR1_CLK As Integer = &H2000
	Public Const CTR2_CLK As Integer = &H4000
	
	Public Const DGND As Integer = &H8000
	
	' -- Signal Direction
	Public Const SIGNAL_IN As Integer = 2
	Public Const SIGNAL_OUT As Integer = 4
	
	Public Const INVERTED As Integer = 1
	Public Const NONINVERTED As Integer = 0
	
	
	
	
	' Types of configuration information
	Public Const GLOBALINFO As Short = 1
	Public Const BOARDINFO As Short = 2
	Public Const DIGITALINFO As Short = 3
	Public Const COUNTERINFO As Short = 4
	Public Const EXPANSIONINFO As Short = 5
	Public Const MISCINFO As Short = 6
	Public Const EXPINFOARRAY As Short = 7
	Public Const MEMINFO As Short = 8
	
	' Types of global configuration information
	Public Const GIVERSION As Short = 36 ' Config file format version number
	Public Const GINUMBOARDS As Short = 38 ' Maximum number of boards
	Public Const GINUMEXPBOARDS As Short = 40 ' Maximum number of expansion boards
	
	' Types of board configuration information
	Public Const BIBASEADR As Short = 0 ' Base Address
	Public Const BIBOARDTYPE As Short = 1 ' Board Type (0x101 - 0x7FFF)
	Public Const BIINTLEVEL As Short = 2 ' Interrupt level
	Public Const BIDMACHAN As Short = 3 ' DMA channel
	Public Const BIINITIALIZED As Short = 4 ' TRUE or FALSE
	Public Const BICLOCK As Short = 5 ' Clock freq (1, 10 or bus)
	Public Const BIRANGE As Short = 6 ' Switch selectable range
	Public Const BINUMADCHANS As Short = 7 ' Number of A/D channels
	Public Const BIUSESEXPS As Short = 8 ' Supports expansion boards TRUE/FALSE
	Public Const BIDINUMDEVS As Short = 9 ' Number of digital devices
	Public Const BIDIDEVNUM As Short = 10 ' Index into digital information
	Public Const BICINUMDEVS As Short = 11 ' Number of counter devices
	Public Const BICIDEVNUM As Short = 12 ' Index into counter information
	Public Const BINUMDACHANS As Short = 13 ' Number of D/A channels
	Public Const BIWAITSTATE As Short = 14 ' Wait state enabled TRUE/FALSE
	Public Const BINUMIOPORTS As Short = 15 ' I/O address space used by board
	Public Const BIPARENTBOARD As Short = 16 ' Board number of parent board
	Public Const BIDTBOARD As Short = 17 ' Board number of connected DT board
	Public Const BINUMEXPS As Short = 18 ' Number of EXPs attached to board
	Public Const BISERIALNUM As Short = 214 ' Serial Number for USB boards
	Public Const BIDACUPDATEMODE As Short = 215 ' Update immediately or upon AOUPDATE command
	Public Const BIDACUPDATECMD As Short = 216 ' Issue D/A UPDATE command
	Public Const BIDACSTARTUP As Short = 217 ' Restore last value written for startup
	Public Const BIADTRIGCOUNT As Short = 219 ' Number of samples to acquire per trigger in retrigger mode
	Public Const BIADFIFOSIZE As Short = 220 ' Set FIFO override size for retrigger mode
	Public Const BIADSOURCE As Short = 221 ' Set A/D source to internal reference(>=0) or external connector(-1)
	Public Const BICALOUTPUT As Short = 222 ' CAL output pin setting
	Public Const BISRCADPACER As Short = 223 ' Source A/D Pacer output
	
	' Types of digital device information
	Public Const DIBASEADR As Short = 0 ' Base address
	Public Const DIINITIALIZED As Short = 1 ' TRUE or FALSE
	Public Const DIDEVTYPE As Short = 2 ' AUXPORT or xPORTA - CH
	Public Const DIMASK As Short = 3 ' Bit mask for this port
	Public Const DIREADWRITE As Short = 4 ' Read required before write
	Public Const DICONFIG As Short = 5 ' Current configuration
	Public Const DINUMBITS As Short = 6 ' Number of bits in port
	Public Const DICURVAL As Short = 7 ' Current value of outputs
	Public Const DIINMASK As Short = 8 ' Input bit mask for port
	Public Const DIOUTMASK As Short = 9 ' Output bit mask for port
	
	' Types of counter device information
	Public Const CIBASEADR As Short = 0 ' Base address
	Public Const CIINITIALIZED As Short = 1 ' TRUE or FALSE
	Public Const CICTRTYPE As Short = 2 ' Counter type 8254, 9513 or 8536
	Public Const CICTRNUM As Short = 3 ' Which counter on chip
	Public Const CICONFIGBYTE As Short = 4 ' Configuration byte
	
	' Types of expansion board information
	Public Const XIBOARDTYPE As Short = 0 ' Expansion board type
	Public Const XIMUXADCHAN1 As Short = 1 ' 0 - 15
	Public Const XIMUXADCHAN2 As Short = 2 ' 0 - 15 or NOTUSED
	Public Const XIRANGE1 As Short = 3 ' Range (gain) of low 16 chans
	Public Const XIRANGE2 As Short = 4 ' Range (gain) of high 16 chans
	Public Const XICJCCHAN As Short = 5 ' 0 - 15 or NOTUSED
	Public Const XITHERMTYPE As Short = 6 ' TYPEJ, TYPEK, TYPEB, TYPET, TYPEE, TYPER, or TYPES
	Public Const XINUMEXPCHANS As Short = 7 ' Number of expansion channels on board
	Public Const XIPARENTBOARD As Short = 8 ' Board number of parent A/D board
	Public Const XISPARE0 As Short = 9 ' 16 words of misc options
	
	' Types of Events
	Public Const ON_SCAN_ERROR As Integer = &H1
	Public Const ON_EXTERNAL_INTERRUPT As Integer = &H2
	Public Const ON_PRETRIGGER As Integer = &H4
	Public Const ON_DATA_AVAILABLE As Integer = &H8
	Public Const ON_END_OF_AI_SCAN As Integer = &H10
	Public Const ON_END_OF_AO_SCAN As Integer = &H20
	Public Const ALL_EVENT_TYPES As Integer = &HFF
	
	
	' If you are using an older version of Visual BASIC that does not recognize the #If
	' statement below then remove all of the lines from #If to #Else and also remove
	' the "End IF statement below
#If Win32 Then
	
	' Option Flags
	'
	Public Const FOREGROUND As Integer = &H0
	Public Const BACKGROUND As Integer = &H1
	
	Public Const SINGLEEXEC As Integer = &H0
	Public Const CONTINUOUS As Integer = &H2
	
	Public Const TIMED As Integer = &H0
	Public Const EXTCLOCK As Integer = &H4
	
	Public Const NOCONVERTDATA As Integer = &H0
	Public Const CONVERTDATA As Integer = &H8
	
	Public Const NODTCONNECT As Integer = &H0
	Public Const DTCONNECT As Integer = &H10
	
	Public Const DEFAULTIO As Integer = &H0
	Public Const SINGLEIO As Integer = &H20
	Public Const DMAIO As Integer = &H40
	Public Const BLOCKIO As Integer = &H60
	Public Const BURSTIO As Integer = &H10000 ' Transfer upon scan completion
	Public Const RETRIGMODE As Integer = &H20000 ' Re-arm trigger upon acquiring trigger count samples
	
	Public Const BYTEXFER As Integer = &H0
	Public Const WORDXFER As Integer = &H100
	
	Public Const INDIVIDUAL As Integer = &H0
	Public Const SIMULTANEOUS As Integer = &H200
	
	'UPGRADE_NOTE: FILTER was upgraded to FILTER_Renamed. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Public Const FILTER_Renamed As Integer = &H0
	Public Const NOFILTER As Integer = &H400
	
	Public Const NORMMEMORY As Integer = &H0
	Public Const EXTMEMORY As Integer = &H800
	
	Public Const BURSTMODE As Integer = &H1000
	
	Public Const NOTODINTS As Integer = &H2000
	
	Public Const EXTTRIGGER As Integer = &H4000
	
	Public Const NOCALIBRATEDATA As Integer = &H8000
	Public Const CALIBRATEDATA As Integer = &H0
    'PB functions
    '    Public Const PULSE_PROGRAM As Integer = 0
    '    Public Declare Function pb_init Lib "C:\WINDOWS\SYSTEM32\spinapi.dll" () As Integer
    '    Public Declare Function pb_set_clock Lib "C:\WINDOWS\SYSTEM32\spinapi.dll" (ByVal clock As Double) As Integer
    '    Public Declare Function pb_start_programming Lib "C:\WINDOWS\SYSTEM32\spinapi.dll" (ByVal device As Integer) As Integer
    '    Public Declare Function pb_outp Lib "C:\WINDOWS\SYSTEM32\spinapi.dll" (ByVal Addr As Short, ByVal Data As Short) As Integer
    '    Public Declare Function pb_close Lib "C:\WINDOWS\SYSTEM32\spinapi.dll" () As Integer
    '    Public Declare Function pb_stop_programming Lib "C:\WINDOWS\SYSTEM32\spinapi.dll" () As Integer
    '    Public Declare Function pb_start Lib "C:\WINDOWS\SYSTEM32\spinapi.dll" () As Integer
    '    Public Declare Function pb_inst_pbonly Lib "C:\WINDOWS\SYSTEM32\spinapi.dll" (ByVal flags As Integer, ByVal inst As Integer, ByVal inst_data As Integer, ByVal length As Double) As Integer

	' 32-bit function prototypes
	'
	Declare Function cbLoadConfig Lib "cbw32.dll" (ByVal CfgFileName As String) As Integer
	Declare Function cbSaveConfig Lib "cbw32.dll" (ByVal CfgFileName As String) As Integer
	Declare Function cbAConvertData Lib "cbw32.dll" (ByVal BoardNum As Integer, ByVal NumPoints As Integer, ByRef ADData As Short, ByRef ChanTags As Short) As Integer
	Declare Function cbACalibrateData Lib "cbw32.dll" (ByVal BoardNum As Integer, ByVal NumPoints As Integer, ByVal Gain As Integer, ByRef ADData As Short) As Integer
	Declare Function cbAConvertPretrigData Lib "cbw32.dll" (ByVal BoardNum As Integer, ByVal PretrigCount As Integer, ByVal TotalCount As Integer, ByRef ADData As Short, ByRef ChanTags As Short) As Integer
	Declare Function cbAIn Lib "cbw32.dll" (ByVal BoardNum As Integer, ByVal Chan As Integer, ByVal Gain As Integer, ByRef DataValue As Short) As Integer
	Declare Function cbAInScan Lib "cbw32.dll" (ByVal BoardNum As Integer, ByVal LowChan As Integer, ByVal HighChan As Integer, ByVal CBCount As Integer, ByRef CBRate As Integer, ByVal Gain As Integer, ByVal MemHandle As Integer, ByVal Options As Integer) As Integer
	Declare Function cbALoadQueue Lib "cbw32.dll" (ByVal BoardNum As Integer, ByRef ChanArray As Short, ByRef GainArray As Short, ByVal NumChans As Integer) As Integer
	Declare Function cbAOut Lib "cbw32.dll" (ByVal BoardNum As Integer, ByVal Chan As Integer, ByVal Gain As Integer, ByVal DataValue As Short) As Integer
	Declare Function cbAOutScan Lib "cbw32.dll" (ByVal BoardNum As Integer, ByVal LowChan As Integer, ByVal HighChan As Integer, ByVal CBCount As Integer, ByRef CBRate As Integer, ByVal Gain As Integer, ByVal MemHandle As Integer, ByVal Options As Integer) As Integer
	Declare Function cbAPretrig Lib "cbw32.dll" (ByVal BoardNum As Integer, ByVal LowChan As Integer, ByVal HighChan As Integer, ByRef PretrigCount As Integer, ByRef CBCount As Integer, ByRef CBRate As Integer, ByVal Gain As Integer, ByVal MemHandle As Integer, ByVal Options As Integer) As Integer
	Declare Function cbATrig Lib "cbw32.dll" (ByVal BoardNum As Integer, ByVal Chan As Integer, ByVal TrigType As Integer, ByVal TrigValue As Short, ByVal Gain As Integer, ByRef DataValue As Short) As Integer
	Declare Function cbC7266Config Lib "cbw32.dll" (ByVal BoardNum As Integer, ByVal CounterNum As Integer, ByVal Quadrature As Integer, ByVal CountingMode As Integer, ByVal DataEncoding As Integer, ByVal IndexMode As Integer, ByVal InvertIndex As Integer, ByVal FlagPins As Integer, ByVal GateEnable As Integer) As Integer
	Declare Function cbC8254Config Lib "cbw32.dll" (ByVal BoardNum As Integer, ByVal CounterNum As Integer, ByVal Config As Integer) As Integer
	Declare Function cbC8536Config Lib "cbw32.dll" (ByVal BoardNum As Integer, ByVal CounterNum As Integer, ByVal OutputControl As Integer, ByVal RecycleMode As Integer, ByVal Retrigger As Integer) As Integer
	Declare Function cbC9513Config Lib "cbw32.dll" (ByVal BoardNum As Integer, ByVal CounterNum As Integer, ByVal GateControl As Integer, ByVal CounterEdge As Integer, ByVal CountSource As Integer, ByVal SpecialGate As Integer, ByVal Reload As Integer, ByVal RecycleMode As Integer, ByVal BCDMode As Integer, ByVal CountDirec As Integer, ByVal OutputCtrl As Integer) As Integer
	Declare Function cbC8536Init Lib "cbw32.dll" (ByVal BoardNum As Integer, ByVal ChipNum As Integer, ByVal Ctr1Output As Integer) As Integer
	'UPGRADE_NOTE: TimeOfDay was upgraded to TimeOfDay_Renamed. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Declare Function cbC9513Init Lib "cbw32.dll" (ByVal BoardNum As Integer, ByVal ChipNum As Integer, ByVal FOutDivider As Integer, ByVal FOutSource As Integer, ByVal Compare1 As Integer, ByVal Compare2 As Integer, ByVal TimeOfDay_Renamed As Integer) As Integer
	Declare Function cbCStoreOnInt Lib "cbw32.dll" (ByVal BoardNum As Integer, ByVal IntCount As Integer, ByRef CntrControl As Short, ByVal DataBuffer As Integer) As Integer
	Declare Function cbCFreqIn Lib "cbw32.dll" (ByVal BoardNum As Integer, ByVal SigSource As Integer, ByVal GateInterval As Integer, ByRef CBCount As Short, ByRef Freq As Integer) As Integer
	
	'*****************************************************************************
	'   Legacy Function Prototypes: to revert to legacy calls, un-comment the
	'          prototypes immediately below.
	'
	'      Declare Function cbCIn Lib "cbw32.dll" (ByVal BoardNum&, ByVal CounterNum&, CBCount&) As Long
	'
	'   Remove the following if using the above legacy function prototypes.
	Declare Function cbCIn Lib "cbw32.dll" (ByVal BoardNum As Integer, ByVal CounterNum As Integer, ByRef CBCount As Short) As Integer
	'*****************************************************************************
	
	Declare Function cbCIn32 Lib "cbw32.dll" (ByVal BoardNum As Integer, ByVal CounterNum As Integer, ByRef CBCount As Integer) As Integer
	Declare Function cbCLoad Lib "cbw32.dll" (ByVal BoardNum As Integer, ByVal RegNum As Integer, ByVal LoadValue As Integer) As Integer
	Declare Function cbCLoad32 Lib "cbw32.dll" (ByVal BoardNum As Integer, ByVal RegNum As Integer, ByVal LoadValue As Integer) As Integer
	Declare Function cbCStatus Lib "cbw32.dll" (ByVal BoardNum As Integer, ByVal CounterNum As Integer, ByRef StatusBits As Integer) As Integer
	Declare Function cbDBitIn Lib "cbw32.dll" (ByVal BoardNum As Integer, ByVal PortType As Integer, ByVal BitNum As Integer, ByRef BitValue As Short) As Integer
	Declare Function cbDBitOut Lib "cbw32.dll" (ByVal BoardNum As Integer, ByVal PortType As Integer, ByVal BitNum As Integer, ByVal BitValue As Integer) As Integer
	Declare Function cbDConfigPort Lib "cbw32.dll" (ByVal BoardNum As Integer, ByVal PortNum As Integer, ByVal Direction As Integer) As Integer
	Declare Function cbDConfigBit Lib "cbw32.dll" (ByVal BoardNum As Integer, ByVal PortNum As Integer, ByVal BitNum As Integer, ByVal Direction As Integer) As Integer
	Declare Function cbDeclareRevision Lib "cbw32.dll" (ByRef RevNum As Single) As Integer
	Declare Function cbDIn Lib "cbw32.dll" (ByVal BoardNum As Integer, ByVal PortNum As Integer, ByRef DataValue As Short) As Integer
	Declare Function cbDInScan Lib "cbw32.dll" (ByVal BoardNum As Integer, ByVal PortNum As Integer, ByVal CBCount As Integer, ByRef CBRate As Integer, ByVal MemHandle As Integer, ByVal Options As Integer) As Integer
	Declare Function cbDOut Lib "cbw32.dll" (ByVal BoardNum As Integer, ByVal PortNum As Integer, ByVal DataValue As Short) As Integer
	Declare Function cbDOutScan Lib "cbw32.dll" (ByVal BoardNum As Integer, ByVal PortNum As Integer, ByVal CBCount As Integer, ByRef CBRate As Integer, ByVal MemHandle As Integer, ByVal Options As Integer) As Integer
	Declare Function cbErrHandling Lib "cbw32.dll" (ByVal ErrReporting As Integer, ByVal ErrHandling As Integer) As Integer
	Declare Function cbFileAInScan Lib "cbw32.dll" (ByVal BoardNum As Integer, ByVal LowChan As Integer, ByVal HighChan As Integer, ByVal CBCount As Integer, ByRef CBRate As Integer, ByVal Gain As Integer, ByVal FileName As String, ByVal Options As Integer) As Integer
	Declare Function cbFileGetInfo Lib "cbw32.dll" (ByVal FileName As String, ByRef LowChan As Short, ByRef HighChan As Short, ByRef PretrigCount As Integer, ByRef TotalCount As Integer, ByRef CBRate As Integer, ByRef Gain As Integer) As Integer
	Declare Function cbFilePretrig Lib "cbw32.dll" (ByVal BoardNum As Integer, ByVal LowChan As Integer, ByVal HighChan As Integer, ByRef PretrigCount As Integer, ByRef CBCount As Integer, ByRef CBRate As Integer, ByVal Gain As Integer, ByVal FileName As String, ByVal Options As Integer) As Integer
	Declare Function cbFileRead Lib "cbw32.dll" (ByVal FileName As String, ByVal FirstPoint As Integer, ByRef NumPoints As Integer, ByRef DataBuffer As Short) As Integer
	Declare Function cbFlashLED Lib "cbw32.dll" (ByVal BoardNum As Integer) As Integer
	Declare Function cbGetErrMsg Lib "cbw32.dll" (ByVal ErrCode As Integer, ByVal ErrMsg As String) As Integer
	Declare Function cbGetRevision Lib "cbw32.dll" (ByRef DLLRevNum As Single, ByRef VXDRevNum As Single) As Integer
	
	'*****************************************************************************
	'   Legacy Function Prototypes: to revert to legacy calls, un-comment the
	'          prototypes immediately below.
	'
	'       Declare Function cbGetStatus Lib "cbw32.dll" (ByVal BoardNum&, Status%, CurCount&, CurIndex&) As Long
	'       Declare Function cbStopBackground Lib "cbw32.dll" (ByVal BoardNum&) As Long
	'
	'   Remove the following if using the above legacy function prototypes.
	Declare Function cbGetStatus Lib "cbw32.dll"  Alias "cbGetIOStatus"(ByVal BoardNum As Integer, ByRef Status As Short, ByRef CurCount As Integer, ByRef CurIndex As Integer, ByVal FunctionType As Integer) As Integer
	Declare Function cbStopBackground Lib "cbw32.dll"  Alias "cbStopIOBackground"(ByVal BoardNum As Integer, ByVal FunctionType As Integer) As Integer
	'*****************************************************************************
	
	Declare Function cbMemSetDTMode Lib "cbw32.dll" (ByVal BoardNum As Integer, ByVal Mode As Integer) As Integer
	Declare Function cbMemReset Lib "cbw32.dll" (ByVal BoardNum As Integer) As Integer
	Declare Function cbMemRead Lib "cbw32.dll" (ByVal BoardNum As Integer, ByRef DataBuffer As Short, ByVal FirstPoint As Integer, ByVal CBCount As Integer) As Integer
	Declare Function cbMemWrite Lib "cbw32.dll" (ByVal BoardNum As Integer, ByRef DataBuffer As Short, ByVal FirstPoint As Integer, ByVal CBCount As Integer) As Integer
	Declare Function cbMemReadPretrig Lib "cbw32.dll" (ByVal BoardNum As Integer, ByRef DataBuffer As Short, ByVal FirstPoint As Integer, ByVal CBCount As Integer) As Integer
	Declare Function cbRS485 Lib "cbw32.dll" (ByVal BoardNum As Integer, ByVal Transmit As Integer, ByVal Receive As Integer) As Integer
	Declare Function cbTIn Lib "cbw32.dll" (ByVal BoardNum As Integer, ByVal Chan As Integer, ByVal CBScale As Integer, ByRef TempValue As Single, ByVal Options As Integer) As Integer
	Declare Function cbTInScan Lib "cbw32.dll" (ByVal BoardNum As Integer, ByVal LowChan As Integer, ByVal HighChan As Integer, ByVal CBScale As Integer, ByRef DataBuffer As Single, ByVal Options As Integer) As Integer
	Declare Function cbWinBufToArray Lib "cbw32.dll" (ByVal MemHandle As Integer, ByRef DataBuffer As Short, ByVal FirstPoint As Integer, ByVal CBCount As Integer) As Integer
	Declare Function cbWinArrayToBuf Lib "cbw32.dll" (ByRef DataBuffer As Short, ByVal MemHandle As Integer, ByVal FirstPoint As Integer, ByVal CBCount As Integer) As Integer
	Declare Function cbWinBufAlloc Lib "cbw32.dll" (ByVal NumPoints As Integer) As Integer
	Declare Function cbWinBufFree Lib "cbw32.dll" (ByVal MemHandle As Integer) As Integer
	Declare Function cbInByte Lib "cbw32.dll" (ByVal BoardNum As Integer, ByVal PortNum As Integer) As Integer
	Declare Function cbOutByte Lib "cbw32.dll" (ByVal BoardNum As Integer, ByVal PortNum As Integer, ByVal PortVal As Short) As Integer
	Declare Function cbInWord Lib "cbw32.dll" (ByVal BoardNum As Integer, ByVal PortNum As Integer) As Integer
	Declare Function cbOutWord Lib "cbw32.dll" (ByVal BoardNum As Integer, ByVal PortNum As Integer, ByVal PortVal As Short) As Integer
	
	'*****************************************************************************
	'   Legacy Function Prototypes: to revert to legacy calls, un-comment the
	'          prototypes immediately below.
	'
	'      Declare Function cbGetConfig Lib "cbw32.dll" (ByVal InfoType&, ByVal BoardNum&, ByVal DevNum&, ByVal ConfigItem&, ByRef ConfigVal%) As Long
	'
	'   Remove the following if using the above legacy function prototypes.
	Declare Function cbGetConfig Lib "cbw32.dll" (ByVal InfoType As Integer, ByVal BoardNum As Integer, ByVal DevNum As Integer, ByVal ConfigItem As Integer, ByRef ConfigVal As Integer) As Integer
	'*****************************************************************************
	
	Declare Function cbSetConfig Lib "cbw32.dll" (ByVal InfoType As Integer, ByVal BoardNum As Integer, ByVal DevNum As Integer, ByVal ConfigItem As Integer, ByVal ConfigVal As Integer) As Integer
	Declare Function cbToEngUnits Lib "cbw32.dll" (ByVal BoardNum As Integer, ByVal Range As Integer, ByVal DataVal As Short, ByRef EngUnits As Single) As Integer
	Declare Function cbFromEngUnits Lib "cbw32.dll" (ByVal BoardNum As Integer, ByVal Range As Integer, ByVal EngUnits As Single, ByRef DataVal As Short) As Integer
	Declare Function cbGetBoardName Lib "cbw32.dll" (ByVal BoardNum As Integer, ByVal BoardName As String) As Integer
	Declare Function cbSetTrigger Lib "cbw32.dll" (ByVal BoardNum As Integer, ByVal TrigType As Integer, ByVal LowThreshold As Short, ByVal HighThreshold As Short) As Integer
	'UPGRADE_ISSUE: Declaring a parameter 'As Any' is not supported. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="FAE78A8D-8978-4FD4-8208-5B7324A8F795"'

    'function not called; may need to fix if function is needed; as any no longer valid as a variable type
    'Declare Function cbEnableEvent Lib "cbw32.dll" (ByVal BoardNum As Integer, ByVal EventType As Integer, ByVal EventSize As Integer, ByVal Callback As Integer, ByRef UserData As Any) As Integer
	Declare Function cbDisableEvent Lib "cbw32.dll" (ByVal BoardNum As Integer, ByVal EventType As Integer) As Integer
	Declare Function cbSelectSignal Lib "cbw32.dll" (ByVal BoardNum As Integer, ByVal Direction As Integer, ByVal Signal As Integer, ByVal Connection As Integer, ByVal Polarity As Integer) As Integer
	Declare Function cbGetSignal Lib "cbw32.dll" (ByVal BoardNum As Integer, ByVal Direction As Integer, ByVal Signal As Integer, ByVal Index As Integer, ByRef Connection As Integer, ByRef Polarity As Integer) As Integer
	
#Else
	'UPGRADE_NOTE: #If #EndIf block was not upgraded because the expression Else did not evaluate to True or was not evaluated. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="27EE2C3C-05AF-4C04-B2AF-657B4FB6B5FC"'
	'
	'  16-bit
	'
	
	' Option Flags
	'
	Global Const FOREGROUND = &H0
	Global Const BACKGROUND = &H1
	
	Global Const SINGLEEXEC = &H0
	Global Const CONTINUOUS = &H2
	
	Global Const TIMED = &H0
	Global Const EXTCLOCK = &H4
	
	Global Const NOCONVERTDATA = &H0
	Global Const CONVERTDATA = &H8
	
	Global Const NODTCONNECT = &H0
	Global Const DTCONNECT = &H10
	
	Global Const DEFAULTIO = &H0
	Global Const SINGLEIO = &H20
	Global Const DMAIO = &H40
	Global Const BLOCKIO = &H60
	
	Global Const BYTEXFER = &H0
	Global Const WORDXFER = &H100
	
	Global Const INDIVIDUAL = &H0
	Global Const SIMULTANEOUS = &H200
	
	Global Const FILTER = &H0
	Global Const NOFILTER = &H400
	
	Global Const NORMMEMORY = &H0
	Global Const EXTMEMORY = &H800
	
	Global Const BURSTMODE = &H1000
	
	Global Const NOTODINTS = &H2000
	
	Global Const EXTTRIGGER = &H4000
	
	Global Const NOCALIBRATEDATA = &H8000
	Global Const CALIBRATEDATA = &H0
	'
	' 16-bit function prototypes
	'
	Declare Function cbAConvertData Lib "cbw.dll" (ByVal BoardNum%, ByVal NumPoints&, ADData%, ChanTags%) As Integer
	Declare Function cbACalibrateData Lib "cbw.dll" (ByVal BoardNum%, ByVal NumPoints&, ByVal Gain%, ADData%) As Integer
	Declare Function cbAConvertPretrigData Lib "cbw.dll" (ByVal BoardNum%, ByVal PretrigCount&, ByVal TotalCount&, ADData%, ChanTags%) As Integer
	Declare Function cbAIn Lib "cbw.dll" (ByVal BoardNum%, ByVal Chan%, ByVal Gain%, DataValue%) As Integer
	Declare Function cbAInScan Lib "cbw.dll" (ByVal BoardNum%, ByVal LowChan%, ByVal HighChan%, ByVal CBCount&, CBRate&, ByVal Gain%, ByVal MemHandle%, ByVal Options%) As Integer
	Declare Function cbALoadQueue Lib "cbw.dll" (ByVal BoardNum%, ChanArray%, GainArray%, ByVal NumChans%) As Integer
	Declare Function cbAOut Lib "cbw.dll" (ByVal BoardNum%, ByVal Chan%, ByVal Gain%, ByVal DataValue%) As Integer
	Declare Function cbAOutScan Lib "cbw.dll" (ByVal BoardNum%, ByVal LowChan%, ByVal HighChan%, ByVal CBCount&, CBRate&, ByVal Gain%, ByVal MemHandle%, ByVal Options%) As Integer
	Declare Function cbAPretrig Lib "cbw.dll" (ByVal BoardNum%, ByVal LowChan%, ByVal HighChan%, PretrigCount&, CBCount&, CBRate&, ByVal Gain%, ByVal MemHandle%, ByVal Options%) As Integer
	Declare Function cbATrig Lib "cbw.dll" (ByVal BoardNum%, ByVal Chan%, ByVal TrigType%, ByVal TrigValue%, ByVal Gain%, DataValue%) As Integer
	Declare Function cbC8254Config Lib "cbw.dll" (ByVal BoardNum%, ByVal CounterNum%, ByVal Config%) As Integer
	Declare Function cbC8536Config Lib "cbw.dll" (ByVal BoardNum%, ByVal CounterNum%, ByVal OutputControl%, ByVal RecycleMode%, ByVal Retrigger%) As Integer
	Declare Function cbC9513Config Lib "cbw.dll" (ByVal BoardNum%, ByVal CounterNum%, ByVal GateControl%, ByVal CounterEdge%, ByVal CountSource%, ByVal SpecialGate%, ByVal Reload%, ByVal RecycleMode%, ByVal BCDMode%, ByVal CountDirec%, ByVal OutputCtrl%) As Integer
	Declare Function cbC8536Init Lib "cbw.dll" (ByVal BoardNum%, ByVal ChipNum%, ByVal Ctr1Output%) As Integer
	Declare Function cbC9513Init Lib "cbw.dll" (ByVal BoardNum%, ByVal ChipNum%, ByVal FOutDivider%, ByVal FOutSource%, ByVal Compare1%, ByVal Compare2%, ByVal TimeOfDay%) As Integer
	Declare Function cbCStoreOnInt Lib "cbw.dll" (ByVal BoardNum%, ByVal IntCount%, CntrControl%, ByVal DataBuffer%) As Integer
	Declare Function cbCFreqIn Lib "cbw.dll" (ByVal BoardNum%, ByVal SigSource%, ByVal GateInterval%, CBCount%, Freq&) As Integer
	Declare Function cbCIn Lib "cbw.dll" (ByVal BoardNum%, ByVal CounterNum%, CBCount As Any) As Integer
	Declare Function cbCLoad Lib "cbw.dll" (ByVal BoardNum%, ByVal RegNum%, ByVal LoadValue%) As Integer
	Declare Function cbDBitIn Lib "cbw.dll" (ByVal BoardNum%, ByVal PortType%, ByVal BitNum%, BitValue%) As Integer
	Declare Function cbDBitOut Lib "cbw.dll" (ByVal BoardNum%, ByVal PortType%, ByVal BitNum%, ByVal BitValue%) As Integer
	Declare Function cbDConfigPort Lib "cbw.dll" (ByVal BoardNum%, ByVal PortNum%, ByVal Direction%) As Integer
	Declare Function cbDeclareRevision Lib "cbw.dll" (RevNum!) As Integer
	Declare Function cbDIn Lib "cbw.dll" (ByVal BoardNum%, ByVal PortNum%, DataValue%) As Integer
	Declare Function cbDInScan Lib "cbw.dll" (ByVal BoardNum%, ByVal PortNum%, ByVal CBCount&, CBRate&, ByVal MemHandle%, ByVal Options%) As Integer
	Declare Function cbDOut Lib "cbw.dll" (ByVal BoardNum%, ByVal PortNum%, ByVal DataValue%) As Integer
	Declare Function cbDOutScan Lib "cbw.dll" (ByVal BoardNum%, ByVal PortNum%, ByVal CBCount&, CBRate&, ByVal MemHandle%, ByVal Options%) As Integer
	Declare Function cbErrHandling Lib "cbw.dll" (ByVal ErrReporting%, ByVal ErrHandling%) As Integer
	Declare Function cbFileAInScan Lib "cbw.dll" (ByVal BoardNum%, ByVal LowChan%, ByVal HighChan%, ByVal CBCount&, CBRate&, ByVal Gain%, ByVal FileName$, ByVal Options%) As Integer
	Declare Function cbFileGetInfo Lib "cbw.dll" (ByVal FileName$, LowChan%, HighChan%, PretrigCount&, TotalCount&, CBRate&, Gain As Any) As Integer
	Declare Function cbFilePretrig Lib "cbw.dll" (ByVal BoardNum%, ByVal LowChan%, ByVal HighChan%, PretrigCount&, CBCount&, CBRate&, ByVal Gain%, ByVal FileName$, ByVal Options%) As Integer
	Declare Function cbFileRead Lib "cbw.dll" (ByVal FileName$, ByVal FirstPoint&, NumPoints&, DataBuffer%) As Integer
	Declare Function cbGetErrMsg Lib "cbw.dll" (ByVal ErrCode%, ByVal ErrMsg$) As Integer
	Declare Function cbGetRevision Lib "cbw.dll" (DLLRevNum!, VXDRevNum!) As Integer
	Declare Function cbGetStatus Lib "cbw.dll" (ByVal BoardNum%, Status%, CurCount&, CurIndex&) As Integer
	Declare Function cbStopBackground Lib "cbw.dll" (ByVal BoardNum%) As Integer
	Declare Function cbMemSetDTMode Lib "cbw.dll" (ByVal BoardNum%, ByVal Mode%) As Integer
	Declare Function cbMemReset Lib "cbw.dll" (ByVal BoardNum%) As Integer
	Declare Function cbMemRead Lib "cbw.dll" (ByVal BoardNum%, DataBuffer%, ByVal FirstPoint&, ByVal CBCount&) As Integer
	Declare Function cbMemWrite Lib "cbw.dll" (ByVal BoardNum%, DataBuffer%, ByVal FirstPoint&, ByVal CBCount&) As Integer
	Declare Function cbMemReadPretrig Lib "cbw.dll" (ByVal BoardNum%, DataBuffer%, ByVal FirstPoint&, ByVal CBCount&) As Integer
	Declare Function cbRS485 Lib "cbw.dll" (ByVal BoardNum%, ByVal Transmit%, ByVal Receive%) As Integer
	Declare Function cbTIn Lib "cbw.dll" (ByVal BoardNum%, ByVal Chan%, ByVal CBScale%, TempValue!, ByVal Options%) As Integer
	Declare Function cbTInScan Lib "cbw.dll" (ByVal BoardNum%, ByVal LowChan%, ByVal HighChan%, ByVal CBScale%, DataBuffer!, ByVal Options%) As Integer
	Declare Function cbWinBufToArray Lib "cbw.dll" (ByVal MemHandle%, DataBuffer%, ByVal FirstPoint&, ByVal CBCount&) As Integer
	Declare Function cbWinArrayToBuf Lib "cbw.dll" (DataBuffer%, ByVal MemHandle%, ByVal FirstPoint&, ByVal CBCount&) As Integer
	Declare Function cbWinBufAlloc Lib "cbw.dll" (ByVal NumPoints&) As Integer
	Declare Function cbWinBufFree Lib "cbw.dll" (ByVal MemHandle%) As Integer
	Declare Function cbInByte Lib "cbw.dll" (ByVal BoardNum%, ByVal PortNum%) As Integer
	Declare Function cbOutByte Lib "cbw.dll" (ByVal BoardNum%, ByVal PortNum%, ByVal PortVal%) As Integer
	Declare Function cbInWord Lib "cbw.dll" (ByVal BoardNum%, ByVal PortNum%) As Integer
	Declare Function cbOutWord Lib "cbw.dll" (ByVal BoardNum%, ByVal PortNum%, ByVal PortVal%) As Integer
	Declare Function cbGetConfig Lib "cbw.dll" (ByVal InfoType%, ByVal BoardNum%, ByVal DevNum%, ByVal ConfigItem%, ConfigVal%) As Integer
	Declare Function cbSetConfig Lib "cbw.dll" (ByVal InfoType%, ByVal BoardNum%, ByVal DevNum%, ByVal ConfigItem%, ByVal ConfigVal%) As Integer
	Declare Function cbToEngUnits Lib "cbw.dll" (ByVal BoardNum%, ByVal Range%, ByVal DataVal%, EngUnits!) As Integer
	Declare Function cbFromEngUnits Lib "cbw.dll" (ByVal BoardNum%, ByVal Range%, ByVal EngUnits!, DataVal%) As Integer
	Declare Function cbGetBoardName Lib "cbw.dll" (ByVal BoardNum%, ByVal BoardName$) As Integer
	Declare Function cbSetTrigger Lib "cbw.dll" (ByVal BoardNum%, ByVal TrigType%, ByVal LowThreshold%, ByVal HighThreshold%) As Integer
#End If
End Module