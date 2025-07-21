namespace EspHomeTools.Classes.Scalars;

/// <summary>
/// Represents a YAML-compatible boolean scalar type.
/// </summary>
/// <remarks>
/// The <c>YamlBool</c> class inherits from <c>YamlScalar&lt;bool&gt;</c> and provides
/// an implementation of a YAML boolean representation. It encapsulates a boolean value
/// and provides functionality for serialization to a YAML format.
/// </remarks>
public class YamlBool : YamlScalar<bool>
{
    /// <summary>
    /// Initializes a new instance of the YamlBool class with the specified boolean value.
    /// </summary>
    /// <param name="value">The boolean value to encapsulate in this YAML scalar.</param>
    public YamlBool(bool value)
    {
        Value = value;
    }

    /// <summary>
    /// Serializes the boolean value to its YAML string representation.
    /// </summary>
    /// <returns>
    /// A lowercase string representation of the boolean value ("true" or "false").
    /// </returns>
    protected override string SerializeValue() => Value.ToString().ToLowerInvariant();
}