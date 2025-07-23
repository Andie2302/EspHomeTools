// using EspHomeTools.Interfaces;

using EspHomeTools.Interfaces;

namespace EspHomeTools.Classes;

public class YamlInteger : IYamlInteger
{
    public void Render(IYamlRenderManager yamlRenderManager, int indentationLevel)
    {
        yamlRenderManager.Append(Value.ToString(), indentationLevel);
    }
    public int Value { get; set; }
}