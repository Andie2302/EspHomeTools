namespace EspHomeTools.Interfaces;

/// <summary>
/// Defines the base interface for YAML structures, which can include sequence and mapping implementations.
/// Serves as a foundation for more specific YAML node structures, extending the <see cref="IYamlNode"/> interface.
/// </summary>
public interface IYamlStructure : IYamlNode { }