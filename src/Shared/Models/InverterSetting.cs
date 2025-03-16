namespace InverterMon.Shared.Models;

public enum Setting : ushort
{
    ChargePriority = 0x212C,
    OutputPriority = 0x212A,
    CombinedChargeCurrent = 0x212E,
    UtilityChargeCurrent = 0x2130,
    BulkVoltage = 0x2122,
    FloatVoltage = 0x2123,
    DischargeCutOff = 0x211F,
    BackToGrid = 0x2156,
    BackToBattery = 0x2159
}

public enum WorkingMode : short
{
    POWER = 0,
    STANDBY = 1,
    BYPASS = 2,
    BATTERY = 3,
    FAULT = 4,
    LINE = 5,
    CHARGING = 6
}