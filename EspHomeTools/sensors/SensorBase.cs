
using YamlDotNet.Serialization;

namespace EspHomeTools.sensors;

public abstract record SensorBase
{
    [YamlMember(Order = -1)] // Plattform immer ganz oben
    public abstract string Platform { get; }

    public string? Name { get; init; }
    
    [YamlMember(Alias = "update_interval")]
    public string? UpdateInterval { get; init; }
}


public record DhtSensor : SensorBase
{
    public override string Platform => "dht";

    public int Pin { get; init; }
    public string? Model { get; init; } // z.B. "DHT22"
}


public record UptimeSensor : SensorBase
{
    public override string Platform => "uptime";
}