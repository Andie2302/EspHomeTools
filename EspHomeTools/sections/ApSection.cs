namespace EspHomeTools.sections;

public record ApSection
{
    public string Ssid { get; init; } = "${ap_ssid}";
    public string Password { get; init; } = "${ap_password}";
}