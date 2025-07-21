using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using EspHomeTools.Interfaces;

namespace EspHomeTools.Classes.Collections;

public sealed class YamlCollection : IEnumerable<IYamlNode>
{
    private const string NodeSeparator = "\r\n\r\n";
    private readonly Dictionary<string, IYamlNode> _nodes = new(StringComparer.OrdinalIgnoreCase);

    public IComparer<KeyValuePair<string, IYamlNode>>? CustomSorter { get; set; }
    public int Count => _nodes.Count;

    public IYamlNode this[string key]
    {
        get => _nodes[key];
        set => _nodes[key] = value;
    }

    public bool Set(IYamlNode node) => ValidateAndAddNode(node);

    public bool Set(IEnumerable<IYamlNode> nodes) => nodes.All(ValidateAndAddNode);

    public bool Set(params IYamlNode[] nodes) => Set((IEnumerable<IYamlNode>)nodes);

    public bool ContainsKey(string key) => _nodes.ContainsKey(key);
    public bool Remove(string key) => _nodes.Remove(key);
    public void Clear() => _nodes.Clear();

    public string ToYaml()
    {
        var sortedPairs = GetSortedNodePairs();
        var yamlStrings = sortedPairs.Select(kvp => kvp.Value.ToYaml(0, kvp.Key));
        return string.Join(NodeSeparator, yamlStrings);
    }

    public IEnumerator<IYamlNode> GetEnumerator() => _nodes.Values.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public static YamlCollection operator +(YamlCollection collection, IYamlNode node)
    {
        collection.Set(node);
        return collection;
    }

    private bool ValidateAndAddNode(IYamlNode node)
    {
        var nodeName = node.Name;
        if (string.IsNullOrWhiteSpace(nodeName))
        {
            return false;
        }

        _nodes[nodeName] = node;
        return true;
    }

    private IOrderedEnumerable<KeyValuePair<string, IYamlNode>> GetSortedNodePairs() =>
        CustomSorter != null ? _nodes.OrderBy(kvp => kvp, CustomSorter) : _nodes.OrderBy(kvp => kvp.Key, StringComparer.Ordinal);
}