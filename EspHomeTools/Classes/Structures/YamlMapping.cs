using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using EspHomeTools.Classes.Scalars;
using EspHomeTools.Interfaces;

namespace EspHomeTools.Classes.Structures;

public class YamlMapping : IYamlMapping
{
    private readonly Dictionary<string, IYamlNode> _nodes = new();
    private readonly IYamlSerializer _serializer;

    public YamlMapping() : this(new YamlSerializer()) { }

    public YamlMapping(IYamlSerializer serializer)
    {
        _serializer = serializer ?? throw new ArgumentNullException(nameof(serializer));
    }

    public string? Name { get; set; }
    public string? Comment { get; set; }
    public string? Tag { get; set; }

    public string ToYaml(int indent, string? name) => _serializer.SerializeMapping(this, indent, name);

    public void Add(string key, IYamlNode value) => _nodes.Add(key, value);
    public bool ContainsKey(string key) => _nodes.ContainsKey(key);
    public bool Remove(string key) => _nodes.Remove(key);
    public bool TryGetValue(string key, out IYamlNode value)
    {
        if (_nodes.TryGetValue(key, out var nodeValue) && nodeValue != null)
        {
            value = nodeValue;
            return true;
        }
        value = new YamlString(string.Empty);
        return false;
    }

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

    internal IEnumerable<KeyValuePair<string, IYamlNode>> GetNodes() => _nodes;
}