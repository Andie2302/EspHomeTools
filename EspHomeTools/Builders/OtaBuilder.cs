using System;
using EspHomeTools.Classes.Scalars;
using EspHomeTools.Classes.Structures;
using EspHomeTools.Interfaces;

namespace EspHomeTools.Builders;

/// <summary>
/// Provides functionality to build and configure the OTA (Over-the-Air) update component for an ESPHome configuration.
/// </summary>
public class OtaBuilder
{
    /// <summary>
    /// Specifies the default platform for the OTA (Over-The-Air) firmware update configuration.
    /// </summary>
    /// <remarks>
    /// The DefaultPlatform constant is primarily utilized in the context of OTA configurations to
    /// set the platform default value in YAML files. It ensures that the OTA builder initializes
    /// with a default platform value "esphome" unless explicitly overridden by the user.
    /// </remarks>
    private const string DefaultPlatform = "esphome";

    /// <summary>
    /// Represents a private instance of a <see cref="YamlMapping"/> used to construct and manage YAML data mappings
    /// within the <see cref="OtaBuilder"/> class. This mapping serves as the core structure for the builder's
    /// functionality to store and manipulate configuration data.
    /// </summary>
    /// <remarks>
    /// The <c>_block</c> variable is initialized as a new <see cref="YamlMapping"/> and is used internally to
    /// hold key-value pairs that represent configuration options for the OTA (Over-The-Air) setup.
    /// It is directly modified by the builder methods such as <see cref="WithPlatform(string)"/>,
    /// <see cref="WithPassword(string)"/>, <see cref="WithComment(string, string)"/>, and related methods.
    /// This variable is ultimately returned through the <see cref="Build"/> method as an <see cref="IYamlMapping"/>
    /// for further usage.
    /// </remarks>
    private readonly YamlMapping _block = new();

    /// Provides a builder for constructing OTA (Over-The-Air) configuration blocks for ESPHome YAML.
    /// This class simplifies the process of configuring OTA-related options, such as platform, password, and comments,
    /// using fluent-style methods.
    /// The default platform is set to 'esphome' when the builder object is instantiated.
    public OtaBuilder()
    {
        SetNodeValue("platform", DefaultPlatform);
    }

    /// <summary>
    /// Sets the platform for the OTA update definition.
    /// </summary>
    /// <param name="platform">The name of the platform to use for the OTA update.</param>
    /// <returns>The current instance of <see cref="OtaBuilder"/> to allow method chaining.</returns>
    public OtaBuilder WithPlatform(string platform)
    {
        SetNodeValue("platform", platform);
        return this;
    }

    /// <summary>
    /// Sets the password for the OTA configuration.
    /// </summary>
    /// <param name="password">The password to use for secure OTA updates.</param>
    /// <returns>The current instance of <see cref="OtaBuilder"/>, allowing for method chaining.</returns>
    public OtaBuilder WithPassword(string password)
    {
        SetNodeValue("password", password);
        return this;
    }

    /// <summary>
    /// Sets the password for OTA (Over-the-Air) updates, storing it as a YAML secret.
    /// </summary>
    /// <param name="password">
    /// A <see cref="YamlSecret"/> object representing the password to be stored securely as a YAML secret.
    /// </param>
    /// <returns>
    /// Returns the current <see cref="OtaBuilder"/> instance, allowing for method chaining.
    /// </returns>
    public OtaBuilder WithPassword(YamlSecret password)
    {
        _block["password"] = password;
        return this;
    }

    /// <summary>
    /// Specifies a password for the OTA (Over-The-Air) configuration. Allows the password to be optionally treated as a YAML secret.
    /// </summary>
    /// <param name="password">The password value to set for the OTA configuration.</param>
    /// <param name="isSecret">Indicates whether the provided password should be treated as a YAML secret.</param>
    /// <returns>An updated instance of <see cref="OtaBuilder"/> with the specified password configuration.</returns>
    public OtaBuilder WithPassword(string password, bool isSecret) => isSecret ? WithPassword(new YamlSecret(password)) : WithPassword(password);

    /// <summary>
    /// Associates a comment with the specified key in the YAML block.
    /// </summary>
    /// <param name="key">The key in the YAML block for which the comment should be set.</param>
    /// <param name="comment">The comment to associate with the specified key.</param>
    /// <returns>This instance of <see cref="OtaBuilder"/> to allow method chaining.</returns>
    public OtaBuilder WithComment(string key, string comment)
    {
        if (_block.TryGetValue(key, out var node))
            node.Comment = comment;

        return this;
    }

    /// Builds and returns the configured YAML mapping structure.
    /// This method finalizes the OTA configuration by producing an
    /// instance of IYamlMapping that represents the accumulated settings.
    /// <return>
    /// An IYamlMapping instance containing the configuration data.
    /// </return>
    internal IYamlMapping Build()
    {
        return _block;
    }

    /// <summary>
    /// Sets the value of a specific YAML node identified by the given key.
    /// </summary>
    /// <param name="key">The key of the node whose value is to be set.</param>
    /// <param name="value">The value to be assigned to the specified node.</param>
    private void SetNodeValue(string key, string value)
    {
        _block[key] = new YamlString(value);
    }
}