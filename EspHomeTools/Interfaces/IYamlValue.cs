namespace EspHomeTools.Interfaces;

public interface IYamlValue: IYamlNode;

public interface IYamlValue<T> : IYamlValue
{
    public T? Value { get; set; }
}