using System;
using EspHomeTools.Classes.Scalars;
using EspHomeTools.Classes.Structures;
using EspHomeTools.Interfaces;

namespace EspHomeTools.Builders;

/// <summary>
/// Provides methods to build and configure a light component in ESPHome configurations.
/// </summary>
/// <remarks>
/// This class utilizes a YAML-based configuration represented by key-value mappings,
/// enabling customization of light platform settings, names, IDs, outputs, and comments.
/// It is designed to simplify the creation of well-structured light configurations.
/// </remarks>
public class LightBuilder
{
    /// Represents the configuration key used to set the platform in a YAML mapping for light configuration.
    private const string PlatformKey = "platform";

    /// A private constant string key used to represent the "name" property within the configuration mapping.
    private const string NameKey = "name";

    /// <summary>
    /// Represents the key used to associate the output configuration in the light configuration mapping.
    /// This key is utilized when specifying the output ID for the light component in a YAML configuration.
    /// </summary>
    private const string OutputKey = "output";

    /// <summary>
    /// Represents the key used to identify a specific light entity within the YAML configuration.
    /// This constant is utilized within the <see cref="LightBuilder"/> to set or retrieve the
    /// unique ID of a light entry in the generated YAML output.
    /// </summary>
    private const string IdKey = "id";

    /// Represents the default platform used in the LightBuilder configuration.
    /// It is a constant string initialized to "monochromatic" and acts as the default
    /// identifier for the platform key in the builder's YAML mapping configuration.
    private const string DefaultPlatform = "monochromatic";

    /// <summary>
    /// A constant error message indicating that a platform must be specified
    /// for the 'light' component in the configuration.
    /// </summary>
    private const string PlatformRequiredError = "A platform must be specified for the 'light' component.";

    /// <summary>
    /// Error message indicating that a name must be specified for the 'light' component
    /// when using the <c>WithName()</c> method in the LightBuilder class.
    /// </summary>
    private const string NameRequiredError = "A name must be specified for the 'light' component with WithName().";

    /// <summary>
    /// Error message indicating that an output is required for the 'light' component.
    /// This constant is used during the validation process to ensure that an output
    /// is specified prior to building or utilizing the 'light' component within the
    /// LightBuilder class.
    /// </summary>
    private const string OutputRequiredError = "An output must be specified for the 'light' component with UseOutput().";

    /// <summary>
    /// Stores the configuration settings for the <see cref="LightBuilder"/> as a mapping of YAML keys and values.
    /// </summary>
    /// <remarks>
    /// This variable is used to define the internal YAML structure for a light
    /// component in ESPHome. It is initialized with the default platform setting
    /// and can be updated as various methods of the <see cref="LightBuilder"/> are called.
    /// </remarks>
    private readonly YamlMapping _config = new();

    /// <summary>
    /// Provides functionality for building a YAML configuration for a light entity in ESPHome.
    /// </summary>
    /// <remarks>
    /// The LightBuilder class facilitates the creation and configuration of light entities
    /// by offering methods to define various properties such as platform, name, ID, output, and comments.
    /// </remarks>
    public LightBuilder()
    {
        _config[PlatformKey] = new YamlString(DefaultPlatform);
    }

    /// <summary>
    /// Sets the platform for the light configuration.
    /// </summary>
    /// <param name="platform">The name of the platform to set for the light configuration.</param>
    /// <returns>The current instance of <c>LightBuilder</c> for method chaining.</returns>
    public LightBuilder WithPlatform(string platform)
    {
        _config[PlatformKey] = new YamlString(platform);
        return this;
    }

    /// <summary>
    /// Assigns a name to the light configuration.
    /// </summary>
    /// <param name="name">The name of the light as a string.</param>
    /// <returns>An instance of <see cref="LightBuilder"/> with the name applied.</returns>
    public LightBuilder WithName(string name)
    {
        _config[NameKey] = new YamlString(name);
        return this;
    }

    /// <summary>
    /// Sets the name for the light configuration.
    /// </summary>
    /// <param name="name">
    /// The name of the light as a <see cref="YamlSecret"/>. This allows specifying a sensitive name
    /// that can be securely handled and serialized as a YAML secret.
    /// </param>
    /// <returns>
    /// Returns the current instance of <see cref="LightBuilder"/> to enable method chaining.
    /// </returns>
    public LightBuilder WithName(YamlSecret name)
    {
        _config[NameKey] = name;
        return this;
    }

    /// <summary>
    /// Sets the name for the light entity, optionally treating it as a secret.
    /// </summary>
    /// <param name="name">The name to assign to the light entity.</param>
    /// <param name="isSecret">
    /// A boolean value indicating whether the name should be treated as a secret.
    /// If true, the name is wrapped in a <c>YamlSecret</c>; otherwise, it is used as a regular string.
    /// </param>
    /// <returns>The current instance of <c>LightBuilder</c> for method chaining.</returns>
    public LightBuilder WithName(string name, bool isSecret) =>
        isSecret ? WithName(new YamlSecret(name)) : WithName(name);

    /// <summary>
    /// Sets the unique identifier for the light configuration in YAML.
    /// </summary>
    /// <param name="id">The unique identifier to assign to the light.</param>
    /// <returns>The current instance of <see cref="LightBuilder"/>, allowing for method chaining.</returns>
    public LightBuilder WithId(string id)
    {
        _config[IdKey] = new YamlString(id);
        return this;
    }

    /// <summary>
    /// Links the light to a specific output by setting the output ID in the configuration.
    /// </summary>
    /// <param name="outputId">The identifier of the output to be associated with the light.</param>
    /// <returns>The current instance of <see cref="LightBuilder"/> to allow for method chaining.</returns>
    public LightBuilder UseOutput(string outputId)
    {
        _config[OutputKey] = new YamlString(outputId);
        return this;
    }

    /// Adds a comment to the specified key in the configuration if the key exists.
    /// If the key is not present, the method does nothing.
    /// <param name="key">The key to which the comment should be added.</param>
    /// <param name="comment">The comment to associate with the given key.</param>
    /// <returns>The current instance of <see cref="LightBuilder"/> for method chaining.
    public LightBuilder WithCommentOn(string key, string comment)
    {
        if (_config.TryGetValue(key, out var node))
            node.Comment = comment;

        return this;
    }

    /// <summary>
    /// Builds and returns the configured YAML mapping for the current light configuration.
    /// </summary>
    /// <return>
    /// A fully configured instance of <see cref="IYamlMapping"/> representing the light configuration.
    /// </return>
    internal IYamlMapping Build()
    {
        ValidateRequiredFields();
        return _config;
    }

    /// <summary>
    /// Validates that all required fields, such as platform, name, and output,
    /// are present in the builder configuration. If any of the required fields
    /// are missing, an <see cref="InvalidOperationException"/> is thrown.
    /// </summary>
    /// <exception cref="InvalidOperationException">
    /// Thrown if the configuration is missing any of the required fields:
    /// "platform", "name", or "output".
    /// </exception>
    private void ValidateRequiredFields()
    {
        ValidatePlatform();
        ValidateName();
        ValidateOutput();
    }

    /// <summary>
    /// Validates whether the 'platform' field is present in the YAML configuration for the
    /// light component. If the 'platform' field is missing, throws an <see cref="InvalidOperationException"/>
    /// indicating that a platform must be specified.
    /// </summary>
    /// <exception cref="InvalidOperationException">
    /// Thrown if the 'platform' key is not found in the YAML configuration.
    /// </exception>
    /// <remarks>
    /// The platform field is a required configuration parameter for the light component,
    /// and its absence would lead to an invalid configuration.
    /// </remarks>
    private void ValidatePlatform()
    {
        if (!_config.ContainsKey(PlatformKey))
        {
            throw new InvalidOperationException(PlatformRequiredError);
        }
    }

    /// <summary>
    /// Validates that the "name" field is present in the light component configuration.
    /// Ensures that the "name" key exists in the YAML mapping and throws an exception if it is missing.
    /// </summary>
    /// <exception cref="InvalidOperationException">
    /// Thrown when the "name" field is not defined in the configuration.
    /// </exception>
    private void ValidateName()
    {
        if (!_config.ContainsKey(NameKey))
        {
            throw new InvalidOperationException(NameRequiredError);
        }
    }

    /// <summary>
    /// Validates whether the required "output" key is defined within the light component configuration.
    /// </summary>
    /// <exception cref="System.InvalidOperationException">
    /// Thrown if the "output" key is not present in the component's configuration.
    /// </exception>
    private void ValidateOutput()
    {
        if (!_config.ContainsKey(OutputKey))
        {
            throw new InvalidOperationException(OutputRequiredError);
        }
    }
}