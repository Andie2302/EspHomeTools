namespace EspHomeTools.Classes;

public class YamlInt : YamlScalar<int>
{
    public YamlInt(int value)
    {
        Value = value;
    }

    protected override string SerializeValue() => Value.ToString();
}