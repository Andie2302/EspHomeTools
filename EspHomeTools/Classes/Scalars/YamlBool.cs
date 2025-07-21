namespace EspHomeTools.Classes.Scalars;

/// <summary>
/// Represents a YAML-compatible boolean scalar type.
/// </summary>
/// <remarks>
/// The <c>YamlBool</c> class provides an implementation for handling boolean scalar values
/// in YAML syntax. It inherits from <c>YamlScalar&lt;bool&gt;</c>, allowing for serialization
/// and deserialization of boolean values in a structured YAML format.
/// </remarks>
public class YamlBool : YamlScalar<bool>
{
    /// <summary>
    /// Represents a YAML scalar value encapsulating a boolean type.
    /// Provides functionality to define and serialize boolean values as part of a YAML structure.
    /// </summary>
    public YamlBool(bool value)
    {
        Value = value;
    }

    /// <summary>
    /// Serializes the boolean value to its YAML string representation.
    /// </summary>
    /// <returns>
    /// A lowercase string that represents the boolean value ("true" or "false") in YAML format.
    /// </returns>
    protected override string SerializeValue() => Value.ToString().ToLowerInvariant();
}