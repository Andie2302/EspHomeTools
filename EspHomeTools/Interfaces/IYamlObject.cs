using EspHomeTools.Interfaces.Render;

namespace EspHomeTools.Interfaces;

public interface IYamlObject : IYamlRenderable { }

public interface IYamlValue<T> : IYamlValue
{
    T? Value { get; set; }
    bool HasValue { get; }
}

public interface IYamlScalar<T> : IYamlValue<T>, IYamlScalar { }