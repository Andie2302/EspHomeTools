using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using EspHomeTools.Interfaces;
namespace EspHomeTools.Classes.Collections;

public sealed class YamlCollection : IDictionary<string, IYamlNode>
{
    private readonly static string NodeSeparator = Environment.NewLine + Environment.NewLine;
    private readonly Dictionary<string, IYamlNode> _nodes = new(StringComparer.OrdinalIgnoreCase);

    public IComparer<KeyValuePair<string, IYamlNode>>? CustomSorter { get; set; }

    public string ToYaml() => SerializeNodes(GetSortedNodes());

    private IEnumerable<KeyValuePair<string, IYamlNode>> GetSortedNodes() =>
        CustomSorter != null
            ? _nodes.OrderBy(kvp => kvp, CustomSorter)
            : _nodes.OrderBy(kvp => kvp.Key, StringComparer.Ordinal);

    private static string SerializeNodes(IEnumerable<KeyValuePair<string, IYamlNode>> sortedNodes)
    {
        var serializedStrings = sortedNodes.Select(SerializeNode);
        return string.Join(NodeSeparator, serializedStrings);
    }

    private static string SerializeNode(KeyValuePair<string, IYamlNode> kvp)
    {
        kvp.Value.Name = kvp.Key;
        return kvp.Value.ToYaml();
    }

    public IYamlNode this[string key]
    {
        get => _nodes[key];
        set => _nodes[key] = value;
    }

    public ICollection<string> Keys => _nodes.Keys;
    public ICollection<IYamlNode> Values => _nodes.Values;
    public int Count => _nodes.Count;
    public bool IsReadOnly => ((ICollection<KeyValuePair<string, IYamlNode>>)_nodes).IsReadOnly;

    public void Add(string key, IYamlNode value) => _nodes.Add(key, value);
    public void Add(KeyValuePair<string, IYamlNode> item) => ((ICollection<KeyValuePair<string, IYamlNode>>)_nodes).Add(item);
    public void Clear() => _nodes.Clear();
    public bool Contains(KeyValuePair<string, IYamlNode> item) => ((ICollection<KeyValuePair<string, IYamlNode>>)_nodes).Contains(item);
    public bool ContainsKey(string key) => _nodes.ContainsKey(key);
    public void CopyTo(KeyValuePair<string, IYamlNode>[] array, int arrayIndex) => ((ICollection<KeyValuePair<string, IYamlNode>>)_nodes).CopyTo(array, arrayIndex);
    public bool Remove(string key) => _nodes.Remove(key);
    public bool Remove(KeyValuePair<string, IYamlNode> item) => ((ICollection<KeyValuePair<string, IYamlNode>>)_nodes).Remove(item);

#if NETSTANDARD2_0 || NETCOREAPP3_1
    public bool TryGetValue(string key, out IYamlNode value) => _nodes.TryGetValue(key, out value);
#else
    public bool TryGetValue(string key, [MaybeNullWhen(false)] out IYamlNode value) => _nodes.TryGetValue(key, out value);
#endif

    public IEnumerator<KeyValuePair<string, IYamlNode>> GetEnumerator() => _nodes.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => _nodes.GetEnumerator();
}