namespace EspHomeTools.Classes;

public abstract class YamlElement
{
    public abstract void Render(YamlRenderManager yamlRenderManager, int indentationLevel);
}