namespace EspHomeTools.Interfaces;

public interface IYamlStructure : IYamlObject, IYamlRenderable
{
    string? BlockName { get; set; }
    bool HasBlockName { get; }
}

public interface IYamlStructure<T> : IYamlStructure { }