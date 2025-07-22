using System;
using System.Collections;
using System.Collections.Generic;

namespace EspHomeTools.Classes;

public class YamlList : IYamlList
{
    private readonly List<IYamlScalar> _items = [];

    public YamlList(params IYamlScalar[] items) => _items.AddRange(items);

    public void Add(IYamlScalar item) => _items.Add(item);

    public int Count => _items.Count;

    public bool HasItems => _items.Count != 0;

    public IEnumerator<IYamlScalar> GetEnumerator() => _items.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    public void Render(YamlRenderManager yamlRenderManager, int indentationLevel)
    {
        Console.WriteLine("Hallo von Render!");
    }
}
public interface IYamlList : IYamlObject, IEnumerable<IYamlScalar>
{
    int Count { get; }
    bool HasItems { get; }
    void Add(IYamlScalar item);
}