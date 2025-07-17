using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EspHomeTools.Interfaces;

namespace EspHomeTools.Classes;

public class YamlSequence : IYamlSequence
{
    private readonly List<IYamlNode> _nodes = new();

    public string? Name { get; set; }
    public string? Comment { get; set; }
    public string? Tag { get; set; }

    public string ToYaml(int indent = 0)
    {
        var sb = new StringBuilder();
        var prefix = new string(' ', indent);
        if (!string.IsNullOrWhiteSpace(Comment))
        {
            sb.Append(prefix).Append("# ").AppendLine(Comment);
        }

        if (!string.IsNullOrWhiteSpace(Name))
        {
            sb.Append(prefix).Append(Name).AppendLine(":");
        }

        foreach (var node in _nodes)
        {
            node.Name = null;
            string childYaml = node.ToYaml(indent + 2);
            string[] lines = childYaml.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
            if (!lines.Any())
                continue;

            sb.Append(new string(' ', indent)).Append("- ").AppendLine(lines[0].TrimStart());
            for (int i = 1; i < lines.Length; i++)
            {
                sb.AppendLine(lines[i]);
            }
        }

        return sb.ToString().TrimEnd();
    }


    #region IList Implementierung (einfache Weiterleitung)

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