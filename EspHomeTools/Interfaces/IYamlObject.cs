using EspHomeTools.Classes;
using EspHomeTools.Classes.Render;

namespace EspHomeTools.Interfaces;

public interface IYamlObject
{
    void Render(YamlRenderManager yamlRenderManager, int indentationLevel);
}