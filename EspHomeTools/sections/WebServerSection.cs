namespace EspHomeTools.sections;

public record WebServerSection
{
    public int? Port { get; init; }
    public bool? Version { get; init; }
}