using EspHomeTools.Classes;
using EspHomeTools.Interfaces;
using System.Collections.Generic;

namespace EspHomeTools.Extensions;

public static class EspHomeExtensions
{
    public static IYamlMapping WithEsphome(this IYamlMapping mapping, string name)
    {
        var esphomeBlock = new YamlMapping
        {
            { "name", new YamlString(name) }
        };

        mapping.Add("esphome", esphomeBlock);
        return mapping;
    }
    public static IYamlMapping WithWifi(this IYamlMapping mapping, string ssid, string password)
    {
        var wifiBlock = new YamlMapping
        {
            { "ssid", new YamlString(ssid) },
            { "password", new YamlString(password) }
        };

        mapping.Add("wifi", wifiBlock);
        return mapping;
    }
    public static IYamlMapping WithLogger(this IYamlMapping mapping)
    {
        mapping.Add("logger", new YamlMapping());
        return mapping;
    }
    public static IYamlMapping WithApi(this IYamlMapping mapping)
    {
        mapping.Add("api", new YamlMapping());
        return mapping;
    }
    public static IYamlMapping WithOta(this IYamlMapping mapping)
    {
        mapping.Add("ota", new YamlMapping());
        return mapping;
    }
    public static IYamlMapping WithComponent(this IYamlMapping root, string componentType, IYamlMapping componentConfig)
    {
        if (!root.TryGetValue(componentType, out var node) || node is not IYamlSequence sequence)
        {
            sequence = new YamlSequence();
            root.Add(componentType, sequence);
        }

        sequence.Add(componentConfig);
        return root;
    }
}