namespace EspHomeTools.Classes.Scalars;

/// <summary>
/// Represents a YAML scalar node containing sensitive configuration data marked as a secret.
/// </summary>
/// <remarks>
/// This class inherits from the YamlScalar base class and is specialized for securely managing secret values
/// in YAML files. Secrets are serialized using the "!secret [value]" format, ensuring compatibility with
/// YAML-based configuration systems that handle sensitive data.
/// </remarks>
public class YamlSecret : YamlScalar<string>
{
    /// <summary>
    /// Represents a YAML secret scalar value.
    /// </summary>
    /// <remarks>
    /// This class is used to handle YAML scalar nodes that contain secret information.
    /// When serialized, it formats the scalar using a "!secret" tag followed by the secret value.
    /// </remarks>
    public YamlSecret(string secretName, string? name = null, string? comment = null, string? tag = null)
    {
        Value = secretName;
        Name = name;
        Comment = comment;
        Tag = tag;
    }

    /// <summary>
    /// Serializes the scalar value into a YAML-compatible string representation.
    /// </summary>
    /// <returns>
    /// The serialized string representation of the scalar value.
    /// </returns>
    protected override string SerializeValue() => $"!secret {Value}";
}