using System;
using System.Linq;

namespace EspHomeTools.Classes;

/// <summary>
/// Provides utility methods for processing YAML data.
/// </summary>
/// <remarks>
/// This class contains methods and properties to assist in handling YAML-specific requirements such as detecting
/// the need for quoting or special processing of strings. It is designed to operate on YAML content and ensures
/// compatibility with YAML formatting rules.
/// </remarks>
public static class YamlTools
{
    /// <summary>
    /// Represents a set of special characters defined in the YAML specification.
    /// These characters have specific meanings in YAML and are used to determine
    /// whether a string requires quoting when serialized.
    /// </summary>
    public readonly static char[] SpecialYamlChars = [':', '{', '}', '[', ']', ',', '&', '*', '#', '?', '|', '-', '<', '>', '!', '%', '@', '`'];

    /// <summary>
    /// Determines whether the given string requires quoting to be valid YAML syntax.
    /// </summary>
    /// <param name="str">The string to be checked for special YAML characters or reserved words.</param>
    /// <returns>
    /// True if the string needs quoting due to the presence of special YAML characters, being a numeric or boolean value,
    /// or matching reserved words such as "null". False otherwise.
    /// </returns>
    public static bool NeedsQuoting(string? str)
    {
        if (str==null||string.IsNullOrWhiteSpace(str)) return true;
        return str.Any(c => SpecialYamlChars.Contains(c)) || double.TryParse(str, out _) || bool.TryParse(str, out _) || str.Equals("null", StringComparison.OrdinalIgnoreCase);
    }
}