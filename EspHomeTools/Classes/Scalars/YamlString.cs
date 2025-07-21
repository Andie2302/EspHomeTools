namespace EspHomeTools.Classes.Scalars;

/// <summary>
/// Represents a YAML scalar node that contains and manipulates string data.
/// </summary>
/// <remarks>
/// The YamlString class is a derivative of YamlScalar specialized for strings.
/// It is designed to facilitate the serialization of string values into a
/// YAML-compatible format, including handling optional quoting or escaping requirements.
/// </remarks>
public class YamlString : YamlScalar<string>
{
    /// <summary>
    /// Represents the quotation mark character used for enclosing string values in YAML serialization.
    /// </summary>
    /// <remarks>
    /// This constant is utilized in the <see cref="YamlString"/> class to indicate and apply quotes
    /// to string values during YAML serialization. It ensures that string values are properly
    /// enclosed, adhering to YAML formatting rules.
    /// </remarks>
    private const string Quote = "\"";

    /// <summary>
    /// A constant string representing an escaped double quote sequence, used for YAML serialization.
    /// </summary>
    /// <remarks>
    /// The <c>EscapedQuote</c> variable is used to insert an escaped double quote (\"\) into YAML string values
    /// during serialization. This ensures that double quotes within a string are properly escaped
    /// to comply with YAML formatting rules.
    /// </remarks>
    private const string EscapedQuote = "\\\"";

    /// <summary>
    /// Represents a YAML scalar node containing a string value.
    /// </summary>
    /// <remarks>
    /// The <c>YamlString</c> class provides functionality for handling string values as part of a YAML
    /// scalar structure. It is used for serializing and managing string data in YAML documents,
    /// including support for proper formatting and quoting.
    /// </remarks>
    public YamlString(string value) => Value = value;

    /// <summary>
    /// Serializes the scalar value of a YAML node into its string representation.
    /// </summary>
    /// <returns>
    /// A string representing the serialized scalar value.
    /// If the value requires quoting, the value is enclosed in quotes with internal quotes escaped;
    /// otherwise, the normalized value is returned unquoted.
    /// </returns>
    protected override string SerializeValue()
    {
        var normalizedValue = GetNormalizedValue();
        return YamlTools.NeedsQuoting(normalizedValue) ? CreateQuotedValue(normalizedValue) : normalizedValue;
    }

    /// <summary>
    /// Retrieves the normalized string value, ensuring it is not null and trims any leading or trailing whitespace.
    /// </summary>
    /// <returns>The normalized string value, guaranteed to be non-null.</returns>
    private string GetNormalizedValue() => (Value ?? string.Empty).Trim();

    /// <summary>
    /// Creates a properly quoted and escaped string value for YAML serialization.
    /// </summary>
    /// <param name="value">The string value to be quoted and escaped.</param>
    /// <returns>A string that is quoted and escaped according to YAML serialization rules.</returns>
    private static string CreateQuotedValue(string value) => Quote + value.Replace(Quote, EscapedQuote) + Quote;
}