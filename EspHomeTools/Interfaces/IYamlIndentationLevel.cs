using System.Text;

namespace EspHomeTools.Interfaces;

public interface IYamlIndentationLevel
{
    int IndentationLevel { get; set; }
    int IndentationMultiplier { get; set; }
    int Count();
    public abstract string GetIndentation();
    public abstract void IncreaseIndentationLevel();
    public abstract void DecreaseIndentationLevel();
    public abstract void AppendIndentation(StringBuilder stringBuilder);
}