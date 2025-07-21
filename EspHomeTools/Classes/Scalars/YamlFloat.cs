using System.Globalization;

namespace EspHomeTools.Classes.Scalars;

/// <summary>
/// Represents a scalar node in a YAML document that contains a floating-point number.
/// Provides functionality for handling and serializing YAML floating-point scalar values.
/// </summary>
public class YamlFloat : YamlScalar<double>
{
    /// <summary>
    /// Represents a YAML scalar value of type double, providing functionality for serialization and manipulation
    /// specific to floating-point numbers.
    /// </summary>
    public YamlFloat(double value)
    {
        Value = value;
    }

    /// <summary>
    /// Serializes the value of the current YAML scalar object into a string representation.
    /// </summary>
    /// <returns>
    /// A string representing the serialized value of the current YAML scalar object.
    /// </returns>
    protected override string SerializeValue() => Value.ToString(CultureInfo.InvariantCulture);
}