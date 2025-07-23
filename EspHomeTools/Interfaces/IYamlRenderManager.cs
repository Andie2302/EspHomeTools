using System.Text;

namespace EspHomeTools.Interfaces;

/// <summary>
/// Defines a manager for rendering YAML content with support for indentation and formatting.
/// </summary>
public interface IYamlRenderManager
{
    /// Appends the specified text to the internal string buffer at a given indentation level.
    /// <param name="indentationLevel">The level of indentation to apply.
    ///     Values beyond recommended bounds are clamped internally.</param>
    /// <param name="text">The text to append.</param>
    void Append(int indentationLevel, string text);
    /// <summary>
    /// Appends a newline character to the internal string builder without any additional text or indentation.
    /// </summary>
    void AppendLine();
    /// <summary>
    /// Appends the specified text followed by a newline to the internal string builder,
    /// using the specified indentation level to determine the number of leading spaces.
    /// </summary>
    /// <param name="indentationLevel">
    ///     The level of indentation to apply. The level determines the amount of space
    ///     added before the text. It will be clamped within a valid range.
    /// </param>
    /// <param name="text">The text to append.</param>
    void AppendLine(int indentationLevel, string text);
    /// Retrieves the current-accumulated result from the internal StringBuilder used for rendering a YAML structure.
    /// <returns>
    /// A string representing the full YAML content accumulated by the YamlRenderManager.
    /// </returns>
    string GetResult();
    /// <summary>
    /// Clears all content from the internal buffer used for building YAML render output.
    /// </summary>
    void Clear();
    public StringBuilder BaseBuilder { get; }
}