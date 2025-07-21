using System;
using System.Linq;

namespace EspHomeTools.Classes;

/// <summary>
/// Provides utility functions for handling YAML strings and determining specific properties
/// of strings, such as whether they need quoting based on YAML syntax.
/// </summary>
public static class YamlTools
{
    /// <summary>
    /// Defines characters that are considered special in YAML and may require
    /// strings containing them to be quoted during serialization or parsing.
    /// </summary>
    public readonly static char[] SpecialYamlChars = [':', '{', '}', '[', ']', ',', '&', '*', '#', '?', '|', '-', '<', '>', '!', '%', '@', '`'];

    /// <summary>
    /// Determines if a given string requires quotes based on YAML syntax rules.
    /// </summary>
    /// <param name="str">The string to evaluate. Can be null or empty.</param>
    /// <returns>
    /// Returns <c>true</c> if the string requires quoting according to YAML rules.
    /// This includes cases where the string contains special YAML characters, resembles a numeric or boolean value,
    /// represents a null value, or is null or whitespace. Returns <c>false</c> otherwise.
    /// </returns>
    public static bool NeedsQuoting(string? str)
    {
        if (IsNullOrWhitespace(str)) return true;
        var trimmedStr = (str ?? string.Empty).Trim();
        return ContainsSpecialYamlCharacters(trimmedStr) || IsNumericValue(trimmedStr) || IsBooleanValue(trimmedStr) || IsNullValue(trimmedStr);
    }
    /// <summary>
    /// Determines whether the specified string is null, empty, or consists only of white-space characters.
    /// </summary>
    /// <param name="str">The string to check, which may be null.</param>
    /// <returns>True if the string is null, empty, or contains only white-space characters; otherwise, false.</returns>
    private static bool IsNullOrWhitespace(string? str) => str == null || string.IsNullOrWhiteSpace(str);
    /// <summary>
    /// Determines if the given string contains any special characters used in YAML syntax.
    /// </summary>
    /// <param name="str">The input string to examine for special YAML characters.</param>
    /// <returns>True if the string contains any special YAML characters; otherwise, false.</returns>
    private static bool ContainsSpecialYamlCharacters(string str) => str.Any(c => SpecialYamlChars.Contains(c));
    /// <summary>
    /// Determines whether the given string represents a numeric value.
    /// </summary>
    /// <param name="str">The input string to check.</param>
    /// <returns>
    /// True if the string can be parsed as a numeric value; otherwise, false.
    /// </returns>
    private static bool IsNumericValue(string str) => double.TryParse(str, out _);
    /// <summary>
    /// Determines whether the given string represents a valid boolean value (true or false).
    /// </summary>
    /// <param name="str">The input string to evaluate.</param>
    /// <returns>
    /// Returns true if the string represents a valid boolean value; otherwise, returns false.
    /// </returns>
    private static bool IsBooleanValue(string str) => bool.TryParse(str, out _);
    /// <summary>
    /// Determines whether the specified string represents a null value in YAML syntax.
    /// </summary>
    /// <param name="str">The input string to evaluate for null equivalence.</param>
    /// <returns>
    /// true if the string represents a null value (e.g., "null", case-insensitive); otherwise, false.
    /// </returns>
    private static bool IsNullValue(string str) => str.Equals("null", StringComparison.OrdinalIgnoreCase);
}