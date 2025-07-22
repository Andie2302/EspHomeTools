using System.Collections.Generic;
using EspHomeTools.Interfaces;

namespace EspHomeTools.Classes;

public interface IYamlList : IYamlObject, IEnumerable<IYamlScalar>
{
    int Count { get; }
    bool HasItems { get; }
    void Add(IYamlScalar item);
}