namespace EspHomeTools.Interfaces;

public interface IYamlScalar<TValue> : IYamlNode
{
    TValue? Value { get; set; }
}