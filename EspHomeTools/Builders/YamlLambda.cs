using System;
using System.Text;
using EspHomeTools.Classes.Scalars;

namespace EspHomeTools.Builders;

public class YamlLambda : YamlScalar<string>
{
    private const int CodeIndentationSpaces = 2;
    private static readonly string[] LineBreakSeparators = ["\r\n", "\r", "\n"];

    public YamlLambda(string value)
    {
        Value = value;
    }

    public override string ToYaml(int indent = 0)
    {
        var sb = new StringBuilder();
        var baseIndentation = new string(' ', indent);
        AppendComments(sb, baseIndentation);
        AppendNameAndLiteralBlock(sb, baseIndentation);
        AppendCodeLines(sb, indent);
        return sb.ToString().TrimEnd('\r', '\n', ' ');
    }

    private void AppendComments(StringBuilder sb, string baseIndentation)
    {
        if (string.IsNullOrWhiteSpace(Comment)) return;
        var commentLines = Comment.Split(LineBreakSeparators, StringSplitOptions.None);
        foreach (var commentLine in commentLines)
        {
            sb.Append(baseIndentation).Append("# ").AppendLine(commentLine);
        }
    }

    private void AppendNameAndLiteralBlock(StringBuilder sb, string baseIndentation)
    {
        sb.Append(baseIndentation);
        if (!string.IsNullOrWhiteSpace(Name))
        {
            sb.Append(Name).Append(':');
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
        throw new NotImplementedException();
    }
}