using System;
using EspHomeTools.Classes.Scalars;
using EspHomeTools.Classes.Structures;
using EspHomeTools.Interfaces;

namespace EspHomeTools.Builders;

/// <summary>
/// Provides a builder for configuring an I2C bus setup.
/// </summary>
/// <remarks>
/// This class enables chained configuration of various I2C bus parameters
/// in a YAML-based structure.
/// </remarks>
public class I2CBuilder
{
    /// <summary>
    /// Represents the key used to configure the SDA (Serial Data Line) pin in the I2C settings.
    /// </summary>
    /// <remarks>
    /// This constant is utilized as an identifier within the I2CBuilder class to associate a specific pin
    /// with the SDA line in an I2C communication setup. The value associated with this key defines the pin number.
    /// </remarks>
    private const string SdaKey = "sda";

    /// <summary>
    /// Represents the key used to define the SCL (clock line) pin configuration in the I2CBuilder.
    /// This key is used to assign or modify the SCL pin for the I2C communication instance.
    /// </summary>
    private const string SclKey = "scl";

    /// <summary>
    /// Represents the configuration key for enabling or disabling I2C scanning.
    /// This constant is used to identify the YAML configuration entry
    /// for the "scan" option within the I2C settings.
    /// </summary>
    private const string ScanKey = "scan";

    /// <summary>
    /// Represents the key used to identify the unique ID field in the I2C configuration.
    /// </summary>
    /// <remarks>
    /// This key is used internally to map and assign the identifier value
    /// for the I2C bus in the YAML configuration. It allows other components
    /// to reference this specific I2C bus when required.
    /// </remarks>
    private const string IdKey = "id";

    /// <summary>
    /// Represents the key used to define the frequency configuration for the I2C bus in the YAML mapping.
    /// </summary>
    private const string FrequencyKey = "frequency";

    /// <summary>
    /// Represents the private configuration section used within the <c>I2CBuilder</c> class
    /// to build and manage I2C bus configurations. This dictionary-like object stores key-value
    /// pairs that define the specific configuration options for an I2C bus.
    /// </summary>
    /// <remarks>
    /// This configuration is based on a YAML mapping structure and supports methods for adding,
    /// updating, and retrieving configuration values. It is used internally by the builder methods
    /// to construct a serialized YAML representation of the I2C configuration when the build process
    /// is finalized.
    /// </remarks>
    private readonly YamlMapping _config = new();

    /// <summary>
    /// Sets the SDA (data line) pin for the I2C configuration.
    /// </summary>
    /// <param name="pin">The GPIO pin number to configure as the SDA pin.</param>
    /// <returns>An instance of the <see cref="I2CBuilder"/> for method chaining.</returns>
    public I2CBuilder SetSdaPin(string pin)
    {
        SetConfigValue(SdaKey, new YamlString(pin));
        return this;
    }

    /// <summary>
    /// Sets the SCL (Serial Clock Line) pin for the I2C configuration.
    /// </summary>
    /// <param name="pin">The GPIO pin to use as the SCL pin.</param>
    /// <returns>An instance of the <see cref="I2CBuilder"/> for method chaining.</returns>
    public I2CBuilder SetSclPin(string pin)
    {
        SetConfigValue(SclKey, new YamlString(pin));
        return this;
    }

    /// <summary>
    /// Enables or disables the scan for I2C devices at startup.
    /// </summary>
    /// <param name="scan">Indicates whether the I2C bus should be scanned for devices on startup.</param>
    /// <returns>An instance of the <see cref="I2CBuilder"/> for method chaining.</returns>
    public I2CBuilder WithScan(bool scan)
    {
        SetConfigValue(ScanKey, new YamlBool(scan));
        return this;
    }

    /// <summary>
    /// Assigns an identifier to the I2C configuration.
    /// </summary>
    /// <param name="id">The unique identifier to assign to the I2C bus.</param>
    /// <returns>An instance of the <see cref="I2CBuilder"/> for method chaining.</returns>
    public I2CBuilder WithId(string id)
    {
        SetConfigValue(IdKey, new YamlString(id));
        return this;
    }

    /// <summary>
    /// Sets the frequency for the I2C bus configuration.
    /// </summary>
    /// <param name="frequency">The frequency value to set for the I2C bus, typically expressed in Hertz.</param>
    /// <returns>An instance of the <see cref="I2CBuilder"/> for method chaining.</returns>
    public I2CBuilder WithFrequency(string frequency)
    {
        SetConfigValue(FrequencyKey, new YamlString(frequency));
        return this;
    }

    /// <summary>
    /// Adds a comment to the specified configuration key in the I2C mapping.
    /// </summary>
    /// <param name="key">The key associated with the configuration entry.</param>
    /// <param name="comment">The comment to add to the specified configuration key.</param>
    /// <returns>An instance of the <see cref="I2CBuilder"/> for method chaining.</returns>
    public I2CBuilder WithCommentOn(string key, string comment)
    {
        if (_config.TryGetValue(key, out var node))
            node.Comment = comment;

        return this;
    }

    /// <summary>
    /// Finalizes the configuration and generates the I2C YAML mapping.
    /// </summary>
    /// <returns>An object representing the YAML mapping structure configured for the I2C bus.</returns>
    internal IYamlMapping Build()
    {
        if (!_config.ContainsKey(IdKey))
        {
            Console.WriteLine("Warning: I2C bus created without an ID. Other components might not be able to use it.");
        }

        return _config;
    }

    /// <summary>
    /// Sets a configuration value for the I2C builder using a specified key and value.
    /// </summary>
    /// <param name="key">The key representing the configuration setting to be modified.</param>
    /// <param name="value">The value to associate with the specified configuration key.</param>
    private void SetConfigValue(string key, IYamlNode value)
    {
        _config[key] = value;
    }
}