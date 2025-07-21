using System;
using EspHomeTools.Classes.Structures;
using EspHomeTools.Interfaces;

namespace EspHomeTools.Builders;

/// <summary>
/// Stellt Fluent-Erweiterungsmethoden zur Verfügung, um eine ESPHome-Konfiguration
/// auf einer <see cref="IYamlMapping"/>-Instanz aufzubauen.
/// </summary>
public static class EspHomeExtensions
{
    private const string SensorComponent = "sensor";
    private const string SwitchComponent = "switch";
    private const string BinarySensorComponent = "binary_sensor";
    private const string TimeComponent = "time";
    private const string OutputComponent = "output";
    private const string LightComponent = "light";
    private const string OtaComponent = "ota";


    #region Private Helfermethoden

    /// <summary>
    /// Erstellt und konfiguriert eine neue Builder-Instanz.
    /// </summary>
    private static T ConfigureBuilder<T>(Action<T> configurator) where T : new()
    {
        var builder = new T();
        configurator(builder);
        return builder;
    }

    /// <summary>
    /// Ruft sicher die Build()-Methode auf einem beliebigen Builder-Typ auf.
    /// Dies ist die zentrale, typsichere Stelle zur Erzeugung der YAML-Mappings.
    /// </summary>
    private static IYamlMapping GetBuilderResult<T>(T builder)
    {
        return builder switch
        {
            EsphomeBuilder b => b.Build(),
            WifiBlockBuilder b => b.Build(),
            Esp8266Builder b => b.Build(),
            Esp32Builder b => b.Build(),
            Rp2040Builder b => b.Build(),
            BekenBuilder b => b.Build(),
            MqttBuilder b => b.Build(),
            I2CBuilder b => b.Build(),
            ApiBuilder b => b.Build(),
            SpiBuilder b => b.Build(),
            DhtSensorBuilder b => b.Build(),
            GpioSwitchBuilder b => b.Build(),
            BinarySensorBuilder b => b.Build(),
            TimeBuilder b => b.Build(),
            OutputBuilder b => b.Build(),
            LightBuilder b => b.Build(),
            EnvironmentalSensorBuilder b => b.Build(),
            OtaBuilder b => b.Build(),
            AccessPointBuilder b => b.Build(),
            _ => throw new NotSupportedException($"Der Builder-Typ {typeof(T).Name} wird nicht unterstützt.")
        };
    }

    /// <summary>
    /// Fügt einen Konfigurationsblock direkt zum Root-Mapping hinzu (z.B. wifi:).
    /// </summary>
    private static IYamlMapping AddToRoot<T>(this IYamlMapping root, string key, Action<T> configurator) where T : new()
    {
        var builder = ConfigureBuilder(configurator);
        root[key] = GetBuilderResult(builder);
        return root;
    }

    /// <summary>
    /// Fügt eine Komponente zu einer Liste im Root-Mapping hinzu (z.B. sensor:).
    /// Erstellt die Liste, falls sie noch nicht existiert.
    /// </summary>
    private static IYamlMapping AddComponent<T>(this IYamlMapping root, string componentType, Action<T> configurator) where T : new()
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