using System.Globalization;

namespace EspHomeTools.Classes.Scalars;

/// <summary>
/// Represents a YAML scalar value of type double, providing functionality for serialization and manipulation
/// specific to floating-point numbers.
/// </summary>
public class YamlFloat : YamlScalar<double>
{
    /// Represents a YAML float scalar value.
    /// This class is derived from `YamlScalar<double>`
    /// and provides a mechanism to handle and serialize
    /// double-precision floating-point numbers as YAML scalar values.
    public YamlFloat(double value)
    {
        Value = value;
    }

    /// Serializes the value of the current YAML scalar object into a string representation.
    /// <returns>
    /// A string representing the serialized value of the current YAML scalar object.
    /// </returns>
    protected override string SerializeValue() => Value.ToString(CultureInfo.InvariantCulture);
}