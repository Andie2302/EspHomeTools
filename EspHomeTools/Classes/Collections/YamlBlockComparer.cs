using System;
using System.Collections.Generic;
using EspHomeTools.Interfaces;

namespace EspHomeTools.Classes.Collections;

public class YamlBlockComparer : IComparer<KeyValuePair<string, IYamlNode>>
{
    private readonly List<string> _customOrder;
    public YamlBlockComparer(List<string> customOrder)
    {
        _customOrder = customOrder;
    }
    public int Compare(KeyValuePair<string, IYamlNode> x, KeyValuePair<string, IYamlNode> y)
    {
        var indexX = _customOrder.FindIndex(key => key.Equals(x.Key, StringComparison.OrdinalIgnoreCase));
        var indexY = _customOrder.FindIndex(key => key.Equals(y.Key, StringComparison.OrdinalIgnoreCase));
        if (indexX == -1) indexX = int.MaxValue;
        if (indexY == -1) indexY = int.MaxValue;
        return indexX == indexY ? string.Compare(x.Key, y.Key, StringComparison.Ordinal) : indexX.CompareTo(indexY);
    }

}