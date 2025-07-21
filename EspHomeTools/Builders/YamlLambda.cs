using System;
using System.Text;
using EspHomeTools.Classes;
using EspHomeTools.Classes.Scalars;

namespace EspHomeTools.Builders;

public sealed class YamlLambda : YamlScalar<string>
{
    private const int CodeIndentationSpaces = 2;
    private readonly static string[] LineBreakSeparators = ["\r\n", "\r", "\n"];

    public YamlLambda(string value) => Value = value;

    public override string ToYaml(int indent, string? name)
    {
        var sb = new StringBuilder();
        var baseIndentation = new string(' ', indent);
        AppendComments(sb, baseIndentation);
        AppendNameAndLiteralBlock(sb, baseIndentation, name);
        AppendCodeLines(sb, indent);
        return sb.ToString().TrimEnd('\r', '\n', ' ');
    }

    private void AppendComments(StringBuilder sb, string baseIndentation)
    {
        if (Comment==null) return;
        var temp = YamlTools.Normalize(Comment);
        var commentLines = temp.Split(LineBreakSeparators, StringSplitOptions.None);
        foreach (var commentLine in commentLines)
        {
            sb.Append(baseIndentation).Append("# ").AppendLine(commentLine);
        }
    }

    private static void AppendNameAndLiteralBlock(StringBuilder sb, string baseIndentation, string? name)
    {
        sb.Append(baseIndentation);
        if (!string.IsNullOrWhiteSpace(name))
        {
            sb.Append(name).Append(':');
        }

        sb.AppendLine(" |-");
    }

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

    protected override string SerializeValue()
    {
        return "YamlLambda::SerializeValue::" + string.Empty;
    }
}