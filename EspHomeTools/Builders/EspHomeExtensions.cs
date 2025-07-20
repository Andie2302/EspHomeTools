using System;
using EspHomeTools.Classes.Structures;
using EspHomeTools.Interfaces;

namespace EspHomeTools.Builders;

public static class EspHomeExtensions
{
    public static IYamlMapping WithEsphome(this IYamlMapping root, Action<EsphomeBuilder> configurator)
    {
        var builder = new EsphomeBuilder();
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

    public static IYamlMapping WithEsp8266(this IYamlMapping root, Action<Esp8266Builder> configurator)
    {
        var builder = new Esp8266Builder();
        configurator(builder);
        root["esp8266"] = builder.Build();
        return root;
    }

    public static IYamlMapping WithEsp32(this IYamlMapping root, Action<Esp32Builder> configurator)
    {
        var builder = new Esp32Builder();
        configurator(builder);
        root["esp32"] = builder.Build();
        return root;
    }

    public static IYamlMapping WithRp2040(this IYamlMapping root, Action<Rp2040Builder> configurator)
    {
        var builder = new Rp2040Builder();
        configurator(builder);
        root["rp2040"] = builder.Build();
        return root;
    }

    public static IYamlMapping WithBeken(this IYamlMapping root, Action<BekenBuilder> configurator)
    {
        var builder = new BekenBuilder();
        configurator(builder);
        root["bk72xx"] = builder.Build();
        return root;
    }
    public static IYamlMapping WithMqtt(this IYamlMapping root, Action<MqttBuilder> configurator)
    {
        var builder = new MqttBuilder();
        configurator(builder);
        root["mqtt"] = builder.Build();
        return root;
    }
    public static IYamlMapping WithBinarySensor(this IYamlMapping root, Action<BinarySensorBuilder> configurator)
    {
        var builder = new BinarySensorBuilder();
        configurator(builder);
        var componentConfig = builder.Build();
        return root.WithComponent("binary_sensor", componentConfig);
    }
    public static IYamlMapping WithTime(this IYamlMapping root, Action<TimeBuilder> configurator)
    {
        var builder = new TimeBuilder();
        configurator(builder);
        var componentConfig = builder.Build();
        return root.WithComponent("time", componentConfig);
    }
    public static IYamlMapping WithOutput(this IYamlMapping root, Action<OutputBuilder> configurator)
    {
        var builder = new OutputBuilder();
        configurator(builder);
        var componentConfig = builder.Build();
        return root.WithComponent("output", componentConfig);
    }

    public static IYamlMapping WithLight(this IYamlMapping root, Action<LightBuilder> configurator)
    {
        var builder = new LightBuilder();
        configurator(builder);
        var componentConfig = builder.Build();
        return root.WithComponent("light", componentConfig);
    }
    public static IYamlMapping WithI2C(this IYamlMapping root, Action<I2CBuilder> configurator)
    {
        var builder = new I2CBuilder();
        configurator(builder);
        root["i2c"] = builder.Build();
        return root;
    }
    public static IYamlMapping WithEnvironmentalSensor(this IYamlMapping root, Action<EnvironmentalSensorBuilder> configurator)
    {
        var builder = new EnvironmentalSensorBuilder();
        configurator(builder);
        var componentConfig = builder.Build();
        return root.WithComponent("sensor", componentConfig);
    }
    public static IYamlMapping WithApi(this IYamlMapping mapping)
    {
        mapping.Add("api", new YamlMapping());
        return mapping;
    }

    public static IYamlMapping WithApi(this IYamlMapping root, Action<ApiBuilder> configurator)
    {
        var builder = new ApiBuilder();
        configurator(builder);
        root["api"] = builder.Build();
        return root;
    }

    public static IYamlMapping WithOta(this IYamlMapping root)
    {
        var builder = new OtaBuilder();
        var componentConfig = builder.Build();
        return root.WithComponent("ota", componentConfig);
    }

    public static IYamlMapping WithOta(this IYamlMapping root, Action<OtaBuilder> configurator)
    {
        var builder = new OtaBuilder();
        configurator(builder);
        var componentConfig = builder.Build();
        return root.WithComponent("ota", componentConfig);
    }

    public static IYamlMapping WithSpi(this IYamlMapping root, Action<SpiBuilder> configurator)
    {
        var builder = new SpiBuilder();
        configurator(builder);
        root["spi"] = builder.Build();
        return root;
    }
}