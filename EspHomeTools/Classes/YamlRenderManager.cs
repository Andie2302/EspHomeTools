using System.Text;

namespace EspHomeTools.Classes;

public class YamlRenderManager
{
    #region Variables
    private readonly YamlRenderSettings _settings;
    private readonly StringBuilder _stringBuilder = new();
    #endregion

    #region Constructors
    public YamlRenderManager(YamlRenderSettings? settings = null) => _settings = settings ?? new YamlRenderSettings();
    public YamlRenderManager(int spacesPerLevel, char indentationCharacter) => _settings = new YamlRenderSettings(spacesPerLevel, indentationCharacter);
    #endregion

    #region Public Append Methods
    public void Append(string text) => _stringBuilder.Append(text);
    public void AppendLine(string text) => _stringBuilder.AppendLine(text);
    public void AppendLine() => _stringBuilder.AppendLine();
    public void AppendIndentation(int indentationLevel) => _stringBuilder.Append(_settings.GetIndentationString(indentationLevel));

    #endregion

    #region Public Control Methods
    public void Clear() => _stringBuilder.Clear();
    public string GetResult() => _stringBuilder.ToString();
    #endregion
}