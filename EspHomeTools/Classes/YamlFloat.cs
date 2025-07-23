using EspHomeTools.Interfaces;
using EspHomeTools.Scratch.Interfaces.RenderManagers;

namespace EspHomeTools.Classes;

public class YamlFloat : IYamlFloat
{
    public void Render(IYamlRenderManager yamlRenderManager, int indentationLevel)
    {
        throw new System.NotImplementedException();
    }
    public float Value { get; set; }
}

public class YamlInteger : IYamlInteger
{
    public void Render(IYamlRenderManager yamlRenderManager, int indentationLevel)
    {
        throw new System.NotImplementedException();
    }
    public int Value { get; set; }
}

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
public class YamlNull : IYamlNull, IYamlNullable
{
    public void Render(IYamlRenderManager yamlRenderManager, int indentationLevel)
    {
        throw new System.NotImplementedException();
    }
    public string? Value { get; set; } = null;
    public bool IsValueNull => true;
}

