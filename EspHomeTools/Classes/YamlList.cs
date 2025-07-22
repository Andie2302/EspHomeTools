using System.Collections;
using System.Collections.Generic;
namespace EspHomeTools.Classes;
public class YamlList : YamlElement, IEnumerable<YamlElement>
{
    private readonly List<YamlElement> _items = [];
    public YamlList(params YamlElement[] items) => _items.AddRange(items);
    public void Add(YamlElement item) => _items.Add(item);
    public int Count => _items.Count;
    public bool HasItems => _items.Count != 0;
    public override void Render(YamlRenderManager manager, int indentationLevel)
    {
        YamlRenderManager.RenderItems(_items, manager, indentationLevel);
    }
    public IEnumerator<YamlElement> GetEnumerator() => _items.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}