using InverterMon.Server.Persistence;
using InverterMon.Server.Persistence.Settings;

namespace InverterMon.Server.InverterService;

class StatusRetriever(Database db, FelicitySolarInverter inverter, UserSettings userSettings) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken c)
    {
        while (!c.IsCancellationRequested)
        {
            inverter.Status.BatteryCapacity = userSettings.BatteryCapacity;
            inverter.Status.PV_MaxCapacity = userSettings.PV_MaxCapacity;

            //todo: get data from inverter and map to CurrentStatus.Result

            db.UpdateTodaysPvGeneration(c);

            await Task.Delay(2000);
        }
    }
}