using YamlDotNet.Serialization;

namespace EspHomeTools;

public record EsphomeValue
{
    public object? Value { get; init; }
    public bool IsSecret { get; init; }

    public static implicit operator EsphomeValue(string s) => new(s,false) { Value = s };
    public static implicit operator EsphomeValue(int i) => new(i,false) { Value = i };

    public EsphomeValue(object value, bool isSecret)
    {
        Value = value;
        IsSecret = isSecret;
    }
}

public record EsphomeDevice
{
    [YamlMember(Alias = "esphome")]
    public EsphomeSection SectionEsphome { get; init; } = new();

    [YamlMember(Alias = "substitutions")]
    public Dictionary<string, EsphomeValue> Substitutions { get; init; } = [];

    [YamlMember(Alias = "wifi")]
    public WifiSection Wifi { get; init; } = new WifiSection();

    [YamlMember(Alias = "sensor")]
    public List<Dictionary<string, object>> Sensors { get; init; } = [];
}

public record EsphomeSection
{
    public string Name { get; init; } = "default-node";
    public string FriendlyName { get; init; } = string.Empty;
}
public record WifiSection
{
    public string Ssid { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;
    public ApSection? Ap { get; init; } = new ApSection();
}
public record ApSection
{
    public string Ssid { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;
}