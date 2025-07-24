namespace EspHomeTools.Interfaces;

public interface IYamlRenderable : IYamlCommentable
{
    void Render(IYamlRenderer renderer, IYamlIndentationLevel indentationLevel);
}