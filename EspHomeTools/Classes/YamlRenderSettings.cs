using System;

namespace EspHomeTools.Classes;


public class YamlRenderSettings
{
    private const char DefaultIndentationCharacter = ' ';
    private const int DefaultSpacesPerIndentationLevel = 2;
    private const int MinSpacesPerLevel = 0;
    private const int MaxSpacesPerLevel = 10;
    private const int MinIndentationLevel = 0;

    public int SpacesPerLevel { get; }
    public char IndentationCharacter { get; }

    public YamlRenderSettings(int spacesPerLevel = DefaultSpacesPerIndentationLevel, char indentationCharacter = DefaultIndentationCharacter)
    {
        ValidateParameters(spacesPerLevel, indentationCharacter);
        SpacesPerLevel = spacesPerLevel;
        IndentationCharacter = indentationCharacter;
    }

    public string GetIndentationString(int indentationLevel)
    {
        ValidateIndentationLevel(indentationLevel);
        return new string(IndentationCharacter, indentationLevel * SpacesPerLevel);
    }

    private static void ValidateParameters(int spacesPerLevel, char indentationCharacter)
    {
        ValidateSpacesPerLevel(spacesPerLevel);
        ValidateIndentationCharacter(indentationCharacter);
    }

    private static void ValidateSpacesPerLevel(int spacesPerLevel)
    {
        switch (spacesPerLevel)
        {
            case < MinSpacesPerLevel:
                throw new ArgumentOutOfRangeException(nameof(spacesPerLevel), "Spaces per level cannot be negative.");
            case > MaxSpacesPerLevel:
                throw new ArgumentOutOfRangeException(nameof(spacesPerLevel), "Spaces per level should not exceed 10 for practical reasons.");
        }
    }

    private static void ValidateIndentationCharacter(char indentationCharacter)
    {
        if (indentationCharacter != ' ' && indentationCharacter != '\t')
            throw new ArgumentException("Indentation character must be either space or tab.", nameof(indentationCharacter));
    }

    private static void ValidateIndentationLevel(int indentationLevel)
    {
        if (indentationLevel < MinIndentationLevel)
            throw new ArgumentOutOfRangeException(nameof(indentationLevel), "Indentation level cannot be negative.");
    }
}