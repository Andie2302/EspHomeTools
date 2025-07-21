using System;
using System.Linq;
using System.Text;
using EspHomeTools.Interfaces;

namespace EspHomeTools.Classes.Scalars;

/// <summary>
/// Represents a base class for YAML scalar nodes with a specific underlying value type.
/// </summary>
/// <typeparam name="TValue">The type of the value this scalar encapsulates.</typeparam>
/// <remarks>
/// The YamlScalar class provides functionality for:
/// - Storing scalar node values, names, comments, and optional tags.
/// - Serializing the scalar node into a YAML format string, including comments and indentation.
/// </remarks>
public abstract class YamlScalar<TValue> : IYamlScalar<TValue>
{
    private const string CommentPrefix = "# ";
    private const string TagSeparator = " ";
    private const string NameValueSeparator = ": ";

    /// <summary>
    /// Gets or sets the scalar value of the YAML node.
    /// </summary>
    /// <typeparamref name="TValue" /> represents the type of the value contained within the scalar
    /// node. This property allows retrieval or assignment of the node's value, which can be
    /// of various data types such as strings, integers, floats, and more.
    /// <remarks>
    /// This property is nullable to ensure flexibility in handling cases where the value is
    /// not explicitly set or represented in the YAML structure.
    /// </remarks>
    public TValue? Value { get; set; }

    /// Gets or sets the name of the YAML scalar element.
    /// The `Name` property represents the key of the YAML scalar when serialized.
    /// It is typically used to identify the key portion in key-value pairs
    /// within YAML structures.
    /// This property can be null or empty, and when it is, the serialization
    /// process may handle it differently or omit it, depending on the implementation.
    public string? Name { get; set; }

    /// <summary>
    /// Gets or sets the comment associated with the YAML scalar.
    /// This comment is typically used to provide additional information or context for
    /// the value represented by the scalar in a YAML document. It can be included as
    /// part of the YAML output, formatted appropriately to adhere to YAML syntax.
    /// </summary>
    public string? Comment { get; set; }

    /// <summary>
    /// Gets or sets the tag associated with the YAML scalar node.
    /// </summary>
    /// <remarks>
    /// The tag is a string used to explicitly define the data type or semantic meaning
    /// of the scalar node in the YAML document. If the tag is not specified,
    /// the scalar node relies on implicit typing determined by its content.
    /// </remarks>
    public string? Tag { get; set; }

    /// <summary>
    /// Converts the scalar object to a YAML-formatted string representation with the specified indentation.
    /// </summary>
    /// <param name="indent">The number of spaces to use for indentation.</param>
    /// <returns>A YAML-formatted string representation of the scalar object.</returns>
    public virtual string ToYaml(int indent = 0)
    {
        var indentPrefix = CreateIndentPrefix(indent);
        var commentSection = BuildCommentSection(indentPrefix);
        var contentLine = BuildContentLine(indentPrefix);

        return commentSection + contentLine;
    }

    /// <summary>
    /// Creates the indentation prefix string based on the specified indent level.
    /// </summary>
    /// <param name="indent">The number of spaces to use for indentation.</param>
    /// <returns>A string containing the appropriate number of spaces for indentation.</returns>
    private static string CreateIndentPrefix(int indent)
    {
        return new string(' ', indent);
    }

    /// <summary>
    /// Builds the comment section of the YAML output if a comment exists.
    /// </summary>
    /// <param name="indentPrefix">The indentation prefix to apply.</param>
    /// <returns>The formatted comment section or an empty string if no comment exists.</returns>
    private string BuildCommentSection(string indentPrefix) => !string.IsNullOrWhiteSpace(Comment) ? FormatComment(Comment, indentPrefix) : string.Empty;

    /// <summary>
    /// Builds the main content line containing the name, tag, and serialized value.
    /// </summary>
    /// <param name="indentPrefix">The indentation prefix to apply.</param>
    /// <returns>The formatted content line.</returns>
    private string BuildContentLine(string indentPrefix)
    {
        var nameWithTag = BuildNameWithTag();
        return indentPrefix + nameWithTag + SerializeValue();
    }

    /// <summary>
    /// Builds the name and tag portion of the content line.
    /// </summary>
    /// <returns>The formatted name with optional tag and separator, or empty string if no name exists.</returns>
    private string BuildNameWithTag()
    {
        if (string.IsNullOrWhiteSpace(Name))
            return string.Empty;

        var tagPart = !string.IsNullOrWhiteSpace(Tag)
            ? TagSeparator + Tag
            : string.Empty;

        return Name + NameValueSeparator + tagPart;
    }

    /// <summary>
    /// Formats the specified comment for YAML output, prefixing each line with '#' and the provided indentation.
    /// </summary>
    /// <param name="comment">The comment text to be formatted.</param>
    /// <param name="prefix">The indentation prefix to apply to each line of the comment.</param>
    /// <returns>A string representing the formatted comment, with each line prefixed and properly indented.</returns>
    private static string FormatComment(string comment, string prefix)
    {
        var commentLines = comment.Split(["\r\n", "\r", "\n"], StringSplitOptions.None);
        return string.Join(Environment.NewLine, commentLines.Select(line => $"{prefix}{CommentPrefix}{line}")) + Environment.NewLine;
    }

    /// <summary>
    /// Serializes the scalar value to its YAML string representation.
    /// </summary>
    /// <returns>A serialized YAML string for the scalar value.</returns>
    protected abstract string SerializeValue();
}