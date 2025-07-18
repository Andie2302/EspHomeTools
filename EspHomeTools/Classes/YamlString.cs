namespace EspHomeTools.Classes;

/// <summary>
/// Represents a YAML scalar node that holds a string value.
/// </summary>
/// <remarks>
/// The YamlString class inherits from the YamlScalar base class,
/// specifically working with string values. It provides functionality
/// to serialize the string value into a YAML-compatible format,
/// optionally applying quotes or escaping characters as necessary.
/// </remarks>
public class YamlString : YamlScalar<string>
{
    /// <summary>
    /// Represents a YAML scalar node containing a string value.
    /// </summary>
    /// <remarks>
    /// The <c>YamlString</c> class extends the functionality of the <c>YamlScalar&lt;string&gt;</c> class
    /// to handle string values specifically. It includes a method to serialize the string value
    /// into a YAML-compatible string, ensuring proper quoting if required.
    /// </remarks>
    public YamlString(string value) => Value = value;
    /// <summary>
    /// Serializes the scalar value of a YAML node into its string representation.
    /// </summary>
    /// <returns>
    /// A string representing the serialized scalar value.
    /// If the value requires quoting (determined by <c>YamlTools.NeedsQuoting</c>), the value will be enclosed
    /// in quotes with any internal quotes escaped; otherwise, the trimmed value is returned unquoted.
    /// </returns>
    protected override string SerializeValue()
    {
        var value = (Value??string.Empty).Trim();
        return YamlTools.NeedsQuoting(value) ? $"\"{value.Replace("\"", "\\\"")}\"" : value;
    }
}