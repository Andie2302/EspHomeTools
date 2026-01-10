using EspHomeTools.devices;
using EspHomeTools.serializers.converters;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace EspHomeTools.serializers;

public class EspHomeSerializer
{
    private readonly ISerializer _serializer = new SerializerBuilder()
        .WithNamingConvention(UnderscoredNamingConvention.Instance)
        .WithTypeConverter(new EspHomeValueConverter())
        .ConfigureDefaultValuesHandling(
            DefaultValuesHandling.OmitDefaults | 
            DefaultValuesHandling.OmitEmptyCollections | 
            DefaultValuesHandling.OmitNull)
        .Build();

    /// <summary>
    /// Serialisiert ein EsphomeDevice Objekt in einen YAML-String.
    /// </summary>
    public string Serialize(EspHomeDevice device)
    {
        return _serializer.Serialize(device);
    }
}