using System.Text.RegularExpressions;
using EspHomeTools.devices;
using EspHomeTools.serializers.converters;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace EspHomeTools.serializers;

public partial class EspHomeSerializer
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
    public string Serialize(EspHomeDevice device,bool prettyPrint = true)
    {
        var yaml = _serializer.Serialize(device);
        return prettyPrint ? AddSectionSeparatorLines().Replace(yaml, Environment.NewLine + "$1").Trim() : yaml;
    }

    [GeneratedRegex(@"(?m)^([^\s\-]+:)")]
    private static partial Regex AddSectionSeparatorLines();
}