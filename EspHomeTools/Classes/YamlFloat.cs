using System.Globalization;
using EspHomeTools.Interfaces;

namespace EspHomeTools.Classes;

public class YamlFloat : IYamlFloat
{
    public void Render(IYamlRenderManager yamlRenderManager, int indentationLevel)
    {
        yamlRenderManager.Append(Value.ToString(CultureInfo.InvariantCulture), indentationLevel);
    }
    public float Value { get; set; }
}