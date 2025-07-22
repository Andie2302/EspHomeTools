using System;
using System.Text;

namespace EspHomeTools.Enums;

public static class YamlKeysExtensions
{
    private const string SubstitutionsKey = "substitutions";
    private const string PackagesKey = "packages";
    private const string EsphomeKey = "esphome";
    private const string Esp8266Key = "esp8266";
    private const string Esp32Key = "esp32";
    private const string Rp2040Key = "rp2040";
    private const string Bk72XxKey = "bk72xx";
    private const string Rtl87XxKey = "rtl87xx";
    private const string WifiKey = "wifi";
    private const string ApiKey = "api";
    private const string OtaKey = "ota";
    private const string TimeKey = "time";
    private const string WebServerKey = "web_server";
    private const string MqttKey = "mqtt";
    private const string CaptivePortalKey = "captive_portal";
    private const string LoggerKey = "logger";
    private const string I2CKey = "i2c";
    private const string SpiKey = "spi";
    private const string UartKey = "uart";
    private const string SensorKey = "sensor";
    private const string SwitchKey = "switch";
    private const string BinarySensorKey = "binary_sensor";
    private const string OutputKey = "output";
    private const string LightKey = "light";
    private const string ButtonKey = "button";
    private const string ClimateKey = "climate";
    private const string CoverKey = "cover";
    private const string DeepSleepKey = "deep_sleep";
    private const string DisplayKey = "display";
    private const string EthernetKey = "ethernet";
    private const string ExternalComponentsKey = "external_components";
    private const string FanKey = "fan";
    private const string FontKey = "font";
    private const string GlobalsKey = "globals";
    private const string NumberKey = "number";
    private const string SafeModeKey = "safe_mode";
    private const string ScriptKey = "script";
    private const string SelectKey = "select";
    private const string TextSensorKey = "text_sensor";

    public static string GetKeyString(this YamlKeys key)
    {
        return key switch
        {
            YamlKeys.Substitutions => SubstitutionsKey,
            YamlKeys.Packages => PackagesKey,
            YamlKeys.Esphome => EsphomeKey,
            YamlKeys.Esp8266 => Esp8266Key,
            YamlKeys.Esp32 => Esp32Key,
            YamlKeys.Rp2040 => Rp2040Key,
            YamlKeys.Bk72Xx => Bk72XxKey,
            YamlKeys.Rtl87Xx => Rtl87XxKey,
            YamlKeys.Wifi => WifiKey,
            YamlKeys.Api => ApiKey,
            YamlKeys.Ota => OtaKey,
            YamlKeys.Time => TimeKey,
            YamlKeys.WebServer => WebServerKey,
            YamlKeys.Mqtt => MqttKey,
            //YamlKeys.CaptivePortal => CaptivePortalKey,
            YamlKeys.Logger => LoggerKey,
            YamlKeys.I2C => I2CKey,
            YamlKeys.Spi => SpiKey,
            YamlKeys.Uart => UartKey,
            YamlKeys.Sensor => SensorKey,
            YamlKeys.Switch => SwitchKey,
            YamlKeys.BinarySensor => BinarySensorKey,
            YamlKeys.Output => OutputKey,
            YamlKeys.Light => LightKey,
            YamlKeys.Button => ButtonKey,
            YamlKeys.Climate => ClimateKey,
            YamlKeys.Cover => CoverKey,
            YamlKeys.DeepSleep => DeepSleepKey,
            YamlKeys.Display => DisplayKey,
            YamlKeys.Ethernet => EthernetKey,
            YamlKeys.ExternalComponents => ExternalComponentsKey,
            YamlKeys.Fan => FanKey,
            YamlKeys.Font => FontKey,
            YamlKeys.Globals => GlobalsKey,
            YamlKeys.Number => NumberKey,
            YamlKeys.SafeMode => SafeModeKey,
            YamlKeys.Script => ScriptKey,
            YamlKeys.Select => SelectKey,
            YamlKeys.TextSensor => TextSensorKey,
            _ => CamelCaseToSnakeCase(key.ToString().ToLowerInvariant())
        };
    }

    private static string CamelCaseToSnakeCase(this string text)
    {
        if (string.IsNullOrEmpty(text)) return text;
        var result = new StringBuilder();
        for (var i = 0; i < text.Length; i++)
        {
            var current = text[i];
            if (char.IsUpper(current) && i > 0)
            {
                result.Append('_');
            }

            result.Append(char.ToLowerInvariant(current));
        }

        return result.ToString();
    }
}
public enum YamlKeys
{
    Substitutions,
    Packages,
    Esphome,
    Esp8266,
    Esp32,
    Rp2040,
    Bk72Xx,
    Rtl87Xx,
    Wifi,
    Api,
    Ota,
    Time,
    WebServer,
    Mqtt,
    CaptivePortal,
    Logger,
    I2C,
    Spi,
    Uart,
    Sensor,
    Switch,
    BinarySensor,
    Output,
    Light,
    Button,
    Climate,
    Cover,
    DeepSleep,
    Display,
    Ethernet,
    ExternalComponents,
    Fan,
    Font,
    Globals,
    Number,
    SafeMode,
    Script,
    Select,
    TextSensor,
}