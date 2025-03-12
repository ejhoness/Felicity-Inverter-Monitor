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