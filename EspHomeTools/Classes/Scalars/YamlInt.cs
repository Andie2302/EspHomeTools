namespace EspHomeTools.Classes.Scalars;

/// <summary>
/// Represents a YAML scalar integer node.
/// </summary>
/// <remarks>
/// This class encapsulates an integer value intended for YAML serialization.
/// It derives from <see cref="YamlScalar{TValue}"/> and provides necessary implementation for handling integer values.
/// </remarks>
public class YamlInt : YamlScalar<int>
{
    /// <summary>
    /// Represents a YAML scalar value of integer type, providing functionality
    /// and structure for handling integer values in YAML documents.
    /// </summary>
    public YamlInt(int value)
    {
        Value = value;
    }

    /// <summary>
    /// Serializes the scalar value of the YAML node into its string representation.
    /// </summary>
    /// <returns>
    /// A string that represents the scalar value of the YAML node.
    /// </returns>
    protected override string SerializeValue() => Value.ToString();
}