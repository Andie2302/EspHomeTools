using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EspHomeTools.Classes;

public class YamlList : YamlElement, IEnumerable<YamlElement>
{
    private readonly List<YamlElement> _items = [];

    public YamlList(params YamlElement[] items)
    {
        _items.AddRange(items);
    }

    public void Add(YamlElement item) => _items.Add(item);

    public override void Render(YamlRenderManager manager, int indentationLevel)
    {
        if (!_items.Any())
        {
            manager.Append("[]");
            return;
        }

        for (var i = 0; i < _items.Count; i++)
        {
            var item = _items[i];
            manager.AppendIndentation(indentationLevel);
            manager.Append("- ");
            item.Render(manager, indentationLevel + 1);
            if (i < _items.Count - 1)
            {
                manager.AppendLine();
            }
        }
    }

    public IEnumerator<YamlElement> GetEnumerator() => _items.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}