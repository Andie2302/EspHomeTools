using System.Text;
using EspHomeTools.Interfaces;

namespace EspHomeTools.Classes;

public sealed class YamlRenderManager : IYamlRenderManager
{
    private readonly StringBuilder _stringBuilder  = new();
    private const char IndentationCharacter = ' ';
    private const char NewLine = '\n';
    private const int SpacesPerLevel = 2;
    private const int MinIndentationLevel = 0;
    private const int MaxLevel = 1000;

    public void Append(string text, int indentationLevel) => _stringBuilder.Append(GetClampedIndentationString(indentationLevel)).Append(text);

    public void AppendLine() => _stringBuilder.Append(NewLine);

    public void AppendLine(string text, int indentationLevel) => _stringBuilder.Append(GetClampedIndentationString(indentationLevel)).Append(text).Append(NewLine);

    public string GetResult() => _stringBuilder.ToString();

    public void Clear() => _stringBuilder.Clear();
    public void Append(int indentationLevel, params string[]? text)
    {
        if (text == null) return;
        if (text.Length == 0) return;
        foreach (var textItem in text)
        {
            Append(textItem, indentationLevel);
        }
    }

    private static string GetClampedIndentationString(int indentationLevel) => GetIndentationString(ClampIndentationLevel(indentationLevel));

    private static string GetIndentationString(int indentationLevel) => indentationLevel <= 0 ? string.Empty : new string(IndentationCharacter, indentationLevel * SpacesPerLevel);

    private static int ClampIndentationLevel(int indentationLevel) => indentationLevel < MinIndentationLevel ? MinIndentationLevel : indentationLevel > MaxLevel ? MaxLevel : indentationLevel;
}