using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using EspHomeTools.Interfaces;

namespace EspHomeTools.Classes;

[SuppressMessage("ReSharper", "UnusedType.Global")]
public static class YamlRenderManagerExtensions
{
    private const string NullValue = "null";
    private const string BackslashEscape = @"\\";
    private const string QuoteEscape = "\\\"";
    private const string CommentPrefix = "# ";

    public static void AppendComment(this YamlRenderManager manager, string comment, int indentationLevel, bool linebreak)
    {
        var commentText = CommentPrefix + comment;
        if (linebreak)
        {
            manager.AppendLine(commentText, indentationLevel);
        }
        else
        {
            manager.Append(commentText, indentationLevel);
        }
    }

    public static void AppendScalar<T>(this IYamlRenderManager manager, YamlScalarBase<T> yamlScalar, int indentationLevel)
    {
        var value = yamlScalar switch
        {
            YamlBoolean yamlBoolean => RenderBoolean(yamlBoolean),
            YamlFloat yamlFloat => RenderFloat(yamlFloat),
            YamlInteger yamlInteger => RenderInteger(yamlInteger),
            YamlNull => NullValue,
            YamlString yamlString => RenderString(yamlString),
            _ => yamlScalar.Value?.ToString() ?? NullValue
        };

        manager.Append(value, indentationLevel);
    }

    private static string RenderBoolean(YamlBoolean yamlBoolean) =>
        yamlBoolean.Value.ToString().ToLower();

    private static string RenderFloat(YamlFloat yamlFloat) =>
        yamlFloat.Value.ToString(CultureInfo.InvariantCulture);

    private static string RenderInteger(YamlInteger yamlInteger) =>
        yamlInteger.Value.ToString();

    private static string RenderString(IYamlString yamlString) =>
        yamlString.Value == null ? NullValue : $"\"{EscapeString(yamlString.Value)}\"";

    private static string EscapeString(string input) =>
        input.Replace("\\", BackslashEscape).Replace("\"", QuoteEscape);

    private static string GetRenderValue(IYamlString yamlString) =>
        RenderString(yamlString);
}