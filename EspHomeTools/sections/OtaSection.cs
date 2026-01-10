namespace EspHomeTools.sections;

public record OtaSection
{
    public string? Password { get; init; } = "${ota_password}";
    public string? Platform { get; init; } = "esphome";
}