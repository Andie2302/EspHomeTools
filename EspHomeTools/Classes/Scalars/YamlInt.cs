namespace EspHomeTools.Classes.Scalars;

/// <summary>
/// Represents a YAML scalar integer node, providing functionality to serialize integer values into YAML format.
/// </summary>
/// <remarks>
/// This class is a specific implementation of <see cref="YamlScalar{TValue}"/> for handling integer values.
/// It supports serialization to YAML with optional naming, commenting, and tagging.
/// </remarks>

public class YamlInt : YamlScalar<int>
{
    /// <summary>
    /// Initializes a new instance of the YamlInt class with the specified integer value.
    /// </summary>
    /// <param name="value">The integer value to encapsulate in this YAML scalar.</param>
    public YamlInt(int value)
    {
        Value = value;
    }

    /// <summary>
    /// Serializes the scalar value of the YAML node into its string representation.
    /// </summary>
    /// <returns>
    /// The string representation of the scalar value.
    /// </returns>
    protected override string SerializeValue() => Value.ToString();
}