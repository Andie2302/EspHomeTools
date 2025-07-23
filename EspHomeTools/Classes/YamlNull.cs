using EspHomeTools.Interfaces;

namespace EspHomeTools.Classes;

public class YamlNull : IYamlNull
{
    public void Render(IYamlRenderManager yamlRenderManager, int indentationLevel)
    {
        throw new System.NotImplementedException();
    }
    private string? Value { get; set; } = null;

    string? IYamlValue<string?>.Value
    {
        get => null;
        set { }
    }

    public bool IsValueNull => true;
}