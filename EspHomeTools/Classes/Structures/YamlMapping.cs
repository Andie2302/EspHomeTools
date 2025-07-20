using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EspHomeTools.Interfaces;

namespace EspHomeTools.Classes.Structures;

/// <summary>
/// Represents a YAML mapping structure that adheres to the IYamlMapping interface.
/// Provides methods for working with key-value pairs, where keys are strings and values implement the IYamlNode interface.
/// </summary>
public class YamlMapping : IYamlMapping
{
    /// <summary>
    /// Stores a collection of key-value pairs where keys are strings, and values are objects implementing the IYamlNode interface.
    /// </summary>
    /// <remarks>
    /// Represents the underlying data structure for a YAML mapping, providing methods and properties for managing
    /// YAML key-value pairs such as adding, removing, and retrieving nodes by their keys. This field is private and
    /// encapsulates the internal state of the mapping.
    /// </remarks>
    private readonly Dictionary<string, IYamlNode> _nodes = new();

    /// <summary>
    /// Gets or sets the name of the YAML mapping node.
    /// </summary>
    /// <remarks>
    /// The name represents the key of the mapping node in YAML format. This property may contain
    /// a string value that identifies a specific mapping node in the YAML structure. If set to null or empty,
    /// the node may not be serialized with a key.
    /// </remarks>
    public string? Name { get; set; }

    /// <summary>
    /// Gets or sets the comment associated with the YAML mapping.
    /// </summary>
    /// <remarks>
    /// The comment provides additional contextual information or documentation for the YAML mapping.
    /// It is included in the serialized YAML output as a comment line if it is not null or whitespace.
    /// </remarks>
    public string? Comment { get; set; }

    /// <summary>
    /// Gets or sets the tag associated with the YAML mapping.
    /// </summary>
    /// <remarks>
    /// The tag is an optional property that specifies a custom type or metadata for
    /// the YAML mapping. It can be used to provide additional context or information
    /// about the contents of the mapping.
    /// </remarks>
    public string? Tag { get; set; }

    /// <summary>
    /// Converts the current YamlMapping and its nested elements into a YAML string representation.
    /// </summary>
    /// <param name="indent">
    /// The number of spaces to use for indentation. Defaults to 0.
    /// </param>
    /// <returns>
    /// A string containing the YAML representation of the current YamlMapping structure, including its comments, name, and child nodes.
    /// </returns>
    public string ToYaml(int indent = 0)
    {
        var text = new StringBuilder();
        var prefix = new string(' ', indent);

        if (!string.IsNullOrWhiteSpace(Comment))
        {
            text.Append(FormatComment(Comment, prefix));
        }

        if (!string.IsNullOrWhiteSpace(Name))
        {
            text.Append(prefix).Append(Name).AppendLine(":");
            indent += 2;
        }

        var nodeStrings = new List<string>();
        foreach (var kvp in _nodes)
        {
            kvp.Value.Name = kvp.Key;
            nodeStrings.Add(kvp.Value.ToYaml(indent));
        }
        text.Append(string.Join(Environment.NewLine, nodeStrings.Where(s => !string.IsNullOrEmpty(s))));

        return text.ToString().TrimEnd('\r', '\n', ' ');
    }

    /// <summary>
    /// Formats a comment string for YAML output by adding a prefix and comment symbols.
    /// </summary>
    /// <param name="comment">The comment text to format.</param>
    /// <param name="prefix">The string prefix to prepend to each line of the comment.</param>
    /// <returns>A formatted comment string suitable for inclusion in a YAML file.</returns>
    private static string FormatComment(string comment, string prefix)
    {
        var commentLines = comment.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
        return string.Join(Environment.NewLine, commentLines.Select(line => $"{prefix}# {line}")) + Environment.NewLine;
    }

    // ... Rest der IDictionary Implementierung bleibt unverändert ...
    #region IDictionary Implementierung (einfache Weiterleitung)

    /// <summary>
    /// Adds a key-value pair to the YamlMapping structure.
    /// </summary>
    /// <param name="key">The key of the element to add to the mapping.</param>
    /// <param name="value">The value associated with the specified key.</param>
    /// <remarks>
    /// This method inserts a new element into the underlying dictionary that backs
    /// the YamlMapping. The specified key must be unique within the mapping; attempting
    /// to add a key that already exists will result in an exception.
    /// </remarks>
    public void Add(string key, IYamlNode value) => _nodes.Add(key, value);
    /// <summary>
    /// Determines whether the YamlMapping contains an element with the specified key.
    /// </summary>
    /// <param name="key">The key to locate in the YamlMapping.</param>
    /// <returns>
    /// true if the YamlMapping contains an element with the specified key; otherwise, false.
    /// </returns>
    public bool ContainsKey(string key) => _nodes.ContainsKey(key);
    /// <summary>
    /// Removes the element with the specified key from the YamlMapping structure.
    /// </summary>
    /// <param name="key">The key of the element to remove.</param>
    /// <returns>
    /// True if the element is successfully removed; otherwise, false.
    /// This method also returns false if the key was not found in the structure.
    /// </returns>
    public bool Remove(string key) => _nodes.Remove(key);
    /// <summary>
    /// Attempts to retrieve the value associated with the specified key.
    /// </summary>
    /// <param name="key">The key whose value should be retrieved.</param>
    /// <param name="value">
    /// When this method returns, contains the value associated with the specified key if the key is found;
    /// otherwise, the default value for the type of the value parameter. This parameter is passed uninitialized.
    /// </param>
    /// <returns>
    /// true if the key is found in the YamlMapping; otherwise, false.
    /// </returns>
    public bool TryGetValue(string key, out IYamlNode value) => _nodes.TryGetValue(key, out value);

    /// <summary>
    /// Gets or sets the <see cref="IYamlNode"/> associated with the specified key in the YamlMapping structure.
    /// </summary>
    /// <param name="key">The key whose associated value is to be retrieved or updated.</param>
    /// <returns>The <see cref="IYamlNode"/> associated with the specified key if the key is found; otherwise, an exception is thrown.</returns>
    /// <remarks>
    /// When getting, this property retrieves the value associated with the specified key in the YamlMapping structure.
    /// When setting, this property adds a new key-value pair or updates the value of an existing key in the YamlMapping structure.
    /// </remarks>
    public IYamlNode this[string key] { get => _nodes[key]; set => _nodes[key] = value; }

    /// <summary>
    /// Gets a collection containing the keys in the YamlMapping structure.
    /// </summary>
    /// <remarks>
    /// This property provides access to the collection of keys stored in the underlying
    /// dictionary. The keys represent the identifiers for each value in the YamlMapping.
    /// The returned collection is read-only.
    /// </remarks>
    public ICollection<string> Keys => _nodes.Keys;

    /// <summary>
    /// Gets the collection of values contained in the YAML mapping.
    /// </summary>
    /// <remarks>
    /// This property provides access to the values stored in the underlying dictionary of the YAML mapping.
    /// Each value in the collection is an instance of <see cref="IYamlNode"/>, which represents an individual
    /// YAML node. The collection does not allow direct modification, but reflects the current state of the
    /// mapping as values are added, removed, or updated.
    /// </remarks>
    public ICollection<IYamlNode> Values => _nodes.Values;

    /// <summary>
    /// Gets the number of key-value pairs contained in the YamlMapping structure.
    /// </summary>
    /// <remarks>
    /// This property returns the total count of elements in the underlying dictionary
    /// that represents the YamlMapping. Accessing this property is an O(1) operation.
    /// </remarks>
    public int Count => _nodes.Count;

    /// <summary>
    /// Gets a value indicating whether the collection is read-only.
    /// </summary>
    /// <value>
    /// This property always returns <c>false</c>, as the collection allows modification of its elements.
    /// </value>
    /// <remarks>
    /// A read-only collection does not allow the addition, removal, or modification of elements after the
    /// collection is created. As the underlying dictionary in this implementation supports these operations,
    /// the property indicates that the collection is not read-only.
    /// </remarks>
    public bool IsReadOnly => false;

    /// <summary>
    /// Adds a key-value pair to the YamlMapping structure.
    /// </summary>
    /// <param name="key">The key associated with the YAML node being added.</param>
    /// <param name="value">The YAML node to be added to the mapping.</param>
    /// <remarks>
    /// This method adds the specified key-value pair to the underlying dictionary
    /// of the YamlMapping. If a duplicate key is provided, an exception will
    /// be thrown as duplicate keys are not allowed.
    /// </remarks>
    public void Add(KeyValuePair<string, IYamlNode> item) => _nodes.Add(item.Key, item.Value);
    /// <summary>
    /// Removes all elements from the YamlMapping structure.
    /// </summary>
    /// <remarks>
    /// Invoking this method clears the internal dictionary that stores
    /// the key-value pairs within the YamlMapping. After execution, the
    /// structure contains no elements, and its count will be zero.
    /// </remarks>
    public void Clear() => _nodes.Clear();
    /// <summary>
    /// Determines whether the YamlMapping contains a specific key-value pair.
    /// </summary>
    /// <param name="item">The key-value pair to locate in the YamlMapping structure.</param>
    /// <returns>
    /// True if the key-value pair is found in the YamlMapping; otherwise, false.
    /// </returns>
    public bool Contains(KeyValuePair<string, IYamlNode> item) => _nodes.Contains(item);
    /// <summary>
    /// Copies the elements of the YamlMapping structure to a specified array, starting at a particular index.
    /// </summary>
    /// <param name="array">The destination array where elements from the YamlMapping will be copied.</param>
    /// <param name="arrayIndex">The zero-based index in the destination array at which copying begins.</param>
    /// <remarks>
    /// This method allows copying the YamlMapping elements into an array of KeyValuePair instances. The array must
    /// have a sufficient amount of available space from the specified index to accommodate all elements in the collection.
    /// </remarks>
    public void CopyTo(KeyValuePair<string, IYamlNode>[] array, int arrayIndex) => ((ICollection<KeyValuePair<string, IYamlNode>>)_nodes).CopyTo(array, arrayIndex);
    /// <summary>
    /// Removes the specified key-value pair from the YamlMapping structure.
    /// </summary>
    /// <param name="item">The key-value pair to remove from the YamlMapping.</param>
    /// <returns>
    /// True if the key-value pair was successfully removed; otherwise, false. This method also returns
    /// false if the specified key-value pair was not found in the YamlMapping.
    /// </returns>
    public bool Remove(KeyValuePair<string, IYamlNode> item) => ((ICollection<KeyValuePair<string, IYamlNode>>)_nodes).Remove(item);
    /// <summary>
    /// Returns an enumerator that iterates through the YamlMapping structure.
    /// </summary>
    /// <remarks>
    /// This method provides support for iterating over the key-value pairs stored in the
    /// underlying dictionary of the YamlMapping. Each key-value pair represents an entry
    /// within the YAML mapping structure.
    /// </remarks>
    /// <returns>
    /// An enumerator for traversing the key-value pairs contained in the YamlMapping.
    /// </returns>
    public IEnumerator<KeyValuePair<string, IYamlNode>> GetEnumerator() => _nodes.GetEnumerator();
    /// <summary>
    /// Returns an enumerator that iterates through the key-value pairs in the YamlMapping structure.
    /// </summary>
    /// <remarks>
    /// This method retrieves an enumerator that facilitates iterative access to the
    /// elements in the underlying dictionary used by the YamlMapping. The enumerator
    /// operates in the same order in which elements were added to the dictionary,
    /// unless explicitly modified.
    /// </remarks>
    /// <returns>
    /// An enumerator that can be used to iterate through the collection of key-value pairs in the YamlMapping.
    /// </returns>
    IEnumerator IEnumerable.GetEnumerator() => _nodes.GetEnumerator();
    #endregion
}