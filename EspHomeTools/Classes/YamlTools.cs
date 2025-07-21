using System;
using System.Collections.Generic;
using System.Linq;

namespace EspHomeTools.Classes;

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

    public static bool NeedsQuoting(string str)
    {
        var trimmedStr = str.Trim();
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