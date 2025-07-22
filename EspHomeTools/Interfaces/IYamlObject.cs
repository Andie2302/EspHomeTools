using EspHomeTools.Classes;

namespace EspHomeTools.Interfaces;

public interface IYamlObject
{
    void Render(YamlRenderManager yamlRenderManager, int indentationLevel);
}