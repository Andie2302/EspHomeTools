using System.Collections.Generic;

namespace EspHomeTools.Interfaces;

/// <summary>
/// Represents a YAML sequence structure as defined in the YAML specification.
/// </summary>
/// <remarks>
/// This interface defines a collection of YAML nodes arranged in a sequential order. It combines
/// functionality from <see cref="IYamlStructure"/> and <see cref="IList{T}"/>, where T is
/// <see cref="IYamlNode"/>. Implementations of this interface enable manipulation and traversal
/// of YAML sequences using standard collection operations while adhering to the structure and
/// serialization rules of YAML.
/// </remarks>
/// <seealso cref="IYamlStructure"/>
/// <seealso cref="IYamlNode"/>
/// <seealso cref="IYamlSerializable"/>
public interface IYamlSequence : IYamlStructure, IList<IYamlNode> { }