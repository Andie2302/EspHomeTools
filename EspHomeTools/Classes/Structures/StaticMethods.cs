using System;
using System.Collections.Generic;
using EspHomeTools.Interfaces;

namespace EspHomeTools.Classes.Structures;

public static class StaticMethods
{
    public static void RenderEntriesWithSeparator<T>(IYamlRenderManager yamlRenderManager, int indentationLevel, IEnumerable<T> entries, Action<IYamlRenderManager, T, int> renderSingleEntry)
    {
        var isFirstItem = true;
        foreach (var entry in entries)
        {
            if (!isFirstItem)
            {
                yamlRenderManager.AppendLine();
            }

            renderSingleEntry(yamlRenderManager, entry, indentationLevel);
            isFirstItem = false;
        }
    }

    public static void RenderMappingEntry(IYamlRenderManager yamlRenderManager, KeyValuePair<string, IYamlNode> pair, int indentationLevel)
    {
        yamlRenderManager.Append($"{pair.Key}: ", indentationLevel);
        if (pair.Value is IYamlStructure)
        {
            yamlRenderManager.AppendLine();
            pair.Value.Render(yamlRenderManager, indentationLevel + 1);
        }
        else
        {
            pair.Value.Render(yamlRenderManager, 0);
        }
    }
    public static void RenderSequenceEntry(IYamlRenderManager yamlRenderManager, IYamlNode child, int indentationLevel)
    {
        yamlRenderManager.Append("- ", indentationLevel);
        child.Render(yamlRenderManager, indentationLevel + 1);
    }
}