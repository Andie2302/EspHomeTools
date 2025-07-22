using EspHomeTools.Classes.Validators;

namespace EspHomeTools.Classes.Render;

public class YamlRenderSettings
{
    private const char DefaultIndentationCharacter = ' ';
    private const int DefaultSpacesPerIndentationLevel = 2;

    public int SpacesPerLevel { get; }
    public char IndentationCharacter { get; }

    public YamlRenderSettings(int spacesPerLevel = DefaultSpacesPerIndentationLevel, char indentationCharacter = DefaultIndentationCharacter)
    {
        YamlIndentationValidator.ValidateSpacesPerLevel(spacesPerLevel);
        YamlIndentationValidator.ValidateIndentationCharacter(indentationCharacter);
        SpacesPerLevel = spacesPerLevel;
        IndentationCharacter = indentationCharacter;
    }

    public string GetIndentationString(int indentationLevel)
    {
        YamlIndentationValidator.ValidateIndentationLevel(indentationLevel);
        return new string(IndentationCharacter, indentationLevel * SpacesPerLevel);
    }
}