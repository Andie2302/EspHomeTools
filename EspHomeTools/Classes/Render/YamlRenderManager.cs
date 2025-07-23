using System.Text;

namespace EspHomeTools.Classes.Render;

/// <summary>
/// Manages the generation and rendering of YAML-formatted text with proper indentation support.
/// </summary>
public class YamlRenderManager : IYamlRenderManager
{
    /// <summary>
    /// Represents the character used for generating indentation in the YAML rendering process.
    /// This character determines how indentation levels are visually represented in the output.
    /// The default value is a single space (' ').
    /// </summary>
    private const char IndentationCharacter = ' ';

    /// <summary>
    /// Represents the newline character used for line separation in the YAML rendering process.
    /// This constant is used in methods that handle appending lines to the internal string builder.
    /// </summary>
    private const char NewLine = '\n';

    /// <summary>
    /// Represents the number of spaces to be used for each level of indentation
    /// in rendered YAML content. The value is used to calculate the indentation
    /// string based on the specified indentation level.
    /// </summary>
    /// <remarks>
    /// This constant determines the spacing granularity for hierarchical structures
    /// in YAML rendering. Adjusting this value affects the visual formatting of
    /// the generated YAML output. By default, its value is set to 2.
    /// </remarks>
    private const int SpacesPerLevel = 2;

    /// <summary>
    /// Represents the minimum permissible indentation level for YAML rendering.
    /// </summary>
    /// <remarks>
    /// This constant is used to enforce a lower bound for indentation levels when rendering
    /// YAML content. Indentation levels below this value will be clamped to meet this threshold.
    /// </remarks>
    private const int MinIndentationLevel = 0;

    /// <summary>
    /// Specifies the maximum number of spaces per indentation level that is recommended
    /// when rendering YAML content. This value is used to clamp the indentation level
    /// to prevent excessive or inconsistent spacing in YAML formatting.
    /// </summary>
    private const int MaxRecommendedSpacesPerLevel = 10;

    /// <summary>
    /// A private instance of <see cref="StringBuilder"/> used to construct and manage
    /// the YAML output dynamically as strings are appended or cleared.
    /// </summary>
    /// <remarks>
    /// This StringBuilder instance provides efficient string manipulation, allowing the
    /// append operation with appropriate indentation, clearing, and retrieval of the
    /// generated YAML content as a final string.
    /// </remarks>
    private readonly StringBuilder _stringBuilder = new();

    /// Appends the specified text to the internal StringBuilder with the given indentation level.
    /// The method automatically adjusts the indentation based on the clamped indentation level.
    /// <param name="text">The text to append to the internal StringBuilder.</param>
    /// <param name="indentationLevel">The level of indentation to apply to the text. The value is clamped between the defined minimum and maximum levels.</param>
    public void Append(string text, int indentationLevel) => _stringBuilder.Append(GetClampedIndentationString(indentationLevel)).Append(text);
    /// <summary>
    /// Appends a new line character to the current string representation without adding any additional text or indentation.
    /// </summary>
    public void AppendLine() => _stringBuilder.Append(NewLine);
    /// <summary>
    /// Appends a newline character to the internal string builder.
    /// </summary>
    public void AppendLine(string text, int indentationLevel) => _stringBuilder.Append(GetClampedIndentationString(indentationLevel)).Append(text).Append(NewLine);
    /// <summary>
    /// Retrieves the current rendered YAML content as a string.
    /// </summary>
    /// <returns>
    /// A string containing the accumulated YAML content.
    /// </returns>
    public string GetResult() => _stringBuilder.ToString();
    /// <summary>
    /// Clears all content from the internal string builder, resetting it to an empty state.
    /// </summary>
    public void Clear() => _stringBuilder.Clear();
    /// <summary>
    /// Generates a string of spaces corresponding to the specified indentation level,
    /// ensuring that the indentation level is clamped within the allowed range.
    /// </summary>
    /// <param name="indentationLevel">The requested indentation level. This value is clamped between the defined minimum and maximum levels.</param>
    /// <returns>A string containing spaces, corresponding to the clamped indentation level.</returns>
    private static string GetClampedIndentationString(int indentationLevel) => GetIndentationString(ClampIndentationLevel(indentationLevel));
    /// Generates a string of spaces representing an indentation level.
    /// <param name="indentationLevel">
    /// The number of indentation levels to generate. Each level consists of a fixed
    /// number of spaces as defined in the class. A value of 0 or fewer produces an
    /// empty string.
    /// </param>
    /// <return>
    /// A string containing spaces equivalent to the specified indentation level.
    /// If the level is less than or equal to 0, it returns an empty string.
    /// </return>
    private static string GetIndentationString(int indentationLevel) => indentationLevel <= 0 ? string.Empty : new string(IndentationCharacter, indentationLevel * SpacesPerLevel);
    /// Clamps the specified indentation level to the allowed range.
    /// <param name="indentationLevel">The indentation level to clamp.</param>
    /// <returns>The clamped indentation level.</returns>
    private static int ClampIndentationLevel(int indentationLevel) => indentationLevel < MinIndentationLevel ? MinIndentationLevel : indentationLevel > MaxRecommendedSpacesPerLevel ? MaxRecommendedSpacesPerLevel : indentationLevel;
}