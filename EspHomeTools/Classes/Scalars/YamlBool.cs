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
    /// Represents a YAML boolean value.
    /// This class extends the functionality of YamlScalar<bool> to specifically handle serialization
    /// of boolean values to their YAML representation.
    /// The boolean value is converted to a lowercase string representation ("true" or "false") during serialization.
    public YamlBool(bool value)
    {
        Value = value;
    }

    /// Serializes the value of the current YAML scalar object into a string representation.
    /// <returns>
    /// A string representation of the value of the current YAML scalar object.
    /// </returns>
    protected override string SerializeValue() => Value.ToString().ToLowerInvariant();
}