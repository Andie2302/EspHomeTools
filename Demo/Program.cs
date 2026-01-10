using EspHomeTools;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

Console.WriteLine("Hello, World!");

var serializer = new SerializerBuilder()
    .WithNamingConvention(UnderscoredNamingConvention.Instance)
    .WithTypeConverter(new EsphomeValueConverter())
    .ConfigureDefaultValuesHandling(DefaultValuesHandling.OmitDefaults|DefaultValuesHandling.OmitEmptyCollections|DefaultValuesHandling.OmitNull)
    .Build();

var myConfig = new EsphomeDevice 
{
    SectionEsphome = new() { Name = "test_node" },
    Substitutions = new()
    {
        ["name"] = "og-sz-bett",
        ["friendly_name"] = "OG-SZ-BETT",
        ["wifi_ssid"] = new EsphomeValue("wifi_ssid", true),
        ["wifi_password"] = new EsphomeValue("wifi_password", true),
        ["ap_ssid"] = new EsphomeValue("ap_ssid", true),
        ["ap_password"] = new EsphomeValue("ap_password", true),
        
    },
    Sensors = [
        new() { ["platform"] = "dht", ["pin"] = 14, ["name"] = "Temp" }
    ]
};

string yamlOutput = serializer.Serialize(myConfig);
Console.WriteLine(yamlOutput);