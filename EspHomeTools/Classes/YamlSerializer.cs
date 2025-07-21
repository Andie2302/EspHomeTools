using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EspHomeTools.Classes.Structures;
using EspHomeTools.Interfaces;

namespace EspHomeTools.Classes;

public class YamlSerializer : IYamlSerializer
{
    private const int DefaultIndentIncrement = 2;
    private const string ColonSeparator = ":";
    private const string CommentPrefix = "# ";
    private const char SpaceChar = ' ';
    private readonly static char[] TrimChars = { '\r', '\n', ' ' };
    private readonly static string[] LineSeparators = { "\r\n", "\r", "\n" };

    public string SerializeMapping(YamlMapping mapping, int indent, string? name)
    {
        var yamlBuilder = new StringBuilder();
        var indentString = CreateIndentString(indent);
        AppendCommentIfPresent(yamlBuilder, indentString, mapping.Comment);
        var childIndent = AppendNameIfPresent(yamlBuilder, indentString, indent, name);
        AppendChildNodes(yamlBuilder, childIndent, mapping.GetNodes());
        return yamlBuilder.ToString().TrimEnd(TrimChars);
    }

    private static string CreateIndentString(int indent) => new(SpaceChar, indent);

    private static void AppendCommentIfPresent(StringBuilder builder, string indentString, string? comment)
    {
        if (!string.IsNullOrWhiteSpace(comment))
        {
            builder.Append(FormatComment(comment ?? string.Empty, indentString));
        }
    }

    private static int AppendNameIfPresent(StringBuilder builder, string indentString, int currentIndent, string? name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return currentIndent;

        builder.Append(indentString).Append(name).AppendLine(ColonSeparator);
        return currentIndent + DefaultIndentIncrement;
    }

    private static void AppendChildNodes(StringBuilder builder, int indent, IEnumerable<KeyValuePair<string, IYamlNode>> nodes)
    {
        var nodeStrings = nodes.Select(kvp => SerializeNode(kvp, indent)).Where(yaml => !string.IsNullOrEmpty(yaml));
        builder.Append(string.Join(Environment.NewLine, nodeStrings));
    }

    private static string SerializeNode(KeyValuePair<string, IYamlNode> kvp, int indent) =>
        kvp.Value.ToYaml(indent, kvp.Key);

    private static string FormatComment(string comment, string prefix)
    {
        var commentLines = comment.Split(LineSeparators, StringSplitOptions.None);
        return string.Join(Environment.NewLine, commentLines.Select(line => $"{prefix}{CommentPrefix}{line}")) + Environment.NewLine;
    }
}