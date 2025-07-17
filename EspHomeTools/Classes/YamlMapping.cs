using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EspHomeTools.Interfaces;

namespace EspHomeTools.Classes;

public class YamlMapping : IYamlMapping
{
    private readonly Dictionary<string, IYamlNode> _nodes = new();

    public string? Name { get; set; }
    public string? Comment { get; set; }
    public string? Tag { get; set; }

    public string ToYaml(int indent = 0)
    {
        var text = new StringBuilder();
        if (!string.IsNullOrWhiteSpace(Name))
        {
            text.AppendLine($"{new string(' ', indent)}{Name}:");
            indent += 2;
        }

        foreach (var kvp in _nodes)
        {
            kvp.Value.Name = kvp.Key;
            text.AppendLine(kvp.Value.ToYaml(indent));
        }

        return text.ToString().TrimEnd();
    }


    #region IDictionary Implementierung (einfache Weiterleitung)

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
    public void CopyTo(KeyValuePair<string, IYamlNode>[] array, int arrayIndex) => ((ICollection<KeyValuePair<string, IYamlNode>>)_nodes).CopyTo(array, arrayIndex);
    public bool Remove(KeyValuePair<string, IYamlNode> item) => ((ICollection<KeyValuePair<string, IYamlNode>>)_nodes).Remove(item);
    public IEnumerator<KeyValuePair<string, IYamlNode>> GetEnumerator() => _nodes.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => _nodes.GetEnumerator();

    #endregion
}