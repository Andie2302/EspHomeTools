using System;
using EspHomeTools.Classes.Structures;
using EspHomeTools.Interfaces;

namespace EspHomeTools.Builders;

public static class EspHomeExtensions
{


    #region Private Helfermethoden
    private static T ConfigureBuilder<T>(Action<T> configurator) where T : IYamlBuilder, new()
    {
        var builder = new T();
        configurator(builder);
        return builder;
    }

    private static IYamlMapping GetBuilderResult<T>(T builder) where T : IYamlBuilder
    {
        return builder.Build();
    }

    private static IYamlMapping AddToRoot<T>(this IYamlMapping root, string key, Action<T> configurator) where T : IYamlBuilder, new()
    {
        var builder = ConfigureBuilder(configurator);
        root[key] = GetBuilderResult(builder);
        return root;
    }

    private static IYamlMapping AddComponent<T>(this IYamlMapping root, string componentType, Action<T> configurator) where T : IYamlBuilder, new()
    {
        var builder = ConfigureBuilder(configurator);
        var componentConfig = GetBuilderResult(builder);
        if (!root.TryGetValue(componentType, out var node) || node is not IYamlSequence sequence)
        {
            sequence = new YamlSequence();
            root.Add(componentType, sequence);
        }
        sequence.Add(componentConfig);
        return root;
    }
    #endregion

    #region Öffentliche Erweiterungsmethoden
    public static IYamlMapping WithEsphome(this IYamlMapping root, Action<EsphomeBuilder> configurator) => root.AddToRoot("esphome", configurator);
    public static IYamlMapping WithWifi(this IYamlMapping root, Action<WifiBlockBuilder> configurator) => root.AddToRoot("wifi", configurator);
    public static IYamlMapping WithLogger(this IYamlMapping mapping)
    {
        mapping.Add("logger", new YamlMapping());
        return mapping;
    }
    public static IYamlMapping WithApi(this IYamlMapping root, Action<ApiBuilder> configurator) => root.AddToRoot("api", configurator);
    public static IYamlMapping WithOta(this IYamlMapping root, Action<OtaBuilder> configurator) => root.AddComponent(OtaComponent, configurator);
    public static IYamlMapping WithEsp8266(this IYamlMapping root, Action<Esp8266Builder> configurator) => root.AddToRoot("esp8266", configurator);
    public static IYamlMapping WithEsp32(this IYamlMapping root, Action<Esp32Builder> configurator) => root.AddToRoot("esp32", configurator);
    public static IYamlMapping WithRp2040(this IYamlMapping root, Action<Rp2040Builder> configurator) => root.AddToRoot("rp2040", configurator);
    public static IYamlMapping WithBeken(this IYamlMapping root, Action<BekenBuilder> configurator) => root.AddToRoot("bk72xx", configurator);
    public static IYamlMapping WithMqtt(this IYamlMapping root, Action<MqttBuilder> configurator) => root.AddToRoot("mqtt", configurator);
    public static IYamlMapping WithI2C(this IYamlMapping root, Action<I2CBuilder> configurator) => root.AddToRoot("i2c", configurator);
    public static IYamlMapping WithSpi(this IYamlMapping root, Action<SpiBuilder> configurator) => root.AddToRoot("spi", configurator);
    public static IYamlMapping WithDhtSensor(this IYamlMapping root, Action<DhtSensorBuilder> configurator) => root.AddComponent(SensorComponent, configurator);
    public static IYamlMapping WithEnvironmentalSensor(this IYamlMapping root, Action<EnvironmentalSensorBuilder> configurator) => root.AddComponent(SensorComponent, configurator);
    public static IYamlMapping WithGpioSwitch(this IYamlMapping root, Action<GpioSwitchBuilder> configurator) => root.AddComponent(SwitchComponent, configurator);
    public static IYamlMapping WithBinarySensor(this IYamlMapping root, Action<BinarySensorBuilder> configurator) => root.AddComponent(BinarySensorComponent, configurator);
    public static IYamlMapping WithTime(this IYamlMapping root, Action<TimeBuilder> configurator) => root.AddComponent(TimeComponent, configurator);
    public static IYamlMapping WithOutput(this IYamlMapping root, Action<OutputBuilder> configurator) => root.AddComponent(OutputComponent, configurator);
    public static IYamlMapping WithLight(this IYamlMapping root, Action<LightBuilder> configurator) => root.AddComponent(LightComponent, configurator);
    #endregion
}