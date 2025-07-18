using System;
using System.Linq;

namespace EspHomeTools.Classes;

public class YamlString : YamlScalar<string>
{
    public YamlString(string value) => Value = value;
    protected override string SerializeValue()
    {
        var value = (Value??string.Empty).Trim();
        return YamlTools.NeedsQuoting(value) ? $"\"{value.Replace("\"", "\\\"")}\"" : value;
    }
}
public static class YamlTools
{
    public readonly static char[] SpecialYamlChars = [':', '{', '}', '[', ']', ',', '&', '*', '#', '?', '|', '-', '<', '>', '!', '%', '@', '`'];
    public static bool NeedsQuoting(string? str)
    {
        if (str==null||string.IsNullOrWhiteSpace(str)) return true;
        return str.Any(c => SpecialYamlChars.Contains(c)) || double.TryParse(str, out _) || bool.TryParse(str, out _) || str.Equals("null", StringComparison.OrdinalIgnoreCase);
    }
}