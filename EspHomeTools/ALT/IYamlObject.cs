namespace EspHomeTools.Interfaces;

public interface IYamlObject : IYamlRender { }

public interface IYamlValue<T> : IYamlValue
{
    T? Value { get; set; }
    bool HasValue { get; }
}

public interface IYamlScalar<T> : IYamlScalar, IYamlValue<T> { }