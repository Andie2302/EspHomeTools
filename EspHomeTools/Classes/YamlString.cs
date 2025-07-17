using System;
using System.Linq;

namespace EspHomeTools.Classes;

public class YamlString : YamlScalarBase<string>
{
    private readonly static char[] SpecialYamlChars = [':', '{', '}', '[', ']', ',', '&', '*', '#', '?', '|', '-', '<', '>', '!', '%', '@', '`'];

    public YamlString(string value)
    {
        Value = value;
    }

    protected override string SerializeValue() => NeedsQuoting(Value ??= "null") ? $"\"{Value.Replace("\"", "\\\"")}\"" : Value;

    private bool NeedsQuoting(string str)
    {
        if (string.IsNullOrEmpty(str)) return true;
        return str.Any(c => SpecialYamlChars.Contains(c)) || double.TryParse(str, out _) || bool.TryParse(str, out _) || str.Equals("null", StringComparison.OrdinalIgnoreCase);
    }
}