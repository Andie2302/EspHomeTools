namespace EspHomeTools.Classes.Structures;

public interface IYamlSerializer
{
    string SerializeMapping(YamlMapping mapping, int indent, string? name);
}