using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;

namespace EspHomeTools;

public class EsphomeValueConverter : IYamlTypeConverter
{
    public bool Accepts(Type type) => type == typeof(EsphomeValue);

    public object? ReadYaml(IParser parser, Type type, ObjectDeserializer rootDeserializer)
    {
        var scalar = parser.Consume<Scalar>();
        return new EsphomeValue(scalar.Value, scalar.Tag == "!secret");
    }

    public void WriteYaml(IEmitter emitter, object? value, Type type, ObjectSerializer serializer)
    {
        var esphomeValue = (EsphomeValue)value!;
        var stringValue = esphomeValue.Value?.ToString() ?? string.Empty;

        var isSecret = esphomeValue.IsSecret;
    
        emitter.Emit(new Scalar(
            null, isSecret ? "!secret" : null, stringValue, isSecret ? ScalarStyle.Plain : ScalarStyle.Any, !isSecret, false));
    }
    
}