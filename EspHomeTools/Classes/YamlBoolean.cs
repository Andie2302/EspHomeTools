using EspHomeTools.Classes.Comment;
using EspHomeTools.Classes.Render;
using EspHomeTools.Interfaces;

namespace EspHomeTools.Classes;

public class YamlBoolean : IYamlBoolean
{
    public void Render(IYamlRenderer renderer, IYamlIndentationLevel indentationLevel)
    {
        throw new System.NotImplementedException();
    }
    public IYamlComment Comment { get; }
    public YamlBoolean() => Comment = new YamlComment();
}