using System.Collections.Generic;
using System.Text;
namespace EspHomeTools.Classes.ExtensionMethods;
public static class StringExtensions
{
    private const int StringBuilderCapacityMultiplier = 2;
    private readonly static Dictionary<char, string> GermanUmlautReplacements = new()
    {
        { 'Ä', "ae" }, { 'ä', "ae" },
        { 'Ö', "oe" }, { 'ö', "oe" },
        { 'Ü', "ue" }, { 'ü', "ue" },
        { 'ß', "ss" }
    };

    public static string ReplaceUmlauts(this string input)
    {
        if (string.IsNullOrEmpty(input)) return input;
        var result = new StringBuilder(input.Length * StringBuilderCapacityMultiplier);
        foreach (var character in input)
        {
            if (GermanUmlautReplacements.TryGetValue(character, out var replacement))
            {
                result.Append(replacement);
            }
            else
            {
                result.Append(character);
            }
        }
        return result.ToString();
    }
    public static string ToLowerCaseWithoutUmlauts(this string input)
    {
        return input.ReplaceUmlauts().ToLowerInvariant();
    }
}