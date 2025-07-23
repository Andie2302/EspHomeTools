using System.Collections.Generic;
using EspHomeTools.Interfaces;

/// <summary>
/// Definiert eine Liste von YAML-Skalarwerten.
/// </summary>
public interface IYamlList : IYamlObject, IEnumerable<IYamlScalar>
{
    void Add(IYamlScalar item);
    int Count { get; }
}