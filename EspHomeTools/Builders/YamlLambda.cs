using System;
using System.Text;
using EspHomeTools.Classes.Scalars;

namespace EspHomeTools.Builders;

/// <summary>
/// Represents a YAML scalar that is serialized as a multi-line literal block (using |-).
/// This is primarily used for lambda scripts in ESPHome.
/// </summary>
public class YamlLambda : YamlScalar<string>
{
    public YamlLambda(string value)
    {
        Value = value;
    }

    /// <summary>
    /// Overrides the default serialization to produce a YAML literal block style.
    /// </summary>
    public override string ToYaml(int indent = 0)
    {
        var sb = new StringBuilder();
        var prefix = new string(' ', indent);

        // Handle comments if they exist
        if (!string.IsNullOrWhiteSpace(Comment))
        {
            var commentLines = Comment?.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            foreach (var line in commentLines)
            {
                sb.Append(prefix).Append("# ").AppendLine(line);
            }
        }

        // Append the key (e.g., "lambda:") and the literal block indicator
        sb.Append(prefix);
        if (!string.IsNullOrWhiteSpace(Name))
        {
            sb.Append(Name).Append(':');
        }
        sb.AppendLine(" |-");

        // Append the indented code lines
        var codeLines = (Value ?? string.Empty).Trim().Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
        var codeIndent = new string(' ', indent + 2);
        foreach (var line in codeLines)
        {
            sb.Append(codeIndent).AppendLine(line);
        }

        return sb.ToString().TrimEnd('\r', '\n', ' ');
    }

    protected override string SerializeValue()
    {
        // This method is not used because ToYaml is fully overridden.
        throw new NotImplementedException();
    }
}