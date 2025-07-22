using System;
using System.Linq;

namespace EspHomeTools.Classes;

public static class YamlIndentationValidator
{
    private const string NegativeSpacesMessage = "Spaces per level cannot be negative.";
    private const string? ExcessiveSpacesMessage = "Spaces per level should not exceed 10 for practical reasons.";
    private const string InvalidIndentationCharacterMessage = "Indentation character must be either space or tab.";
    private const string NegativeIndentationLevelMessage = "Indentation level cannot be negative.";

    public static void ValidateSpacesPerLevel(int spacesPerLevel) => ValidateRange(spacesPerLevel, YamlIndentationConstants.MinSpacesPerLevel, YamlIndentationConstants.MaxRecommendedSpacesPerLevel, nameof(spacesPerLevel), NegativeSpacesMessage, ExcessiveSpacesMessage);

    public static void ValidateIndentationCharacter(char indentationCharacter)
    {
        if (!YamlIndentationConstants.AllowedIndentationCharacters.Contains(indentationCharacter))
            throw new ArgumentException(InvalidIndentationCharacterMessage, nameof(indentationCharacter));
    }

    public static void ValidateIndentationLevel(int indentationLevel) => ValidateRange(indentationLevel, YamlIndentationConstants.MinIndentationLevel, int.MaxValue, nameof(indentationLevel), NegativeIndentationLevelMessage, null);

    private static void ValidateRange(int value, int minValue, int maxValue, string paramName, string belowMinMessage, string? aboveMaxMessage)
    {
        if (value < minValue)
            throw new ArgumentOutOfRangeException(paramName, belowMinMessage);

        if (aboveMaxMessage != null && value > maxValue)
            throw new ArgumentOutOfRangeException(paramName, aboveMaxMessage);
    }
}