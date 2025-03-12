using InverterMon.Server.InverterService;
using InverterMon.Server.Persistence.PVGen;
using InverterMon.Server.Persistence.Settings;
using LiteDB;

namespace InverterMon.Server.Persistence;

public class Database
{
    readonly LiteDatabase _db;
    readonly CurrentStatus _currentStatus;
    readonly UserSettings _settings;
    readonly ILiteCollection<PVGeneration> _pvGenCollection;
    readonly ILiteCollection<UserSettings> _usrSettingsCollection;
    PVGeneration? _today;

    public Database(IHostApplicationLifetime lifetime, CurrentStatus status, UserSettings settings)
    {
        _currentStatus = status;
        _settings = settings;
        _db = new("InverterMon.db") { CheckpointSize = 0 };
        lifetime.ApplicationStopping.Register(() => _db?.Dispose());
        _pvGenCollection = _db.GetCollection<PVGeneration>();
        _usrSettingsCollection = _db.GetCollection<UserSettings>();
        RestoreTodaysPvWattHours();
        RestoreUserSettings();
    }

    public void RestoreTodaysPvWattHours()
    {
        var todayDayNumber = DateOnly.FromDateTime(DateTime.Now).DayNumber;

        _today = _pvGenCollection
                 .Query()
                 .Where(pg => pg.Id == todayDayNumber)
                 .SingleOrDefault();

        if (_today is not null)
            _currentStatus.Result.RestorePVWattHours(_today.TotalWattHours);
        else
        {
            _today = new() { Id = todayDayNumber };
            _today.SetTotalWattHours(0);
            _currentStatus.Result.RestorePVWattHours(0);
            _pvGenCollection.Insert(_today);
            _db.Checkpoint();
        }
    }

    public async Task UpdateTodaysPvGeneration(CurrentStatus cmd, CancellationToken c)
    {
        var hourNow = DateTime.Now.Hour;

        if (hourNow < _settings.SunlightStartHour || hourNow >= _settings.SunlightEndHour)
            return;

        var todayDayNumber = DateOnly.FromDateTime(DateTime.Now).DayNumber;

        if (_today?.Id == todayDayNumber)
        {
            _today.SetWattPeaks(cmd.Result.PVInputWatt);
            _today.SetTotalWattHours(cmd.Result.PVInputWattHour);
            _pvGenCollection.Update(_today);
        }
        else
        {
            cmd.Result.ResetPVWattHourAccumulation(); //it's a new day. start accumulation from scratch.
            _today = new() { Id = todayDayNumber };
            _today.SetTotalWattHours(0);
            _pvGenCollection.Insert(_today);
        }
        _db.Checkpoint();
    }

    public PVGeneration? GetPvGenForDay(int dayNumber)
        => _pvGenCollection.FindOne(p => p.Id == dayNumber);

    public void RestoreUserSettings()
    {
        var settings = _usrSettingsCollection.FindById(1);

        if (settings is not null)
        {
            _settings.PV_MaxCapacity = settings.PV_MaxCapacity;
            _settings.BatteryCapacity = settings.BatteryCapacity;
            _settings.BatteryNominalVoltage = settings.BatteryNominalVoltage;
            _settings.SunlightStartHour = settings.SunlightStartHour;
            _settings.SunlightEndHour = settings.SunlightEndHour;
        }
        else
        {
            _usrSettingsCollection.Insert(_settings);
            _db.Checkpoint();
        }
    }

    public void UpdateUserSettings(UserSettings settings)
    {
        _usrSettingsCollection.Update(settings);
        _db.Checkpoint();
    }
}