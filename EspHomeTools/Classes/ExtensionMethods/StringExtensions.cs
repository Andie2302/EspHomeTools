using System.Globalization;
using System.Linq;
using System.Text;

namespace EspHomeTools.Classes.Render;

public static class YamlConstants
{
    public const char SpaceCharacter = ' ';
    public const char NewLineCharacter = '\n';
    public readonly static string NewLine = NewLineCharacter.ToString();
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
public static class EspHomeIdentifierValidator
{
    private const int MaxIdentifierLength = 24;

    public static bool IsValidEspHomeIdentifier(string identifier) =>
        identifier.Length <= MaxIdentifierLength && identifier.All(c => char.IsLower(c) || char.IsDigit(c) || c == '-');

    public static string ToValidEspHomeDeviceName(string name) =>
        ToValidEspHomeIdentifier(name);

    public static string ToValidEspHomeDeviceId(string deviceId) =>
        ToValidEspHomeIdentifier(deviceId);

    public static string ToValidEspHomeIdentifier(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return "device";

        // .NET's eingebaute Unicode-Normalisierung verwenden
        var normalized = input.Normalize(NormalizationForm.FormD);
        var asciiOnly = new string(normalized.Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark).ToArray()).Normalize(NormalizationForm.FormC);
        var processedName = asciiOnly.ToLowerInvariant().Where(c => char.IsLetterOrDigit(c) || c == '-' || c == '_' || c == ' ').Aggregate(new StringBuilder(), (sb, c) => sb.Append(c)).ToString().Replace('_', '-').Replace(' ', '-');

        // Mehrfache Bindestriche entfernen
        while (processedName.Contains("--"))
            processedName = processedName.Replace("--", "-");

        // Bindestriche am Anfang/Ende entfernen
        processedName = processedName.Trim('-');

        // Sicherstellen dass es mit einem Buchstaben beginnt
        if (processedName.Length > 0 && char.IsDigit(processedName[0]))
            processedName = "device-" + processedName;

        return processedName.Length > MaxIdentifierLength ? processedName.Substring(0, MaxIdentifierLength).TrimEnd('-') : processedName;
    }

    public static string ToValidDeviceName(string name) => ToValidEspHomeIdentifier(name);
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