using EspHomeTools.Interfaces;

namespace EspHomeTools.Classes;

public class YamlInteger : IYamlInteger
{
    public void Render(IYamlRenderManager yamlRenderManager, int indentationLevel)
    {
        throw new System.NotImplementedException();
    }
    public int Value { get; set; }
}