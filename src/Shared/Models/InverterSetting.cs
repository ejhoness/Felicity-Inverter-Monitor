namespace InverterMon.Shared.Models;

public enum Setting
{
    ChargePriority = 1,
    OutputPriority = 2,
    CombinedChargeCurrent = 3,
    UtilityChargeCurrent = 4,
    BulkVoltage = 5,
    FloatVoltage = 6,
    DischargeCutOff = 7,
    BackToGrid = 8,
    BackToBattery = 9
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