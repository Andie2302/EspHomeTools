namespace EspHomeTools.Interfaces;

/// <summary>
/// Represents a YAML scalar node that holds a single value.
/// </summary>
/// <typeparam name="TValue">
/// The type of the value contained within the scalar node. This value can represent various
/// scalar data types such as strings, integers, floats, or booleans.
/// </typeparam>
/// <remarks>
/// This interface extends <see cref="IYamlNode"/>, inheriting common properties such as
/// name, comment, and tag, and adds a specific property to manage the scalar value.
/// It serves as a contract for YAML scalar components to implement a strongly typed value.
/// </remarks>
public interface IYamlScalar<TValue> : IYamlNode
{
    /// <summary>
    /// Gets or sets the value represented by the YAML scalar node.
    /// </summary>
    /// <typeparamref name="TValue"/> is the underlying type of the value.
    /// <remarks>
    /// The <c>Value</c> property holds the actual data encapsulated by the scalar node. This value is
    /// serializable into a YAML-compatible string and can be deserialized into its corresponding
    /// .NET type.
    /// </remarks>
     TValue? Value { get; set; }
}