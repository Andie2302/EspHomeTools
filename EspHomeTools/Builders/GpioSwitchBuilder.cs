using System;
using EspHomeTools.Classes.Scalars;
using EspHomeTools.Classes.Structures;
using EspHomeTools.Interfaces;

namespace EspHomeTools.Builders;

/// <summary>
/// A builder class for creating and configuring GPIO switch components in an ESPHome YAML configuration.
/// </summary>
public class GpioSwitchBuilder
{
    /// <summary>
    /// Represents the internal configuration mapping used within the <see cref="GpioSwitchBuilder"/> class.
    /// This variable stores configuration properties in the form of key-value pairs, allowing
    /// customization of GPIO switch settings.
    /// </summary>
    private readonly YamlMapping _config = new();

    /// <summary>
    /// A builder class for constructing a GPIO switch configuration for use in ESPHome YAML configurations.
    /// </summary>
    /// <remarks>
    /// The <c>GpioSwitchBuilder</c> provides a fluent interface for specifying various attributes of a GPIO-based switch,
    /// such as pin assignment, name, id, and icon. The builder pattern is used to allow chaining of method calls
    /// for setting configuration parameters in a user-friendly manner. After configuration is complete, an internal
    /// YAML structure is built and can be used for further processing or serialization.
    /// </remarks>
    public GpioSwitchBuilder()
    {
        _config["platform"] = new YamlString("gpio");
    }

    /// <summary>
    /// Sets a configuration value using a specified key and value, and returns the current <see cref="GpioSwitchBuilder"/> instance.
    /// </summary>
    /// <param name="key">The key for the configuration setting.</param>
    /// <param name="value">The value to assign to the specified key.</param>
    /// <returns>The current instance of <see cref="GpioSwitchBuilder"/> for method chaining.</returns>
    private GpioSwitchBuilder SetConfigValue(string key, string value)
    {
        _config[key] = new YamlString(value);
        return this;
    }

    /// Sets a configuration value in the builder.
    /// <param name="key">
    /// The configuration key to set.
    /// </param>
    /// <param name="value">
    /// The configuration value to associate with the specified key. This can be a string or a YamlSecret.
    /// </param>
    /// <return>
    /// Returns the current instance of the <see cref="GpioSwitchBuilder"/> for method chaining.
    /// </return>
    private GpioSwitchBuilder SetConfigValue(string key, YamlSecret value)
    {
        _config[key] = value;
        return this;
    }

    /// <summary>
    /// Sets a configuration value for the GPIO switch, supporting both plain and secret values.
    /// </summary>
    /// <param name="key">The configuration key to set.</param>
    /// <param name="value">The configuration value to associate with the key.</param>
    /// <param name="isSecret">Determines if the value is a secret. If true, it will be treated as a YAML secret.</param>
    /// <returns>An instance of <see cref="GpioSwitchBuilder"/> for method chaining.</returns>
    private GpioSwitchBuilder SetConfigValue(string key, string value, bool isSecret)
    {
        return isSecret ? SetConfigValue(key, new YamlSecret(value)) : SetConfigValue(key, value);
    }

    /// Sets the GPIO pin to be used for the switch configuration.
    /// <param name="pin">The pin to be set for the GPIO switch. This is represented as a string.</param>
    /// <returns>Returns the current instance of GpioSwitchBuilder for method chaining.</returns>
    public GpioSwitchBuilder UsePin(string pin) => SetConfigValue("pin", pin);
    /// <summary>
    /// Sets the pin configuration for the GPIO switch.
    /// </summary>
    /// <param name="pin">The pin value to set. This can either be a plain string or a <see cref="YamlSecret"/> to indicate a secure pin configuration.</param>
    /// <returns>An instance of <see cref="GpioSwitchBuilder"/> to allow method chaining.</returns>
    public GpioSwitchBuilder UsePin(YamlSecret pin) => SetConfigValue("pin", pin);
    /// Configures the pin to be used for the GPIO switch.
    /// <param name="pin">The name or identifier of the pin to be used.</param>
    /// <param name="isSecret">Indicates whether the pin configuration should be treated as a secret.</param>
    /// <returns>An instance of GpioSwitchBuilder for method chaining.</returns>
    public GpioSwitchBuilder UsePin(string pin, bool isSecret) => SetConfigValue("pin", pin, isSecret);

    /// Sets the name for the GPIO switch configuration.
    /// <param name="name">The name to be assigned to the GPIO switch.</param>
    /// <returns>An instance of <see cref="GpioSwitchBuilder"/> to allow for method chaining.</returns>
    public GpioSwitchBuilder WithName(string name) => SetConfigValue("name", name);
    /// Configures the name of the GPIO switch using a standard string value.
    /// <param name="name">The name to associate with the GPIO switch.</param>
    /// <return>Returns the updated instance of <see cref="GpioSwitchBuilder"/> to allow for method chaining.</return>
    public GpioSwitchBuilder WithName(YamlSecret name) => SetConfigValue("name", name);
    /// Sets the name for the GPIO switch configuration.
    /// <param name="name">The name to be assigned to the switch.</param>
    /// <param name="isSecret">
    /// A boolean value indicating whether the name is a secret.
    /// If true, the name will be stored securely as a secret.
    /// If false, it will be stored as plain text.
    /// </param>
    /// <returns>
    /// Returns the updated <see cref="GpioSwitchBuilder"/> instance to allow method chaining.
    /// </returns>
    public GpioSwitchBuilder WithName(string name, bool isSecret) => SetConfigValue("name", name, isSecret);

    /// Sets the identifier for the GPIO switch configuration.
    /// <param name="id">The identifier to assign to the GPIO switch.</param>
    /// <returns>The current instance of <see cref="GpioSwitchBuilder"/> to allow method chaining.</returns>
    public GpioSwitchBuilder WithId(string id) => SetConfigValue("id", id);
    /// <summary>
    /// Sets the ID for the GPIO switch.
    /// </summary>
    /// <param name="id">The ID as a plain string.</param>
    /// <returns>The current instance of <see cref="GpioSwitchBuilder"/> to allow method chaining.</returns>
    public GpioSwitchBuilder WithId(YamlSecret id) => SetConfigValue("id", id);
    /// <summary>
    /// Sets the "id" configuration value for the GPIO switch.
    /// </summary>
    /// <param name="id">The ID to assign to the GPIO switch.</param>
    /// <param name="isSecret">Indicates whether the ID should be treated as a secret.</param>
    /// <returns>The current instance of the <see cref="GpioSwitchBuilder"/> for method chaining.</returns>
    public GpioSwitchBuilder WithId(string id, bool isSecret) => SetConfigValue("id", id, isSecret);

    /// Configures the icon for the GPIO switch.
    /// <param name="icon">The icon string to represent the switch. It is typically a Material Design Icon (e.g., "mdi:lightbulb").</param>
    /// <returns>The current instance of GpioSwitchBuilder, enabling method chaining.</returns>
    public GpioSwitchBuilder WithIcon(string icon) => SetConfigValue("icon", icon);
    /// <summary>
    /// Sets the "icon" configuration value for the GPIO switch.
    /// </summary>
    /// <param name="icon">The name of the icon to set, as a string.</param>
    /// <returns>The current instance of <see cref="GpioSwitchBuilder"/> for method chaining.</returns>
    public GpioSwitchBuilder WithIcon(YamlSecret icon) => SetConfigValue("icon", icon);
    /// Configures the icon for the GPIO switch.
    /// <param name="icon">The icon to be used for the switch, represented as a string.</param>
    /// <returns>Returns the current instance of the GpioSwitchBuilder to allow method chaining.</returns>
    public GpioSwitchBuilder WithIcon(string icon, bool isSecret) => SetConfigValue("icon", icon, isSecret);

    /// Adds a comment to the specified configuration key in the GPIO switch builder.
    /// <param name="key">The key in the configuration to which the comment should be added.</param>
    /// <param name="comment">The comment text to associate with the specified key.</param>
    /// <returns>The current instance of <c>GpioSwitchBuilder</c> with the comment applied.</returns>
    public GpioSwitchBuilder WithCommentOn(string key, string comment)
    {
        if (_config.TryGetValue(key, out var node))
            node.Comment = comment;

        return this;
    }

    /// Builds the GPIO switch configuration and validates the required parameters.
    /// Throws an InvalidOperationException if the required "pin" or "name" parameters are not set.
    /// <returns>A validated IYamlMapping instance containing the GPIO switch configuration.</returns>
    internal IYamlMapping Build()
    {
        if (!_config.ContainsKey("pin"))
        {
            throw new InvalidOperationException("A pin must be specified for the GPIO switch using UsePin().");
        }

        if (!_config.ContainsKey("name"))
        {
            throw new InvalidOperationException("A name must be specified for the GPIO switch using WithName().");
        }

        return _config;
    }
}