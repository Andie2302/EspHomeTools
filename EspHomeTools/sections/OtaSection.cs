namespace EspHomeTools.sections;

public record OtaSection
{
    public string? Password { get; init; }
    public string? Platform { get; init; }
}