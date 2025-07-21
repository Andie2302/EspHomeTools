namespace EspHomeTools.Interfaces;

/// <summary>
/// Defines an object capable of serializing itself into a YAML formatted string.
/// Provides methods to represent the object's data structure in YAML syntax.
/// </summary>
public interface IYamlSerializable
{
    /// <summary>
    /// Converts the object to a YAML string representation.
    /// </summary>
    /// <param name="name">The name (key) to be used for this node when it's part of a mapping. Can be null.</param>
    /// <param name="indent">The number of spaces to prepend to each line for indentation. Default is 0.</param>
    /// <returns>A string representing the YAML formatted content of the object.</returns>
    string ToYaml(int indent, string? name);
}