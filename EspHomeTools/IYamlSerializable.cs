namespace EspHomeTools.scratch;

public interface IYamlSerializable
{
    string ToYaml(int indent = 0);
}