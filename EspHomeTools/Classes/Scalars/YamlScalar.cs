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
    /// <summary>
    /// Represents the prefix used for formatting comments in YAML serialization.
    /// </summary>
    /// <remarks>
    /// The <c>CommentPrefix</c> is utilized as the default string added before each line
    /// of a comment when representing YAML scalar nodes. By default, it is initialized with
    /// the value "# " to conform to YAML comment syntax. This prefix is incorporated
    /// when building the comment section of a YAML-formatted string.
    /// </remarks>
    private const string CommentPrefix = "# ";

    /// <summary>
    /// Defines the character or sequence of characters used to separate a tag
    /// from the value or name within a YAML scalar node representation.
    /// </summary>
    /// <remarks>
    /// This constant is primarily used within YAML serialization processes where a tag
    /// needs to be explicitly appended to a scalar node. It ensures proper formatting
    /// when tags are included in the YAML output.
    /// </remarks>
    private const string TagSeparator = " ";

    /// <summary>
    /// Defines the separator used between the name and the value of a scalar
    /// when constructing YAML content.
    /// </summary>
    /// <remarks>
    /// This separator ensures proper formatting of YAML scalar nodes, maintaining
    /// the structure where names and their associated values are clearly
    /// delineated according to YAML standards.
    /// </remarks>
    private const string NameValueSeparator = ": ";

    /// <summary>
    /// Gets or sets the value of the YAML scalar node.
    /// </summary>
    /// <typeparamref name="TValue" /> indicates the type of the value contained within
    /// the scalar node. This property enables access to the value of the node,
    /// supporting various types such as strings, numbers, or booleans.
    /// <remarks>
    /// The property is nullable, allowing scenarios where the value
    /// is either undefined or deliberately omitted in the YAML structure.
    /// </remarks>
    public TValue? Value { get; set; }

    /// <summary>
    /// Gets or sets the name of the YAML scalar node.
    /// </summary>
    /// <remarks>
    /// This property represents an optional textual identifier for the YAML scalar node,
    /// which can be utilized to provide context or a meaningful label for the node.
    /// When set, the name value may be included within serialization or used
    /// in configurations requiring named nodes. A null or whitespace value indicates
    /// the absence of a specified name.
    /// </remarks>
    public string? Name { get; set; }

    /// <summary>
    /// Gets or sets the comment associated with the YAML scalar node.
    /// </summary>
    /// This property allows storing a textual comment related to a YAML scalar node,
    /// enabling the inclusion of additional context or documentation for the node. It supports
    /// multi-line comments and can be left null or empty if no comment is specified.
    /// <remarks>
    /// Use this property to enhance the readability or maintainability of the YAML structure by
    /// providing descriptive notes or explanations alongside the scalar node.
    /// </remarks>
    public string? Comment { get; set; }

    /// <summary>
    /// Gets or sets the tag of the YAML scalar node.
    /// </summary>
    /// This property represents an optional identifier that can provide additional context
    /// or classification to the YAML scalar node. The tag can be used to denote
    /// a specific data type or category associated with the node.
    /// <remarks>
    /// The tag serves as a mechanism for enhancing the semantic meaning of the
    /// scalar node within the YAML document. If not explicitly set, it may be null,
    /// indicating that the scalar node does not have a defined tag.
    /// </remarks>
    public string? Tag { get; set; }

    public virtual string ToYaml(int indent, string? name)
    {
        var indentPrefix = CreateIndentPrefix(indent);
        var commentSection = BuildCommentSection(indentPrefix);

        // KORREKTUR: Der 'name'-Parameter wird hier an BuildContentLine weitergegeben.
        var contentLine = BuildContentLine(indentPrefix, name);

        // Deine neue return-Logik ist in Ordnung, aber die ursprüngliche ist etwas kürzer.
        // Beide funktionieren.
        return commentSection + contentLine;
    }

    private string BuildContentLine(string indentPrefix, string? name)
    {
        // Der 'name' wird hier an BuildNameWithTag weitergereicht.
        var nameWithTag = BuildNameWithTag(name);
        return indentPrefix + nameWithTag + SerializeValue();
    }

    private string BuildNameWithTag(string? name)
    {
        // Hier wird der übergebene 'name' schließlich verwendet.
        if (string.IsNullOrWhiteSpace(name))
            return string.Empty;

        var tagPart = !string.IsNullOrWhiteSpace(Tag) ? TagSeparator + Tag : string.Empty;
        return name + NameValueSeparator + tagPart;
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
    /// Builds the comment section of the YAML output if a comment is present.
    /// </summary>
    /// <param name="indentPrefix">The string used to represent the indentation level in the output.</param>
    /// <returns>The formatted comment section as a string, or an empty string if no comment is present.</returns>
    private string BuildCommentSection(string indentPrefix) => !string.IsNullOrWhiteSpace(Comment) ? FormatComment(Comment, indentPrefix) : string.Empty;

    /// <summary>
    /// Builds the main content line by combining the name, tag, and serialized value with the specified indentation prefix.
    /// </summary>
    /// <param name="indentPrefix">The prefix used to apply the desired level of indentation.</param>
    /// <returns>A string representing the formatted content line, including the name, tag, and serialized value.</returns>
    private string BuildContentLine(string indentPrefix)
    {
        var nameWithTag = BuildNameWithTag();
        return indentPrefix + nameWithTag + SerializeValue();
    }

    /// <summary>
    /// Builds the formatted representation of the name with an optional tag, separated by predefined delimiters.
    /// </summary>
    /// <returns>A non-empty string containing the name, optional tag, and separators if the name is defined; otherwise, an empty string.</returns>
    private string BuildNameWithTag()
    {
        if (string.IsNullOrWhiteSpace(Name))
            return string.Empty;

        var tagPart = !string.IsNullOrWhiteSpace(Tag) ? TagSeparator + Tag : string.Empty;
        return Name + NameValueSeparator + tagPart;
    }

    /// <summary>
    /// Formats the provided comment string for YAML output, ensuring each line is prefixed with a hash (#) character and aligned with the specified indentation prefix.
    /// </summary>
    /// <param name="comment">The text of the comment to be formatted.</param>
    /// <param name="prefix">The prefix used to indent each line of the comment.</param>
    /// <returns>A formatted string containing the comment with proper indentation and hash (#) prefixes.</returns>
    private static string FormatComment(string comment, string prefix)
    {
        var commentLines = comment.Split(["\r\n", "\r", "\n"], StringSplitOptions.None);
        return string.Join(Environment.NewLine, commentLines.Select(line => $"{prefix}{CommentPrefix}{line}")) + Environment.NewLine;
    }

    /// <summary>
    /// Serializes the scalar value to its YAML string representation.
    /// </summary>
    /// <returns>A YAML-formatted string representation of the scalar value.</returns>
    protected abstract string SerializeValue();
}