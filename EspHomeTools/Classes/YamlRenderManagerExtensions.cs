using System.Diagnostics.CodeAnalysis;
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
    public static void AppendScalar(this IYamlRenderManager manager, YamlBoolean yamlBoolean, int indentationLevel) => manager.Append(yamlBoolean.ToString()?.ToLower() ?? string.Empty, indentationLevel);
}