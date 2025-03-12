using InverterMon.Server.Persistence;
using InverterMon.Server.Persistence.Settings;

namespace InverterMon.Server.InverterService;

class StatusRetriever(Database db, CurrentStatus currentStatus, UserSettings userSettings) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken c)
    {
        while (!c.IsCancellationRequested)
        {
            currentStatus.Result.BatteryCapacity = userSettings.BatteryCapacity;
            currentStatus.Result.PV_MaxCapacity = userSettings.PV_MaxCapacity;

            //todo: get data from inverter and map to CurrentStatus.Result

            _ = db.UpdateTodaysPvGeneration(currentStatus, c);

            await Task.Delay(2000);
        }
    }
}