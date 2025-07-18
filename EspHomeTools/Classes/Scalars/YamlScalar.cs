using System.Text;
using EspHomeTools.Interfaces;

namespace EspHomeTools.Classes.Scalars;

/// <summary>
/// Abstract base class for YAML scalar nodes.
/// </summary>
/// <typeparam name="TValue">The underlying type of the value represented by the YAML scalar.</typeparam>
/// <remarks>
/// The <c>YamlScalar&lt;TValue&gt;</c> class provides a foundation for representing scalar values
/// in YAML. It incorporates optional properties such as a name, comment, and tag, which can be used
/// to enhance the representation of the YAML node. Additionally, it enforces the implementation of
/// value serialization to a YAML-compatible string through the <c>SerializeValue</c> method.
/// </remarks>
public abstract class YamlScalar<TValue> : IYamlScalar<TValue>
{
    /// Gets or sets the value of the YAML scalar.
    /// This property allows access to the underlying scalar value, which can be of a generic type.
    /// It is used to define and manipulate the scalar content of the YAML node.
    public TValue? Value { get; set; }

    /// Gets or sets the name of the YAML node.
    /// This property represents the identifier or key associated with the
    /// YAML element in serialized output.
    /// When serialized to YAML, the value of this property is used as the
    /// key. For sequences, it may represent a label or descriptor.
    /// Setting this property to null or an empty string will omit it
    /// from the serialized output.
    public string? Name { get; set; }

    /// <summary>
    /// Gets or sets the comment associated with the YAML node.
    /// This comment is typically used to annotate the corresponding YAML structure
    /// with additional information or notes and is serialized as a comment line in the YAML output.
    /// </summary>
    public string? Comment { get; set; }

    /// Gets or sets the explicit YAML tag associated with the node.
    /// This property allows specifying a data type or category for the YAML node within the serialized output.
    /// For example, tags are used in YAML to denote types, such as `!!int`, `!!str`, or custom application-specific tags.
    public string? Tag { get; set; }

    /// Converts the YAML scalar object to its YAML string representation.
    /// <param name="indent">
    /// The number of spaces to prepend to each line for indentation. Default is 0.
    /// </param>
    /// <returns>
    /// A string representing the YAML scalar, including its name, tag, value, and optional comment.
    /// </returns>
    public string ToYaml(int indent = 0)
    {
        var sb = new StringBuilder();
        var prefix = new string(' ', indent);
        if (!string.IsNullOrWhiteSpace(Comment))
        {
            sb.Append(prefix).Append("# ").AppendLine(Comment);
        }

        if (!string.IsNullOrWhiteSpace(Name))
        {
            sb.Append(prefix).Append(Name).Append(':');
            if (!string.IsNullOrWhiteSpace(Tag))
            {
                sb.Append(' ').Append(Tag);
            }

            sb.Append(' ');
        }

        sb.Append(SerializeValue());
        return sb.ToString();
    }

    /// <summary>
    /// Serializes the scalar value of the YAML node into its string representation.
    /// </summary>
    /// <returns>
    /// The string representation of the scalar value.
    /// </returns>
    protected abstract string SerializeValue();
}