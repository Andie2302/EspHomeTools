namespace EspHomeTools.sections;

public record WifiSection
{
    public string Ssid { get; init; } =  "${wifi_ssid}";
    public string Password { get; init; } =  "${wifi_password}";
    public ApSection? Ap { get; init; } = new();
}