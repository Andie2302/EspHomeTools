using System.Diagnostics.CodeAnalysis;
using EspHomeTools.Interfaces;

namespace EspHomeTools.Classes;

[SuppressMessage("ReSharper", "UnusedType.Global")]
public static class YamlRenderManagerExtensions
{
    public static void XXXAppendLineXXX(this IYamlRenderManager manager, int indentationLevel, string prefix, string v) { }

    public static void ExAppendKeyValueLine(this IYamlRenderManager manager, int indentationLevel, string key, string value)
    {
        manager.Append(indentationLevel, $"{key}: ");
        manager.Append(0, value);
        manager.AppendLine();
    }

    public static void ExAppendCommentLine(this IYamlRenderManager manager, int indentationLevel, string comment)
    {
        manager.AppendLine(indentationLevel, $"# {comment}");
    }
}