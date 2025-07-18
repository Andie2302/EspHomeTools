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