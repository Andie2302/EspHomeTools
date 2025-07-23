using EspHomeTools.Interfaces.RenderManagers;
using EspHomeTools.Interfaces.Scalars;

namespace EspHomeTools.Interfaces.Base;

public interface IYamlObject : IYamlRenderable { }

public interface IYamlValue<T> : IYamlValue
{
    T? Value { get; set; }
    bool HasValue { get; }
}

public interface IYamlScalar<T> : IYamlValue<T>, IYamlScalar { }