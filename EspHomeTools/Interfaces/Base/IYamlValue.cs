namespace EspHomeTools.Interfaces.Base;

public interface IYamlValue { }

public interface IYamlValue<T> : IYamlValue
{
    T? Value { get; set; }
    bool HasValue { get; }
}
