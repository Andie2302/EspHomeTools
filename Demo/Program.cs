using EspHomeTools.devices;
using EspHomeTools.sections;
using EspHomeTools.serializers;
using EspHomeTools.values;

Console.WriteLine("Hello, World!");

var serializer = new EspHomeSerializer();

var myConfig = new EspHomeDevice 
{
    SectionEspHome = new EspHomeSection { Name = "test_node" },
    Substitutions = new SubstitutionsSection
    {
        ["name"] = "og-sz-bett",
        ["friendly_name"] = "OG-SZ-BETT",
        ["wifi_ssid"] = new EspHomeValue("wifi_ssid", true),
        ["wifi_password"] = new EspHomeValue("wifi_password", true),
        ["ap_ssid"] = new EspHomeValue("ap_ssid", true),
        ["ap_password"] = new EspHomeValue("ap_password", true),
    },
    Sensors = [
        new Dictionary<string, object> { ["platform"] = "dht", ["pin"] = 14, ["name"] = "Temp" }
    ]
};

var yamlOutput = serializer.Serialize(myConfig);
Console.WriteLine(yamlOutput);