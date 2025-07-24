using System.Linq;
using System.Text;
using EspHomeTools.Classes.ExtensionMethods;
using EspHomeTools.Interfaces;

namespace EspHomeTools.Classes.Render;

public static class YamlConstants
{
    public const char SpaceCharacter = ' ';
    public const char NewLineCharacter = '\n';
    public static readonly string NewLine = NewLineCharacter.ToString();
}
public static class LineEndingConverter
{
    private const string WindowsLineEnding = "\r\n";
    private const char MacClassicLineEnding = '\r';

    public static string ToUnixLineEndings(string input) =>
        input.Replace(WindowsLineEnding, YamlConstants.NewLine).Replace(MacClassicLineEnding, YamlConstants.NewLineCharacter);
}
public static class YamlStringTools
{
    public static string[] ToStringArray(string input) =>
        LineEndingConverter.ToUnixLineEndings(input).Split(YamlConstants.NewLineCharacter);

    public static string ToInlineString(string input) =>
        LineEndingConverter.ToUnixLineEndings(input).Replace(YamlConstants.NewLineCharacter, YamlConstants.SpaceCharacter);

    public static bool IsValidYamlBasicChar(char c) =>
        char.IsLetterOrDigit(c) || c == '_' || c == '-' || c == '.';
}
public static class EspHomeDeviceNameValidator
{
    private const int MaxDeviceNameLength = 24;

    public static bool IsValidEsphomeDeviceName(string name) =>
        name.Length <= MaxDeviceNameLength && name.All(c => char.IsLower(c) || char.IsDigit(c) || c == '-');

    public static string ToValidEsphomeDeviceName(string name) =>
        name.ToLowerInvariant().Replace(" ", "-");

    public static string ToValidDeviceName(string name)
    {
        var validName = name.ToLowerInvariant().Where(c => char.IsLetterOrDigit(c) || c == '-' || c == '_').Aggregate(new StringBuilder(), (sb, c) => sb.Append(c)).ToString().Replace('_', '-');
        return validName.Length > MaxDeviceNameLength ? validName.Substring(0, MaxDeviceNameLength) : validName;
    }
}
public interface IYamlRenderer
{
    StringBuilder Output { get; }
    void WriteLine(string line);
    void Write(string text);
}
public class YamlRenderer : IYamlRenderer
{
    public StringBuilder Output { get; } = new();

    public void WriteLine(string text) =>
        Output.Append(LineEndingConverter.ToUnixLineEndings(text)).Append(YamlConstants.NewLine);

    public void Write(string text) =>
        Output.Append(LineEndingConverter.ToUnixLineEndings(text));
}