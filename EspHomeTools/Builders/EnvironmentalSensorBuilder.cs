using System;
using EspHomeTools.Classes.Scalars;
using EspHomeTools.Classes.Structures;
using EspHomeTools.Interfaces;

namespace EspHomeTools.Builders;

/// <summary>
/// Provides a builder for creating and configuring environmental sensor configurations
/// for Bosch sensor platforms (e.g., BME280, BME680).
/// </summary>
public class EnvironmentalSensorBuilder
{
    /// <summary>
    /// Stores configuration data for an environmental sensor as a YAML mapping.
    /// </summary>
    /// <remarks>
    /// The <c>_config</c> variable is utilized to construct and manage sensor configuration
    /// settings in YAML format. It is initialized with a default platform value and is
    /// updated through various builder methods to include additional sensor properties
    /// like platform type, I2C address, temperature, pressure, and others.
    /// </remarks>
    private readonly YamlMapping _config = new();

    /// <summary>
    /// Builder class for configuring and constructing YAML mappings for environmental sensors.
    /// </summary>
    /// <remarks>
    /// The <c>EnvironmentalSensorBuilder</c> class simplifies the process of creating YAML configurations for supported environmental sensor platforms.
    /// It provides a fluent API to configure various parameters, such as sensor platform, I2C address, sensor readings, update intervals, and YAML comments.
    /// </remarks>
    public EnvironmentalSensorBuilder()
    {
        _config["platform"] = new YamlString("bme280");
    }

    /// Sets the platform for the builder configuration.
    /// <param name="platform">
    /// The platform to be used in the configuration.
    /// </param>
    /// <returns>
    /// The current instance of the builder, allowing for fluent configuration.
    /// </returns>
    public EnvironmentalSensorBuilder WithPlatform(string platform)
    {
        _config["platform"] = new YamlString(platform);
        return this;
    }

    /// Configures the I2C address to be used by the environmental sensor.
    /// <param name="address">The I2C address to set for the environmental sensor.</param>
    /// <returns>
    /// An instance of the current builder with the specified I2C address configured.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown when the specified address is outside the valid range for I2C addresses.
    /// </exception>
    public EnvironmentalSensorBuilder WithI2CAddress(int address)
    {
        _config["address"] = new YamlInt(address);
        return this;
    }

    /// Specifies the temperature value for the configuration.
    /// <param name="temperature">
    /// The temperature value to be set.
    /// </param>
    /// <returns>
    /// The current configuration object with the temperature set.
    /// </returns>
    public EnvironmentalSensorBuilder WithTemperature(string name, string oversampling = "16x")
    {
        _config["temperature"] = CreateSensorConfig(name, oversampling);
        return this;
    }

    /// Configures the environmental sensor to include pressure measurement in the mapping.
    /// <param name="unit">The unit of pressure measurement to be used in the mapping.</param>
    /// <returns>
    /// An updated builder instance configured to include pressure measurement.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown when the provided unit parameter is null.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Thrown if the sensor is in an invalid state for this configuration.
    /// </exception>
    public EnvironmentalSensorBuilder WithPressure(string name, string oversampling = "16x")
    {
        _config["pressure"] = CreateSensorConfig(name, oversampling);
        return this;
    }

    /// Configures the environmental sensor to include humidity measurements.
    /// <param name="humidity">
    /// The humidity value to be configured in the sensor.
    /// </param>
    /// <returns>
    /// Returns an instance of the builder with the humidity configuration applied.
    /// </returns>
    public EnvironmentalSensorBuilder WithHumidity(string name, string oversampling = "16x")
    {
        _config["humidity"] = CreateSensorConfig(name, oversampling);
        return this;
    }

    /// Configures gas resistance settings for the environmental sensor.
    /// <param name="name">The name of the gas resistance sensor.</param>
    /// <return>An instance of the <see cref="EnvironmentalSensorBuilder"/> class with updated gas resistance settings.</return>
    public EnvironmentalSensorBuilder WithGasResistance(string name)
    {
        _config["gas_resistance"] = new YamlMapping { { "name", new YamlString(name) } };
        return this;
    }

    /// Configures the update interval for the environmental sensor readings.
    /// <param name="interval">The time interval, in milliseconds, between updates.</param>
    /// <returns>
    /// The current instance, allowing for method chaining.
    /// </returns>
    public EnvironmentalSensorBuilder WithUpdateInterval(string interval)
    {
        _config["update_interval"] = new YamlString(interval);
        return this;
    }

    /// Enables or disables comments on a specified element.
    /// <param name="element">The element on which to enable or disable comments.</param>
    /// <param name="enable">A boolean value indicating whether to enable (true) or disable (false) comments.</param>
    /// <returns>
    /// A boolean value indicating whether the operation to enable or disable comments was successful.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown when the provided element is null.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Thrown when the comment feature cannot be modified for the specified element.
    /// </exception>
    public EnvironmentalSensorBuilder WithCommentOn(string key, string comment)
    {
        if (_config.TryGetValue(key, out var node))
            node.Comment = comment;

        return this;
    }

    /// <summary>
    /// Creates a configuration for a sensor with the provided name and oversampling settings.
    /// </summary>
    /// <param name="name">The name of the sensor to be configured.</param>
    /// <param name="oversampling">The oversampling setting for the sensor. If not specified, a default value is used.</param>
    /// <returns>Returns a <see cref="YamlMapping"/> containing the sensor configuration.</returns>
    private static YamlMapping CreateSensorConfig(string name, string oversampling)
    {
        var sensorConfig = new YamlMapping { { "name", new YamlString(name) } };
        if (!string.IsNullOrEmpty(oversampling))
        {
            sensorConfig["oversampling"] = new YamlString(oversampling);
        }

        return sensorConfig;
    }

    /// Builds and returns the configured YAML mapping for the environmental sensor.
    /// <returns>
    /// An instance of IYamlMapping representing the configured environmental sensor.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown when the platform is not specified, or if no measurements
    /// (temperature, pressure, humidity) are configured for the sensor.
    /// </exception>
    internal IYamlMapping Build()
    {
        if (!_config.ContainsKey("platform"))
        {
            throw new InvalidOperationException("A platform must be specified for the Bosch sensor (e.g. 'bme280').");
        }

        if (!_config.ContainsKey("temperature") && !_config.ContainsKey("pressure") && !_config.ContainsKey("humidity"))
        {
            throw new InvalidOperationException("At least one measurement value (temperature, pressure, humidity) must be configured for the sensor.");
        }

        return _config;
    }
}