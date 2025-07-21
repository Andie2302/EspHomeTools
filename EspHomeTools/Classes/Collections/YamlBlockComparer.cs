using System;
using System.Collections.Generic;
using EspHomeTools.Interfaces;

namespace EspHomeTools.Classes.Collections;


public class YamlBlockComparer : IComparer<KeyValuePair<string, IYamlNode>>
{
    private const int NotFoundPriority = int.MaxValue;
    private readonly List<string> _customOrder;

    public YamlBlockComparer(List<string> customOrder) => _customOrder = customOrder;

    public int Compare(KeyValuePair<string, IYamlNode> x, KeyValuePair<string, IYamlNode> y)
    {
        var priorityX = GetPriorityIndex(x.Key);
        var priorityY = GetPriorityIndex(y.Key);
        return priorityX == priorityY ? string.Compare(x.Key, y.Key, StringComparison.Ordinal) : priorityX.CompareTo(priorityY);
    }

    private int GetPriorityIndex(string key)
    {
        var index = _customOrder.FindIndex(orderKey => orderKey.Equals(key, StringComparison.OrdinalIgnoreCase));
        return index == -1 ? NotFoundPriority : index;
    }
}