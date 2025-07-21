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

    public bool Set(IYamlNode node)
    {
        if (string.IsNullOrWhiteSpace(node.Name))
            return false;

        _nodes[node.Name] = node;
        return true;
    }

    public bool Set(IEnumerable<IYamlNode> nodes)
    {
        return SetNodes(nodes);
    }

    public bool Set(params IYamlNode[] nodes)
    {
        return SetNodes(nodes);
    }

    public bool ContainsKey(string key) => _nodes.ContainsKey(key);
    public bool Remove(string key) => _nodes.Remove(key);
    public void Clear() => _nodes.Clear();

    public string ToYaml()
    {
        var sortedNodes = GetSortedNodes();
        var yamlNodes = sortedNodes.Select(ConvertNodeToYaml);
        return string.Join(NodeSeparator, yamlNodes);
    }

    public IEnumerator<IYamlNode> GetEnumerator() => _nodes.Values.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public static YamlCollection operator +(YamlCollection collection, IYamlNode node)
    {
        collection.Set(node);
        return collection;
    }

    private bool SetNodes(IEnumerable<IYamlNode> nodes)
    {
        foreach (var node in nodes)
        {
            if (!Set(node))
                return false;
        }

        return true;
    }

    private IOrderedEnumerable<KeyValuePair<string, IYamlNode>> GetSortedNodes()
    {
        var hasCustomSorter = CustomSorter != null;
        return hasCustomSorter ? _nodes.OrderBy(kvp => kvp, CustomSorter) : _nodes.OrderBy(kvp => kvp.Key, StringComparer.Ordinal);
    }

    private static string ConvertNodeToYaml(KeyValuePair<string, IYamlNode> kvp)
    {
        return kvp.Value.ToYaml(0, kvp.Key);
    }
}