using System;
using EspHomeTools.Classes.Scalars;
using EspHomeTools.Classes.Structures;
using EspHomeTools.Interfaces;

namespace EspHomeTools.Builders;

/// <summary>
/// A builder class used for constructing an MQTT configuration block for ESPHome devices.
/// </summary>
/// <remarks>
/// The <c>MqttBuilder</c> class provides methods to configure various MQTT connection properties,
/// including broker, port, username, password, and client ID. It also supports adding comments
/// to specific configuration keys and ensures that a broker is specified before building the configuration.
/// </remarks>
/// <example>
/// This class allows fluent configuration of MQTT-related settings to generate a valid YAML mapping
/// for use in ESPHome devices.
/// </example>
public class MqttBuilder
{
    /// <summary>
    /// Represents an internal instance of the YamlMapping used to store key-value pairs
    /// relevant to the MQTT configuration being built.
    /// </summary>
    /// <remarks>
    /// This field is utilized internally within the <see cref="MqttBuilder"/> class to
    /// accumulate configuration data for an MQTT setup. Each entry corresponds to a specific
    /// setting, such as broker, port, username, and more.
    /// The field holds a mapping of keys to values, where both the keys and values are part
    /// of the YAML serialization structure.
    /// It is not exposed outside the <see cref="MqttBuilder"/> class. Configuration settings
    /// are added through the public methods provided by <see cref="MqttBuilder"/>.
    /// </remarks>
    private readonly YamlMapping _block = new();

    /// Configures the MQTT broker address.
    /// <param name="broker">
    /// The address of the MQTT broker as a string. Typically, this will be an IP address or domain name.
    /// </param>
    /// <returns>
    /// Returns the current instance of the <see cref="MqttBuilder"/> allowing for method chaining.
    /// </returns>
    public MqttBuilder WithBroker(string broker) => AddValueToBlock("broker", broker, false);
    /// <summary>
    /// Sets the broker configuration for the MQTT connection.
    /// </summary>
    /// <param name="broker">The broker address to be set.</param>
    /// <returns>The current instance of <see cref="MqttBuilder"/> for method chaining.</returns>
    public MqttBuilder WithBroker(YamlSecret broker) => AddValueToBlock("broker", broker);
    /// Configures the MQTT broker for the builder.
    /// Allows specifying the broker's address or hostname in various formats.
    /// Overloads:
    /// 1. Accepts a plain string representing the broker address.
    /// 2. Accepts a `YamlSecret` instance to secure the broker address.
    /// 3. Accepts a string with an additional boolean indicating whether the broker's address should be treated as a secret.
    /// Parameters:
    /// - broker: The address or hostname of the MQTT broker.
    /// - isSecret: A flag indicating if the broker should be secured as a secret (optional, applicable to the third overload).
    /// Returns:
    /// - An instance of `MqttBuilder` configured with the provided broker settings, allowing further chaining of methods.
    public MqttBuilder WithBroker(string broker, bool isSecret) => AddValueToBlock("broker", broker, isSecret);

    /// <summary>
    /// Sets the port value for the MQTT configuration.
    /// </summary>
    /// <param name="port">The port number to be used for the MQTT connection.</param>
    /// <return>Returns the current instance of <see cref="MqttBuilder"/> to support method chaining.</return>
    public MqttBuilder WithPort(int port)
    {
        _block["port"] = new YamlInt(port);
        return this;
    }

    /// Adds a username to the MQTT configuration.
    /// The username can be specified as a string or marked as a secret based on the overload.
    /// <param name="username">The username to be added to the MQTT configuration. It can be a plain string.</param>
    /// <returns>The current instance of <c>MqttBuilder</c> for method chaining.</returns>
    public MqttBuilder WithUsername(string username) => AddValueToBlock("username", username, false);
    /// <summary>
    /// Sets the username for the MQTT connection.
    /// </summary>
    /// <param name="username">The username as a plain text string.</param>
    /// <returns>An instance of <see cref="MqttBuilder"/> to allow chained calls.</returns>
    public MqttBuilder WithUsername(YamlSecret username) => AddValueToBlock("username", username);
    /// <summary>
    /// Sets the username for the MQTT configuration.
    /// </summary>
    /// <param name="username">The username to use for the MQTT connection.</param>
    /// <param name="isSecret">A boolean indicating whether the username should be treated as a secret.</param>
    /// <returns>Returns the current instance of <see cref="MqttBuilder"/> to allow for method chaining.</returns>
    public MqttBuilder WithUsername(string username, bool isSecret) => AddValueToBlock("username", username, isSecret);

    /// Configures the MQTT password for the connection.
    /// <param name="password">The password to use for MQTT authentication.</param>
    /// <returns>The updated instance of <see cref="MqttBuilder"/>.</returns>
    public MqttBuilder WithPassword(string password) => AddValueToBlock("password", password, false);
    /// Configures the password for the MQTT connection.
    /// <param name="password">The password value for the MQTT connection. Can either be a plain string or a YamlSecret for sensitive configurations.</param>
    /// <returns>Returns the instance of the MqttBuilder to allow method chaining.</returns>
    public MqttBuilder WithPassword(YamlSecret password) => AddValueToBlock("password", password);
    /// Configures the MQTT password to be used for authentication.
    /// <param name="password">The password string for MQTT authentication. This can either be plain text or a secret value.</param>
    /// <param name="isSecret">A boolean indicating whether the password should be treated as a secret.</param>
    /// <returns>An instance of MqttBuilder for method chaining.</returns>
    public MqttBuilder WithPassword(string password, bool isSecret) => AddValueToBlock("password", password, isSecret);

    /// <summary>
    /// Sets the MQTT client ID for the builder.
    /// </summary>
    /// <param name="clientId">The client ID to be set for the MQTT connection.</param>
    /// <returns>The current instance of <see cref="MqttBuilder"/> to allow method chaining.</returns>
    public MqttBuilder WithClientId(string clientId)
    {
        _block["client_id"] = new YamlString(clientId);
        return this;
    }

    /// Adds a comment to a specified key in the MQTT configuration block.
    /// <param name="key">The key for which the comment will be added.</param>
    /// <param name="comment">The comment to associate with the specified key.</param>
    /// <return>Returns the updated instance of <see cref="MqttBuilder"/>.</return>
    public MqttBuilder WithCommentOn(string key, string comment)
    {
        if (_block.TryGetValue(key, out var node))
            node.Comment = comment;

        return this;
    }

    /// Builds and returns the configuration represented by the MqttBuilder as an IYamlMapping object.
    /// <returns>Returns an object implementing IYamlMapping that represents the MQTT configuration.</returns>
    /// <exception cref="InvalidOperationException">Thrown if a broker is not specified before building the MQTT block.</exception>
    internal IYamlMapping Build()
    {
        if (!_block.ContainsKey("broker"))
        {
            throw new InvalidOperationException("A broker must be specified for the 'mqtt' block using WithBroker().");
        }

        return _block;
    }

    /// Adds a key-value pair to the internal YAML mapping block.
    /// The key represents the property, while the value can be a plain string
    /// or marked as a secret based on the isSecret parameter.
    /// <param name="key">The key to be added to the YAML mapping block.</param>
    /// <param name="value">The value associated with the key, which can either be a plain string or wrapped as a secret.</param>
    /// <param name="isSecret">Determines if the value is treated as a secret. A true value wraps the value in a YamlSecret; otherwise, a YamlString is used.</param>
    /// <returns>Returns the MqttBuilder object for method chaining.</returns>
    private MqttBuilder AddValueToBlock(string key, string value, bool isSecret)
    {
        _block[key] = isSecret ? new YamlSecret(value) : new YamlString(value);
        return this;
    }

    /// <summary>
    /// Adds a value to the internal YAML block with the specified key and secret.
    /// </summary>
    /// <param name="key">The key under which the value will be stored in the YAML block.</param>
    /// <param name="secret">The YamlSecret instance containing the secret value to be added.</param>
    /// <returns>
    /// Returns the current instance of <see cref="MqttBuilder"/> to allow method chaining.
    /// </returns>
    private MqttBuilder AddValueToBlock(string key, YamlSecret secret)
    {
        _block[key] = secret;
        return this;
    }
}