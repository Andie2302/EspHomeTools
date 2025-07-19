using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using EspHomeTools.Interfaces;

namespace EspHomeTools.Classes.Structures;

/// <summary>
/// Represents a YAML sequence structure. This class implements the <see cref="IYamlSequence"/> interface and is used
/// to construct, manage, and serialize a YAML sequence element in a structured format.
/// </summary>
public class YamlSequence : IYamlSequence
{
    /// <summary>
    /// Represents the internal collection of YAML nodes within a <see cref="YamlSequence"/>.
    /// The <c>_nodes</c> variable is a private field that stores a list of <see cref="IYamlNode"/> objects,
    /// allowing the <see cref="YamlSequence"/> to manage and manipulate its contained YAML nodes.
    /// </summary>
    private readonly List<IYamlNode> _nodes = new();

    /// Gets or sets the name associated with the YAML sequence.
    /// This represents the key or identifier for this YAML node when
    /// serialized in YAML format. If `Name` is not null or empty,
    /// it will be included in the serialized output as a key.
    public string? Name { get; set; }

    /// <summary>
    /// Gets or sets the comment associated with the YAML node or sequence.
    /// This property is used to represent optional comments that can be embedded
    /// in the YAML output. Comments typically provide additional descriptive
    /// information or context about the node or sequence.
    /// </summary>
    public string? Comment { get; set; }

    /// <summary>
    /// Gets or sets the tag associated with the YAML node.
    /// The tag provides additional type information about the node in YAML.
    /// It can be used to specify or override the expected data type of the node during serialization or deserialization.
    /// </summary>
    public string? Tag { get; set; }

    /// Converts the current YAML sequence into a string representation in YAML format with the specified level of indentation.
    /// <param name="indent">
    /// The number of spaces to use for indenting the YAML output. Defaults to 0 if not specified.
    /// </param>
    /// <returns>
    /// A string representing the YAML structure of the current sequence, including indentation, comments, and any child nodes.
    /// </returns>
    public string ToYaml(int indent = 0)
    {
        var sb = new StringBuilder();
        var basePrefix = new string(' ', indent);
        if (!string.IsNullOrWhiteSpace(Comment))
        {
            sb.Append(basePrefix).Append("# ").AppendLine(Comment);
        }

        if (!string.IsNullOrWhiteSpace(Name))
        {
            sb.Append(basePrefix).Append(Name).AppendLine(":");
            indent += 2;
        }

        var itemIndentStr = new string(' ', indent);
        var childIndent = indent + 2;
        foreach (var node in _nodes)
        {
            string childYaml = node.ToYaml(childIndent);
            string trimmedChildYaml = childYaml.TrimStart();
            sb.Append(itemIndentStr).Append("- ").AppendLine(trimmedChildYaml);
        }

        return sb.ToString().TrimEnd();
    }


    #region IList Implementierung

    /// <summary>
    /// Adds an <see cref="IYamlNode"/> item to the sequence.
    /// </summary>
    /// <param name="item">The <see cref="IYamlNode"/> item to be added to the sequence.</param>
    public void Add(IYamlNode item) => _nodes.Add(item);
    /// <summary>
    /// Removes all elements from the sequence.
    /// </summary>
    public void Clear() => _nodes.Clear();
    /// <summary>
    /// Determines whether the sequence contains a specific IYamlNode item.
    /// </summary>
    /// <param name="item">The IYamlNode item to locate in the sequence.</param>
    /// <returns>True if the item is found in the sequence; otherwise, false.</returns>
    public bool Contains(IYamlNode item) => _nodes.Contains(item);
    /// <summary>
    /// Copies the elements of the <see cref="IYamlNode"/> collection to a specified array, starting at the given array index.
    /// </summary>
    /// <param name="array">The destination array to which the elements of the collection should be copied.</param>
    /// <param name="arrayIndex">The zero-based index in the destination array at which copying begins.</param>
    public void CopyTo(IYamlNode[] array, int arrayIndex) => _nodes.CopyTo(array, arrayIndex);
    /// Removes the first occurrence of a specific IYamlNode item from the sequence.
    /// <param name="item">The IYamlNode item to remove from the sequence.</param>
    /// <returns>True if the item was successfully removed; otherwise, false. This method also returns false if the item is not found in the sequence.</returns>
    public bool Remove(IYamlNode item) => _nodes.Remove(item);

    /// <summary>
    /// Gets the number of elements contained in the sequence.
    /// </summary>
    /// <value>
    /// The total count of <see cref="IYamlNode"/> objects within the sequence.
    /// </value>
    public int Count => _nodes.Count;

    /// <summary>
    /// Gets a value indicating whether the <see cref="YamlSequence"/> is read-only.
    /// </summary>
    /// <remarks>
    /// This property always returns <c>false</c>, as the <see cref="YamlSequence"/> supports modification
    /// of its contents, such as adding, removing, or changing items.
    /// </remarks>
    public bool IsReadOnly => false;

    /// Returns the zero-based index of the first occurrence of a specified IYamlNode within the sequence.
    /// <param name="item">The IYamlNode to locate in the sequence.</param>
    /// <returns>
    /// The zero-based index of the first occurrence of the specified IYamlNode if found; otherwise, -1.
    /// </returns>
    public int IndexOf(IYamlNode item) => _nodes.IndexOf(item);
    /// Inserts an IYamlNode item into the sequence at the specified index.
    /// <param name="index">The zero-based index at which item should be inserted.</param>
    /// <param name="item">The IYamlNode element to insert into the sequence.</param>
    public void Insert(int index, IYamlNode item) => _nodes.Insert(index, item);
    /// Removes the element at the specified index in the sequence.
    /// <param name="index">The zero-based index of the element to remove.</param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown if the index is less than 0 or greater than or equal to the count of elements in the sequence.
    /// </exception>
    public void RemoveAt(int index) => _nodes.RemoveAt(index);

    /// <summary>
    /// Represents a sequence node in a YAML document that can contain other YAML nodes as items.
    /// </summary>
    public IYamlNode this[int index]
    {
        get => _nodes[index];
        set => _nodes[index] = value;
    }

    /// Returns an enumerator that iterates through the collection.
    /// <returns>
    /// An enumerator for the collection.
    /// </returns>
    public IEnumerator<IYamlNode> GetEnumerator() => _nodes.GetEnumerator();
    /// Returns an enumerator that iterates through the collection.
    /// <return>An enumerator for the collection.</return>
    IEnumerator IEnumerable.GetEnumerator() => _nodes.GetEnumerator();

    #endregion
}