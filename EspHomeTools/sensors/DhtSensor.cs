namespace EspHomeTools.sensors;

public record DhtSensor : SensorBase
{
    public override string Platform => "dht";

    public int Pin { get; init; }
    public string? Model { get; init; }
}