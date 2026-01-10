
using YamlDotNet.Serialization;

namespace EspHomeTools.sensors;

public abstract record SensorBase
{
    [YamlMember(Order = -1)] public abstract string Platform { get; }

    public string? Name { get; init; }
    
    [YamlMember(Alias = "update_interval")]
    public string? UpdateInterval { get; init; }
}