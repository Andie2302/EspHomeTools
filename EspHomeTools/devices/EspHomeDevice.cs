namespace EspHomeTools.devices;

using sections;
using sensors;
using YamlDotNet.Serialization;

public record EspHomeDevice
{
    [YamlMember(Alias = "substitutions", Order = 1)]
    public SubstitutionsSection Substitutions { get; init; } = new();

    [YamlMember(Alias = "esphome", Order = 2)]
    public EspHomeSection SectionEspHome { get; init; } = new();

    [YamlMember(Alias = "esp32", Order = 3)]
    public Dictionary<string, object>? Esp32 { get; init; }

    [YamlMember(Alias = "logger", Order = 10)]
    public LoggerSection? Logger { get; init; } = new();

    [YamlMember(Alias = "api", Order = 11)]
    public ApiSection? Api { get; init; } = new();

    [YamlMember(Alias = "ota", Order = 12)]
    public OtaSection? Ota { get; init; } = new();

    [YamlMember(Alias = "web_server", Order = 13)]
    public WebServerSection? WebServer { get; init; } = new();

    [YamlMember(Alias = "wifi", Order = 20)]
    public WifiSection? Wifi { get; init; } = new();

    [YamlMember(Alias = "captive_portal", Order = 21)]
    public CaptivePortalSection? CaptivePortal { get; init; } = new();

    [YamlMember(Alias = "sensor", Order = 30)]
    public List<SensorBase> Sensors { get; init; } = [];
}