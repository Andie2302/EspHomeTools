using System;
using System.Text;
using EspHomeTools.Classes.Scalars;

namespace EspHomeTools.Builders;

/// <summary>
/// Stellt einen YAML-Skalarknoten zum Einbetten von Lambda-Funktionen in ein YAML-Dokument dar.
/// </summary>
public class YamlLambda : YamlScalar<string>
{
    private const int CodeIndentationSpaces = 2;
    private readonly static string[] LineBreakSeparators = ["\r\n", "\r", "\n"];

    public YamlLambda(string value) => Value = value;

    /// <summary>
    /// Konvertiert die aktuelle Instanz des YamlLambda-Objekts in ihre YAML-Zeichenfolgendarstellung.
    /// </summary>
    /// <param name="indent">Die Anzahl der Leerzeichen, die für die Einrückung verwendet werden sollen. Standard ist 0.</param>
    /// <param name="name">Der Name (Schlüssel), der für diesen Lambda-Block gerendert werden soll.</param>
    /// <returns>Eine YAML-formatierte Zeichenfolgendarstellung des Objekts mit der angegebenen Einrückung.</returns>
    public override string ToYaml(int indent = 0, string? name = null)
    {
        var sb = new StringBuilder();
        var baseIndentation = new string(' ', indent);

        AppendComments(sb, baseIndentation);
        AppendNameAndLiteralBlock(sb, baseIndentation, name);
        AppendCodeLines(sb, indent);

        return sb.ToString().TrimEnd('\r', '\n', ' ');
    }

    private void AppendComments(StringBuilder sb, string baseIndentation)
    {
        if (string.IsNullOrWhiteSpace(Comment)) return;

        var commentLines = Comment.Split(LineBreakSeparators, StringSplitOptions.None);
        foreach (var commentLine in commentLines)
        {
            sb.Append(baseIndentation).Append("# ").AppendLine(commentLine);
        }
    }

    private static void AppendNameAndLiteralBlock(StringBuilder sb, string baseIndentation, string? name)
    {
        sb.Append(baseIndentation);
        if (!string.IsNullOrWhiteSpace(name))
        {
            sb.Append(name).Append(':');
        }
        // Der literal block operator sorgt für die korrekte mehrzeilige Darstellung.
        sb.AppendLine(" |-");
    }

    private void AppendCodeLines(StringBuilder sb, int indent)
    {
        var normalizedValue = (Value ?? string.Empty).Trim();
        var codeLines = normalizedValue.Split(LineBreakSeparators, StringSplitOptions.None);
        var codeIndentation = new string(' ', indent + CodeIndentationSpaces);

        foreach (var codeLine in codeLines)
        {
            sb.Append(codeIndentation).AppendLine(codeLine);
        }
    }

    /// <summary>
    /// Serialisiert den Wert des Skalarknotens in seine Zeichenfolgendarstellung.
    /// Bei einem Lambda-Block wird der Wert selbst nicht weiter formatiert, da dies die ToYaml-Methode übernimmt.
    /// </summary>
    /// <returns>Einen leeren String, da die Logik in ToYaml liegt.</returns>
    protected override string SerializeValue()
    {
        // Diese Methode wird hier nicht benötigt, da ToYaml die gesamte Logik für
        // die mehrzeilige Ausgabe übernimmt. Wir geben einen leeren String zurück.
        return "YamlLambda::SerializeValue::"+string.Empty;
    }
}