namespace InverterMon.Server.Endpoints.Settings.SetSettingValue;

public class Endpoint : Endpoint<Shared.Models.SetSetting, bool>
{
    public override void Configure()
    {
        Get("settings/set-setting/{Command}/{Value}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(Shared.Models.SetSetting r, CancellationToken c)
    {
        //todo: set settings using inveter

        // var cmd = new InverterService.Commands.SetSetting(r.Command, r.Value);
        // Queue.AddCommands(cmd);
        // await cmd.WhileProcessing(c);
        // await SendAsync(cmd.Result);
    }
}