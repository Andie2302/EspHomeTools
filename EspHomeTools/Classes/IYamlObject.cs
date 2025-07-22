namespace EspHomeTools.Classes;

public interface IYamlObject
{
    void Render(YamlRenderManager yamlRenderManager, int indentationLevel);
}