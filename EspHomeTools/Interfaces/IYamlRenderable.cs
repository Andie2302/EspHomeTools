namespace EspHomeTools.Interfaces;

public interface IYamlRenderable
{
    void Render(IYamlRenderer renderer, IYamlIndentationLevel indentationLevel);
}