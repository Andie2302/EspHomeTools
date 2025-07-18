using System.Collections.Generic;

namespace EspHomeTools.Interfaces;

/// <summary>
/// Represents a YAML sequence structure as defined in the YAML specification.
/// </summary>
/// <remarks>
/// An implementation of this interface provides functionality to manage a list of YAML nodes
/// in a sequential order. It extends both <see cref="IYamlStructure"/> and <see cref="IList{T}"/>,
/// where T is <see cref="IYamlNode"/>. This allows the sequence to behave as a collection of
/// YAML nodes that can be indexed, enumerated, and manipulated while also supporting YAML-specific
/// serialization and structural behavior.
/// </remarks>
/// <seealso cref="IYamlStructure"/>
/// <seealso cref="IYamlNode"/>
/// <seealso cref="IYamlSerializable"/>
public interface IYamlSequence : IYamlStructure, IList<IYamlNode> { }