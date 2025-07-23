using EspHomeTools.Interfaces;

namespace EspHomeTools.Classes;

public class YamlBoolean : IYamlBoolean
{
    public void Render(IYamlRenderManager yamlRenderManager, int indentationLevel) => yamlRenderManager.AppendScalar(this,indentationLevel);
    public bool Value { get; set; }
}