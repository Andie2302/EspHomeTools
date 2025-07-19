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
    /// <summary>
    /// Represents a YAML scalar node containing a secret value.
    /// </summary>
    /// <remarks>
    /// The <c>YamlSecret</c> class extends the functionality of the <c>YamlScalar&lt;string&gt;</c> class
    /// to handle secret values in YAML. It formats the value using the syntax <c>!secret [value]</c>
    /// to comply with the YAML secret representation convention.
    /// </remarks>
    public YamlSecret(string secretName) => Value = secretName;

    /// <summary>
    /// Serializes the value of the scalar node into its YAML representation.
    /// </summary>
    /// <returns>
    /// A string representation of the scalar value formatted according to YAML syntax.
    /// </returns>
    protected override string SerializeValue()
    {
        return $"!secret {Value}";
    }
}