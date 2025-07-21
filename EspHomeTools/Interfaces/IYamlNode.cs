namespace EspHomeTools.Interfaces;

/// <summary>
/// Defines the basic functionality and properties for a YAML node representation.
/// </summary>
/// <remarks>
/// This interface represents the core abstraction for YAML elements, providing foundational
/// properties and serving as a base for more specific YAML constructs such as scalars, sequences,
/// and structures. It includes common attributes such as name, comment, and tag.
/// </remarks>
public interface IYamlNode : IYamlSerializable
{
    /// <summary>
    /// Gets or sets the name associated with the YAML node.
    /// </summary>
    /// <remarks>
    /// This property represents an optional identifier for a YAML node, which can be used
    /// to assign a specific name or key to the node within YAML documents. It allows for
    /// more organized and identifiable nodes. The property is nullable, enabling scenarios
    /// where a name may not be explicitly assigned.
    /// </remarks>
    string? Name { get; set; }

    /// <summary>
    /// Gets or sets the comment associated with the YAML node.
    /// </summary>
    /// <remarks>
    /// The Comment property allows for adding optional descriptive text or metadata
    /// to a YAML node. This property is useful for annotations or explanations
    /// within YAML documents, enhancing their readability and providing context
    /// about the node's purpose or use. It supports null values if no comment is provided.
    /// </remarks>
    string? Comment { get; set; }

    /// <summary>
    /// Gets or sets the YAML tag associated with the node.
    /// </summary>
    /// <remarks>
    /// The Tag property specifies the data type or additional semantic information
    /// for the YAML node. It aids in providing context or type definition within
    /// a YAML document. The value of this property is optional and can be null if
    /// no specific tag is required.
    /// </remarks>
    string? Tag { get; set; }
}