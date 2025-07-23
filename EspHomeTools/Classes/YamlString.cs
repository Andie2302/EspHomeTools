using EspHomeTools.Interfaces;

namespace EspHomeTools.Classes;

public class YamlString : IYamlString
{
    public void Render(IYamlRenderManager yamlRenderManager, int indentationLevel)
    {
        if (!IsValueNull)
        {
            var value = Value ?? string.Empty;
            var escapedValue = value.Replace("\\", @"\\").Replace("\"", @"\""");
            yamlRenderManager.Append($"\"{escapedValue}\"", indentationLevel);
            return;
        }

        yamlRenderManager.Append("null", indentationLevel);
    }

    public string? Value { get; set; }
    public bool IsValueNull => Value == null;
    public bool HasValue => string.IsNullOrWhiteSpace(Value) == false;
}