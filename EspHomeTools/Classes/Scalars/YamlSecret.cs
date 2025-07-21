namespace EspHomeTools.Classes.Scalars;

/// <summary>
/// Represents a YAML scalar node specifically designed to hold sensitive configuration secrets.
/// </summary>
/// <remarks>
/// The YamlSecret class inherits from the YamlScalar base class and is used to manage and serialize
/// secret values in YAML. It formats the serialized output as "!secret [value]" to comply with YAML secret-handling conventions.
/// This class ensures that secrets are represented in a consistent and secure way in YAML files.
/// </remarks>

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