namespace EspHomeTools.Interfaces;

/// <summary>
/// Represents a YAML scalar node containing a single, strongly typed value.
/// </summary>
/// <typeparam name="TValue">
/// The type of data contained in the scalar node. This scalar value can represent
/// various data types, such as strings, numbers, or boolean values.
/// </typeparam>
/// <remarks>
/// This interface defines a scalar YAML element and extends the <see cref="IYamlNode"/> interface.
/// Implementations of this interface should provide access to the scalar's value, exposing it through
/// a strongly typed property. It forms a foundational building block for YAML components that need
/// to represent scalar (single-value) elements in a YAML hierarchy.
/// </remarks>
public interface IYamlScalar<TValue> : IYamlNode
{
    /// <summary>
    /// Gets or sets the scalar value represented by this YAML node.
    /// </summary>
    /// <typeparamref name="TValue"/> is the type of the scalar value.
    /// <remarks>
    /// This property encapsulates the data associated with a YAML scalar node. The value
    /// can represent various primitive or simple types, such as strings, numbers, or booleans.
    /// It is designed to be serializable and deserializable while maintaining compatibility
    /// with YAML data representations.
    /// </remarks>
    TValue? Value { get; set; }
}