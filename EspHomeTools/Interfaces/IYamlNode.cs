namespace EspHomeTools.Interfaces;

/// <summary>
/// Represents a general YAML node abstraction that can be used as the base for various types of YAML structures.
/// </summary>
/// <remarks>
/// This interface defines the common properties shared by all YAML nodes, such as name, comment, and tag. It
/// also serves as a foundational type for more specific YAML components like scalars, sequences, and mappings.
/// </remarks>
public interface IYamlNode : IYamlSerializable
{
    /// <summary>
    /// Gets or sets the name of the YAML node.
    /// </summary>
    /// <remarks>
    /// The Name property provides an optional identifier for the YAML node.
    /// It can be used to represent the key or title associated with the node
    /// in a structured YAML document. This property is nullable, allowing it to
    /// remain unset if a name is not explicitly required.
    /// </remarks>
    string? Name { get; set; }

    /// <summary>
    /// Gets or sets the comment associated with the YAML node.
    /// </summary>
    /// <remarks>
    /// The comment property represents optional user-defined metadata or annotations for the YAML node.
    /// It can be used to add descriptive text or notes that may assist in understanding or documenting
    /// the purpose of the node within the YAML structure. This property is serialized as a comment
    /// in the generated YAML output, prefixed by the YAML comment indicator (#).
    /// </remarks>
    string? Comment { get; set; }

    /// <summary>
    /// Gets or sets the tag associated with the YAML node.
    /// </summary>
    /// <remarks>
    /// The tag is an optional property that can be used to specify additional metadata about
    /// the YAML node. It may be utilized for custom processing, type annotations, or other
    /// advanced YAML features. When not specified, the YAML node is treated as a standard
    /// untaged entity.
    /// </remarks>
    string? Tag { get; set; }
}