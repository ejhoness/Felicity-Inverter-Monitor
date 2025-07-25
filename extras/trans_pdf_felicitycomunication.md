tanks Gemini
20210608B.1.1.pdf

Alright, this is excellent! You've provided the communication protocol for a High-Frequency Inverter, which uses UART communication and the Modbus protocol. This document contains all the crucial information needed to interact with the device.

Let's break down the configuration and how to get data based on this document.

Inverter Modbus Communication Configuration ￼


Communication Interface: UART ￼


Baud Rate: 2400 bps ￼


Data Bits: 8 bits ￼


Stop Bits: 1 bit ￼


Parity: None ￼


Flow Control: None ￼


Communication Mode: Half-duplex (Master initiates, Inverter responds) ￼


Protocol: MODBUS protocol frames ￼


Slave (Inverter) Address: 1-31 (decimal), where 31 is a broadcast address ￼

Modbus Function Codes Used ￼

The document specifies the following Modbus Function Codes (Commands):


0x03: Read Multiple Parameters (Read Multiple Registers) ￼
￼


0x06: Write Single Parameter (Write Single Register) ￼
￼


0x10: Write Multiple Parameters (Write Multiple Registers) ￼
￼


0x17: Master/Slave Data Synchronization ￼


0x41: Firmware Upgrade ￼

Data Register Definitions ￼

The document provides two main categories of registers:

1. Information Data Registers  (Read-Only)￼

These registers seem to contain static or semi-static information about the device. All listed are Read-only (R).


0xF800 (Hex) / 63488 (Decimal): DeviceType (Type - INT16U), 1 Word ￼

Description: Main category, e.g., "050" ￼


0xF801 (Hex) / 63489 (Decimal): SubType (INT16U), 1 Word ￼

Description: Device Subclass, e.g., "00204 (3024)", "00408 (5048)" ￼


0xF804 (Hex) / 63492 (Decimal): Serial number (INT16U), 5 Words ￼

Description: 14-digit pure number. Example: SN-01354820250001 ￼

Invalid value: 

0x00 ￼


0xF80B (Hex) / 63499 (Decimal): CPU1 FW/Version (INT16U), 1 Word ￼

Invalid value: 

0xFFFF ￼


0xF80C (Hex) / 63500 (Decimal): CPU2 FW/Version (INT16U), 1 Word ￼

Invalid value: 

0xFFFF ￼

2. Realtime Data Registers  (Read-Only)￼

These registers provide live operational data from the inverter. All listed are Read-only (R).


0x1100 (Hex) / 4352 (Decimal): SettingDataSn (INT16U), 1 Word ￼

Description: Serial number (incremented when setting data changes) ￼


0x1101 (Hex) / 4353 (Decimal): Working mode (INT16U), 1 Word ￼

Description:

0: PowerOnMode

1: Standby Mode

2: BypassMode

3: BatteryMode

4: FaultMode

5: Line Mode ￼


0x1102 (Hex) / 4354 (Decimal): Battery charging stage (INT16U), 1 Word ￼

Description:

0: No charge

1: Bulk charge

2: Absorption charge

3: Float charge ￼


0x1103 (Hex) / 4355 (Decimal): Fault Code (INT16U), 1 Word ￼

Description: Fault ID (refer to inverter fault table) ￼


0x1104 (Hex) / 4356 (Decimal): PowerFlowMsg (INT16U), 1 Word ￼

Description: Various bit flags for power flow status (e.g., Battery connected/disconnected, Line normal/abnormal, PV input normal/abnormal, Load connect allowed/unallowed, Battery charging/discharging) ￼


0x1108 (Hex) / 4360 (Decimal): Battery voltage (INT16U), 1 Word ￼

Unit: V ￼


0x1109 (Hex) / 4361 (Decimal): Battery current (INT16S), 1 Word ￼

Unit: A ￼

Note: Positive is charge current, negative is discharge current ￼


0x110A (Hex) / 4362 (Decimal): Battery power (INT16S), 1 Word ￼

Unit: W ￼

Note: Positive/negative for power ￼


0x1111 (Hex) / 4369 (Decimal): AC output voltage (INT16U), 1 Word ￼

Unit: V ￼


0x1117 (Hex) / 4375 (Decimal): AC input voltage (INT16U), 1 Word ￼

Unit: V ￼


0x1119 (Hex) / 4377 (Decimal): AC input frequency (INT16U), 1 Word ￼

Unit: Hz ￼


0x111B (Hex) / 4379 (Decimal): AC output active power (INT16S), 1 Word ￼

Unit: W ￼


0x111C (Hex) / 4380 (Decimal): AC output apparent power (INT16U), 1 Word ￼

Unit: VA ￼


0x1120 (Hex) / 4384 (Decimal): Load percentage (INT16U), 1 Word ￼

Unit: % ￼


0x1126 (Hex) / 4390 (Decimal): PV input voltage (INT16U), 1 Word ￼

Unit: V ￼


0x112A (Hex) / 4394 (Decimal): PV input power (INT16S), 1 Word ￼

Unit: W ￼

3. Setting Data Registers  (Read/Write)￼

These registers are for configuring the inverter's behavior. Most are Read/Write (R/W).


0x211F (Hex) / 8479 (Decimal): Battery cut-off voltage (INT16U), 1 Word ￼

Unit: V ￼

Range examples: 3024 model: 21.0V-27.0V; 5048 model: 42.0V-54.0V ￼


0x2122 (Hex) / 8482 (Decimal): Battery CV charging voltage (INT16U), 1 Word ￼

Unit: V ￼

Range examples: 3024: 24.0V-30.0V; 5048: 48.0V-60.0V ￼


0x2123 (Hex) / 8483 (Decimal): Battery floating charging voltage (INT16U), 1 Word ￼

Unit: V ￼

Range examples: 3024: 24.0V-30.0V; 5048: 48.0V-50.0V ￼


0x2129 (Hex) / 8489 (Decimal): AC output frequency (INT8U), 1 Word ￼

Description: 0-50Hz / 1-60Hz ￼


0x212A (Hex) / 8490 (Decimal): Output source priority (INT8U), 1 Word ￼

Description:

0: Utility First

1: Solar First

2: Solar+Battery+Utility ￼


0x212B (Hex) / 8491 (Decimal): Application Mode (INT8U), 1 Word ￼

Description: APL/T-UPS ￼


0x212C (Hex) / 8492 (Decimal): Charging source priority (INT8U), 1 Word ￼

Description:

1: Solar First

2: Solar And Utility First

3: Solar Only ￼


0x212D (Hex) / 8493 (Decimal): Battery type (INT8U), 1 Word ￼

Description:

0: AGM Battery

1: Flooded Battery

2: User defined

3: LiFePo4 Battery ￼


0x212E (Hex) / 8494 (Decimal): Max charging current (INT8U), 1 Word ￼

Unit: A ￼

Range: 10A-100A ￼

0x2130 (Hex) / 8496 (Decimal): Max. AC charging current (INT8U), 1 Word ￼

Unit: A ￼

Range: 10A-100A ￼


0x2131 (Hex) / 8497 (Decimal): Buzzer enable (INT8U), 1 Word ￼

Description: 0: Disable / 1: Enable ￼


0x2133 (Hex) / 8499 (Decimal): OverLoad restart enable (INT8U), 1 Word ￼

Description: 0: Disable / 1: Enable ￼


0x2134 (Hex) / 8500 (Decimal): Over temperature restart enable (INT8U), 1 Word ￼

Description: 0: Disable / 1: Enable ￼


0x2135 (Hex) / 8501 (Decimal): LCD backlight enable (INT8U), 1 Word ￼

Description: 0: Disable / 1: Enable ￼


0x2137 (Hex) / 8503 (Decimal): OverLoad to bypass (INT8U), 1 Word ￼

Description: 0: Disable / 1: Enable ￼


0x2156 (Hex) / 8534 (Decimal): Battery back to charge voltage (INT16U), 1 Word ￼

Unit: V ￼

Range examples: 3024: 22.0V-27.0V; 5048: 44.0V-54.0V ￼


0x2159 (Hex) / 8537 (Decimal): Battery back to discharge voltage (INT16U), 1 Word ￼

Unit: V ￼

Note: If exceeds max value, displays FULL (e.g., 60.1V for 5048 model is FULL) ￼

Key Takeaways for Getting Data
Serial Port Settings: When connecting with screen or any Modbus RTU software, use:

/dev/ttyUSBx (replace x with your port number)


Baud Rate: 2400 ￼


Data Bits: 8 ￼


Parity: None ￼


Stop Bits: 1 ￼


Slave ID: You'll need to know the specific decimal ID (1-31) of your inverter. ￼


Function Code 0x03 for Reading: All the Information Data and Realtime Data are read-only and use Function Code 0x03 (Read Holding Registers). ￼

Addressing: The document uses Hexadecimal addresses (e.g., 0xF800, 0x1100). Most Modbus libraries/software will expect these as 0-based addresses for Holding Registers (4xxxx) or Input Registers (3xxxx).

Important: Even though the document lists these as "registers" and provides examples with Function Code 0x03 (Read Multiple Registers), which is typically for Holding Registers (4xxxx series), the addresses 0xF800 (63488 decimal) and 0x1100 (4352 decimal) are quite high. Sometimes manufacturers use custom addressing or map these to Holding Registers starting from 40001 (meaning address 0 in a 0-based system corresponds to 40001).

Recommendation: When using a Modbus client/library, try reading these addresses directly as Holding Registers. If that doesn't work, try as Input Registers.

Data Interpretation:


Word Size: Each register holds a 16-bit word (two bytes). ￼


Byte Order: "first byte is high bit and second byte is low bit" (Big-Endian for a single 16-bit word).  This means if you read ￼

0xAD (high) and 0x01 (low), the value is 0xAD01.

Multi-Word Values: For Serial number (5 words) or potentially 32-bit values if registers are combined, you'll need to pay attention to how they are arranged (e.g., which word is the most significant). The document implies standard 16-bit registers.

Signed vs. Unsigned: INT16U means 16-bit unsigned integer. INT16S means 16-bit signed integer. This affects how values like Battery current (which can be positive or negative) are interpreted.

This protocol document gives you everything you need to start programming your master (client) or configuring a Modbus polling software.

upload file pdf from felicity
Alright, this is excellent! You've provided the communication protocol for a High-Frequency Inverter, which uses UART communication and the Modbus protocol. This document contains all the crucial information needed to interact with the device.

Let's break down the configuration and how to get data based on this document.

Inverter Modbus Communication Configuration ￼


Communication Interface: UART ￼


Baud Rate: 2400 bps ￼


Data Bits: 8 bits ￼


Stop Bits: 1 bit ￼


Parity: None ￼


Flow Control: None ￼


Communication Mode: Half-duplex (Master initiates, Inverter responds) ￼


Protocol: MODBUS protocol frames ￼


Slave (Inverter) Address: 1-31 (decimal), where 31 is a broadcast address ￼

Modbus Function Codes Used ￼

The document specifies the following Modbus Function Codes (Commands):


0x03: Read Multiple Parameters (Read Multiple Registers) ￼
￼


0x06: Write Single Parameter (Write Single Register) ￼
￼


0x10: Write Multiple Parameters (Write Multiple Registers) ￼
￼


0x17: Master/Slave Data Synchronization ￼


0x41: Firmware Upgrade ￼

Data Register Definitions ￼

The document provides two main categories of registers:

1. Information Data Registers  (Read-Only)￼

These registers seem to contain static or semi-static information about the device. All listed are Read-only (R).


0xF800 (Hex) / 63488 (Decimal): DeviceType (Type - INT16U), 1 Word ￼

Description: Main category, e.g., "050" ￼


0xF801 (Hex) / 63489 (Decimal): SubType (INT16U), 1 Word ￼

Description: Device Subclass, e.g., "00204 (3024)", "00408 (5048)" ￼


0xF804 (Hex) / 63492 (Decimal): Serial number (INT16U), 5 Words ￼

Description: 14-digit pure number. Example: SN-01354820250001 ￼

Invalid value: 

0x00 ￼


0xF80B (Hex) / 63499 (Decimal): CPU1 FW/Version (INT16U), 1 Word ￼

Invalid value: 

0xFFFF ￼


0xF80C (Hex) / 63500 (Decimal): CPU2 FW/Version (INT16U), 1 Word ￼

Invalid value: 

0xFFFF ￼

2. Realtime Data Registers  (Read-Only)￼

These registers provide live operational data from the inverter. All listed are Read-only (R).


0x1100 (Hex) / 4352 (Decimal): SettingDataSn (INT16U), 1 Word ￼

Description: Serial number (incremented when setting data changes) ￼


0x1101 (Hex) / 4353 (Decimal): Working mode (INT16U), 1 Word ￼

Description:

0: PowerOnMode

1: Standby Mode

2: BypassMode

3: BatteryMode

4: FaultMode

5: Line Mode ￼


0x1102 (Hex) / 4354 (Decimal): Battery charging stage (INT16U), 1 Word ￼

Description:

0: No charge

1: Bulk charge

2: Absorption charge

3: Float charge ￼


0x1103 (Hex) / 4355 (Decimal): Fault Code (INT16U), 1 Word ￼

Description: Fault ID (refer to inverter fault table) ￼


0x1104 (Hex) / 4356 (Decimal): PowerFlowMsg (INT16U), 1 Word ￼

Description: Various bit flags for power flow status (e.g., Battery connected/disconnected, Line normal/abnormal, PV input normal/abnormal, Load connect allowed/unallowed, Battery charging/discharging) ￼


0x1108 (Hex) / 4360 (Decimal): Battery voltage (INT16U), 1 Word ￼

Unit: V ￼


0x1109 (Hex) / 4361 (Decimal): Battery current (INT16S), 1 Word ￼

Unit: A ￼

Note: Positive is charge current, negative is discharge current ￼


0x110A (Hex) / 4362 (Decimal): Battery power (INT16S), 1 Word ￼

Unit: W ￼

Note: Positive/negative for power ￼


0x1111 (Hex) / 4369 (Decimal): AC output voltage (INT16U), 1 Word ￼

Unit: V ￼


0x1117 (Hex) / 4375 (Decimal): AC input voltage (INT16U), 1 Word ￼

Unit: V ￼


0x1119 (Hex) / 4377 (Decimal): AC input frequency (INT16U), 1 Word ￼

Unit: Hz ￼


0x111B (Hex) / 4379 (Decimal): AC output active power (INT16S), 1 Word ￼

Unit: W ￼


0x111C (Hex) / 4380 (Decimal): AC output apparent power (INT16U), 1 Word ￼

Unit: VA ￼


0x1120 (Hex) / 4384 (Decimal): Load percentage (INT16U), 1 Word ￼

Unit: % ￼


0x1126 (Hex) / 4390 (Decimal): PV input voltage (INT16U), 1 Word ￼

Unit: V ￼


0x112A (Hex) / 4394 (Decimal): PV input power (INT16S), 1 Word ￼

Unit: W ￼

3. Setting Data Registers  (Read/Write)￼

These registers are for configuring the inverter's behavior. Most are Read/Write (R/W).


0x211F (Hex) / 8479 (Decimal): Battery cut-off voltage (INT16U), 1 Word ￼

Unit: V ￼

Range examples: 3024 model: 21.0V-27.0V; 5048 model: 42.0V-54.0V ￼


0x2122 (Hex) / 8482 (Decimal): Battery CV charging voltage (INT16U), 1 Word ￼

Unit: V ￼

Range examples: 3024: 24.0V-30.0V; 5048: 48.0V-60.0V ￼


0x2123 (Hex) / 8483 (Decimal): Battery floating charging voltage (INT16U), 1 Word ￼

Unit: V ￼

Range examples: 3024: 24.0V-30.0V; 5048: 48.0V-50.0V ￼


0x2129 (Hex) / 8489 (Decimal): AC output frequency (INT8U), 1 Word ￼

Description: 0-50Hz / 1-60Hz ￼


0x212A (Hex) / 8490 (Decimal): Output source priority (INT8U), 1 Word ￼

Description:

0: Utility First

1: Solar First

2: Solar+Battery+Utility ￼


0x212B (Hex) / 8491 (Decimal): Application Mode (INT8U), 1 Word ￼

Description: APL/T-UPS ￼


0x212C (Hex) / 8492 (Decimal): Charging source priority (INT8U), 1 Word ￼

Description:

1: Solar First

2: Solar And Utility First

3: Solar Only ￼


0x212D (Hex) / 8493 (Decimal): Battery type (INT8U), 1 Word ￼

Description:

0: AGM Battery

1: Flooded Battery

2: User defined

3: LiFePo4 Battery ￼


0x212E (Hex) / 8494 (Decimal): Max charging current (INT8U), 1 Word ￼

Unit: A ￼

Range: 10A-100A ￼

0x2130 (Hex) / 8496 (Decimal): Max. AC charging current (INT8U), 1 Word ￼

Unit: A ￼

Range: 10A-100A ￼


0x2131 (Hex) / 8497 (Decimal): Buzzer enable (INT8U), 1 Word ￼

Description: 0: Disable / 1: Enable ￼


0x2133 (Hex) / 8499 (Decimal): OverLoad restart enable (INT8U), 1 Word ￼

Description: 0: Disable / 1: Enable ￼


0x2134 (Hex) / 8500 (Decimal): Over temperature restart enable (INT8U), 1 Word ￼

Description: 0: Disable / 1: Enable ￼


0x2135 (Hex) / 8501 (Decimal): LCD backlight enable (INT8U), 1 Word ￼

Description: 0: Disable / 1: Enable ￼


0x2137 (Hex) / 8503 (Decimal): OverLoad to bypass (INT8U), 1 Word ￼

Description: 0: Disable / 1: Enable ￼


0x2156 (Hex) / 8534 (Decimal): Battery back to charge voltage (INT16U), 1 Word ￼

Unit: V ￼

Range examples: 3024: 22.0V-27.0V; 5048: 44.0V-54.0V ￼


0x2159 (Hex) / 8537 (Decimal): Battery back to discharge voltage (INT16U), 1 Word ￼

Unit: V ￼

Note: If exceeds max value, displays FULL (e.g., 60.1V for 5048 model is FULL) ￼

Key Takeaways for Getting Data
Serial Port Settings: When connecting with screen or any Modbus RTU software, use:

/dev/ttyUSBx (replace x with your port number)


Baud Rate: 2400 ￼


Data Bits: 8 ￼


Parity: None ￼


Stop Bits: 1 ￼


Slave ID: You'll need to know the specific decimal ID (1-31) of your inverter. ￼


Function Code 0x03 for Reading: All the Information Data and Realtime Data are read-only and use Function Code 0x03 (Read Holding Registers). ￼

Addressing: The document uses Hexadecimal addresses (e.g., 0xF800, 0x1100). Most Modbus libraries/software will expect these as 0-based addresses for Holding Registers (4xxxx) or Input Registers (3xxxx).

Important: Even though the document lists these as "registers" and provides examples with Function Code 0x03 (Read Multiple Registers), which is typically for Holding Registers (4xxxx series), the addresses 0xF800 (63488 decimal) and 0x1100 (4352 decimal) are quite high. Sometimes manufacturers use custom addressing or map these to Holding Registers starting from 40001 (meaning address 0 in a 0-based system corresponds to 40001).

Recommendation: When using a Modbus client/library, try reading these addresses directly as Holding Registers. If that doesn't work, try as Input Registers.

Data Interpretation:


Word Size: Each register holds a 16-bit word (two bytes). ￼


Byte Order: "first byte is high bit and second byte is low bit" (Big-Endian for a single 16-bit word).  This means if you read ￼

0xAD (high) and 0x01 (low), the value is 0xAD01.

Multi-Word Values: For Serial number (5 words) or potentially 32-bit values if registers are combined, you'll need to pay attention to how they are arranged (e.g., which word is the most significant). The document implies standard 16-bit registers.

Signed vs. Unsigned: INT16U means 16-bit unsigned integer. INT16S means 16-bit signed integer. This affects how values like Battery current (which can be positive or negative) are interpreted.

This protocol document gives you everything you need to start programming your master (client) or configuring a Modbus polling software.

