using System.Globalization;
using EspHomeTools.Interfaces.Base;
using EspHomeTools.Interfaces.RenderManagers;

namespace EspHomeTools.Classes.Scalars;

public class YamlScalar<T> : IYamlScalar<T>
{
    public YamlScalar()
    {
    }
    public YamlScalar(T? value)
    {
        Value = value;
    }
    public void Render(IYamlRenderManager yamlRenderManager, int indentationLevel)
    {
        if (!HasValue)
        {
            yamlRenderManager.Append("null", 0);
            return;
        }

        var formattedValue = Value switch
        {
            string s => $"\"{s}\"",
            bool b => b ? "true" : "false",
            float f => f.ToString(CultureInfo.InvariantCulture),
            double d => d.ToString(CultureInfo.InvariantCulture),
            decimal m => m.ToString(CultureInfo.InvariantCulture),
            _ => Value?.ToString() ?? "null"
        };

        yamlRenderManager.Append(formattedValue, 0);
    }
    public T? Value { get; set; }
    public bool HasValue => Value != null;
}