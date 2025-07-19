using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EspHomeTools.Interfaces;

namespace EspHomeTools.Classes.Structures;

/// <summary>
/// Represents a YAML mapping structure which consists of key-value pairs, where both
/// keys and values are YAML nodes. Enables serialization to YAML format and provides
/// functionalities for managing the mapping's content.
/// </summary>
public class YamlMapping : IYamlMapping
{
    /// <summary>
    /// A private dictionary that holds the mapping between string keys and their corresponding
    /// <see cref="IYamlNode"/> values. This dictionary represents the node structure within the YAML mapping
    /// and is utilized for storing, retrieving, and managing key-value pairs that define the YAML data model.
    /// </summary>
    private readonly Dictionary<string, IYamlNode> _nodes = new();

    /// <summary>
    /// Gets or sets the name of the YAML node. This is used as the key
    /// when converting the object into a YAML representation.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Represents a comment associated with the YAML mapping or node.
    /// The comment provides additional information, annotation, or metadata
    /// for the YAML structure. It is stored as a string and can be used to
    /// improve readability or provide context to the YAML content.
    /// </summary>
    public string? Comment { get; set; }

    /// Gets or sets the tag for the YAML node.
    /// The tag provides additional type or semantic information
    /// and is typically used to identify the node as a specific kind of data in YAML serialization.
    public string? Tag { get; set; }

    /// <summary>
    /// Generates a YAML representation of the current object, including all child nodes,
    /// with optional indentation for formatting.
    /// </summary>
    /// <param name="indent">The number of spaces to use for indentation at the current level. Default is 0.</param>
    /// <returns>A string containing the YAML representation of the object.</
    public string ToYaml(int indent = 0)
    {
        var text = new StringBuilder();
        if (!string.IsNullOrWhiteSpace(Name))
        {
            text.AppendLine($"{new string(' ', indent)}{Name}:");
            indent += 2;
        }

        foreach (var kvp in _nodes)
        {
            kvp.Value.Name = kvp.Key;
            text.AppendLine(kvp.Value.ToYaml(indent));
        }

        return text.ToString().Trim();
    }


    #region IDictionary Implementierung (einfache Weiterleitung)

    /// Adds a new key-value pair to the dictionary of YAML nodes.
    /// <param name="key">
    /// The key associated with the value to add.
    /// </param>
    /// <param name="value">
    /// The value to associate with the key. Must implement the IYamlNode interface.
    /// </param>
    /// <exception cref="ArgumentException">
    /// Thrown if an element with the same key already exists in the dictionary.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// Thrown if the key is null.
    /// </exception>
    public void Add(string key, IYamlNode value) => _nodes.Add(key, value);
    /// Checks whether the mapping contains a specific key.
    /// <param name="key">The key to locate in the mapping.</param>
    /// <returns>True if the mapping contains an element with the specified key; otherwise, false.</returns>
    public bool ContainsKey(string key) => _nodes.ContainsKey(key);
    /// Removes an element with the specified key from the collection.
    /// <param name="key">The key of the element to remove.</param>
    /// <returns>
    /// True if the element is successfully removed; otherwise, false.
    /// This method also returns false if the key is not found in the collection.
    /// </returns>
    public bool Remove(string key) => _nodes.Remove(key);
    /// <summary>
    /// Attempts to retrieve the value associated with the specified key from the mapping.
    /// </summary>
    /// <param name="key">The key whose value is to be retrieved.</param>
    /// <param name="value">When this method returns, contains the value associated with the specified key, if the key is found; otherwise, the default value for the type of the value parameter. This parameter is passed uninitialized.</param>
    /// <returns>True if the mapping contains an element with the specified key; otherwise, false.</returns>
    public bool TryGetValue(string key, out IYamlNode value) => _nodes.TryGetValue(key, out value);

    /// <summary>
    /// Implements a collection of <see cref="IYamlNode"/> objects, representing the mapping
    /// of keys to YAML nodes. Provides methods for modifying the mapping, serializing the structure
    /// to YAML format, and other dictionary-like operations.
    /// </summary>
    /// <remarks>
    /// This class allows structured YAML content to be dynamically created, manipulated, and
    /// serialized into valid YAML text. Each entry in the mapping corresponds to a key-value
    /// pair where the value is an implementation of <see cref="IYamlNode"/>.
    /// </remarks>
    public IYamlNode this[string key]
    {
        get => _nodes[key];
        set => _nodes[key] = value;
    }

    /// <summary>
    /// Gets a collection containing the keys in the <see cref="YamlMapping"/>.
    /// </summary>
    /// <remarks>
    /// This property provides access to all keys stored in the underlying dictionary of the <see cref="YamlMapping"/>.
    /// The keys represent the names of child nodes within the YAML structure.
    /// </remarks>
    /// <value>
    /// A collection of strings representing the keys in the mapping.
    /// </value>
    public ICollection<string> Keys => _nodes.Keys;

    /// Gets a collection containing the values in the YAML mapping.
    /// This property provides access to all the values associated with keys
    /// within the YAML mapping. Each value represents an instance of
    /// `IYamlNode`, which can contain additional structure or data
    /// relevant to the YAML configuration.
    /// The collection is read-only, reflecting the current state of the
    /// mapping's values.
    /// Returns:
    /// A collection of type `ICollection<IYamlNode>` containing all the values
    /// in the mapping.
    public ICollection<IYamlNode> Values => _nodes.Values;

    /// Gets the number of key-value pairs contained in the mapping.
    /// This property returns an integer representing the total count of elements
    /// in the underlying dictionary that consists of keys and their associated values.
    public int Count => _nodes.Count;

    /// Gets a value indicating whether the collection is read-only.
    /// This property returns a boolean value that specifies whether elements
    /// in the collection can be modified. If the value is `true`, the collection
    /// is immutable and does not allow adding, removing, or updating elements.
    /// For this instance, the property always returns `false`, indicating that
    /// the collection is not read-only and modification is permitted.
    public bool IsReadOnly => false;
    /// Adds the specified key and value pair to the YAML mapping.
    /// <param name="item">
    /// The key-value pair to add to the YAML mapping. The key represents the name of the YAML node,
    /// and the value represents the corresponding YAML node object.
    /// </param>
    /// <exception cref="ArgumentException">
    /// Thrown when a key with the same name already exists in the mapping.
    /// </exception>
    public void Add(KeyValuePair<string, IYamlNode> item) => _nodes.Add(item.Key, item.Value);
    /// <summary>
    /// Removes all key-value pairs from the underlying dictionary within the YamlMapping instance.
    /// </summary>
    public void Clear() => _nodes.Clear();
    /// <summary>
    /// Determines whether the mapping contains the specified key-value pair.
    /// </summary>
    /// <param name="item">The key-value pair to locate in the mapping.</param>
    /// <returns>
    /// true if the key-value pair is found in the mapping; otherwise, false.
    /// </returns>
    public bool Contains(KeyValuePair<string, IYamlNode> item) => _nodes.Contains(item);
    /// <summary>
    /// Copies the elements of the <see cref="YamlMapping"/> instance to a specified array, starting at a particular index.
    /// </summary>
    /// <param name="array">The one-dimensional array that is the destination of the elements copied from the dictionary.
    /// The array must have zero-based indexing.</param>
    /// <param name="arrayIndex">The zero-based index in the array at which copying begins.</param>
    /// <exception cref="ArgumentNullException">Thrown if the <paramref name="array"/> is null.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if the <paramref name="arrayIndex"/> is less than 0.</exception>
    /// <exception cref="ArgumentException">
    /// Thrown if the number of elements in the source <see cref="YamlMapping"/> is greater
    /// than the available space from <paramref name="arrayIndex"/> to the end of the destination <paramref name="array"/>.
    /// </exception>
    public void CopyTo(KeyValuePair<string, IYamlNode>[] array, int arrayIndex) => ((ICollection<KeyValuePair<string, IYamlNode>>)_nodes).CopyTo(array, arrayIndex);
    /// Removes the specified key-value pair from the collection.
    /// <param name="item">The KeyValuePair to remove, containing the key and value to identify the element.</param>
    /// <returns>true if the element is successfully removed; otherwise, false. This method also returns false if the key was not found in the collection.</returns>
    public bool Remove(KeyValuePair<string, IYamlNode> item) => ((ICollection<KeyValuePair<string, IYamlNode>>)_nodes).Remove(item);
    /// Returns an enumerator that iterates through the collection of key-value pairs in the mapping structure.
    /// <return>
    /// An enumerator to iterate through the collection of key-value pairs where the key is a string, and the value is an object implementing IYamlNode.
    /// </return>
    public IEnumerator<KeyValuePair<string, IYamlNode>> GetEnumerator() => _nodes.GetEnumerator();
    /// Returns an enumerator that iterates through the collection.
    /// <return>An enumerator for the collection.</return>
    IEnumerator IEnumerable.GetEnumerator() => _nodes.GetEnumerator();

    #endregion
}