using EspHomeTools.Classes.Structures;

namespace EspHomeTools.Interfaces;

public interface IYamlSerializer
{
    string SerializeMapping(YamlMapping mapping, int indent, string? name);
}