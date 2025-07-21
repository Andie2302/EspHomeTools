namespace EspHomeTools.Classes.Scalars;

public class YamlSecret : YamlScalar<string>
{
    public YamlSecret(string secretName, string? name = null, string? comment = null, string? tag = null)
    {
        Value = secretName;
        Name = name;
        Comment = comment;
        Tag = tag;
    }

    protected override string SerializeValue() => $"!secret {Value}";
}