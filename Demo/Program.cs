using EspHomeTools.devices;
using EspHomeTools.sections;
using EspHomeTools.sensors;
using EspHomeTools.serializers;
using EspHomeTools.values;

Console.WriteLine("Hello, World!");

var serializer = new EspHomeSerializer();

var myConfig = new EspHomeDevice 
{
    SectionEspHome = new EspHomeSection(),
    Substitutions = new SubstitutionsSection
    {
        ["name"] = "og-sz-bett",
        ["friendly_name"] = "OG-SZ-BETT",
        ["wifi_ssid"] = new EspHomeValue("wifi_ssid", true),
        ["wifi_password"] = new EspHomeValue("wifi_password", true),
        ["ap_ssid"] = new EspHomeValue("ap_ssid", true),
        ["ap_password"] = new EspHomeValue("ap_password", true),
        ["ota_password"] = new EspHomeValue("ota_password", true),
        ["api_key"] = new EspHomeValue("api_key", true),
    },

    Sensors = [
        new DhtSensor 
        { 
            Name = "Temperatur", 
            Pin = 14, 
            Model = "DHT22",
            UpdateInterval = "60s" 
        },
        new UptimeSensor 
        { 
            Name = "Laufzeit" 
        }
    ]
};

var yamlOutput = serializer.Serialize(myConfig);
Console.WriteLine(yamlOutput);