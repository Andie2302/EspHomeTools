using System;
using System.Collections.Generic;
using EspHomeTools.Interfaces;

namespace EspHomeTools.Classes.Structures;

public class YamlMapping : YamlStructureBase, IYamlMapping
{
    private readonly Dictionary<string, IYamlNode> _children = new();

    public override IEnumerable<IYamlNode> Children => _children.Values;

    public IYamlNode this[string key]
    {
        get => _children[key];
        set => _children[key] = value;
    }

    public bool TryGetValue(string key, out IYamlNode? value) => _children.TryGetValue(key, out value);

    public bool ContainsKey(string key) => _children.ContainsKey(key);

    public override void Add(IYamlNode node)
    {
        if (node is IYamlKeyValue keyValueNode && keyValueNode.HasKey)
        {
            _children[keyValueNode.Key ?? string.Empty] = keyValueNode;
        }
        else
        {
            throw new ArgumentException("Einem YamlMapping können nur Knoten vom Typ IYamlKeyValue mit einem gültigen Schlüssel hinzugefügt werden.", nameof(node));
        }
    }

    public override void Clear()
    {
        _children.Clear();
    }

    protected override bool IsEmpty() => _children.Count == 0;

    protected override void RenderEmpty(IYamlRenderManager yamlRenderManager, int indentationLevel)
    {
        yamlRenderManager.AppendLine("{}", indentationLevel);
    }

    protected override void RenderEntries(IYamlRenderManager yamlRenderManager, int indentationLevel)
    {
        StaticMethods.RenderEntriesWithSeparator(yamlRenderManager, indentationLevel, _children, StaticMethods.RenderMappingEntry);
    }


}