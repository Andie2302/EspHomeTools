namespace EspHomeTools.sections;

public record ApiSection
{
    public string? Password { get; init; }
    public int? Port { get; init; }
}