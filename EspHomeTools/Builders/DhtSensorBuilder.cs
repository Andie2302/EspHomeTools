using System;
using EspHomeTools.Classes.Scalars;
using EspHomeTools.Classes.Structures;
using EspHomeTools.Interfaces;

namespace EspHomeTools.Builders;

/// <summary>
/// Provides a fluent builder for constructing and configuring a DHT sensor configuration
/// for use in the ESPHome system. The builder generates a YAML configuration structure
/// that specifies key attributes for the DHT sensor, such as the platform, pin, update interval,
/// temperature and humidity sensors, and model details.
/// </summary>
public class DhtSensorBuilder
{
    /// <summary>
    /// Represents the configuration key for specifying the platform in YAML configuration data.
    /// Used internally for defining the "platform" attribute within sensor configurations.
    /// </summary>
    private const string PlatformKey = "platform";

    /// <summary>
    /// Represents the key used to specify the pin configuration for a DHT sensor in the YAML mapping.
    /// </summary>
    /// <remarks>
    /// The value associated with this key in the configuration defines the GPIO pin that the DHT sensor is connected to.
    /// This key is required for the proper setup of the DHT sensor and must be provided using the appropriate methods in the builder.
    /// </remarks>
    private const string PinKey = "pin";

    /// <summary>
    /// Represents the key used to identify and configure the temperature property for a DHT sensor in the YAML configuration.
    /// This key is utilized within the <see cref="DhtSensorBuilder" /> class to set up the temperature-related settings for a sensor.
    /// </summary>
    private const string TemperatureKey = "temperature";

    /// <summary>
    /// Represents the configuration key used to specify the humidity sensor configuration
    /// in the DHT sensor YAML mapping.
    /// </summary>
    /// <remarks>
    /// This key is used within the internal sensor configuration to define properties
    /// related to the humidity measurements, such as the associated name or value.
    /// It ensures proper identification and structure within the YAML setup for
    /// the DHT sensor.
    /// </remarks>
    private const string HumidityKey = "humidity";

    /// <summary>
    /// Represents the configuration key used for defining the update interval in
    /// the sensor configuration mapping.
    /// </summary>
    /// <remarks>
    /// The <c>UpdateIntervalKey</c> is a constant string utilized within the
    /// configuration of a <c>DhtSensorBuilder</c> to specify the key for
    /// setting the update interval. The associated value typically determines
    /// the frequency at which updates are generated for the DHT sensor.
    /// </remarks>
    private const string UpdateIntervalKey = "update_interval";

    /// <summary>
    /// Represents the key used to specify the model for configuring a DHT sensor.
    /// </summary>
    /// <remarks>
    /// The <c>ModelKey</c> is a constant string utilized within the <see cref="DhtSensorBuilder"/> class to define
    /// the YAML key associated with the sensor's model configuration.
    /// This key is used when setting or retrieving the model value in the YAML mapping structure.
    /// </remarks>
    private const string ModelKey = "model";

    /// Represents a key used in YAML mappings to define the "name" property for DHT sensor configurations.
    private const string NameKey = "name";

    /// <summary>
    /// Represents the platform key value "dht" used in the configuration of a DHT sensor.
    /// This value is assigned to the platform key in the YAML mapping to identify a DHT sensor platform.
    /// </summary>
    private const string DhtPlatform = "dht";

    /// <summary>
    /// Represents the configuration data used for building a DHT sensor
    /// in YAML format. This variable holds mappings of configuration keys
    /// to their corresponding values, enabling customization of the sensor setup.
    /// </summary>
    /// <remarks>
    /// This variable is initialized as an instance of <see cref="YamlMapping"/>
    /// and stores the key-value pairs that define the properties of a DHT sensor.
    /// It is utilized throughout the <see cref="DhtSensorBuilder"/> class to add
    /// or modify configuration details.
    /// </remarks>
    private readonly YamlMapping _config = new();

    /// <summary>
    /// Builder class to facilitate the creation and configuration of DHT sensor objects.
    /// </summary>
    /// <remarks>
    /// The <c>DhtSensorBuilder</c> class provides fluent methods to configure various properties of a DHT sensor,
    /// such as the pin, temperature and humidity properties, model, and update interval.
    /// This class simplifies the process of configuring the DHT sensor by using a builder pattern and
    /// handles configuration values using YAML structures.
    /// </remarks>
    public DhtSensorBuilder()
    {
        _config[PlatformKey] = new YamlString(DhtPlatform);
    }

    /// <summary>
    /// Sets the data pin for the DHT sensor to the specified pin value.
    /// </summary>
    /// <param name="pin">The data pin to use for the DHT sensor, typically in GPIO notation (e.g., "GPIO2").</param>
    /// <returns>The current instance of <see cref="DhtSensorBuilder"/>, allowing for method chaining.</returns>
    public DhtSensorBuilder UsePin(string pin)
    {
        _config[PinKey] = CreateYamlNode(pin);
        return this;
    }

    /// Sets the pin configuration for the DHT sensor.
    /// <param name="pin">
    /// The pin to be used for the sensor.
    /// The parameter can be provided as a string or as a YAML secret for sensitive values.
    /// </param>
    /// <return>
    /// Returns the current instance of <see cref="DhtSensorBuilder"/>,
    /// allowing method chaining for additional configurations.
    /// </return>
    public DhtSensorBuilder UsePin(YamlSecret pin)
    {
        _config[PinKey] = pin;
        return this;
    }

    /// Specifies the GPIO pin to be used.
    /// Overloaded versions of this method allow specifying the pin as a simple string, a YamlSecret for encrypted values,
    /// or a string combined with the ability to set the value as secret.
    /// Parameters:
    /// - pin: The identifier of the GPIO pin. Accepts plain text, or a string marked as a secret.
    /// - isSecret (optional): If true, treats the pin value as a secret, storing it as a YamlSecret object.
    /// Returns:
    /// An instance of DhtSensorBuilder configured with the specified pin.
    public DhtSensorBuilder UsePin(string pin, bool isSecret) =>
        SetConfigValue(PinKey, pin, isSecret);

    /// <summary>
    /// Sets the temperature configuration for a DHT sensor and associates it with a specified name.
    /// </summary>
    /// <param name="name">The name to assign to the temperature configuration.</param>
    /// <returns>The current <see cref="DhtSensorBuilder"/> instance with the updated temperature configuration.</returns>
    public DhtSensorBuilder WithTemperature(string name)
    {
        _config[TemperatureKey] = new YamlMapping { { NameKey, CreateYamlNode(name) } };
        return this;
    }

    /// <summary>
    /// Adds a humidity configuration to the DHT sensor with the specified name.
    /// </summary>
    /// <param name="name">The name to assign to the humidity reading.</param>
    /// <returns>An instance of <see cref="DhtSensorBuilder"/> to allow method chaining.</returns>
    public DhtSensorBuilder WithHumidity(string name)
    {
        _config[HumidityKey] = new YamlMapping { { NameKey, CreateYamlNode(name) } };
        return this;
    }

    /// Configures the update interval for the DHT sensor data readings.
    /// <param name="interval">A string representing the update interval, typically in a format like "60s" for 60 seconds.</param>
    /// <returns>The updated instance of <c>DhtSensorBuilder</c> to allow for method chaining.</returns>
    public DhtSensorBuilder WithUpdateInterval(string interval)
    {
        _config[UpdateIntervalKey] = CreateYamlNode(interval);
        return this;
    }

    /// <summary>
    /// Sets the update interval for the DHT sensor in the configuration.
    /// </summary>
    /// <param name="interval">The update interval as a <c>YamlSecret</c> representing a sensitive configuration value.</param>
    /// <returns>Returns the <c>DhtSensorBuilder</c> instance for method chaining.</returns>
    public DhtSensorBuilder WithUpdateInterval(YamlSecret interval)
    {
        _config[UpdateIntervalKey] = interval;
        return this;
    }

    /// <summary>
    /// Configures the update interval for the DHT sensor.
    /// </summary>
    /// <param name="interval">The update interval as a string.</param>
    /// <returns>The current instance of the <c>DhtSensorBuilder</c> for method chaining.</returns>
    public DhtSensorBuilder WithUpdateInterval(string interval, bool isSecret) =>
        SetConfigValue(UpdateIntervalKey, interval, isSecret);

    /// Configures the model for the DHT sensor.
    /// <param name="model">The model of the DHT sensor to be configured, represented as a string.</param>
    /// <returns>A reference to the current DhtSensorBuilder instance, allowing for method chaining.</returns>
    public DhtSensorBuilder WithModel(string model)
    {
        _config[ModelKey] = CreateYamlNode(model);
        return this;
    }

    /// <summary>
    /// Configures the model for the DHT sensor using a string value.
    /// </summary>
    /// <param name="model">The name of the model as a plain string.</param>
    /// <returns>The current instance of <see cref="DhtSensorBuilder"/> for method chaining.</returns>
    public DhtSensorBuilder WithModel(YamlSecret model)
    {
        _config[ModelKey] = model;
        return this;
    }

    /// <summary>
    /// Sets the model configuration for the DHT sensor. A model must be supplied to properly define
    /// the sensor type in the configuration.
    /// </summary>
    /// <param name="model">The name of the DHT sensor model to be used.</param>
    /// <param name="isSecret">Indicates whether the model value should be stored as a secret.</param>
    /// <returns>Returns the instance of <see cref="DhtSensorBuilder"/> to allow method chaining.</returns>
    public DhtSensorBuilder WithModel(string model, bool isSecret) =>
        SetConfigValue(ModelKey, model, isSecret);

    /// Adds a comment to a configuration key in the builder, if the key exists.
    /// <param name="key">The configuration key to which the comment should be added.</param>
    /// <param name="comment">The comment to associate with the specified key.</param>
    /// <returns>The current instance of the DhtSensorBuilder for method chaining.
    public DhtSensorBuilder WithCommentOn(string key, string comment)
    {
        if (_config.TryGetValue(key, out var node))
            node.Comment = comment;

        return this;
    }

    /// Builds the DHT sensor configuration and returns the corresponding YAML mapping object.
    /// <returns>
    /// A YAML mapping object representing the DHT sensor configuration.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if the configuration is incomplete:
    /// - If no pin has been specified using UsePin().
    /// - If neither temperature nor humidity has been configured using WithTemperature() or WithHumidity().
    /// </exception>
    internal IYamlMapping Build()
    {
        if (!_config.ContainsKey(PinKey))
        {
            throw new InvalidOperationException("A pin must be specified for the DHT sensor using UsePin().");
        }

        if (!_config.ContainsKey(TemperatureKey) && !_config.ContainsKey(HumidityKey))
        {
            throw new InvalidOperationException("For a DHT sensor, at least temperature (WithTemperature) or humidity (WithHumidity) must be configured.");
        }

        return _config;
    }

    /// <summary>
    /// Creates a YAML scalar node from the provided string value.
    /// </summary>
    /// <param name="value">The string value to convert into a YAML scalar node.</param>
    /// <returns>A YamlString instance representing the specified value as a YAML scalar node.</returns>
    private static YamlString CreateYamlNode(string value) => new(value);

    /// Sets a configuration value in the DHT sensor configuration.
    /// <param name="key">The key associated with the configuration value.</param>
    /// <param name="value">The value to be set for the specified key.</param>
    /// <param name="isSecret">Indicates whether the value should be treated as a secret.</param>
    /// <return>The current instance of DhtSensorBuilder with the updated configuration.</return>
    private DhtSensorBuilder SetConfigValue(string key, string value, bool isSecret)
    {
        _config[key] = isSecret ? new YamlSecret(value) : CreateYamlNode(value);
        return this;
    }
}