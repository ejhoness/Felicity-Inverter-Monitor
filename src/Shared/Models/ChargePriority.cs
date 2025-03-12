namespace InverterMon.Shared.Models;

public static class ChargePriority
{
    public const string SolarFirst = "1";
    public const string SolarAndUtility = "2";
    public const string OnlySolar = "3";
    public const string UtilityFirst = "0";
}