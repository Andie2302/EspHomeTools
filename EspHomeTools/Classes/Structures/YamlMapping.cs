using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EspHomeTools.Interfaces;

namespace EspHomeTools.Classes.Structures;

public class YamlMapping : IYamlMapping
{
    private readonly Dictionary<string, IYamlNode> _nodes = new();
    public string? Name { get; set; }
    public string? Comment { get; set; }
    public string? Tag { get; set; }

    public string ToYaml(int indent = 0)
    {
        var yamlBuilder = new StringBuilder();
        var indentString = CreateIndentString(indent);
        AppendCommentIfPresent(yamlBuilder, indentString);
        var childIndent = AppendNameIfPresent(yamlBuilder, indentString, indent);
        AppendChildNodes(yamlBuilder, childIndent);
        return yamlBuilder.ToString().TrimEnd('\r', '\n', ' ');
    }

    private static string CreateIndentString(int indent) => new(' ', indent);

    private void AppendCommentIfPresent(StringBuilder builder, string indentString)
    {
        if (!string.IsNullOrWhiteSpace(Comment))
        {
            builder.Append(FormatComment(Comment ?? string.Empty, indentString));
        }
    }

    private int AppendNameIfPresent(StringBuilder builder, string indentString, int currentIndent)
    {
        if (string.IsNullOrWhiteSpace(Name))
            return currentIndent;

        builder.Append(indentString).Append(Name).AppendLine(":");
        return currentIndent + 2;
    }

    private void AppendChildNodes(StringBuilder builder, int indent) => builder.Append(string.Join(Environment.NewLine, _nodes.Select(kvp => SerializeNode(kvp, indent)).Where(yaml => !string.IsNullOrEmpty(yaml))));

    private static string SerializeNode(KeyValuePair<string, IYamlNode> kvp, int indent)
    {
        kvp.Value.Name = kvp.Key;
        return kvp.Value.ToYaml(indent);
    }

    private static string FormatComment(string comment, string prefix)
    {
        var commentLines = comment.Split(["\r\n", "\r", "\n"], StringSplitOptions.None);
        return string.Join(Environment.NewLine, commentLines.Select(line => $"{prefix}# {line}")) + Environment.NewLine;
    }

    public void Add(string key, IYamlNode value) => _nodes.Add(key, value);
    public bool ContainsKey(string key) => _nodes.ContainsKey(key);
    public bool Remove(string key) => _nodes.Remove(key);
    public bool TryGetValue(string key, out IYamlNode value) => _nodes.TryGetValue(key, out value);

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
}