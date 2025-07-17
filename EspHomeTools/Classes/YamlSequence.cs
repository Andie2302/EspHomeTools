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
        var basePrefix = new string(' ', indent);
        if (!string.IsNullOrWhiteSpace(Comment))
        {
            sb.Append(basePrefix).Append("# ").AppendLine(Comment);
        }

        if (!string.IsNullOrWhiteSpace(Name))
        {
            sb.Append(basePrefix).Append(Name).AppendLine(":");
            indent += 2; // Einrückung für die Listenelemente erhöhen
        }

        var itemIndentStr = new string(' ', indent);
        var childIndent = indent + 2;
        foreach (var node in _nodes)
        {
            string childYaml = node.ToYaml(childIndent);

            // Entferne die Einrückung der ersten Zeile, die von der Kind-Methode kommt
            string trimmedChildYaml = childYaml.TrimStart();
            sb.Append(itemIndentStr).Append("- ").AppendLine(trimmedChildYaml);
        }

        return sb.ToString().TrimEnd();
    }


    #region IList Implementierung

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