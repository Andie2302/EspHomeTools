using System;
using System.Collections.Generic;
using System.Linq;
using EspHomeTools.Classes.Scalars;
using EspHomeTools.Interfaces.Fluent;

namespace EspHomeTools.Classes;

public interface IWithPassword<out T> : IWithPassword
{
    T WithPassword(string password);
    T WithPassword(string password, bool isSecret);
    T WithPassword(YamlString password);
    T WithPassword(YamlString password, bool isSecret);
    T WithPassword(YamlSecret password);
}
public interface IWithSsid<out T> : IWithSsid
{
    T WithSsid(string ssid);
    T WithSsid(string ssid, bool isSecret);
    T WithSsid(YamlString ssid);
    T WithSsid(YamlString ssid, bool isSecret);
    T WithSsid(YamlSecret ssid);
}
public interface IWithComment<out T> : IWithComment
{
    T WithComment(string ssid);
    T WithComment(YamlString ssid);
}

public static class YamlTools
{
    private readonly static char[] SpecialYamlChars = [':', '{', '}', '[', ']', ',', '&', '*', '#', '?', '|', '-', '<', '>', '!', '%', '@', '`'];

    private readonly static HashSet<char> SpecialYamlCharSet = [..SpecialYamlChars];

    private readonly static string[] DefaultYamlElements =
    [
        "substitutions",
        "packages",
        "esphome",
        "esp8266",
        "esp32",
        "rp2040",
        "bk72xx",
        "rtl87xx",
        "wifi",
        "api",
        "ota",
        "time",
        "web_server",
        "mqtt",
        "captive_portal",
        "logger",
        "i2c",
        "spi",
        "uart",
        "sensor",
        "switch",
        "binary_sensor",
        "output",
        "light"
    ];

    public static bool NeedsQuoting(string? str)
    {
        var trimmedStr = Normalize(str);
        return ContainsSpecialYamlCharacters(trimmedStr) || IsNumericValue(trimmedStr) || IsBooleanValue(trimmedStr) || IsNullValue(trimmedStr);
    }

    private static bool ContainsSpecialYamlCharacters(string str) => str.Any(c => SpecialYamlCharSet.Contains(c));

    private static bool IsNumericValue(string str) => double.TryParse(str, out _);

    private static bool IsBooleanValue(string str) => bool.TryParse(str, out _);

    private static bool IsNullValue(string str) => str.Equals("null", StringComparison.OrdinalIgnoreCase);

    public static IEnumerable<string> DefaultYamlCollectionOrder() => DefaultYamlElements;

    private const string Quote = "\"";

    private const string EscapedQuote = "\\\"";

    public static string CreateQuotedValue(string value) => Quote + value.Replace(Quote, EscapedQuote) + Quote;

    public static string Normalize(string? value) => value?.Trim() ?? string.Empty;
}