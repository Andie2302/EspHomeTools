using System.Globalization;

namespace EspHomeTools.Classes.Scalars;

/// <summary>
/// Represents a YAML scalar value of type double, providing functionality for serialization and manipulation
/// specific to floating-point numbers.
/// </summary>
public class YamlFloat : YamlScalar<double>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="YamlFloat"/> class with the specified double value.
    /// </summary>
    /// <param name="value">The double-precision floating-point value to be represented as a YAML scalar.</param>
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