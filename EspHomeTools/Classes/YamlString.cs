using EspHomeTools.Interfaces;

namespace EspHomeTools.Classes;

public class YamlString : IYamlString
{
    private const string NullValue = "null";
    private const string BackslashEscape = @"\\";
    private const string QuoteEscape = "\\\"";
    public string? Value { get; set; }
    public bool IsValueNull => Value == null;
    public bool HasValue => string.IsNullOrWhiteSpace(Value) == false;
    public void Render(IYamlRenderManager yamlRenderManager, int indentationLevel)
    {
        var renderValue = GetRenderValue();
        yamlRenderManager.Append(renderValue, indentationLevel);
    }
    private string GetRenderValue() => IsValueNull ? NullValue : $"\"{EscapeString(Value ?? string.Empty)}\"";
    private static string EscapeString(string input) => input.Replace("\\", BackslashEscape).Replace("\"", QuoteEscape);
}