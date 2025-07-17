namespace EspHomeTools.Classes;

public class YamlInt : YamlScalarBase<int>
{
    public YamlInt(int value)
    {
        Value = value;
    }

    protected override string SerializeValue()
    {
        return Value.ToString();
    }
}