using System.Collections.Generic;
using EspHomeTools.Interfaces.Base;

namespace EspHomeTools.Interfaces.Mappings;

public interface IYamlKeyValuePair<T> : IYamlKey, IYamlValue<T> { }