namespace EspHomeTools.Interfaces;

/// <summary>
/// Represents an object that can be serialized into a YAML string format.
/// Provides functionality to convert the implementing object to its
/// equivalent YAML representation with optional indentation.
/// </summary>
public interface IYamlSerializable
{
    /// Converts the object to a YAML string representation.
    /// <param name="indent">
    /// The number of spaces to prepend to each line for indentation. Default is 0.
    /// </param>
    /// <returns>
    /// A string representing the YAML formatted content of the object.
    /// </returns>
    string ToYaml(int indent = 0);
}