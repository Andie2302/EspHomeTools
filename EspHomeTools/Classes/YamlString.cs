using EspHomeTools.Interfaces;

namespace EspHomeTools.Classes;

public class YamlString : YamlScalarBase<string?>, IYamlString
{
    public bool IsValueNull => Value == null;
    public bool HasValue => string.IsNullOrWhiteSpace(Value) == false;
}