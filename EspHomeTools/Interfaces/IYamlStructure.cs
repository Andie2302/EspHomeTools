namespace EspHomeTools.Interfaces;

/// <summary>
/// Represents a YAML structure element, which can either be a sequence or a mapping.
/// It provides a base interface for structured YAML data and extends the functionality of <see cref="IYamlNode"/>.
/// </summary>
public interface IYamlStructure : IYamlNode { }