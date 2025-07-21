using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using EspHomeTools.Interfaces;

namespace EspHomeTools.Classes.Structures;

/// <summary>
/// Represents a YAML sequence structure. This class is designed to handle multiple YAML elements in sequence,
/// allowing addition, removal, and ordered access. It adheres to the <see cref="IYamlSequence"/> interface,
/// ensuring compatibility with other YAML structures.
/// </summary>
public class YamlSequence : IYamlSequence
{
    /// <summary>
    /// Specifies the indentation size in spaces for YAML formatting within the <see cref="YamlSequence"/> class.
    /// This constant defines the width of each indentation level, ensuring consistent spacing and structure
    /// when constructing or serializing YAML sequences.
    /// </summary>
    private const int IndentSize = 2;

    /// <summary>
    /// Represents the prefix used for each sequence item in a YAML sequence.
    /// This constant value, <c>"- "</c>, is utilized to denote the start of each item in the sequence
    /// when serialized into YAML format.
    /// </summary>
    private const string SequenceItemPrefix = "- ";

    /// <summary>
    /// A private field that holds the collection of YAML nodes within the <see cref="YamlSequence"/> structure.
    /// This list of <see cref="IYamlNode"/> objects is used internally to store and manage the sequence's
    /// elements, providing functionalities such as adding, removing, and iterating over the nodes.
    /// </summary>
    private readonly List<IYamlNode> _nodes = new();

    /// <summary>
    /// Represents the name property of a YAML sequence.
    /// This property is used to store the optional name associated with the sequence, allowing for identification or organization of YAML data.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Represents an optional comment associated with the YAML sequence.
    /// This property allows users to attach a descriptive or explanatory comment
    /// that can be rendered as a comment line in the generated YAML output.
    /// </summary>
    public string? Comment { get; set; }

    /// <summary>
    /// Specifies the YAML tag associated with the <see cref="YamlSequence"/>.
    /// The <c>Tag</c> property enables the assignment of a custom YAML tag, allowing for
    /// greater flexibility in defining and interpreting YAML structures.
    /// </summary>
    public string? Tag { get; set; }

    /// <summary>
    /// Converts the current YAML sequence to a YAML-formatted string with the specified indentation level.
    /// </summary>
    /// <param name="indent">
    /// The number of spaces to use for indentation in the generated YAML output. Defaults to 0 if not specified.
    /// </param>
    /// <returns>
    /// A string representation of the YAML sequence, including indentation, comments, and nested elements.
    /// </returns>
    public string ToYaml(int indent = 0)
    {
        var yamlBuilder = new StringBuilder();
        var sequenceIndent = indent;
        AppendCommentIfPresent(yamlBuilder, indent);
        sequenceIndent = AppendNameIfPresent(yamlBuilder, sequenceIndent);
        AppendSequenceItems(yamlBuilder, sequenceIndent);
        return yamlBuilder.ToString().TrimEnd();
    }
    /// <summary>
    /// Appends a comment to the provided StringBuilder if a comment is present in the current YAML sequence.
    /// </summary>
    /// <param name="sb">
    /// The StringBuilder instance to which the comment should be appended.
    /// </param>
    /// <param name="indent">
    /// The number of spaces to prefix the comment with, defining the indentation level in the YAML structure.
    /// </param>
    private void AppendCommentIfPresent(StringBuilder sb, int indent)
    {
        if (string.IsNullOrWhiteSpace(Comment)) return;
        var indentPrefix = new string(' ', indent);
        sb.Append(indentPrefix).Append("# ").AppendLine(Comment);
    }
    /// <summary>
    /// Appends the name of the YAML sequence to the provided StringBuilder if a name is set, followed by a colon and a newline.
    /// Adjusts the indentation level for subsequent lines if the name is present.
    /// </summary>
    /// <param name="sb">
    /// The StringBuilder to append the name to.
    /// </param>
    /// <param name="indent">
    /// The current level of indentation before appending the name.
    /// </param>
    /// <returns>
    /// The updated indentation level. If the name is not present, the original indentation value is returned.
    /// </returns>
    private int AppendNameIfPresent(StringBuilder sb, int indent)
    {
        if (string.IsNullOrWhiteSpace(Name)) return indent;
        var indentPrefix = new string(' ', indent);
        sb.Append(indentPrefix).Append(Name).AppendLine(":");
        return indent + IndentSize;
    }
    /// <summary>
    /// Appends all items in the YAML sequence to the specified <see cref="StringBuilder"/> in a formatted YAML string representation.
    /// </summary>
    /// <param name="sb">
    /// The <see cref="StringBuilder"/> instance to which the YAML sequence items will be appended.
    /// </param>
    /// <param name="indent">
    /// The number of spaces to use for indentation of the sequence items within the YAML output.
    /// </param>
    private void AppendSequenceItems(StringBuilder sb, int indent)
    {
        var itemIndentPrefix = new string(' ', indent);
        var childNodeIndent = indent + IndentSize;
        foreach (var node in _nodes)
        {
            var childYamlContent = node.ToYaml(childNodeIndent);
            var trimmedChildYaml = childYamlContent.TrimStart();
            sb.Append(itemIndentPrefix).Append(SequenceItemPrefix).AppendLine(trimmedChildYaml);
        }
    }


    #region IList Implementation

    /// <summary>
    /// Adds an <see cref="IYamlNode"/> item to the sequence.
    /// </summary>
    /// <param name="item">
    /// The <see cref="IYamlNode"/> item to be added to the sequence.
    /// </param>
    public void Add(IYamlNode item) => _nodes.Add(item);
    /// <summary>
    /// Removes all nodes from the YAML sequence, clearing its contents entirely.
    /// </summary>
    public void Clear() => _nodes.Clear();
    /// <summary>
    /// Determines whether the sequence contains a specific IYamlNode item.
    /// </summary>
    /// <param name="item">
    /// The IYamlNode item to locate in the sequence.
    /// </param>
    /// <returns>
    /// True if the item is found in the sequence; otherwise, false.
    /// </returns>
    public bool Contains(IYamlNode item) => _nodes.Contains(item);
    /// <summary>
    /// Copies the elements of the <see cref="IYamlNode"/> collection to a specified array, starting at the given array index.
    /// </summary>
    /// <param name="array">
    /// The destination array to which the elements of the collection should be copied. This array must have zero-based indexing.
    /// </param>
    /// <param name="arrayIndex">
    /// The zero-based index in the destination array at which copying begins.
    /// </param>
    public void CopyTo(IYamlNode[] array, int arrayIndex) => _nodes.CopyTo(array, arrayIndex);
    /// <summary>
    /// Removes the first occurrence of the specified node from the YAML sequence.
    /// </summary>
    /// <param name="item">
    /// The <see cref="IYamlNode"/> instance to be removed from the sequence.
    /// </param>
    /// <returns>
    /// True if the specified node was successfully removed from the sequence; otherwise, false.
    /// Returns false if the specified node is not found in the sequence.
    /// </returns>
    public bool Remove(IYamlNode item) => _nodes.Remove(item);

    /// <summary>
    /// Gets the total number of YAML nodes contained within the <see cref="YamlSequence"/>.
    /// This property provides the count of elements currently managed by the internal collection of the sequence.
    /// </summary>
    public int Count => _nodes.Count;

    /// <summary>
    /// Gets a value indicating whether the <see cref="YamlSequence"/> is read-only.
    /// The <c>IsReadOnly</c> property always returns <c>false</c>, as the <see cref="YamlSequence"/>
    /// allows modification of its elements, including adding, removing, and updating items.
    /// </summary>
    public bool IsReadOnly => false;

    /// <summary>
    /// Returns the zero-based index of the first occurrence of a specified <see cref="IYamlNode"/> in the sequence.
    /// </summary>
    /// <param name="item">
    /// The <see cref="IYamlNode"/> to locate in the sequence.
    /// </param>
    /// <returns>
    /// The zero-based index of the first occurrence of the specified <see cref="IYamlNode"/> if found; otherwise, -1.
    /// </returns>
    public int IndexOf(IYamlNode item) => _nodes.IndexOf(item);
    /// <summary>
    /// Inserts an <see cref="IYamlNode"/> element into the sequence at the specified index.
    /// </summary>
    /// <param name="index">
    /// The zero-based index at which the specified item should be inserted.
    /// </param>
    /// <param name="item">
    /// The <see cref="IYamlNode"/> instance to insert into the sequence.
    /// </param>
    public void Insert(int index, IYamlNode item) => _nodes.Insert(index, item);
    /// <summary>
    /// Removes the element at the specified index in the sequence.
    /// </summary>
    /// <param name="index">
    /// The zero-based index of the element to remove.
    /// </param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown if the index is less than 0 or greater than or equal to the count of elements in the sequence.
    /// </exception>
    public void RemoveAt(int index) => _nodes.RemoveAt(index);

    /// <summary>
    /// Refers to the current instance of the class or struct within which it is used.
    /// It is commonly used to access instance members, resolve naming conflicts, or explicitly pass the current instance.
    /// </summary>
    public IYamlNode this[int index]
    {
        get => _nodes[index];
        set => _nodes[index] = value;
    }

    /// <summary>
    /// Returns an enumerator that iterates through the elements of the YAML sequence.
    /// </summary>
    /// <returns>
    /// An enumerator that can be used to iterate through the elements in the sequence.
    /// </returns>
    public IEnumerator<IYamlNode> GetEnumerator() => _nodes.GetEnumerator();
    /// <summary>
    /// Returns an enumerator that iterates through the collection of YAML nodes in the current sequence.
    /// </summary>
    /// <returns>
    /// An enumerator for the collection of <see cref="IYamlNode"/> objects contained within the sequence.
    /// </returns>
    IEnumerator IEnumerable.GetEnumerator() => _nodes.GetEnumerator();

    #endregion
}