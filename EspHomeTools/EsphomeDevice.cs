using YamlDotNet.Serialization;

namespace EspHomeTools;

public record EsphomeDevice
{
    [YamlMember(Alias = "esphome")]
    public EsphomeSection SectionEsphome { get; init; } = new();

    [YamlMember(Alias = "substitutions")]
    public Dictionary<string, EsphomeValue> Substitutions { get; init; } = [];

    [YamlMember(Alias = "wifi")]
    public WifiSection? Wifi { get; init; } = new();

    [YamlMember(Alias = "sensor")]
    public List<Dictionary<string, object>> Sensors { get; init; } = [];
}