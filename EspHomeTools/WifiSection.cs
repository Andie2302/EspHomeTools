namespace EspHomeTools;

public record WifiSection
{
    public string Ssid { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;
    public ApSection? Ap { get; init; } = new ApSection();
}