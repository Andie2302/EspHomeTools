using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using EspHomeTools.Interfaces;

namespace EspHomeTools.Classes.Structures;

public class YamlSequence : IYamlSequence
{
    private const int IndentSize = 2;
    private const string SequenceItemPrefix = "- ";
    private static readonly string[] LineSeparators = { "\r\n", "\r", "\n" };
    private readonly List<IYamlNode> _nodes = new();

    public string? Name { get; set; }
    public string? Comment { get; set; }
    public string? Tag { get; set; }

    public string ToYaml(int indent, string? name)
    {
        var yamlBuilder = new StringBuilder();
        var sequenceIndent = indent;
        AppendCommentIfPresent(yamlBuilder, indent);
        sequenceIndent = AppendNameIfPresent(yamlBuilder, sequenceIndent, name);
        AppendSequenceItems(yamlBuilder, sequenceIndent);
        return yamlBuilder.ToString().TrimEnd();
    }

    private void AppendCommentIfPresent(StringBuilder sb, int indent)
    {
        if (string.IsNullOrWhiteSpace(Comment)) return;
        var indentPrefix = new string(' ', indent);
        sb.Append(indentPrefix).Append("# ").AppendLine(Comment);
    }

    private static int AppendNameIfPresent(StringBuilder sb, int indent, string? name)
    {
        if (string.IsNullOrWhiteSpace(name)) return indent;
        var indentPrefix = new string(' ', indent);
        sb.Append(indentPrefix).Append(name).AppendLine(":");
        return indent + IndentSize;
    }

    private void AppendSequenceItems(StringBuilder sb, int indent)
    {
        var itemIndentPrefix = new string(' ', indent);
        foreach (var node in _nodes)
        {
            var childYamlContent = node.ToYaml(indent + IndentSize, null);
            var lines = childYamlContent.Split(LineSeparators, StringSplitOptions.None);
            sb.Append(itemIndentPrefix).Append(SequenceItemPrefix).AppendLine(lines[0].TrimStart());
            for (var i = 1; i < lines.Length; i++)
            {
                sb.AppendLine(lines[i]);
            }
        }
    }


    #region IList Implementation

    public void Add(IYamlNode item) => _nodes.Add(item);
    public void Clear() => _nodes.Clear();
    public bool Contains(IYamlNode item) => _nodes.Contains(item);
    public void CopyTo(IYamlNode[] array, int arrayIndex) => _nodes.CopyTo(array, arrayIndex);
    public bool Remove(IYamlNode item) => _nodes.Remove(item);
    public int Count => _nodes.Count;
    public bool IsReadOnly => false;
    public int IndexOf(IYamlNode item) => _nodes.IndexOf(item);
    public void Insert(int index, IYamlNode item) => _nodes.Insert(index, item);
    public void RemoveAt(int index) => _nodes.RemoveAt(index);

    public IYamlNode this[int index]
    {
        get => _nodes[index];
        set => _nodes[index] = value;
    }

    public IEnumerator<IYamlNode> GetEnumerator() => _nodes.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => _nodes.GetEnumerator();

    #endregion
}