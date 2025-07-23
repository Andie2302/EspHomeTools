using EspHomeTools.Interfaces;

namespace EspHomeTools.Classes;

public class YamlBoolean : IYamlBoolean
{
    public void Render(IYamlRenderManager yamlRenderManager, int indentationLevel)
    {
        yamlRenderManager.Append(Value.ToString().ToLower(), indentationLevel);
    }
    public bool Value { get; set; }
}