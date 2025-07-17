using System.Globalization;

namespace EspHomeTools.scratch;

public class YamlFloat : YamlScalarBase<double>
{
    public YamlFloat(double value)
    {
        Value = value;
    }




    protected override string SerializeValue()
    {
        if (Value == null) return "null";

        return Value.ToString(CultureInfo.InvariantCulture);
    }
}