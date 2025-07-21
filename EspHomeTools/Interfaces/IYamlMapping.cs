using System.Collections.Generic;

namespace EspHomeTools.Interfaces;

/// <summary>
/// Defines a structure representing a YAML mapping, consisting of a collection of key-value pairs.
/// The keys are represented as strings, while the values are YAML nodes implementing the IYamlNode interface.
/// </summary>
/// <remarks>
/// This interface extends IDictionary to provide standard dictionary operations for managing
/// the key-value pairs and integrates with YAML-specific functionalities through the IYamlStructure interface.
/// It is intended to encapsulate YAML mapping behavior, supporting features such as metadata handling
/// and YAML serialization or deserialization.
/// </remarks>
public interface IYamlMapping : IYamlStructure, IDictionary<string, IYamlNode> { }