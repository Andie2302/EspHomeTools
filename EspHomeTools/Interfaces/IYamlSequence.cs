using System.Collections.Generic;

namespace EspHomeTools.Interfaces;

public interface IYamlSequence : IYamlStructure, IList<IYamlNode> { }