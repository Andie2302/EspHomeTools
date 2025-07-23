using System;
using System.Collections.Generic;
using System.Linq;
using EspHomeTools.Interfaces;

namespace EspHomeTools.Classes.Structures;

public class YamlSequence : YamlStructureBase, IYamlSequence
{
    private readonly List<IYamlNode> _children = [];

    public override IEnumerable<IYamlNode> Children => _children.AsReadOnly();

    public override void Add(IYamlNode node)
    {
        if (node == null)
            throw new ArgumentNullException(nameof(node), "Einem YamlSequence kann kein null-Knoten hinzugefügt werden.");

        _children.Add(node);
    }

    public override void Clear()
    {
        _children.Clear();
    }

    protected override bool IsEmpty() => !_children.Any();

    protected override void RenderEmpty(IYamlRenderManager yamlRenderManager, int indentationLevel)
    {
        yamlRenderManager.AppendLine("[]", indentationLevel > 0 ? indentationLevel - 1 : 0);
    }

    protected override void RenderEntries(IYamlRenderManager yamlRenderManager, int indentationLevel)
    {
        StaticMethods.RenderEntriesWithSeparator(yamlRenderManager, indentationLevel, _children, StaticMethods.RenderSequenceEntry);
    }


}