using EspHomeTools.Interfaces;

namespace EspHomeTools.Classes;

public class YamlNull : YamlScalarBase<string?>
{
    public override string? Value
    {
        get => null;
        set { }
    }

    public bool IsValueNull => true;
}