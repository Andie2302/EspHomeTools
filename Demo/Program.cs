using EspHomeTools.devices;
using EspHomeTools.serializers;
using EspHomeTools.values;

Console.WriteLine("Hello, World!");

var serializer = new EspHomeSerializer();

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
        new Dictionary<string, object> { ["platform"] = "dht", ["pin"] = 14, ["name"] = "Temp" }
    ]
};

string yamlOutput = serializer.Serialize(myConfig);
Console.WriteLine(yamlOutput);