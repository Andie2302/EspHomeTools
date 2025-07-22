using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace EspHomeTools.Classes;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
[SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
public class YamlRenderManager
{
    private readonly StringBuilder _stringBuilder = new();
    private readonly YamlRenderSettings _settings;

    public YamlRenderManager(YamlRenderSettings? settings = null) => _settings = settings ?? new YamlRenderSettings();

    public YamlRenderManager(int spacesPerLevel, char indentationCharacter) => _settings = new YamlRenderSettings(spacesPerLevel, indentationCharacter);

    public void Append(string text, int indentationLevel) => AppendIndentedText(indentationLevel, text);

    public void AppendEmptyLine() => _stringBuilder.Append(YamlConstants.EmptyLine);

    public void AppendLine(string text, int indentationLevel) => AppendIndentedText(indentationLevel, text, YamlConstants.EmptyLine);

    public void AppendComment(string line, int indentationLevel) => AppendIndentedText(indentationLevel, YamlConstants.CommentPrefix, line, YamlConstants.EmptyLine);

    public void AppendComment(IEnumerable<string> lines, int indentationLevel) => AppendMultipleComments(lines, indentationLevel);

    private void AppendMultipleComments(IEnumerable<string> lines, int indentationLevel)
    {
        foreach (var line in lines)
        {
            AppendComment(line, indentationLevel);
        }
    }

    private void AppendIndentedText(int indentationLevel, params string[] textParts)
    {
        _stringBuilder.Append(_settings.GetIndentationString(indentationLevel));
        foreach (var part in textParts)
            _stringBuilder.Append(part);
    }

    public string Result => _stringBuilder.ToString();
}