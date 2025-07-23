using EspHomeTools.Interfaces;

namespace EspHomeTools.Classes;

public class YamlNull : IYamlNull
{
    public void Render(IYamlRenderManager yamlRenderManager, int indentationLevel) => yamlRenderManager.AppendScalar(this,indentationLevel);

    private string? Value { get; set; } = null;

    string? IYamlValue<string?>.Value
    {
        get => null;
        set { }
    }

    public bool IsValueNull => true;
}