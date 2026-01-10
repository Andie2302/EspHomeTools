using EspHomeTools.values;

namespace EspHomeTools.sections;

public class SubstitutionsSection : Dictionary<string, EspHomeValue>
{
    public void AddSecret(string key, object value) => this[key] = new EspHomeValue(value, true);
}