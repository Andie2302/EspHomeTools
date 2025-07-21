using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using EspHomeTools.Interfaces;

namespace EspHomeTools.Classes.Structures;

/// Represents a mapping node in a YAML structure. This class provides functionality
/// to store key-value pairs where keys are strings and values are YAML nodes. It also
/// supports converting the mapping into a YAML-formatted string, including optional
/// comments and tags.
/// Implements the IYamlMapping interface, which itself includes capabilities of both
/// IYamlStructure and IDictionary<string, IYamlNode>.
/// The class supports full dictionary operations such as adding, removing,
/// and enumerating key-value pairs. Additionally, it provides mechanisms to serialize
/// the mapping into a formatted YAML string.
public class YamlMapping : IYamlMapping
{
    /// <summary>
    /// Represents the default increment value for indentation in YAML serialization.
    /// This constant is used to determine the amount of spaces to increase
    /// the indentation level for nested YAML nodes.
    /// </summary>
    private const int DefaultIndentIncrement = 2;

    /// <summary>
    /// Represents a constant string used as the separator between keys and values
    /// in a YAML mapping.
    /// </summary>
    private const string ColonSeparator = ":";

    /// <summary>
    /// Defines the prefix used to denote comments in a YAML document.
    /// </summary>
    /// <remarks>
    /// This constant is used to prepend a comment symbol (e.g., <c>#</c>) to lines
    /// of text that are intended to be treated as comments in a YAML structure.
    /// </remarks>
    private const string CommentPrefix = "# ";

    /// Represents the space character used for indentation in YAML formatting.
    private const char SpaceChar = ' ';

    /// <summary>
    /// Specifies the set of characters to trim from the start and end of strings
    /// when processing YAML data in the <see cref="YamlMapping"/> class.
    /// The trimming is applied to ensure the cleanliness and consistency
    /// of the output YAML content, particularly for formatting purposes.
    /// </summary>
    private readonly static char[] TrimChars = { '\r', '\n', ' ' };

    /// Represents a collection of line separators used to split text into logical lines.
    /// This constant is utilized to determine line boundaries in various operations, such as splitting comments
    /// or processing multi-line strings.
    /// The array includes the following line terminators:
    /// - Windows-style ("\r\n")
    /// - Old Mac-style ("\r")
    /// - Unix/Linux-style ("\n")
    private readonly static string[] LineSeparators = { "\r\n", "\r", "\n" };

    /// <summary>
    /// Represents the internal collection of key-value pairs that make up the YAML mapping structure.
    /// </summary>
    /// <remarks>
    /// This dictionary stores the mapping between keys (represented as strings) and their respective
    /// YAML nodes (implemented as <see cref="IYamlNode"/>). It serves as the backbone of the <see cref="YamlMapping"/>
    /// class, enabling hierarchical data representation common in YAML documents.
    /// </remarks>
    private readonly Dictionary<string, IYamlNode> _nodes = new();

    /// <summary>
    /// Gets or sets the name of the YAML mapping node.
    /// </summary>
    /// <remarks>
    /// This property represents the name identifier associated with the YAML mapping.
    /// It is typically used as a key or title in YAML structures to denote a specific section
    /// or component. Setting this property to a valid value allows the mapping to be
    /// appropriately identified and serialized.
    /// </remarks>
    public string? Name { get; set; }

    /// <summary>
    /// Represents an optional comment associated with a YAML mapping.
    /// The comment, if provided, can be used to include additional descriptive
    /// or explanatory text that is appended to the generated YAML representation.
    /// </summary>
    public string? Comment { get; set; }

    /// <summary>
    /// Gets or sets the tag associated with the YAML mapping.
    /// </summary>
    /// <remarks>
    /// This property represents the YAML tag linked to the mapping, which can be used for custom identification or categorization of the mapping element.
    /// </remarks>
    public string? Tag { get; set; }

    /// Converts the current object to a YAML representation.
    /// The method generates the YAML output with the provided indent level.
    /// <param name="indent">
    /// The number of spaces to use for indentation. Default is 0.
    /// </param>
    /// <returns>
    /// A string containing the YAML representation of the object.
    /// </returns>
    public string ToYaml(int indent = 0)
    {
        var yamlBuilder = new StringBuilder();
        var indentString = CreateIndentString(indent);
        AppendCommentIfPresent(yamlBuilder, indentString);
        var childIndent = AppendNameIfPresent(yamlBuilder, indentString, indent);
        AppendChildNodes(yamlBuilder, childIndent);
        return yamlBuilder.ToString().TrimEnd(TrimChars);
    }

    /// <summary>
    /// Creates a string consisting of a specified number of space characters for indentation.
    /// </summary>
    /// <param name="indent">The number of spaces to include in the resulting string.</param>
    /// <returns>A string containing the specified number of spaces for indentation.</returns>
    private static string CreateIndentString(int indent) => new(SpaceChar, indent);

    /// <summary>
    /// Appends the comment, if it is present, to the provided StringBuilder instance.
    /// </summary>
    /// <param name="builder">The StringBuilder instance to which the comment will be appended.</param>
    /// <param name="indentString">The string representation of the indentation to prepend to the comment.</param>
    private void AppendCommentIfPresent(StringBuilder builder, string indentString)
    {
        if (!string.IsNullOrWhiteSpace(Comment))
        {
            builder.Append(FormatComment(Comment, indentString));
        }
    }

    /// <summary>
    /// Appends the name property of the YAML mapping to the given StringBuilder if it is not null or whitespace.
    /// </summary>
    /// <param name="builder">The StringBuilder to which the name will be appended if present.</param>
    /// <param name="indentString">The string used for indentation.</param>
    /// <param name="currentIndent">The current indentation level.</param>
    /// <returns>The updated indentation level after appending the name, or the original indentation level if the name is not present.</returns>
    private int AppendNameIfPresent(StringBuilder builder, string indentString, int currentIndent)
    {
        if (string.IsNullOrWhiteSpace(Name))
            return currentIndent;

        builder.Append(indentString).Append(Name).AppendLine(ColonSeparator);
        return currentIndent + DefaultIndentIncrement;
    }

    /// Appends the child nodes of the current YAML structure to the specified StringBuilder instance
    /// with the specified indentation level.
    /// <param name="builder">The StringBuilder instance to which the YAML representation of the child nodes will be appended.</param>
    /// <param name="indent">The number of spaces used for indentation when appending the child nodes.</param>
    private void AppendChildNodes(StringBuilder builder, int indent)
    {
        var nodeStrings = _nodes.Select(kvp => SerializeNode(kvp, indent)).Where(yaml => !string.IsNullOrEmpty(yaml));
        builder.Append(string.Join(Environment.NewLine, nodeStrings));
    }

    /// Serializes a key-value pair representing a YAML node into its YAML string representation.
    /// <param name="kvp">
    /// The key-value pair where the key is a string name of the node and the value is an object implementing the IYamlNode interface.
    /// </param>
    /// <param name="indent">
    /// The indentation level to apply to the serialized YAML string. Default value is 0.
    /// </param>
    /// <returns>
    /// A string containing the YAML representation of the node, or an empty string if the serialization fails or is invalid.
    /// </returns>
    private static string SerializeNode(KeyValuePair<string, IYamlNode> kvp, int indent)
    {
        kvp.Value.Name = kvp.Key;
        return kvp.Value.ToYaml(indent);
    }

    /// Formats a comment string by appending a prefix and the standard comment symbol to each line of the comment.
    /// <param name="comment">
    /// The comment text to be formatted. Each line in the text will be prefixed with the given prefix and a comment symbol.
    /// </param>
    /// <param name="prefix">
    /// The prefix to prepend to each line of the comment, typically representing indentation or spacing.
    /// </param>
    /// <returns>
    /// A formatted comment string where each line begins with the specified prefix followed by a comment symbol.
    /// </returns>
    private static string FormatComment(string comment, string prefix)
    {
        var commentLines = comment.Split(LineSeparators, StringSplitOptions.None);
        return string.Join(Environment.NewLine, commentLines.Select(line => $"{prefix}{CommentPrefix}{line}")) + Environment.NewLine;
    }

    /// <summary>
    /// Adds a key-value pair to the YAML mapping.
    /// </summary>
    /// <param name="key">The key associated with the value to add to the YAML mapping.</param>
    /// <param name="value">The value to associate with the specified key in the YAML mapping.</param>
    public void Add(string key, IYamlNode value) => _nodes.Add(key, value);
    /// <summary>
    /// Determines whether the mapping contains the specified key.
    /// </summary>
    /// <param name="key">The key to locate in the mapping.</param>
    /// <returns>True if the mapping contains an element with the specified key; otherwise, false.</returns>
    public bool ContainsKey(string key) => _nodes.ContainsKey(key);
    /// Removes the element with the specified key from the mapping.
    /// <param name="key">The key of the element to remove.</param>
    /// <returns>
    /// true if the element is successfully removed; otherwise, false.
    /// This method also returns false if the key was not found in the mapping.
    /// </returns>
    public bool Remove(string key) => _nodes.Remove(key);

#if NETSTANDARD2_0 || NETCOREAPP3_1
    public bool TryGetValue(string key, out IYamlNode value) => _nodes.TryGetValue(key, out value);
#else
    /// <summary>
    /// Attempts to retrieve the value associated with the specified key.
    /// </summary>
    /// <param name="key">The key whose value is to be retrieved.</param>
    /// <param name="value">When this method returns, contains the value associated with the specified key, if the key is found; otherwise, the default value for the type of the value parameter. This parameter is passed uninitialized.</param>
    /// <returns>Returns true if the key was found; otherwise, returns false.</returns>
    public bool TryGetValue(string key, [MaybeNullWhen(false)] out IYamlNode value) => _nodes.TryGetValue(key, out value);
#endif

    /// <summary>
    /// Provides indexed access to the nodes in the YAML mapping,
    /// allowing retrieval and modification of values associated with specified keys.
    /// </summary>
    /// <param name="key">The key corresponding to the desired YAML node.</param>
    /// <returns>The <see cref="IYamlNode"/> associated with the specified key.</returns>
    /// <exception cref="KeyNotFoundException">Thrown if the specified key does not exist in the mapping.</exception>
    public IYamlNode this[string key]
    {
        get => _nodes[key];
        set => _nodes[key] = value;
    }

    /// <summary>
    /// Gets a collection containing the keys in the YAML mapping.
    /// </summary>
    /// <remarks>
    /// This property provides access to the set of keys for the key-value pairs stored in the instance of the <c>YamlMapping</c> class.
    /// The keys are returned as a <c>ICollection&lt;string&gt;</c>.
    /// Modifications to this collection directly affect the internal dictionary.
    /// </remarks>
    public ICollection<string> Keys => _nodes.Keys;

    /// <summary>
    /// Gets the collection of values stored in the mapping.
    /// </summary>
    /// <remarks>
    /// This property provides access to all the values (instances of <see cref="IYamlNode"/>)
    /// within the mapping. The values correspond to the elements stored with their associated
    /// keys in the dictionary-like structure of the YAML mapping.
    /// </remarks>
    public ICollection<IYamlNode> Values => _nodes.Values;

    /// Gets the number of key-value pairs contained in the mapping.
    /// This property represents the total count of entries stored
    /// in the underlying dictionary. It provides a way to retrieve
    /// the size of the current mapping.
    public int Count => _nodes.Count;

    /// Gets a value indicating whether the collection is read-only.
    /// This property indicates whether the current instance of the collection
    /// prevents modifications such as adding, removing, or updating its elements.
    /// Returns:
    /// Always returns `false` for this implementation, as the collection
    /// allows modification operations.
    public bool IsReadOnly => false;

    /// <summary>
    /// Adds a key-value pair to the YAML mapping.
    /// </summary>
    /// <param name="key">The key to associate with the specified value.</param>
    /// <param name="value">The value to add to the mapping.</param>
    /// <exception cref="System.ArgumentException">Thrown when an item with the same key already exists in the mapping.</exception>
    public void Add(KeyValuePair<string, IYamlNode> item) => _nodes.Add(item.Key, item.Value);
    /// <summary>
    /// Removes all key-value pairs from the mapping.
    /// </summary>
    /// <remarks>
    /// This method clears the internal dictionary that stores all nodes in the YAML mapping, resulting in an empty mapping.
    /// Any previously stored keys and values are discarded.
    /// </remarks>
    public void Clear() => _nodes.Clear();
    /// <summary>
    /// Determines whether the YAML mapping contains the specified key-value pair.
    /// </summary>
    /// <param name="item">The key-value pair to locate in the YAML mapping.</param>
    /// <returns>true if the specified key-value pair is found in the YAML mapping; otherwise, false.</returns>
    public bool Contains(KeyValuePair<string, IYamlNode> item) => _nodes.Contains(item);
    /// <summary>
    /// Copies the elements of the YamlMapping to a specified array, starting at a particular array index.
    /// </summary>
    /// <param name="array">The one-dimensional array that is the destination of the elements copied from the YamlMapping. The array must have zero-based indexing.</param>
    /// <param name="arrayIndex">The zero-based index in the array at which copying begins.</param>
    /// <exception cref="ArgumentNullException">Thrown when the provided array is null.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when the arrayIndex is less than 0.</exception>
    /// <exception cref="ArgumentException">Thrown when the number of elements in the YamlMapping is greater than the available space from arrayIndex to the end of the destination array.</exception>
    public void CopyTo(KeyValuePair<string, IYamlNode>[] array, int arrayIndex) =>
        ((ICollection<KeyValuePair<string, IYamlNode>>)_nodes).CopyTo(array, arrayIndex);
    /// <summary>
    /// Removes the specified key-value pair from the YAML mapping.
    /// </summary>
    /// <param name="item">
    /// The KeyValuePair to remove, consisting of the key and value.
    /// </param>
    /// <returns>
    /// Returns true if the key-value pair was successfully removed; otherwise, false.
    /// </returns>
    public bool Remove(KeyValuePair<string, IYamlNode> item) =>
        ((ICollection<KeyValuePair<string, IYamlNode>>)_nodes).Remove(item);
    /// <summary>
    /// Returns an enumerator that iterates through the key-value pairs in the YAML mapping.
    /// </summary>
    /// <returns>
    /// An enumerator for the collection of key-value pairs contained within the YAML mapping.
    /// </returns>
    public IEnumerator<KeyValuePair<string, IYamlNode>> GetEnumerator() => _nodes.GetEnumerator();
    /// Retrieves an enumerator that iterates through the collection.
    /// <returns>
    /// An enumerator for the collection.
    /// </returns>
    IEnumerator IEnumerable.GetEnumerator() => _nodes.GetEnumerator();
}