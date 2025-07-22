using System.Collections;
using System.Collections.Generic;
namespace EspHomeTools.Classes;
public class YamlList : IYamlObject, IEnumerable<IYamlScalar>
{
    private readonly List<IYamlScalar> _items = [];
    public YamlList(params IYamlScalar[] items) => _items.AddRange(items);
    public void Add(IYamlScalar item) => _items.Add(item);
    public int Count => _items.Count;
    public bool HasItems => _items.Count != 0;
    //public void Render(YamlRenderManager manager, int indentationLevel) => //YamlRenderManager.RenderItems(_items, manager, indentationLevel);
    public IEnumerator<IYamlScalar> GetEnumerator() => _items.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

public interface IYamlScalar : IYamlObject
{

}