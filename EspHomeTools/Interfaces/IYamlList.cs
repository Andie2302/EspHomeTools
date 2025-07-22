using System.Collections.Generic;

namespace EspHomeTools.Interfaces;

public interface IYamlList : IYamlObject, IEnumerable<IYamlScalar>
{
    int Count { get; }
    bool HasItems { get; }
    void Add(IYamlScalar item);
}