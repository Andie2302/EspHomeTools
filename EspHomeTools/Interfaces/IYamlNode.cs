namespace EspHomeTools.Interfaces;

public interface IYamlNode : IYamlSerializable
{
    string? Name { get; set; }
    string? Comment { get; set; }
    string? Tag { get; set; }
}