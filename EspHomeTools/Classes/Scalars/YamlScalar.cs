using System;
using System.Linq;
using System.Text;
using EspHomeTools.Interfaces;

namespace EspHomeTools.Classes.Scalars;

public abstract class YamlScalar<TValue> : IYamlScalar<TValue>
{
    private const string CommentPrefix = "# ";
    private const string TagSeparator = " ";
    private const string NameValueSeparator = ": ";
    private readonly static string[] LineEndingSeparators = ["\r\n", "\r", "\n"];
    public virtual TValue? Value { get; set; }
    public string? Name { get; set; }
    public string? Comment { get; set; }
    public string? Tag { get; set; }
    public virtual string ToYaml(int indent, string? name)
    {
        var indentPrefix = CreateIndentPrefix(indent);
        return BuildCommentSection(indentPrefix) + BuildContentLine(indentPrefix, name);
    }
    private string BuildContentLine(string indentPrefix, string? name) => indentPrefix + BuildNameWithTag(name) + SerializeValue();
    private string BuildNameWithTag(string? name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return string.Empty;
        var nameWithSeparator = name + NameValueSeparator;
        var tagPart = !string.IsNullOrWhiteSpace(Tag) ? TagSeparator + Tag : string.Empty;
        return nameWithSeparator + tagPart;
    }
    private static string CreateIndentPrefix(int indent) => new(' ', indent);
    private string BuildCommentSection(string indentPrefix) => !string.IsNullOrWhiteSpace(Comment) ? FormatComment(Comment, indentPrefix) : string.Empty;
    private static string FormatComment(string? comment, string prefix) => string.Join(Environment.NewLine, YamlTools.Normalize(comment).Split(LineEndingSeparators, StringSplitOptions.None).Select(line => $"{prefix}{CommentPrefix}{line}")) + Environment.NewLine;
    protected abstract string SerializeValue();
}