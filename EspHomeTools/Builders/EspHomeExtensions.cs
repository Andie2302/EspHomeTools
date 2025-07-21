using System;
using EspHomeTools.Classes.Structures;
using EspHomeTools.Interfaces;

namespace EspHomeTools.Builders;

/// <summary>
/// Provides a set of fluent extension methods for building an ESPHome configuration
/// on an <see cref="IYamlMapping"/> instance.
/// </summary>
public static class EspHomeExtensions
{
    /// <summary>
    /// Represents the identifier used to define or reference sensor components
    /// within an ESPHome configuration context.
    /// </summary>
    /// <remarks>
    /// The <c>SensorComponent</c> variable is utilized as a constant in the
    /// <c>EspHomeExtensions</c> class to identify sensor-related components,
    /// such as DHT sensors or environmental sensors, in the building process
    /// of YAML mappings for ESPHome configurations.
    /// </remarks>
    private const string SensorComponent = "sensor";

    /// <summary>
    /// Represents the identifier for the "switch" component in ESPHome configuration.
    /// This constant is utilized for adding or configuring switch components within
    /// the Fluent API provided by the ESPHome extensions.
    /// </summary>
    private const string SwitchComponent = "switch";

    /// <summary>
    /// Represents the identifier for a binary sensor component within the ESPHome configuration.
    /// </summary>
    private const string BinarySensorComponent = "binary_sensor";

    /// <summary>
    /// Represents the key or identifier used to add a time component
    /// to an ESPHome YAML configuration.
    /// </summary>
    private const string TimeComponent = "time";

    /// <summary>
    /// Represents the key used to define or reference an output component
    /// within an ESPHome configuration. This const string is commonly
    /// utilized in fluent extension methods to append or modify output component
    /// configurations in a YAML mapping structure.
    /// </summary>
    private const string OutputComponent = "output";

    /// <summary>
    /// Represents the "light" component type within the ESPHome configuration system.
    /// It is used internally as an identifier for configuring and integrating light components
    /// into an ESPHome YAML-based configuration.
    /// </summary>
    private const string LightComponent = "light";

    /// <summary>
    /// Represents the internal string identifier for the OTA (Over-The-Air) component
    /// used within the ESPHome configuration system.
    /// </summary>
    private const string OtaComponent = "ota";


    #region Private Helfermethoden

    /// <summary>
    /// Creates and configures a new builder instance.
    /// </summary>
    /// <param name="configurator">The configuration action to be applied to the new builder instance.</param>
    /// <typeparam name="T">The type of the builder to be created. Must have a parameterless constructor.</typeparam>
    /// <returns>A configured builder instance of type <typeparamref name="T"/>.</returns>
    private static T ConfigureBuilder<T>(Action<T> configurator) where T : new()
    {
        var builder = new T();
        configurator(builder);
        return builder;
    }

    /// <summary>
    /// Invokes the Build() method on a given builder type in a type-safe manner.
    /// This provides a centralized and type-safe way to generate YAML mappings.
    /// </summary>
    /// <typeparam name="T">The type of the builder from which the YAML mapping is created.</typeparam>
    /// <param name="builder">The builder instance used to generate the YAML mapping.</param>
    /// <returns>The generated YAML mapping as an <see cref="IYamlMapping"/> instance.</returns>
    /// <exception cref="NotSupportedException">Thrown if the given builder type is not supported.</exception>
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
            _ => throw new NotSupportedException($"The builder type {typeof(T).Name} is not supported.")
        };
    }

    /// <summary>
    /// Adds a configuration block directly to the root mapping with the specified key.
    /// </summary>
    /// <param name="root">The root IYamlMapping to which the configuration block is added.</param>
    /// <param name="key">The key representing the configuration block (e.g., "wifi").</param>
    /// <param name="configurator">An action to configure the builder for the configuration block.</param>
    /// <typeparam name="T">The type of the builder used to create the configuration block.</typeparam>
    /// <returns>The updated IYamlMapping including the new configuration block.</returns>
    private static IYamlMapping AddToRoot<T>(this IYamlMapping root, string key, Action<T> configurator) where T : new()
    {
        var builder = ConfigureBuilder(configurator);
        root[key] = GetBuilderResult(builder);
        return root;
    }

    /// <summary>
    /// Adds a component to a list in the root mapping (e.g., sensor:).
    /// Creates the list if it does not already exist.
    /// </summary>
    /// <param name="root">The root mapping to which the component will be added.</param>
    /// <param name="componentType">The type of the component as a string (e.g., "sensor").</param>
    /// <param name="configurator">An action to configure the builder for the specified component.</param>
    /// <typeparam name="T">The type of the builder associated with the component.</typeparam>
    /// <returns>The updated root mapping containing the newly added component.</returns>
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

    /// <summary>
    /// Adds an "esphome" block to the root YAML mapping and allows its configuration using a specified configurator.
    /// </summary>
    /// <param name="root">The root YAML mapping to which the "esphome" block will be added.</param>
    /// <param name="configurator">A delegate to configure the EsphomeBuilder instance for the "esphome" block.</param>
    /// <returns>The modified YAML mapping, including the configured "esphome" block.</returns>
    public static IYamlMapping WithEsphome(this IYamlMapping root, Action<EsphomeBuilder> configurator) => root.AddToRoot("esphome", configurator);
    /// <summary>
    /// Adds a WiFi configuration to the YAML mapping, allowing further setup of WiFi settings.
    /// </summary>
    /// <param name="root">The root YAML mapping to which the WiFi configuration will be added.</param>
    /// <param name="configurator">An action to configure the WiFi settings via the <see cref="WifiBlockBuilder"/>.</param>
    /// <returns>The updated YAML mapping containing the WiFi configuration.</returns>
    public static IYamlMapping WithWifi(this IYamlMapping root, Action<WifiBlockBuilder> configurator) => root.AddToRoot("wifi", configurator);
    /// <summary>
    /// Adds a "logger" entry to the provided YAML mapping.
    /// </summary>
    /// <param name="mapping">The mapping to which the "logger" entry will be added.</param>
    /// <returns>The updated YAML mapping with the "logger" entry included.</returns>
    public static IYamlMapping WithLogger(this IYamlMapping mapping)
    {
        mapping.Add("logger", new YamlMapping());
        return mapping;
    }
    /// <summary>
    /// Adds an "api" block to the YAML mapping and configures it using the provided configurator.
    /// </summary>
    /// <param name="root">The root YAML mapping to which the "api" block will be added.</param>
    /// <param name="configurator">An action used to configure the "api" block.</param>
    /// <returns>The updated YAML mapping with the "api" block added.</returns>
    public static IYamlMapping WithApi(this IYamlMapping root, Action<ApiBuilder> configurator) => root.AddToRoot("api", configurator);
    /// <summary>
    /// Adds an OTA (Over-the-Air) update component to the YAML mapping and allows its configuration.
    /// </summary>
    /// <param name="root">The root YAML mapping to which the OTA component will be added.</param>
    /// <param name="configurator">A delegate to configure the OTA component using an <see cref="OtaBuilder"/>.</param>
    /// <returns>The modified YAML mapping instance with the OTA component.</returns>
    public static IYamlMapping WithOta(this IYamlMapping root, Action<OtaBuilder> configurator) => root.AddComponent(OtaComponent, configurator);

    /// <summary>
    /// Adds an ESP8266 configuration block to the YAML mapping and allows customization through the provided builder.
    /// </summary>
    /// <param name="root">The YAML mapping to which the ESP8266 configuration will be added.</param>
    /// <param name="configurator">A delegate to configure the ESP8266 settings.</param>
    /// <returns>The updated YAML mapping containing the ESP8266 configuration block.</returns>
    public static IYamlMapping WithEsp8266(this IYamlMapping root, Action<Esp8266Builder> configurator) => root.AddToRoot("esp8266", configurator);
    /// <summary>
    /// Configures an ESP32 platform section within the YAML mapping.
    /// </summary>
    /// <param name="root">The root YAML mapping to add the ESP32 configuration to.</param>
    /// <param name="configurator">
    /// A delegate used to specify additional configurations for the ESP32 platform.
    /// </param>
    /// <returns>The updated YAML mapping with the ESP32 configuration included.</returns>
    public static IYamlMapping WithEsp32(this IYamlMapping root, Action<Esp32Builder> configurator) => root.AddToRoot("esp32", configurator);
    /// <summary>
    /// Adds an "rp2040" platform configuration to the specified YAML mapping and allows further customization through the provided configurator.
    /// </summary>
    /// <param name="root">The root YAML mapping to which the "rp2040" configuration will be added.</param>
    /// <param name="configurator">An action to configure the "rp2040" platform options using an <see cref="Rp2040Builder"/> instance.</param>
    /// <returns>The modified YAML mapping instance with the configured "rp2040" platform entry.</returns>
    public static IYamlMapping WithRp2040(this IYamlMapping root, Action<Rp2040Builder> configurator) => root.AddToRoot("rp2040", configurator);
    /// <summary>
    /// Adds a Beken platform configuration to the current YAML mapping.
    /// </summary>
    /// <param name="root">The root YAML mapping to which the Beken platform configuration will be added.</param>
    /// <param name="configurator">An action to configure the BekenBuilder instance.</param>
    /// <returns>The updated YAML mapping with the Beken platform configuration.</returns>
    public static IYamlMapping WithBeken(this IYamlMapping root, Action<BekenBuilder> configurator) => root.AddToRoot("bk72xx", configurator);

    /// <summary>
    /// Adds an MQTT configuration block to the root structure.
    /// </summary>
    /// <param name="root">The root YAML mapping to which the MQTT block will be added.</param>
    /// <param name="configurator">An action to configure the MQTT block.</param>
    /// <returns>The updated YAML mapping with the MQTT configuration added.</returns>
    public static IYamlMapping WithMqtt(this IYamlMapping root, Action<MqttBuilder> configurator) => root.AddToRoot("mqtt", configurator);
    /// <summary>
    /// Adds an I2C configuration block to the YAML mapping, allowing further customization via the provided configurator.
    /// </summary>
    /// <param name="root">The root YAML mapping to which the I2C configuration is added.</param>
    /// <param name="configurator">An action to configure the I2C settings within the mapping.</param>
    /// <returns>The updated YAML mapping with the I2C configuration added.</returns>
    public static IYamlMapping WithI2C(this IYamlMapping root, Action<I2CBuilder> configurator) => root.AddToRoot("i2c", configurator);
    /// <summary>
    /// Appends an SPI configuration to the root node by using the provided <see cref="SpiBuilder"/> configurator.
    /// </summary>
    /// <param name="root">The root node to which the SPI configuration will be added.</param>
    /// <param name="configurator">The action used to configure the SPI settings.</param>
    /// <returns>The updated root node with the SPI configuration applied.</returns>
    public static IYamlMapping WithSpi(this IYamlMapping root, Action<SpiBuilder> configurator) => root.AddToRoot("spi", configurator);

    /// <summary>
    /// Adds a DHT sensor component to the YAML mapping and configures it using the provided configurator.
    /// </summary>
    /// <param name="root">The root mapping to which the DHT sensor component will be added.</param>
    /// <param name="configurator">The configuration action for the DHT sensor builder.</param>
    /// <returns>The updated YAML mapping with the DHT sensor added.</returns>
    public static IYamlMapping WithDhtSensor(this IYamlMapping root, Action<DhtSensorBuilder> configurator) => root.AddComponent(SensorComponent, configurator);
    /// <summary>
    /// Adds configuration for an environmental sensor component to the YAML mapping.
    /// </summary>
    /// <param name="root">The root YAML mapping to add the environmental sensor configuration to.</param>
    /// <param name="configurator">An action to configure the environmental sensor.</param>
    /// <returns>The updated YAML mapping with the environmental sensor configuration added.</returns>
    public static IYamlMapping WithEnvironmentalSensor(this IYamlMapping root, Action<EnvironmentalSensorBuilder> configurator) => root.AddComponent(SensorComponent, configurator);
    /// <summary>
    /// Adds a GPIO switch component configuration to the YAML mapping.
    /// </summary>
    /// <param name="root">The root YAML mapping to which the GPIO switch configuration will be added.</param>
    /// <param name="configurator">An action to configure the GPIO switch settings using a <see cref="GpioSwitchBuilder"/>.</param>
    /// <returns>The updated YAML mapping with the GPIO switch component included.</returns>
    public static IYamlMapping WithGpioSwitch(this IYamlMapping root, Action<GpioSwitchBuilder> configurator) => root.AddComponent(SwitchComponent, configurator);
    /// <summary>
    /// Adds and configures a binary sensor component in the YAML mapping structure.
    /// </summary>
    /// <param name="root">The root YAML mapping to which the binary sensor component will be added.</param>
    /// <param name="configurator">A delegate to configure the binary sensor using the <see cref="BinarySensorBuilder"/>.</param>
    /// <returns>The updated YAML mapping with the configured binary sensor component.</returns>
    public static IYamlMapping WithBinarySensor(this IYamlMapping root, Action<BinarySensorBuilder> configurator) => root.AddComponent(BinarySensorComponent, configurator);
    /// <summary>
    /// Adds a "time" component to the YAML mapping and configures it using the provided configurator callback.
    /// </summary>
    /// <param name="root">The YAML mapping to which the "time" component will be added.</param>
    /// <param name="configurator">A callback function to configure the "time" component.</param>
    /// <returns>The modified YAML mapping with the "time" component added.</returns>
    public static IYamlMapping WithTime(this IYamlMapping root, Action<TimeBuilder> configurator) => root.AddComponent(TimeComponent, configurator);
    /// <summary>
    /// Adds an output component to the YAML mapping and allows for additional configuration of the output component.
    /// </summary>
    /// <param name="root">The root YAML mapping to which the output component will be added.</param>
    /// <param name="configurator">An action to configure the output component.</param>
    /// <returns>The updated YAML mapping with the configured output component added.</returns>
    public static IYamlMapping WithOutput(this IYamlMapping root, Action<OutputBuilder> configurator) => root.AddComponent(OutputComponent, configurator);
    /// <summary>
    /// Adds a light component to the YAML mapping and allows configuration of the light component.
    /// </summary>
    /// <param name="root">The root YAML mapping to which the light component will be added.</param>
    /// <param name="configurator">A delegate to configure the light component using a <see cref="LightBuilder"/>.</param>
    /// <returns>The updated YAML mapping with the light component added.</returns>
    public static IYamlMapping WithLight(this IYamlMapping root, Action<LightBuilder> configurator) => root.AddComponent(LightComponent, configurator);

    #endregion
}