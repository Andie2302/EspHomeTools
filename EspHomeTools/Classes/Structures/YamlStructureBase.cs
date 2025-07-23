using System.Collections.Generic;
using EspHomeTools.Interfaces;

namespace EspHomeTools.Classes.Structures;

public abstract class YamlStructureBase : IYamlStructure
{
    public abstract IEnumerable<IYamlNode> Children { get; }
    public abstract void Add(IYamlNode node);
    public abstract void Clear();

    public void Render(IYamlRenderManager yamlRenderManager, int indentationLevel)
    {
        if (IsEmpty())
        {
            RenderEmpty(yamlRenderManager, indentationLevel);
            return;
        }

        RenderEntries(yamlRenderManager, indentationLevel);
    }

    protected abstract bool IsEmpty();
    protected abstract void RenderEmpty(IYamlRenderManager yamlRenderManager, int indentationLevel);
    protected abstract void RenderEntries(IYamlRenderManager yamlRenderManager, int indentationLevel);
}