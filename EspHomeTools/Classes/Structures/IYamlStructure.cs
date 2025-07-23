using System.Collections.Generic;
using EspHomeTools.Interfaces;

namespace EspHomeTools.Classes.Structures;

public interface IYamlStructure : IYamlNode
{
    IEnumerable<IYamlNode> Children { get; }
    void Add(IYamlNode node);
    void Clear();
}