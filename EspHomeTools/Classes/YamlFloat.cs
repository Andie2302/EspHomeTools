using EspHomeTools.Interfaces;

namespace EspHomeTools.Classes;

public class YamlFloat : IYamlFloat
{
    public void Render(IYamlRenderManager yamlRenderManager, int indentationLevel)
    {
        throw new System.NotImplementedException();
    }
    public float Value { get; set; }
}