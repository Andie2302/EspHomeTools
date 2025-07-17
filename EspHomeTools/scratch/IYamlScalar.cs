namespace EspHomeTools.scratch;

public interface IYamlScalar<TValue> : IYamlNode
{
    TValue? Value { get; set; }
}