using EspHomeTools.Interfaces;

namespace EspHomeTools.Classes;

public class YamlInteger : IYamlInteger
{
    public void Render(IYamlRenderManager yamlRenderManager, int indentationLevel) => yamlRenderManager.AppendScalar(this,indentationLevel);
    public int Value { get; set; }
}