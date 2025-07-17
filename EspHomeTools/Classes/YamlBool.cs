namespace EspHomeTools.Classes;

public class YamlBool : YamlScalarBase<bool>
{
    public YamlBool(bool value)
    {
        Value = value;
    }

    protected override string SerializeValue()
    {
        return Value.ToString().ToLowerInvariant();
    }
}