using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using EspHomeTools.Interfaces;

namespace EspHomeTools.Classes;

[SuppressMessage("ReSharper", "UnusedType.Global")]
public static class YamlRenderManagerExtensions
{
    public static void AppendComment(this YamlRenderManager manager, string comment, int indentationLevel, bool linebreak)
    {
        if (linebreak)
        {
            manager.AppendLine($"# {comment}", indentationLevel);
        }
        else
        {
            manager.Append($"# {comment}", indentationLevel);
        }
    }
    public static void AppendScalar(this IYamlRenderManager manager, YamlBoolean yamlBoolean, int indentationLevel) => manager.Append(yamlBoolean.Value.ToString().ToLower(), indentationLevel);
    public static void AppendScalar(this IYamlRenderManager manager, YamlFloat yamlFloat, int indentationLevel) => manager.Append(yamlFloat.Value.ToString(CultureInfo.InvariantCulture), indentationLevel);
    public static void AppendScalar(this IYamlRenderManager manager, YamlInteger yamlInteger, int indentationLevel) => manager.Append(yamlInteger.Value.ToString(), indentationLevel);
    public static void AppendScalar(this IYamlRenderManager manager, YamlNull yamlNull, int indentationLevel) => manager.Append("null", indentationLevel);
    public static void AppendScalar(this IYamlRenderManager manager, YamlString yamlString, int indentationLevel) => manager.Append(GetRenderValue(yamlString), indentationLevel);

    private const string NullValue = "null";
    private const string BackslashEscape = @"\\";
    private const string QuoteEscape = "\\\"";
    private static string EscapeString(string input) => input.Replace("\\", BackslashEscape).Replace("\"", QuoteEscape);
    private static string GetRenderValue(IYamlString yamlString) => yamlString.Value == null ? NullValue : $"\"{EscapeString(yamlString.Value)}\"";
}