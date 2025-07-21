using System;
using System.Text;
using EspHomeTools.Classes.Scalars;

namespace EspHomeTools.Builders;

/// <summary>
/// Represents a YAML scalar node for embedding lambda functions in a YAML document.
/// </summary>
/// <remarks>
/// YamlLambda is derived from YamlScalar and encapsulates a C++-style lambda function
/// as a string value, designed for integration in YAML configurations. It supports
/// serialization to YAML format and allows the inclusion of comments and custom formatting.
/// </remarks>
public class YamlLambda : YamlScalar<string>
{
    /// <summary>
    /// Represents the number of spaces used for code indentation in the YAML output.
    /// </summary>
    /// <remarks>
    /// This constant defines the spacing level used to indent code sections, ensuring consistent formatting
    /// when rendering YAML blocks with embedded code. It is applied in the construction of indented strings
    /// for proper alignment within the YAML serialization process.
    /// </remarks>
    private const int CodeIndentationSpaces = 2;

    /// <summary>
    /// Defines the line break characters used to split strings into multiple lines.
    /// </summary>
    /// <remarks>
    /// The array contains platform-specific and common line break separators:
    /// - "\r\n" (Windows-style)
    /// - "\r" (Classic Mac-style)
    /// - "\n" (Unix/Linux-style)
    /// These separators are used to normalize and handle multi-line text processing.
    /// </remarks>
    private readonly static string[] LineBreakSeparators = ["\r\n", "\r", "\n"];

    /// Represents a YAML scalar that contains a lambda block.
    /// This class is specialized to serialize and format YAML content for lambda expressions.
    /// Inherits from:
    /// - YamlScalar<string>: Represents a generic base class for YAML scalar values.
    /// Key functionality:
    /// - Custom serialization of the lambda block content into YAML format.
    /// - Optionally appends comments, name, and formatted code lines during conversion to YAML.
    public YamlLambda(string value) => Value = value;
    /// Converts the current instance of the YamlLambda object into its YAML string representation.
    /// <param name="indent">
    /// The number of spaces to use for indentations. Defaults to 0.
    /// </param>
    /// <returns>
    /// A YAML formatted string representation of the object with the specified indentation.
    /// </returns>
    public override string ToYaml(int indent = 0)
    {
        var sb = new StringBuilder();
        var baseIndentation = new string(' ', indent);
        AppendComments(sb, baseIndentation);
        AppendNameAndLiteralBlock(sb, baseIndentation);
        AppendCodeLines(sb, indent);
        return sb.ToString().TrimEnd('\r', '\n', ' ');
    }
    /// <summary>
    /// Appends comments to the specified StringBuilder instance using the given base indentation.
    /// Each line of the comment is prefixed with the base indentation and a comment marker (#).
    /// </summary>
    /// <param name="sb">The StringBuilder to which the comments will be appended.</param>
    /// <param name="baseIndentation">The string representing the base indentation to prefix each line of the comment.</param>
    private void AppendComments(StringBuilder sb, string baseIndentation)
    {
        if (string.IsNullOrWhiteSpace(Comment)) return;
        var commentLines = Comment?.Split(LineBreakSeparators, StringSplitOptions.None);
        if (commentLines == null) return;
        foreach (var commentLine in commentLines)
        {
            sb.Append(baseIndentation).Append("# ").AppendLine(commentLine);
        }
    }
    /// <summary>
    /// Appends the name of the YAML scalar node and a literal block indicator to the specified StringBuilder.
    /// </summary>
    /// <param name="sb">The StringBuilder to append the name and literal block to.</param>
    /// <param name="baseIndentation">The base indentation string to use for formatting.</param>
    private void AppendNameAndLiteralBlock(StringBuilder sb, string baseIndentation)
    {
        sb.Append(baseIndentation);
        if (!string.IsNullOrWhiteSpace(Name))
        {
            sb.Append(Name).Append(':');
        }
        sb.AppendLine(" |-");
    }
    /// <summary>
    /// Appends code lines to the provided <see cref="StringBuilder"/> with the specified indentation.
    /// </summary>
    /// <param name="sb">The <see cref="StringBuilder"/> instance to which the code lines will be appended.</param>
    /// <param name="indent">The level of indentation to be applied to each code line.</param>
    private void AppendCodeLines(StringBuilder sb, int indent)
    {
        var normalizedValue = (Value ?? string.Empty).Trim();
        var codeLines = normalizedValue.Split(LineBreakSeparators, StringSplitOptions.None);
        var codeIndentation = new string(' ', indent + CodeIndentationSpaces);
        foreach (var codeLine in codeLines)
        {
            sb.Append(codeIndentation).AppendLine(codeLine);
        }
    }
    /// <summary>
    /// Converts the encapsulated value of the scalar node into its string representation, suitable for serialization in YAML format.
    /// </summary>
    /// <returns>
    /// A string representation of the scalar's value. If the value is null, an empty string is returned.
    /// </returns>
    protected override string SerializeValue() => Value ?? string.Empty;
}