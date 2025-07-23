using EspHomeTools.Interfaces;

namespace EspHomeTools.Classes;

/// <summary>
/// Enthält Erweiterungsmethoden für Klassen, die das IYamlRenderManager-Interface implementieren.
/// </summary>
public static class YamlRenderManagerExtensions
{
    /// <summary>
    /// Fügt eine formatierte Key-Value-Zeile zum YAML-Output hinzu.
    /// Beispiel: "  key: value"
    /// </summary>
    /// <param name="manager">Die Instanz des Render-Managers.</param>
    /// <param name="key">Der Schlüssel, der geschrieben werden soll.</param>
    /// <param name="value">Der Wert, der geschrieben werden soll.</param>
    /// <param name="indentationLevel">Die Einrückungsebene.</param>
    public static void AppendKeyValueLine(this IYamlRenderManager manager, string key, string value, int indentationLevel)
    {
        // Benutzt die bestehenden Methoden des Managers
        manager.Append($"{key}: ", indentationLevel);
        manager.Append(value, 0); // Wert ohne zusätzliche Einrückung anhängen
        manager.AppendLine(); // Zeilenumbruch am Ende
    }

    /// <summary>
    /// Fügt eine YAML-Kommentarzeile hinzu.
    /// Beispiel: "  # Das ist ein Kommentar"
    /// </summary>
    public static void AppendCommentLine(this IYamlRenderManager manager, string comment, int indentationLevel)
    {
        manager.AppendLine($"# {comment}", indentationLevel);
    }
}