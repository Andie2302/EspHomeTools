namespace EspHomeTools.Interfaces;

public interface IYamlSerializable
{
    string ToYaml(int indent = 0);
}