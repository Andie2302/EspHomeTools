using System.Diagnostics.CodeAnalysis;
using EspHomeTools.Interfaces;

namespace EspHomeTools.Classes;

[SuppressMessage("ReSharper", "UnusedType.Global")]
public static class YamlRenderManagerExtensions
{
    public static void XXXAppendLineXXX(this IYamlRenderManager manager, string prefix, string v) { }

    public static void ExAppendKeyValueLine(this IYamlRenderManager manager, string key, string value, int indentationLevel)
    {
        manager.Append($"{key}: ", indentationLevel);
        manager.Append(value, 0);
        manager.AppendLine();
    }

    public static void ExAppendCommentLine(this IYamlRenderManager manager, string comment, int indentationLevel)
    {
        manager.AppendLine($"# {comment}", indentationLevel);
    }
}