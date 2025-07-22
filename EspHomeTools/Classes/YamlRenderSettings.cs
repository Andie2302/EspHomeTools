
using System.Text;
namespace EspHomeTools.Classes;
public class YamlRenderSettings
{
    private const char DefaultIndentationCharacter = ' ';
    private const int DefaultSpacesPerLevel = 2;
    public int SpacesPerLevel { get; }
    public char IndentationCharacter { get; }
    public YamlRenderSettings(int spacesPerLevel = DefaultSpacesPerLevel, char indentationCharacter = DefaultIndentationCharacter)
    {
        SpacesPerLevel = spacesPerLevel;
        IndentationCharacter = indentationCharacter;
    }
    public string GetIndentationString(int indentationLevel) => new(IndentationCharacter, indentationLevel * SpacesPerLevel);
}