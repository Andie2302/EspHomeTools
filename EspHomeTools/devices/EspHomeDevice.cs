using EspHomeTools.sections;
using EspHomeTools.values;
using YamlDotNet.Serialization;

namespace EspHomeTools.devices;

public record EspHomeDevice
{
    [YamlMember(Alias = "esphome")]
    public EspHomeSection SectionEspHome { get; init; } = new();

    [YamlMember(Alias = "substitutions")]
    public Dictionary<string, EspHomeValue> Substitutions { get; init; } = [];

    [YamlMember(Alias = "wifi")]
    public WifiSection? Wifi { get; init; } = new();

    [YamlMember(Alias = "sensor")]
    public List<Dictionary<string, object>> Sensors { get; init; } = [];
}