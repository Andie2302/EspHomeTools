using EspHomeTools.values;
using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;

namespace EspHomeTools.serializers.converters;

public class EspHomeValueConverter : IYamlTypeConverter
{
    public bool Accepts(Type type) => type == typeof(EspHomeValue);

    public object? ReadYaml(IParser parser, Type type, ObjectDeserializer rootDeserializer)
    {
        var scalar = parser.Consume<Scalar>();
        return new EspHomeValue(scalar.Value, scalar.Tag == "!secret");
    }

    public void WriteYaml(IEmitter emitter, object? value, Type type, ObjectSerializer serializer)
    {
        var esphomeValue = (EspHomeValue)value!;
        var stringValue = esphomeValue.Value?.ToString() ?? string.Empty;

        var isSecret = esphomeValue.IsSecret;
    
        emitter.Emit(new Scalar(
            null, isSecret ? "!secret" : null, stringValue, isSecret ? ScalarStyle.Plain : ScalarStyle.Any, !isSecret, false));
    }
    
}