using System;
using EspHomeTools.Classes.Scalars;
using EspHomeTools.Classes.Structures;
using EspHomeTools.Interfaces;

namespace EspHomeTools.Builders;

/// <summary>
/// The <c>BinarySensorBuilder</c> class provides a fluent API for configuring binary sensors in ESPHome configurations.
/// </summary>
/// <remarks>
/// This builder allows the user to define various properties and behaviors for a binary sensor,
/// such as platform, pin configuration, identifiers, device class, and event handlers for
/// press and release actions. Configuration is internally mapped to YAML structure for ESPHome.
/// </remarks>
public class BinarySensorBuilder
{
    /// <summary>
    /// A constant string used as the key representing the platform
    /// in the YAML configuration for a BinarySensor.
    /// </summary>
    private const string PlatformKey = "platform";

    /// <summary>
    /// A constant string key used to reference the pin configuration in the BinarySensorBuilder class.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This key is utilized within the associated YAML mapping structure to specify a pin assignment.
    /// It plays a crucial role in the configuration of binary sensors, ensuring that a valid pin
    /// is provided through the UsePin() method.
    /// </para>
    /// <para>
    /// When a pin is not configured, an exception is thrown during validation to
    /// enforce the requirement for pin specification.
    /// </para>
    /// </remarks>
    private const string PinKey = "pin";

    /// Represents the key name used to identify a name property in configurations.
    private const string NameKey = "name";

    /// <summary>
    /// Represents the key used within the configuration mapping for referencing the ID property
    /// of the binary sensor in the ESPHome YAML structure.
    /// </summary>
    /// <remarks>
    /// This constant is utilized internally to assign or retrieve the 'id' field value
    /// when building or modifying the configuration using the <see cref="BinarySensorBuilder"/> class.
    /// </remarks>
    private const string IdKey = "id";

    /// <summary>
    /// Represents the key used to specify the device class in the binary sensor configuration.
    /// </summary>
    /// <remarks>
    /// This key is utilized within the configuration of a binary sensor to define its device class.
    /// The value associated with this key determines the type of binary sensor being configured.
    /// Common examples of device classes include "motion", "door", "window", etc., depending on the context.
    /// </remarks>
    private const string DeviceClassKey = "device_class";

    /// <summary>
    /// A constant string key representing the "on_press" action configuration in the BinarySensorBuilder.
    /// Used to configure specific actions to be executed when the associated binary sensor is pressed.
    /// </summary>
    private const string OnPressKey = "on_press";

    /// <summary>
    /// Represents the key used to configure actions triggered upon the release of a binary sensor.
    /// This key is utilized to bind and setup operations that occur when the sensor transitions from an active to an inactive state.
    /// </summary>
    private const string OnReleaseKey = "on_release";

    /// <summary>
    /// Represents the default platform used in the <see cref="BinarySensorBuilder"/> class.
    /// Provides a predefined value for the platform configuration key, ensuring a consistent
    /// and default behavior unless explicitly overridden during the configuration process.
    /// </summary>
    private const string DefaultPlatform = "gpio";

    /// <summary>
    /// Represents a private field that holds the YAML configuration for a binary sensor.
    /// </summary>
    /// <remarks>
    /// This variable is an instance of <c>YamlMapping</c> and is used to build and store the configuration
    /// for a binary sensor in the form of YAML key-value pairs. It is initialized with a default platform
    /// and can be further modified using various builder methods in the <c>BinarySensorBuilder</c> class.
    /// </remarks>
    private readonly YamlMapping _config = new();

    /// <summary>
    /// Constructs and configures a binary sensor using YAML mapping for integration
    /// with ESPHome. The builder pattern is applied to simplify the creation and
    /// configuration process of the binary sensor's YAML configuration.
    /// </summary>
    /// <remarks>
    /// By default, the platform for the binary sensor is set to "gpio". The class
    /// allows for customization of the platform, pin assignment, naming, device
    /// class, and other configuration fields. Comments can also be added to specific
    /// YAML keys to provide explanations or context within the YAML file. Additionally,
    /// custom actions can be configured for press and release events using the provided
    /// builder methods.
    /// The builder internally uses an instance of <c>YamlMapping</c> to manage key-value
    /// pairs that represent the configuration of the binary sensor.
    /// </remarks>
    public BinarySensorBuilder()
    {
        _config[PlatformKey] = new YamlString(DefaultPlatform);
    }

    /// <summary>
    /// Sets the platform key in the YAML configuration for the binary sensor.
    /// </summary>
    /// <param name="platform">The platform name to set in the binary sensor configuration.</param>
    /// <returns>The current instance of <c>BinarySensorBuilder</c> to allow method chaining.</returns>
    public BinarySensorBuilder WithPlatform(string platform)
    {
        _config[PlatformKey] = new YamlString(platform);
        return this;
    }

    /// <summary>
    /// Configures the binary sensor to use the specified GPIO pin.
    /// </summary>
    /// <param name="pin">The GPIO pin to associate with the binary sensor.</param>
    /// <returns>The instance of <see cref="BinarySensorBuilder"/> for method chaining.</returns>
    public BinarySensorBuilder UsePin(string pin)
    {
        _config[PinKey] = new YamlString(pin);
        return this;
    }

    /// <summary>
    /// Assigns the specified pin to the binary sensor configuration as a secret.
    /// </summary>
    /// <param name="pin">The pin value wrapped in a <see cref="YamlSecret"/> object to secure its content in the configuration.</param>
    /// <returns>The current instance of <see cref="BinarySensorBuilder"/>, with the pin configuration applied.</returns>
    public BinarySensorBuilder UsePin(YamlSecret pin)
    {
        _config[PinKey] = pin;
        return this;
    }

    /// <summary>
    /// Assigns a pin configuration to the binary sensor, using either a plaintext string or a YAML secret based on the provided parameters.
    /// </summary>
    /// <param name="pin">The value representing the pin to be assigned. This can be a plaintext string.</param>
    /// <param name="isSecret">A boolean indicating whether the pin should be treated as a YAML secret. If true, the pin is stored as a YamlSecret; otherwise, it remains plaintext.</param>
    /// <returns>Returns the current instance of <see cref="BinarySensorBuilder"/> for method chaining.</returns>
    public BinarySensorBuilder UsePin(string pin, bool isSecret) =>
        isSecret ? UsePin(new YamlSecret(pin)) : UsePin(pin);

    /// Sets the name of the binary sensor.
    /// <param name="name">The name to assign to the binary sensor.</param>
    /// <return>Returns the current instance of <see cref="BinarySensorBuilder"/> to allow method chaining.</return>
    public BinarySensorBuilder WithName(string name)
    {
        _config[NameKey] = new YamlString(name);
        return this;
    }

    /// <summary>
    /// Sets the name of the binary sensor.
    /// </summary>
    /// <param name="name">The name to assign to the binary sensor.</param>
    /// <returns>An instance of <see cref="BinarySensorBuilder"/> to allow method chaining.</returns>
    public BinarySensorBuilder WithName(YamlSecret name)
    {
        _config[NameKey] = name;
        return this;
    }

    /// <summary>
    /// Sets the name of the binary sensor and optionally marks it as a secret.
    /// </summary>
    /// <param name="name">The name to assign to the binary sensor.</param>
    /// <param name="isSecret">Indicates whether the name should be handled as a secret.</param>
    /// <returns>The updated instance of <see cref="BinarySensorBuilder"/>.</returns>
    public BinarySensorBuilder WithName(string name, bool isSecret) =>
        isSecret ? WithName(new YamlSecret(name)) : WithName(name);

    /// <summary>
    /// Sets the unique identifier for the binary sensor being configured.
    /// </summary>
    /// <param name="id">The identifier to assign to the binary sensor.</param>
    /// <returns>
    /// The current <c>BinarySensorBuilder</c> instance, allowing for method chaining.
    /// </returns>
    public BinarySensorBuilder WithId(string id)
    {
        _config[IdKey] = new YamlString(id);
        return this;
    }

    /// Configures the device class for a binary sensor.
    /// <param name="deviceClass">The specific class for the binary sensor (e.g., "motion", "door", etc.).</param>
    /// <return>A reference to the current <see cref="BinarySensorBuilder"/> instance for method chaining.</return>
    public BinarySensorBuilder WithDeviceClass(string deviceClass)
    {
        _config[DeviceClassKey] = new YamlString(deviceClass);
        return this;
    }

    /// Adds a comment to a specific configuration key if it exists in the configuration mapping.
    /// <param name="key">
    /// The key in the configuration mapping to which the comment should be added.
    /// </param>
    /// <param name="comment">
    /// The comment to associate with the given key.
    /// </param>
    /// <returns>
    /// Returns the current instance of <see cref="BinarySensorBuilder"/> to enable method chaining.
    /// </returns>
    public BinarySensorBuilder WithCommentOn(string key, string comment)
    {
        if (_config.TryGetValue(key, out var node))
            node.Comment = comment;

        return this;
    }

    /// Configures an action sequence to be executed when the binary sensor is pressed.
    /// <param name="configurator">
    /// A delegate that is used to configure the action sequence. The provided
    /// ActionSequenceBuilder object allows defining a series of actions to be executed
    /// in response to the press event of the binary sensor.
    /// </param>
    /// <returns>
    /// The current instance of the BinarySensorBuilder for method chaining.
    /// </returns>
    public BinarySensorBuilder OnPress(Action<ActionSequenceBuilder> configurator)
    {
        ConfigureAction(OnPressKey, configurator);
        return this;
    }

    /// <summary>
    /// Configures an action sequence to be executed when the binary sensor is released.
    /// </summary>
    /// <param name="configurator">A delegate that defines the action sequence using an <see cref="ActionSequenceBuilder"/>.</param>
    /// <returns>An instance of <see cref="BinarySensorBuilder"/>, enabling further configuration of the binary sensor.</returns>
    public BinarySensorBuilder OnRelease(Action<ActionSequenceBuilder> configurator)
    {
        ConfigureAction(OnReleaseKey, configurator);
        return this;
    }

    /// Builds and validates the binary sensor configuration and returns the corresponding YAML mapping.
    /// <returns>
    /// An IYamlMapping object representing the finalized binary sensor configuration.
    /// </returns>
    internal IYamlMapping Build()
    {
        ValidateRequiredPin();
        ValidateRequiredName();
        return _config;
    }

    /// Configures an action sequence for a specific action key.
    /// <param name="actionKey">
    /// The key representing the action to be configured, such as "on_press" or "on_release".
    /// </param>
    /// <param name="configurator">
    /// A delegate that accepts an instance of <see cref="ActionSequenceBuilder"/> to set up the desired action sequence.
    /// </param>
    private void ConfigureAction(string actionKey, Action<ActionSequenceBuilder> configurator)
    {
        var builder = new ActionSequenceBuilder();
        configurator(builder);
        _config[actionKey] = builder.Build();
    }

    /// Validates if a required pin configuration has been provided for the binary sensor.
    /// Throws an InvalidOperationException if the pin key is not present in the internal configuration.
    /// This method enforces the requirement that a pin must be set using the UsePin() method
    /// before building the binary sensor configuration.
    private void ValidateRequiredPin()
    {
        if (!_config.ContainsKey(PinKey))
        {
            throw new InvalidOperationException("A pin must be specified for the Binary Sensor with UsePin().");
        }
    }

    /// <summary>
    /// Validates whether a required name has been specified in the configuration.
    /// </summary>
    /// <exception cref="InvalidOperationException">
    /// Thrown when the name is not specified using the <c>WithName()</c> method.
    /// </exception>
    private void ValidateRequiredName()
    {
        if (!_config.ContainsKey(NameKey))
        {
            throw new InvalidOperationException("A name must be specified for the Binary Sensor using WithName().");
        }
    }
}