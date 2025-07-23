using EspHomeTools.Interfaces;
using EspHomeTools.Scratch.Interfaces.RenderManagers;

namespace EspHomeTools.Classes;

public class YamlString : IYamlString
{
    public void Render(IYamlRenderManager yamlRenderManager, int indentationLevel)
    {
        throw new System.NotImplementedException();
    }
    public string? Value { get; set; }
    public bool IsValueNull => Value == null;
    public bool HasValue => string.IsNullOrWhiteSpace(Value) == false;
}