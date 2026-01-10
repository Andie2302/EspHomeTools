using EspHomeTools.values;

namespace EspHomeTools.sections;

public class SubstitutionsSection
{
    public Dictionary<string, EspHomeValue> Substitutions { get; init; } = new();
}