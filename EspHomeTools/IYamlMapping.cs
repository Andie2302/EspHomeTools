using System.Collections.Generic;

namespace EspHomeTools.scratch;

public interface IYamlMapping : IYamlStructure, IDictionary<string, IYamlNode> { }