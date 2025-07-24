namespace EspHomeTools.Interfaces;

public interface IYamlStructure : IYamlObject
{
    string? BlockName { get; set; }
    bool HasBlockName { get; }
}

public interface IYamlStructure<T> : IYamlStructure { }