using EspHomeTools.Interfaces;

namespace EspHomeTools.Classes;

public class YamlBoolean : IYamlBoolean
{
    public void Render(IYamlRenderManager yamlRenderManager, int indentationLevel)
    {
        throw new System.NotImplementedException();
    }
    public bool Value { get; set; }
}