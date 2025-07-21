using System;
using System.Linq;

namespace EspHomeTools.Classes;

public static class YamlTools
{
    public static readonly char[] SpecialYamlChars = [':', '{', '}', '[', ']', ',', '&', '*', '#', '?', '|', '-', '<', '>', '!', '%', '@', '`'];

    public static bool NeedsQuoting(string? str)
    {
        if (IsNullOrWhitespace(str)) return true;
        var trimmedStr = (str ?? string.Empty).Trim();
        return ContainsSpecialYamlCharacters(trimmedStr) || IsNumericValue(trimmedStr) || IsBooleanValue(trimmedStr) || IsNullValue(trimmedStr);
    }
    private static bool IsNullOrWhitespace(string? str) => str == null || string.IsNullOrWhiteSpace(str);
    private static bool ContainsSpecialYamlCharacters(string str) => str.Any(c => SpecialYamlChars.Contains(c));
    private static bool IsNumericValue(string str) => double.TryParse(str, out _);
    private static bool IsBooleanValue(string str) => bool.TryParse(str, out _);
    private static bool IsNullValue(string str) => str.Equals("null", StringComparison.OrdinalIgnoreCase);
}