using System.Globalization;

namespace EspHomeTools.Classes.Scalars;

public class YamlFloat : YamlScalar<double>
{
    public YamlFloat(double value)
    {
        Value = value;
    }

    protected override string SerializeValue() => Value.ToString(CultureInfo.InvariantCulture);
}