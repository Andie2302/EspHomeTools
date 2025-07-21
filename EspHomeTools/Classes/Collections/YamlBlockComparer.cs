using System;
using System.Collections.Generic;
using EspHomeTools.Interfaces;

namespace EspHomeTools.Classes.Collections;

/// <summary>
/// Provides a mechanism to compare key-value pairs of YAML nodes based on a custom order
/// and fallbacks to alphabetical comparison if the custom order does not apply.
/// </summary>
/// <remarks>
/// This comparer is particularly useful for sorting YAML blocks where a specific order of keys is required,
/// ensuring a consistent structure for YAML serialization or output.
/// If a key is not found in the custom order list, it is assigned the lowest priority and is sorted
/// alphabetically among other keys with the same priority.
/// </remarks>
public class YamlBlockComparer : IComparer<KeyValuePair<string, IYamlNode>>
{
    /// <summary>
    /// Represents the priority value assigned when a key is not found in the custom order list.
    /// </summary>
    /// <remarks>
    /// This constant is used to ensure keys not present in the custom order are given the lowest
    /// comparison priority when sorting. The value is set to <see cref="int.MaxValue"/> to ensure
    /// such keys are processed after all explicitly ordered keys.
    /// </remarks>
    private const int NotFoundPriority = int.MaxValue;

    /// <summary>
    /// Specifies a list of custom priorities that determines the ordering of YAML nodes during sorting operations.
    /// </summary>
    /// <remarks>
    /// The custom order defines the precedence of keys by their position in the list. Keys appearing earlier in the list
    /// have higher priority, while keys not included in the list will be assigned a default maximum priority value.
    /// This helps to enforce specific ordering rules when comparing YAML blocks.
    /// </remarks>
    private readonly List<string> _customOrder;

    /// Provides comparison logic for sorting YAML nodes based on a custom-defined order.
    /// Implements the IComparer interface for KeyValuePair objects containing a string key and an IYamlNode value.
    public YamlBlockComparer(List<string> customOrder) => _customOrder = customOrder;

    /// Compares two KeyValuePair<string, IYamlNode> objects based on a custom order
    /// and their keys, providing a prioritized sorting mechanism.
    /// <param name="x">
    /// The first KeyValuePair<string, IYamlNode> to compare.
    /// </param>
    /// <param name="y">
    /// The second KeyValuePair<string, IYamlNode> to compare.
    /// </param>
    /// <returns>
    /// An integer that indicates the relative ordering of the two objects:
    /// - A negative value if x precedes y in the sort order.
    /// - Zero if x and y have the same order priority.
    /// - A positive value if x follows y in the sort order.
    /// </returns>
    public int Compare(KeyValuePair<string, IYamlNode> x, KeyValuePair<string, IYamlNode> y)
    {
        var priorityX = GetPriorityIndex(x.Key);
        var priorityY = GetPriorityIndex(y.Key);
        return priorityX == priorityY ? string.Compare(x.Key, y.Key, StringComparison.Ordinal) : priorityX.CompareTo(priorityY);
    }

    /// <summary>
    /// Retrieves the priority index of a specified key from the custom order list.
    /// </summary>
    /// <param name="key">
    /// The key whose priority index is to be determined.
    /// </param>
    /// <returns>
    /// An integer representing the index of the key in the custom order list, or a default fallback value if the key is not found.
    /// </returns>
    private int GetPriorityIndex(string key)
    {
        var index = _customOrder.FindIndex(orderKey => orderKey.Equals(key, StringComparison.OrdinalIgnoreCase));
        return index == -1 ? NotFoundPriority : index;
    }
}