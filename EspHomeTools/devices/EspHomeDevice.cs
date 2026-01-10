using EspHomeTools.sections;
using EspHomeTools.values;
using YamlDotNet.Serialization;

namespace EspHomeTools.devices;

public record EspHomeDevice
{
    [YamlMember(Alias = "esphome", Order = 0)]
    public EspHomeSection SectionEspHome { get; init; } = new();

    [YamlMember(Alias = "substitutions", Order = -1)]
    public Dictionary<string, EspHomeValue> Substitutions { get; init; } = [];

    [YamlMember(Alias = "wifi", Order = 1)]
    public WifiSection? Wifi { get; init; } = new();

    [YamlMember(Alias = "sensor")]
    public List<Dictionary<string, object>> Sensors { get; init; } = [];
}