using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using EspHomeTools.Interfaces;

namespace EspHomeTools.Classes.Structures;

public class YamlMapping : IYamlMapping
{
    private const int DefaultIndentIncrement = 2;
    private const string ColonSeparator = ":";
    private const string CommentPrefix = "# ";
    private const char SpaceChar = ' ';
    private readonly static char[] TrimChars = { '\r', '\n', ' ' };
    private readonly static string[] LineSeparators = { "\r\n", "\r", "\n" };

    private readonly Dictionary<string, IYamlNode> _nodes = new();

    public string? Name { get; set; }
    public string? Comment { get; set; }
    public string? Tag { get; set; }

    public string ToYaml(int indent, string? name)
    {
        var yamlBuilder = new StringBuilder();
        var indentString = CreateIndentString(indent);
        AppendCommentIfPresent(yamlBuilder, indentString);
        var childIndent = AppendNameIfPresent(yamlBuilder, indentString, indent, name);
        AppendChildNodes(yamlBuilder, childIndent);
        return yamlBuilder.ToString().TrimEnd(TrimChars);
    }

    private static string CreateIndentString(int indent) => new(SpaceChar, indent);

    private void AppendCommentIfPresent(StringBuilder builder, string indentString)
    {
        if (!string.IsNullOrWhiteSpace(Comment))
        {
            builder.Append(FormatComment(Comment, indentString));
        }
    }

    private static int AppendNameIfPresent(StringBuilder builder, string indentString, int currentIndent, string? name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return currentIndent;

        builder.Append(indentString).Append(name).AppendLine(ColonSeparator);
        return currentIndent + DefaultIndentIncrement;
    }

    private void AppendChildNodes(StringBuilder builder, int indent)
    {
        var nodeStrings = _nodes.Select(kvp => SerializeNode(kvp, indent)).Where(yaml => !string.IsNullOrEmpty(yaml));
        builder.Append(string.Join(Environment.NewLine, nodeStrings));
    }

    private static string SerializeNode(KeyValuePair<string, IYamlNode> kvp, int indent)
    {
        // Hier ist der korrekte Aufruf:
        // Nutze den Key des Dictionary-Eintrags als 'name' für den Kind-Knoten.
        return kvp.Value.ToYaml(indent, kvp.Key);
    }

    private static string FormatComment(string comment, string prefix)
    {
        var commentLines = comment.Split(LineSeparators, StringSplitOptions.None);
        return string.Join(Environment.NewLine, commentLines.Select(line => $"{prefix}{CommentPrefix}{line}")) + Environment.NewLine;
    }


    #region IDictionary Implementation

    public void Add(string key, IYamlNode value) => _nodes.Add(key, value);
    public bool ContainsKey(string key) => _nodes.ContainsKey(key);
    public bool Remove(string key) => _nodes.Remove(key);

#if NETSTANDARD2_0 || NETCOREAPP3_1
    public bool TryGetValue(string key, out IYamlNode value) => _nodes.TryGetValue(key, out value);
#else
    public bool TryGetValue(string key, [MaybeNullWhen(false)] out IYamlNode value) => _nodes.TryGetValue(key, out value);
#endif

    public IYamlNode this[string key]
    {
        get => _nodes[key];
        set => _nodes[key] = value;
    }

    public ICollection<string> Keys => _nodes.Keys;
    public ICollection<IYamlNode> Values => _nodes.Values;
    public int Count => _nodes.Count;
    public bool IsReadOnly => false;
    public void Add(KeyValuePair<string, IYamlNode> item) => _nodes.Add(item.Key, item.Value);
    public void Clear() => _nodes.Clear();
    public bool Contains(KeyValuePair<string, IYamlNode> item) => _nodes.Contains(item);
    public void CopyTo(KeyValuePair<string, IYamlNode>[] array, int arrayIndex) =>
        ((ICollection<KeyValuePair<string, IYamlNode>>)_nodes).CopyTo(array, arrayIndex);
    public bool Remove(KeyValuePair<string, IYamlNode> item) =>
        ((ICollection<KeyValuePair<string, IYamlNode>>)_nodes).Remove(item);
    public IEnumerator<KeyValuePair<string, IYamlNode>> GetEnumerator() => _nodes.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => _nodes.GetEnumerator();

    #endregion
}