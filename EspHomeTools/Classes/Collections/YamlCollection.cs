using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using EspHomeTools.Interfaces;
namespace EspHomeTools.Classes.Collections;

/// <summary>
/// Represents a specialized collection for managing YAML nodes, providing functionalities
/// for adding, accessing, and organizing nodes. The collection uses a case-insensitive key comparison
/// and is capable of serializing YAML nodes into a valid YAML string format.
/// </summary>
/// <remarks>
/// This class implements <see cref="IDictionary{TKey, TValue}"/> where the key is the name of the node
/// (a <see cref="string"/>) and the value is an instance of <see cref="IYamlNode"/>. It allows usage
/// of custom sorting through a defined comparer for YAML serialization and ensures operations remain
/// consistent regardless of key casing.
/// </remarks>
/// <example>
/// Custom sorting of nodes can be applied during serialization by setting the <see cref="CustomSorter"/> property.
/// </example>
/// <threadsafety>
/// This class is not thread-safe and should be externally synchronized if accessed concurrently.
/// </threadsafety>
public sealed class YamlCollection : IDictionary<string, IYamlNode>
{
    /// <summary>
    /// Defines the separator used between nodes during YAML serialization in the <see cref="YamlCollection"/> class.
    /// </summary>
    /// <remarks>
    /// The <c>NodeSeparator</c> is a static string that consists of two newline characters. It is used to separate
    /// serialized YAML nodes, ensuring a clear distinction between individual nodes in the generated YAML output.
    /// </remarks>
    private readonly static string NodeSeparator = Environment.NewLine + Environment.NewLine;

    /// <summary>
    /// Represents the internal dictionary responsible for storing YAML nodes within the collection.
    /// </summary>
    /// <remarks>
    /// The dictionary uses a case-insensitive string comparer for keys, allowing flexibility in key casing.
    /// It maps string keys to objects implementing the <see cref="IYamlNode"/> interface, enabling storage
    /// and manipulation of YAML data structures with node-specific metadata and serialization capabilities.
    /// </remarks>
    private readonly Dictionary<string, IYamlNode> _nodes = new(StringComparer.OrdinalIgnoreCase);

    /// <summary>
    /// Gets or sets the custom sorter used to determine the order of nodes in the collection.
    /// </summary>
    /// <remarks>
    /// When a custom sorter is assigned, it overrides the default sorting behavior by allowing
    /// the user to specify a custom <see cref="IComparer{KeyValuePair}"/> for ordering the key-value
    /// pairs in the collection. If not set, the collection will use an ordinal string comparison
    /// based on the keys for sorting.
    /// </remarks>
    /// <value>
    /// An implementation of <see cref="IComparer{T}"/> that defines a custom sort order for the
    /// key-value pairs in the collection, or <c>null</c> if default sorting is applied.
    /// </value>
    public IComparer<KeyValuePair<string, IYamlNode>>? CustomSorter { get; set; }

    /// Converts the YAML collection to its serialized YAML string representation.
    /// The serialization includes all nodes in the collection, optionally sorted based on a custom comparer if provided.
    /// The resulting YAML string adheres to standard YAML formatting rules.
    /// <returns>
    /// A string that represents the serialized YAML content of the collection.
    /// </returns>
    public string ToYaml() => SerializeNodes(GetSortedNodes());

    /// <summary>
    /// Retrieves the nodes in the collection sorted based on a custom sorter if provided,
    /// otherwise orders them alphabetically by their keys in a case-insensitive manner.
    /// </summary>
    /// <returns>
    /// An enumerable collection of key-value pairs representing the sorted nodes in the dictionary.
    /// </returns>
    private IEnumerable<KeyValuePair<string, IYamlNode>> GetSortedNodes() =>
        CustomSorter != null
            ? _nodes.OrderBy(nodeKeyValuePair => nodeKeyValuePair, CustomSorter)
            : _nodes.OrderBy(nodeKeyValuePair => nodeKeyValuePair.Key, StringComparer.Ordinal);

    /// <summary>
    /// Serializes a collection of sorted YAML nodes into a formatted YAML string.
    /// </summary>
    /// <param name="sortedNodes">
    /// An enumerable collection of key-value pairs representing sorted YAML nodes. Each key is the node's name,
    /// and the value is the corresponding serialized YAML node.
    /// </param>
    /// <returns>
    /// A formatted string that represents the YAML serialization of the given collection of nodes, separated by
    /// a defined node separator.
    /// </returns>
    private static string SerializeNodes(IEnumerable<KeyValuePair<string, IYamlNode>> sortedNodes)
    {
        var serializedNodeStrings = sortedNodes.Select(SerializeNode);
        return string.Join(NodeSeparator, serializedNodeStrings);
    }

    /// <summary>
    /// Serializes a YAML node and its associated key into a YAML format string.
    /// </summary>
    /// <param name="nodeKeyValuePair">
    /// The key-value pair where the key is the name of the node and the value is the YAML node to serialize.
    /// </param>
    /// <returns>
    /// A string representing the YAML serialization of the provided node.
    /// </returns>
    private static string SerializeNode(KeyValuePair<string, IYamlNode> nodeKeyValuePair)
    {
        nodeKeyValuePair.Value.Name = nodeKeyValuePair.Key;
        return nodeKeyValuePair.Value.ToYaml();
    }

    /// <summary>
    /// Provides indexed access to the YAML nodes contained in the collection.
    /// </summary>
    /// <param name="key">The key representing the name or identifier of the YAML node.</param>
    /// <returns>The <see cref="IYamlNode"/> object associated with the specified key.</returns>
    /// <exception cref="KeyNotFoundException">Thrown if the specified key does not exist in the collection.</exception>
    public IYamlNode this[string key]
    {
        get => _nodes[key];
        set => _nodes[key] = value;
    }

    /// <summary>
    /// Gets a collection containing the keys in the <see cref="YamlCollection"/>.
    /// </summary>
    /// <remarks>
    /// This property returns an <see cref="ICollection{T}"/> of type <see cref="string"/>, representing the keys
    /// stored within the <see cref="YamlCollection"/>. These keys are case-insensitive due to the use of
    /// <see cref="StringComparer.OrdinalIgnoreCase"/> in the underlying dictionary. Modifications to the
    /// dictionary's keys will be reflected in this collection.
    /// </remarks>
    public ICollection<string> Keys => _nodes.Keys;

    /// <summary>
    /// Gets the collection of values contained in the YAML collection.
    /// </summary>
    /// <remarks>
    /// This property provides access to all the values in the YAML collection as an <see cref="ICollection{T}"/>
    /// of <see cref="IYamlNode"/>. The collection reflects the current state of the underlying dictionary
    /// and changes dynamically as items are added or removed from the dictionary.
    /// </remarks>
    public ICollection<IYamlNode> Values => _nodes.Values;

    /// <summary>
    /// Gets the number of key-value pairs contained in the collection.
    /// </summary>
    /// <remarks>
    /// This property retrieves the total number of key-value pairs currently stored in the <see cref="YamlCollection"/>.
    /// The count reflects the actual number of entries in the underlying dictionary.
    /// </remarks>
    /// <value>
    /// An <see cref="int"/> representing the total number of elements in the collection.
    /// </value>
    public int Count => _nodes.Count;

    /// <summary>
    /// Gets a value indicating whether the collection is read-only.
    /// </summary>
    /// <remarks>
    /// This property returns a boolean indicating if the underlying collection
    /// of YAML nodes can be modified. A value of <c>true</c> means the collection
    /// is read-only and does not permit addition, removal, or modification of elements.
    /// </remarks>
    public bool IsReadOnly => GetNodesAsCollection().IsReadOnly;

    /// <summary>
    /// Adds the specified key and value to the collection.
    /// </summary>
    /// <param name="key">The key of the element to add to the collection.</param>
    /// <param name="value">The value of the element to add to the collection, implementing <see cref="IYamlNode"/>.</param>
    /// <exception cref="ArgumentNullException">Thrown if the key or value is null.</exception>
    /// <exception cref="ArgumentException">Thrown if an element with the same key already exists in the collection.</exception>
    public void Add(string key, IYamlNode value) => _nodes.Add(key, value);
    /// <summary>
    /// Adds the specified key-value pair to the collection.
    /// </summary>
    /// <param name="item">The key-value pair to add to the collection. The key represents the identifier, and the value is the corresponding <see cref="IYamlNode"/> implementation.</param>
    public void Add(KeyValuePair<string, IYamlNode> item) => GetNodesAsCollection().Add(item);
    /// <summary>
    /// Removes all elements from the current <see cref="YamlCollection"/> instance.
    /// </summary>
    /// <remarks>
    /// This method clears the internal dictionary that stores the collection of YAML nodes,
    /// effectively resetting the collection to an empty state. All previously stored keys
    /// and values will be removed. Use this with caution as the operation is irreversible for
    /// the current instance.
    /// </remarks>
    public void Clear() => _nodes.Clear();
    /// <summary>
    /// Determines whether the collection contains the specified key-value pair.
    /// </summary>
    /// <param name="item">The key-value pair to locate in the collection.</param>
    /// <returns>True if the key-value pair exists in the collection; otherwise, false.</returns>
    public bool Contains(KeyValuePair<string, IYamlNode> item) => GetNodesAsCollection().Contains(item);
    /// <summary>
    /// Checks whether the collection contains an element with the specified key.
    /// </summary>
    /// <param name="key">The key to locate in the collection.</param>
    /// <returns>
    /// True if the collection contains an element with the specified key; otherwise, false.
    /// </returns>
    public bool ContainsKey(string key) => _nodes.ContainsKey(key);
    /// <summary>
    /// Copies the elements of the dictionary to a specified array, starting at a specific index in the target array.
    /// </summary>
    /// <param name="array">The one-dimensional array that is the destination of the elements copied. The array must have zero-based indexing.</param>
    /// <param name="arrayIndex">The zero-based index in the array at which copying begins.</param>
    /// <exception cref="ArgumentNullException">Thrown when the array is null.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when arrayIndex is less than 0.</exception>
    /// <exception cref="ArgumentException">Thrown when the number of elements in the source dictionary is greater than the space available in the destination array from arrayIndex to the end of the destination array.</exception>
    public void CopyTo(KeyValuePair<string, IYamlNode>[] array, int arrayIndex) =>
        GetNodesAsCollection().CopyTo(array, arrayIndex);
    /// <summary>
    /// Removes the element with the specified key from the collection.
    /// </summary>
    /// <param name="key">The key of the element to remove.</param>
    /// <returns>
    /// true if the element is successfully removed; otherwise, false.
    /// This method also returns false if the key was not found in the collection.
    /// </returns>
    public bool Remove(string key) => _nodes.Remove(key);
    /// <summary>
    /// Removes the specified key-value pair from the collection.
    /// </summary>
    /// <param name="item">The key-value pair to remove from the collection.</param>
    /// <returns>
    /// True if the specified key-value pair is successfully removed; otherwise, false.
    /// Also returns false if the key-value pair was not found in the collection.
    /// </returns>
    public bool Remove(KeyValuePair<string, IYamlNode> item) => GetNodesAsCollection().Remove(item);

#if NETSTANDARD2_0 || NETCOREAPP3_1
    public bool TryGetValue(string key, out IYamlNode value) => _nodes.TryGetValue(key, out value);
#else
    /// <summary>
    /// Attempts to get the value associated with the specified key in the collection.
    /// </summary>
    /// <param name="key">The key of the value to retrieve.</param>
    /// <param name="value">
    /// When this method returns, contains the value associated with the specified key, if the key is found;
    /// otherwise, the default value for the type of the value parameter. This parameter is passed uninitialized.
    /// </param>
    /// <returns>
    /// true if the collection contains an element with the specified key; otherwise, false.
    /// </returns>
    public bool TryGetValue(string key, [MaybeNullWhen(false)] out IYamlNode value) =>
        _nodes.TryGetValue(key, out value);
#endif

    /// <summary>
    /// Returns an enumerator that iterates through the collection.
    /// </summary>
    /// <returns>
    /// An enumerator for the collection that provides sequential access to its elements.
    /// </returns>
    public IEnumerator<KeyValuePair<string, IYamlNode>> GetEnumerator() => _nodes.GetEnumerator();
    /// <summary>
    /// Returns an enumerator that iterates through the collection.
    /// </summary>
    /// <returns>
    /// An enumerator that can be used to iterate through the collection of key-value pairs in the dictionary.
    /// </returns>
    IEnumerator IEnumerable.GetEnumerator() => _nodes.GetEnumerator();

    /// <summary>
    /// Retrieves the nodes as a collection of key-value pairs in the form of an <see cref="ICollection{T}"/>.
    /// </summary>
    /// <returns>
    /// An <see cref="ICollection{T}"/> representing the internal dictionary of YAML nodes as key-value pairs.
    /// </returns>
    private ICollection<KeyValuePair<string, IYamlNode>> GetNodesAsCollection() =>
        (ICollection<KeyValuePair<string, IYamlNode>>)_nodes;
}