namespace EspHomeTools.Classes.Scalars;

public class YamlBool : YamlScalar<bool>
{
    public YamlBool(bool value)
    {
        Value = value;
    }

    protected override string SerializeValue() => Value.ToString().ToLowerInvariant();
}