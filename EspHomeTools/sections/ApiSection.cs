namespace EspHomeTools.sections;

public record ApiSection
{
    // Die neue Verschachtelung für den Encryption Key
    public ApiEncryption Encryption { get; init; } = new();
    
    public int? Port { get; init; }
}

public record ApiEncryption
{
    public string? Key { get; init; } = "${api_key}";
}