using EspHomeTools.Interfaces;

namespace EspHomeTools.Classes;

public class YamlString : IYamlString
{
    public string? Value { get; set; }
    public bool IsValueNull => Value == null;
    public bool HasValue => string.IsNullOrWhiteSpace(Value) == false;
    public void Render(IYamlRenderManager yamlRenderManager, int indentationLevel) => yamlRenderManager.AppendScalar(this, indentationLevel);
}
