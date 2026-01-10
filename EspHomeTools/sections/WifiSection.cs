namespace EspHomeTools.sections;

public record WifiSection
{
    public string Ssid { get; init; } =  "${wifi_ssid}";
    public string Password { get; init; } =  "${wifi_password}";
    public ApSection? Ap { get; init; } = new();
}


public record PlatformSectionBase
{
    public string Board { get; init; } = string.Empty;
    public string? Framework { get; init; } // Optional: arduino oder esp-idf
}


public record Esp32Section : PlatformSectionBase
{
    public Esp32Section()
    {
        Board = "esp32dev";
        Framework = "esp-idf";
    }
}

public record Esp32S2Section : PlatformSectionBase
{
    public Esp32S2Section()
    {
        Board = "esp32-s2-saola-1";
        Framework = "esp-idf";
    }
}

public record Esp32S3Section : PlatformSectionBase
{
    public Esp32S3Section()
    {
        Board = "esp32-s3-devkitc-1";
        Framework = "esp-idf";
    }
}

public record Esp32C3Section : PlatformSectionBase
{
    public Esp32C3Section()
    {
        Board = "esp32-c3-devkitm-1";
        Framework = "esp-idf";
    }
}

public record Esp32C6Section : PlatformSectionBase
{
    public Esp32C6Section()
    {
        Board = "esp32-c6-devkitc-1";
        Framework = "esp-idf";
    }
}

public record Esp8266Section : PlatformSectionBase
{
    public Esp8266Section()
    {
        Board = "esp01_1m";
        Framework = null;
    }
}

public record Rp2040 : PlatformSectionBase
{
    public Rp2040()
    {
        Board = "rp2040";
        Framework = null;
    }
}

public record Rtl87Xx : PlatformSectionBase
{
    public Rtl87Xx()
    {
        Board = "bw12";
        Framework = null;
    }
}

public record Bk72Xx : PlatformSectionBase
{
    public Bk72Xx()
    {
        Board = "cb1s";
        Framework = null;
    }
}

public record Ln882X : PlatformSectionBase
{
    public Ln882X()
    {
        Board = "generic-ln882hki"; // ln-02   wl2s
        Framework = null;
    }
}

public record RaspBerryPicoW: PlatformSectionBase
{
    public RaspBerryPicoW()
    {
        Board = "rpipicow";
        Framework = null;
    }
}

