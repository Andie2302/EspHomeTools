namespace EspHomeTools.sensors;

public record UptimeSensor : SensorBase
{
    public override string Platform => "uptime";
}