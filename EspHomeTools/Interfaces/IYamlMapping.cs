using System.Collections.Generic;

namespace EspHomeTools.Interfaces;

public interface IYamlMapping : IYamlStructure, IDictionary<string, IYamlNode> { }