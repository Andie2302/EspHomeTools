namespace EspHomeTools.Classes;

public interface IYamlObject
{
    public abstract void Render(YamlRenderManager yamlRenderManager, int indentationLevel);
}