using System;
using EspHomeTools.Classes.Scalars;
using EspHomeTools.Classes.Structures;
using EspHomeTools.Interfaces;

namespace EspHomeTools.Builders;

/// <summary>
/// A builder class for constructing 'wifi' configuration blocks for YAML-based configurations.
/// </summary>
/// <remarks>
/// The <c>WifiBlockBuilder</c> provides methods to configure various properties of a Wi-Fi block,
/// such as SSID, password, and access point configurations. This class ensures that the generated
/// Wi-Fi configuration is valid by requiring essential fields such as SSID and password.
/// </remarks>
public class WifiBlockBuilder
{
    /// <summary>
    /// Represents the key used to identify the SSID field in a Wi-Fi configuration block.
    /// </summary>
    /// <remarks>
    /// The <c>SsidKey</c> is a constant string set to "ssid" and is used as a lookup key
    /// within the internal YAML structure for managing Wi-Fi settings.
    /// This key is required when defining the SSID in a Wi-Fi configuration block.
    /// </remarks>
    private const string SsidKey = "ssid";

    /// <summary>
    /// Represents the key associated with the Wi-Fi password in the YAML mapping for Wi-Fi configuration.
    /// This key is used to identify and store password data within the configuration block.
    /// </summary>
    private const string PasswordKey = "password";

    /// <summary>
    /// Represents the key used to identify the Access Point block within a YAML configuration.
    /// This key is utilized in the <see cref="WifiBlockBuilder"/> to configure and represent
    /// an access point by associating it with relevant configuration details.
    /// </summary>
    private const string AccessPointKey = "ap";

    /// <summary>
    /// Represents an internal YAML mapping structure used for constructing
    /// Wi-Fi configuration blocks in the WifiBlockBuilder class.
    /// </summary>
    /// <remarks>
    /// This variable stores key-value pairs for YAML nodes, including SSID, password,
    /// and access point configuration, to define network settings for a device.
    /// </remarks>
    private readonly YamlMapping _block = new();
    /// Configures the SSID (WiFi network name) for the WiFi block.
    /// <param name="ssid">The SSID of the WiFi network.</param>
    /// <returns>The current instance of <see cref="WifiBlockBuilder"/> for method chaining.</returns>
    public WifiBlockBuilder WithSsid(string ssid)
    {
        SetValue(SsidKey, ssid, false);
        return this;
    }
    /// Adds the SSID to the Wi-Fi configuration block using a plain text string.
    /// <param name="ssid">The SSID as a plain text string to be set in the configuration.</param>
    /// <returns>The current instance of WifiBlockBuilder with the SSID set.</returns>
    public WifiBlockBuilder WithSsid(YamlSecret ssid)
    {
        _block[SsidKey] = ssid;
        return this;
    }
    /// <summary>
    /// Configures the Wi-Fi block with the specified SSID value.
    /// </summary>
    /// <param name="ssid">The SSID value to assign.</param>
    /// <returns>The current instance of <see cref="WifiBlockBuilder"/> for method chaining.</returns>
    public WifiBlockBuilder WithSsid(string ssid, bool isSecret)
    {
        SetValue(SsidKey, ssid, isSecret);
        return this;
    }
    /// Configures the Wi-Fi block with the specified password.
    /// <param name="password">The password to be used for the Wi-Fi connection.</param>
    /// <returns>The <see cref="WifiBlockBuilder"/> instance with the password configured.</returns>
    public WifiBlockBuilder WithPassword(string password)
    {
        SetValue(PasswordKey, password, false);
        return this;
    }
    /// <summary>
    /// Adds a password to the WiFi block configuration using a secure YAML secret.
    /// </summary>
    /// <param name="password">The password represented as a <see cref="YamlSecret"/> object, which ensures secure storage of sensitive data.</param>
    /// <returns>The current instance of <see cref="WifiBlockBuilder"/>, allowing for method chaining.</returns>
    public WifiBlockBuilder WithPassword(YamlSecret password)
    {
        _block[PasswordKey] = password;
        return this;
    }
    /// <summary>
    /// Sets the Wi-Fi password, optionally marking it as a secret.
    /// </summary>
    /// <param name="password">The Wi-Fi password to set.</param>
    /// <param name="isSecret">A boolean value indicating whether the password should be stored as a secret.</param>
    /// <returns>The current instance of <see cref="WifiBlockBuilder"/> to allow method chaining.</returns>
    public WifiBlockBuilder WithPassword(string password, bool isSecret)
    {
        SetValue(PasswordKey, password, isSecret);
        return this;
    }
    /// <summary>
    /// Configures the WiFi block with an access point by using the specified configurator.
    /// </summary>
    /// <param name="configurator">
    /// An action that accepts an instance of <see cref="AccessPointBuilder"/> to configure the access point settings.
    /// </param>
    /// <returns>
    /// Returns the current instance of <see cref="WifiBlockBuilder"/> for method chaining.
    /// </returns>
    public WifiBlockBuilder WithAccessPoint(Action<AccessPointBuilder> configurator)
    {
        var builder = new AccessPointBuilder();
        configurator(builder);
        _block[AccessPointKey] = builder.Build();
        return this;
    }
    /// <summary>
    /// Adds a comment to the specified key in the YAML block if it exists.
    /// </summary>
    /// <param name="key">The key within the YAML block to which the comment should be added.</param>
    /// <param name="comment">The comment to be associated with the specified key.</param>
    /// <returns>Returns the current instance of <see cref="WifiBlockBuilder"/> for method chaining.</returns>
    public WifiBlockBuilder AddComment(string key, string comment)
    {
        if (_block.TryGetValue(key, out var node))
            node.Comment = comment;

        return this;
    }
    /// Builds and returns the configured WiFi block represented as an IYamlMapping.
    /// Throws InvalidOperationException if the required SSID or password is not provided.
    /// <returns>
    /// An instance of IYamlMapping representing the constructed WiFi block.
    /// </returns>
    internal IYamlMapping Build()
    {
        if (!_block.ContainsKey(SsidKey) || !_block.ContainsKey(PasswordKey))
        {
            throw new InvalidOperationException("SSID and password are required in the 'wifi' block.");
        }

        return _block;
    }
    /// <summary>
    /// Sets a value in the YAML mapping with the specified key, differentiating between plain strings and secrets.
    /// </summary>
    /// <param name="key">The key under which the value will be stored in the YAML mapping.</param>
    /// <param name="value">The value to be stored. This can be a plain string or a secret based on the isSecret flag.</param>
    /// <param name="isSecret">Indicates whether the value is a secret. If true, the value is stored as a YamlSecret; otherwise, as a YamlString.</param>
    private void SetValue(string key, string value, bool isSecret) => _block[key] = isSecret ? new YamlSecret(value) : new YamlString(value);
}