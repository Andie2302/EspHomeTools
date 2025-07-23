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

    // Neue generische AppendScalar-Methode für YamlScalarBase<T>
    public static void AppendScalar<T>(this IYamlRenderManager manager, YamlScalarBase<T> yamlScalar, int indentationLevel)
    {
        switch (yamlScalar)
        {
            case YamlBoolean yamlBoolean:
                manager.Append(yamlBoolean.Value.ToString().ToLower(), indentationLevel);
                break;
            case YamlFloat yamlFloat:
                manager.Append(yamlFloat.Value.ToString(CultureInfo.InvariantCulture), indentationLevel);
                break;
            case YamlInteger yamlInteger:
                manager.Append(yamlInteger.Value.ToString(), indentationLevel);
                break;
            case YamlNull yamlNull:
                manager.Append("null", indentationLevel);
                break;
            case YamlString yamlString:
                manager.Append(GetRenderValue(yamlString), indentationLevel);
                break;
            default:
                manager.Append(yamlScalar.Value?.ToString() ?? "null", indentationLevel);
                break;
        }
    }

    private const string NullValue = "null";
    private const string BackslashEscape = @"\\";
    private const string QuoteEscape = "\\\"";
    private static string EscapeString(string input) => input.Replace("\\", BackslashEscape).Replace("\"", QuoteEscape);
    private static string GetRenderValue(IYamlString yamlString) => yamlString.Value == null ? NullValue : $"\"{EscapeString(yamlString.Value)}\"";
}