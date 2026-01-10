// EspHomeTools/devices/EspHomeDevice.cs
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

    // --- Hardware Plattformen (Nur eine sollte aktiv sein) ---

    [YamlMember(Alias = "esp32", Order = 3)]
    public Esp32Section? Esp32 { get; init; }

    [YamlMember(Alias = "esp32_s2", Order = 4)]
    public Esp32S2Section? Esp32S2 { get; init; }

    [YamlMember(Alias = "esp32_s3", Order = 5)]
    public Esp32S3Section? Esp32S3 { get; init; }

    [YamlMember(Alias = "esp32_c3", Order = 6)]
    public Esp32C3Section? Esp32C3 { get; init; }

    [YamlMember(Alias = "esp32_c6", Order = 7)]
    public Esp32C6Section? Esp32C6 { get; init; }

    [YamlMember(Alias = "esp8266", Order = 8)]
    public Esp8266Section? Esp8266 { get; init; }

    [YamlMember(Alias = "rp2040", Order = 9)]
    public Rp2040? Rp2040 { get; init; }

    [YamlMember(Alias = "rtl87xx", Order = 10)]
    public Rtl87Xx? Rtl87Xx { get; init; }

    [YamlMember(Alias = "bk72xx", Order = 11)]
    public Bk72Xx? Bk72Xx { get; init; }

    [YamlMember(Alias = "ln882x", Order = 12)]
    public Ln882X? Ln882X { get; init; }

    [YamlMember(Alias = "raspberry_pi_pico_w", Order = 13)]
    public RaspBerryPicoW? RaspBerryPicoW { get; init; }

    // --- Standard Sektionen ---

    [YamlMember(Alias = "logger", Order = 20)]
    public LoggerSection? Logger { get; init; } = new();

    [YamlMember(Alias = "api", Order = 21)]
    public ApiSection? Api { get; init; } = new();

    [YamlMember(Alias = "ota", Order = 22)]
    public List<OtaSection>? Ota { get; init; } = [new OtaSection()];

    [YamlMember(Alias = "web_server", Order = 23)]
    public WebServerSection? WebServer { get; init; } = new();

    [YamlMember(Alias = "wifi", Order = 30)]
    public WifiSection? Wifi { get; init; } = new();

    [YamlMember(Alias = "captive_portal", Order = 31)]
    public CaptivePortalSection? CaptivePortal { get; init; } = new();

    [YamlMember(Alias = "sensor", Order = 40)]
    public List<SensorBase> Sensors { get; init; } = [];
}