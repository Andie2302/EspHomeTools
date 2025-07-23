using EspHomeTools.Interfaces;

namespace EspHomeTools.Classes;

public class YamlNull : IYamlNull, IYamlNullable
{
    public void Render(IYamlRenderManager yamlRenderManager, int indentationLevel)
    {
        throw new System.NotImplementedException();
    }
    public string? Value { get; set; } = null;
    public bool IsValueNull => true;
}