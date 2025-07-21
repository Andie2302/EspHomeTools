using EspHomeTools.Classes.Scalars;
using EspHomeTools.Classes.Structures;
using EspHomeTools.Interfaces;

namespace EspHomeTools.Builders;

/// <summary>
/// The ApiBuilder class is responsible for constructing and managing the API-related YAML configurations.
/// </summary>
/// <remarks>
/// This class provides a fluent interface for configuring API settings, including passwords and encryption keys.
/// It allows adding optional comments on specific keys in the YAML structure. The configurations are stored
/// in a YAML mapping structure and can be built for further use in YAML serialization or other processes.
/// </remarks>
public class ApiBuilder
{
    /// <summary>
    /// Represents a private instance of <see cref="YamlMapping"/> used within the <see cref="ApiBuilder"/> class to store
    /// and manage configurable API-related data such as passwords, encryption keys, and other mappings.
    /// This serves as the internal state of the builder for constructing a YAML mapping structure.
    /// </summary>
    private readonly YamlMapping _block = new();

    /// <summary>
    /// Sets the password in the API configuration.
    /// </summary>
    /// <param name="password">A string representing the password to set.</param>
    /// <returns>An instance of <see cref="ApiBuilder"/> with the password added to the configuration.</returns>
    public ApiBuilder WithPassword(string password)
    {
        _block["password"] = CreateYamlNode(password, false);
        return this;
    }

    /// <summary>
    /// Sets the password for the API configuration.
    /// </summary>
    /// <param name="password">The password to associate with the API. This can either be a plain string or a reference to a YAML secret.</param>
    /// <returns>The current instance of <see cref="ApiBuilder"/> for method chaining.</returns>
    public ApiBuilder WithPassword(YamlSecret password)
    {
        _block["password"] = password;
        return this;
    }

    /// <summary>
    /// Adds a password to the API block in the YAML configuration with the specified protection level.
    /// </summary>
    /// <param name="password">
    /// The password value to be added to the API configuration.
    /// </param>
    /// <param name="isSecret">
    /// Specifies whether the password should be treated as a secret.
    /// </param>
    /// <returns>
    /// The current instance of <see cref="ApiBuilder"/> to allow method chaining.
    /// </returns>
    public ApiBuilder WithPassword(string password, bool isSecret)
    {
        _block["password"] = CreateYamlNode(password, isSecret);
        return this;
    }

    /// <summary>
    /// Configures the API encryption with a specified encryption key.
    /// </summary>
    /// <param name="key">The encryption key to secure communication.</param>
    /// <returns>The current instance of <see cref="ApiBuilder"/> for method chaining.</returns>
    public ApiBuilder WithEncryptionKey(string key)
    {
        _block["encryption"] = CreateEncryptionMapping(key, false);
        return this;
    }

    /// <summary>
    /// Configures the API with the specified encryption key.
    /// </summary>
    /// <param name="key">The encryption key to be used, represented as a <see cref="YamlSecret"/>.</param>
    /// <returns>The current instance of <see cref="ApiBuilder"/> for method chaining.</returns>
    public ApiBuilder WithEncryptionKey(YamlSecret key)
    {
        _block["encryption"] = CreateEncryptionMapping(key);
        return this;
    }

    /// <summary>
    /// Configures the encryption key for securing native API communication.
    /// </summary>
    /// <param name="key">The encryption key value to be set.</param>
    /// <param name="isSecret">Indicates whether the encryption key is defined as a secret.</param>
    /// <returns>The current instance of <see cref="ApiBuilder"/> for method chaining.</returns>
    public ApiBuilder WithEncryptionKey(string key, bool isSecret)
    {
        _block["encryption"] = CreateEncryptionMapping(key, isSecret);
        return this;
    }

    /// <summary>
    /// Adds a comment to an existing YAML node identified by the specified key.
    /// </summary>
    /// <param name="key">The key of the YAML node to which the comment will be added.</param>
    /// <param name="comment">The comment text to associate with the specified YAML node.</param>
    /// <returns>The current instance of <see cref="ApiBuilder"/> to allow method chaining.</returns>
    public ApiBuilder WithCommentOn(string key, string comment)
    {
        if (_block.TryGetValue(key, out var node))
            node.Comment = comment;

        return this;
    }

    /// Builds and returns the YAML mapping structure configured in the ApiBuilder instance.
    /// <return>
    /// An object implementing the IYamlMapping interface, representing the YAML structure
    /// configured in the builder.
    /// </return>
    internal IYamlMapping Build()
    {
        return _block;
    }

    /// <summary>
    /// Creates an instance of <see cref="IYamlNode"/> based on the provided value and whether it is marked as secret.
    /// </summary>
    /// <param name="value">The value to be represented as a YAML node.</param>
    /// <param name="isSecret">Indicates whether the value should be treated as a secret, creating a <see cref="YamlSecret"/> if true, or a <see cref="YamlString"/> if false.</param>
    /// <returns>An <see cref="IYamlNode"/> instance representing the input value.</returns>
    private static IYamlNode CreateYamlNode(string value, bool isSecret)
    {
        return isSecret ? new YamlSecret(value) : new YamlString(value);
    }

    /// <summary>
    /// Creates a <see cref="YamlMapping"/> containing an encryption key mapping.
    /// </summary>
    /// <param name="key">The encryption key to be included in the mapping.</param>
    /// <param name="isSecret">Specifies whether the key is a secret.</param>
    /// <returns>A <see cref="YamlMapping"/> that includes the encryption key, marked as secret if specified.</returns>
    private static YamlMapping CreateEncryptionMapping(string key, bool isSecret)
    {
        return new YamlMapping { { "key", CreateYamlNode(key, isSecret) } };
    }

    /// Creates an encryption mapping with the provided key, wrapped in a YamlMapping object.
    /// <param name="key">The encryption key represented as a YamlSecret.</param>
    /// <returns>A YamlMapping containing the encryption key as a key-value pair.</returns>
    private static YamlMapping CreateEncryptionMapping(YamlSecret key)
    {
        return new YamlMapping { { "key", key } };
    }
}