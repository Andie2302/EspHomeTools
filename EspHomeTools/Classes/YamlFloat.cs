using System.Globalization;

namespace EspHomeTools.Classes;

public class YamlFloat : YamlScalar<double>
{
    public YamlFloat(double value)
    {
        Value = value;
    }

    protected override string SerializeValue() => Value.ToString(CultureInfo.InvariantCulture);
}