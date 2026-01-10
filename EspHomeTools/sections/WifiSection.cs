namespace EspHomeTools.sections;

public record WifiSection
{
    public string Ssid { get; init; } =  "${wifi_ssid}";
    public string Password { get; init; } =  "${wifi_password}";
    public ApSection? Ap { get; init; } = new();
}


public abstract record PlatformSectionBase
{
    public string Board { get; init; } = string.Empty;
    public string? Framework { get; init; } // Optional: arduino oder esp-idf
}


public record Esp32Section : PlatformSectionBase { }
public record Esp32S2Section : PlatformSectionBase { }
public record Esp32S3Section : PlatformSectionBase { }
public record Esp32C3Section : PlatformSectionBase { }
public record Esp32C6Section : PlatformSectionBase { }
public record Esp8266Section : PlatformSectionBase { }
public record Rp2040 : PlatformSectionBase { }
public record Rtl87Xx : PlatformSectionBase { }
public record Bk72Xx : PlatformSectionBase {}
public record Ln882X : PlatformSectionBase {}
public record RaspBerryPicoW : PlatformSectionBase { }

