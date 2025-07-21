using System;
using EspHomeTools.Classes.Scalars;
using EspHomeTools.Classes.Structures;
using EspHomeTools.Interfaces;

namespace EspHomeTools.Builders;

/// <summary>
/// Provides functionality to build and configure an output component for ESPHome YAML configurations.
/// </summary>
public class OutputBuilder
{
    /// <summary>
    /// Represents the key used to identify the platform configuration in YAML mappings.
    /// </summary>
    /// <remarks>
    /// This constant is utilized in the context of configuring output platforms for YAML-based configurations.
    /// It is predefined with the value "platform" and acts as the identifier for specifying platform-related settings.
    /// </remarks>
    private const string PlatformKey = "platform";

    /// <summary>
    /// Represents the key used to specify the pin configuration within the output component.
    /// </summary>
    /// <remarks>
    /// This constant is utilized internally when assigning or retrieving
    /// the pin setting in the output configuration for ESPHome components.
    /// The pin is a required field and must be defined during the output setup.
    /// </remarks>
    private const string PinKey = "pin";

    /// <summary>
    /// Represents the constant string key used within the internal configuration
    /// mapping to identify the 'id' field of an output component.
    /// </summary>
    /// <remarks>
    /// This key is utilized to set or retrieve the unique identifier for the specific
    /// output component within the configuration being built.
    /// </remarks>
    private const string IdKey = "id";

    /// <summary>
    /// Represents the default platform value used for output creation
    /// within the <c>OutputBuilder</c> class. This constant determines
    /// the platform type when none is explicitly specified.
    /// </summary>
    private const string DefaultPlatform = "gpio";

    /// <summary>
    /// Represents the internal configuration storage for the <see cref="OutputBuilder"/> class.
    /// This variable serves as a mapping for YAML key-value pairs, allowing the construction
    /// of structured output configurations. It is backed by a <see cref="YamlMapping"/> instance
    /// and is primarily used to define various parameters such as platform type, output ID, and pin settings.
    /// </summary>
    /// <remarks>
    /// The <c>_config</c> field is initialized with a default platform key-value pair, where the platform
    /// is set to "gpio". Additional parameters are added or modified through builder methods such as
    /// <see cref="OutputBuilder.WithPlatform(string)"/>, <see cref="OutputBuilder.WithId(string)"/> and others.
    /// </remarks>
    private readonly YamlMapping _config = new();

    /// <summary>
    /// Provides a convenient way to build and configure YAML mappings for outputs in ESPHome configurations.
    /// </summary>
    /// <remarks>
    /// The OutputBuilder class allows for constructing output configurations by setting platform, ID,
    /// pin values, and adding comments to specific keys. It utilizes a YAML mapping to store and organize
    /// configuration data. The default platform is set to "gpio".
    /// </remarks>
    public OutputBuilder()
    {
        _config[PlatformKey] = new YamlString(DefaultPlatform);
    }

    /// <summary>
    /// Sets the platform for the output configuration.
    /// </summary>
    /// <param name="platform">
    /// The platform name to be set for the output configuration. This value determines
    /// the type of output, for example, "ledc" for PWM control.
    /// </param>
    /// <returns>
    /// Returns the current instance of <see cref="OutputBuilder"/> to allow method chaining.
    /// </returns>
    public OutputBuilder WithPlatform(string platform)
    {
        _config[PlatformKey] = new YamlString(platform);
        return this;
    }

    /// <summary>
    /// Sets the identifier for the output configuration.
    /// </summary>
    /// <param name="id">The identifier to associate with the output configuration.</param>
    /// <returns>An instance of <see cref="OutputBuilder"/> to allow for method chaining.</returns>
    public OutputBuilder WithId(string id)
    {
        _config[IdKey] = new YamlString(id);
        return this;
    }

    /// <summary>
    /// Configures the pin to be used for the output.
    /// </summary>
    /// <param name="pin">The name of the pin to be used.</param>
    /// <returns>An instance of <see cref="OutputBuilder"/> for chaining further configurations.</returns>
    public OutputBuilder UsePin(string pin)
    {
        SetPinValue(new YamlString(pin));
        return this;
    }

    /// <summary>
    /// Configures the pin to be used in the output definition.
    /// </summary>
    /// <param name="pin">The identifier of the pin to use.</param>
    /// <returns>An instance of <see cref="OutputBuilder"/> for chaining method calls.</returns>
    public OutputBuilder UsePin(YamlSecret pin)
    {
        SetPinValue(pin);
        return this;
    }

    /// <summary>
    /// Specifies a pin to use for the output configuration.
    /// </summary>
    /// <param name="pin">The pin identifier to use. This can be a string representing the pin.</param>
    /// <param name="isSecret">Indicates whether the provided pin identifier is a secret.</param>
    /// <returns>The current instance of <see cref="OutputBuilder"/> to allow method chaining.</returns>
    public OutputBuilder UsePin(string pin, bool isSecret)
    {
        return isSecret ? UsePin(new YamlSecret(pin)) : UsePin(pin);
    }

    /// <summary>
    /// Attaches a comment to a specified key in the YAML configuration.
    /// </summary>
    /// <param name="key">The key in the YAML configuration to which the comment should be added.</param>
    /// <param name="comment">The comment text to be attached to the specified key.</param>
    /// <returns>
    /// Returns the current instance of <see cref="OutputBuilder"/> for method chaining.
    /// </returns>
    public OutputBuilder WithCommentOn(string key, string comment)
    {
        if (_config.TryGetValue(key, out var node))
            node.Comment = comment;
        return this;
    }

    /// <summary>
    /// Constructs and returns the final YAML mapping representation.
    /// This method validates necessary fields before completing the build process.
    /// </summary>
    /// <returns>
    /// An instance of <see cref="IYamlMapping"/> representing the constructed YAML configuration.
    /// </returns>
    internal IYamlMapping Build()
    {
        ValidateRequiredFields();
        return _config;
    }

    /// <summary>
    /// Sets the value of the pin in the configuration.
    /// </summary>
    /// <param name="pinValue">The YAML node representing the pin value to be set.</param>
    private void SetPinValue(IYamlNode pinValue)
    {
        _config[PinKey] = pinValue;
    }

    /// <summary>
    /// Validates that all required fields are present in the configuration
    /// before building the output component.
    /// Ensures that a pin and an ID have been specified for the component.
    /// </summary>
    /// <exception cref="InvalidOperationException">
    /// Thrown if the configuration does not contain a value for the necessary keys:
    /// - "pin": Indicates that a specific pin must be provided using the UsePin() method.
    /// - "id": Indicates that a unique identifier must be set using the WithId() method.
    /// </exception>
    private void ValidateRequiredFields()
    {
        if (!_config.ContainsKey(PinKey))
        {
            throw new InvalidOperationException("A pin must be specified for the 'output' component using UsePin().");
        }

        if (!_config.ContainsKey(IdKey))
        {
            throw new InvalidOperationException("An ID must be specified for the 'output' component using WithId() so other components can reference it.");
        }
    }
}