using System;
using EspHomeTools.Classes;
using EspHomeTools.Classes.Structures;
using EspHomeTools.Interfaces;

namespace EspHomeTools.Builder;

public static class EspHomeExtensions
{
    public static IYamlMapping WithEsphome(this IYamlMapping root, Action<EsphomeBlockBuilder> configurator)
    {
        var builder = new EsphomeBlockBuilder();
        configurator(builder);
        root["esphome"] = builder.Build();
        return root;
    }

    public static IYamlMapping WithWifi(this IYamlMapping root, Action<WifiBlockBuilder> configurator)
    {
        var builder = new WifiBlockBuilder();
        configurator(builder);
        root["wifi"] = builder.Build();
        return root;
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

    public static IYamlMapping WithDhtSensor(this IYamlMapping root, Action<DhtSensorBuilder> configurator)
    {
        var builder = new DhtSensorBuilder();
        configurator(builder);
        var sensorConfig = builder.Build();
        return root.WithComponent("sensor", sensorConfig);
    }
    public static IYamlMapping WithGpioSwitch(this IYamlMapping root, Action<GpioSwitchBuilder> configurator)
    {
        var builder = new GpioSwitchBuilder();
        configurator(builder);
        var switchConfig = builder.Build();
        return root.WithComponent("switch", switchConfig);
    }
}