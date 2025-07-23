using System.Collections;
using System.Collections.Generic;
using EspHomeTools.Interfaces.RenderManagers;
using EspHomeTools.Interfaces.Scalars;

namespace EspHomeTools.DEMO;

public class YamlList : IYamlList
{
    private readonly List<IYamlScalar> _items = new();

    public int Count => _items.Count;

    public void Add(IYamlScalar item)
    {
        _items.Add(item);
    }

    public IEnumerator<IYamlScalar> GetEnumerator() => _items.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public void Render(IYamlRenderManager yamlRenderManager, int indentationLevel)
    {
        if (Count == 0)
        {
            yamlRenderManager.Append("[]", 0);
            return;
        }

        foreach (var item in _items)
        {
            // Für jeden Eintrag:
            // 1. Füge die Einrückung und den Listen-Prefix "- " hinzu.
            yamlRenderManager.Append("- ", indentationLevel);

            // 2. Delegiere das Rendern des Werts an das Skalar-Objekt selbst.
            item.Render(yamlRenderManager, indentationLevel);

            // 3. Füge einen Zeilenumbruch hinzu.
            yamlRenderManager.AppendLine();
        }
    }
}