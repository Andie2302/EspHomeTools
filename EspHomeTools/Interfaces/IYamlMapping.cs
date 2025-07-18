using System.Collections.Generic;

namespace EspHomeTools.Interfaces;

/// <summary>
/// Represents a YAML mapping structure, which contains a collection of key-value pairs.
/// Each key is of type string, and each value is a YAML node (IYamlNode).
/// </summary>
/// <remarks>
/// A YAML mapping is equivalent to a dictionary in terms of functionality,
/// but it also supports additional YAML-specific features such as a
/// "Name", "Comment", and "Tag". It inherits from IDictionary to provide
/// methods for managing the mapping's content and from IYamlStructure
/// for YAML-specific behavior.
/// </remarks>
public interface IYamlMapping : IYamlStructure, IDictionary<string, IYamlNode> { }