namespace EspHomeTools.Enums;

public static class YamlKeysExtensions
{
    public static string GetKeyString(this YamlKeys key) => key.ToString().ToLowerInvariant();
}