using EspHomeTools.Interfaces;

namespace EspHomeTools.Classes.Structures;

public interface IYamlMapping : IYamlStructure
{
    IYamlNode this[string key] { get; set; }
    bool TryGetValue(string key, out IYamlNode? value);
    bool ContainsKey(string key);
}