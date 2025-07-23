namespace EspHomeTools.Interfaces;

public interface IYamlValue;

public interface IYamlValue<T> : IYamlValue
{
    public T? Value { get; set; }
}