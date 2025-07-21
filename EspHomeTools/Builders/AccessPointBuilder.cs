using System;
using EspHomeTools.Classes.Scalars;
using EspHomeTools.Classes.Structures;
using EspHomeTools.Interfaces;

namespace EspHomeTools.Builders;

/// <summary>
/// A builder for constructing and configuring an access point configuration block,
/// including WiFi SSID, password, and additional comments in a YAML mapping structure.
/// </summary>
public class AccessPointBuilder
{
    /// <summary>
    /// Represents a constant string key used within the YAML mapping for specifying the SSID
    /// of an Access Point. This key is utilized internally in conjunction with methods
    /// configuring the Access Point's SSID value in the builder class.
    /// </summary>
    private const string SsidKey = "ssid";

    /// <summary>
    /// Represents the key used to associate a password value in the YAML structure.
    /// </summary>
    /// <remarks>
    /// This constant key serves as an identifier for specifying the password in the
    /// associated mapping within the AccessPoint configuration.
    /// </remarks>
    private const string PasswordKey = "password";

    /// <summary>
    /// Represents a private YamlMapping instance that serves as the internal storage structure
    /// for key-value pairs of configuration data within the AccessPointBuilder class.
    /// </summary>
    /// <remarks>
    /// The _block variable is used to store and manage configuration details such as SSID and Password
    /// in a structured format. It supports operations such as adding, retrieving, and updating
    /// configuration entries. The variable enforces required settings, such as the SSID, to ensure
    /// proper configuration of an access point.
    /// </remarks>
    private readonly YamlMapping _block = new();

    /// Configures the SSID for the access point.
    /// <param name="ssid">The SSID value to be set for the access point.</param>
    /// <returns>The current instance of <see cref="AccessPointBuilder"/> for method chaining.</returns>
    public AccessPointBuilder WithSsid(string ssid)
    {
        SetValue(SsidKey, ssid, false);
        return this;
    }

    /// Adds the specified SSID to the access point configuration.
    /// <param name="ssid">The SSID to be added to the access point configuration as a plain string.</param>
    /// <returns>The current instance of AccessPointBuilder for method chaining.</returns>
    public AccessPointBuilder WithSsid(YamlSecret ssid)
    {
        _block[SsidKey] = ssid;
        return this;
    }

    /// <summary>
    /// Sets the SSID for the Access Point. The SSID can be passed as a plain string or a secret.
    /// </summary>
    /// <param name="ssid">The SSID of the access point as a string.</param>
    /// <param name="isSecret">A boolean indicating whether the SSID should be stored as a secret.</param>
    /// <returns>Returns the current instance of <see cref="AccessPointBuilder"/> for method chaining.</returns>
    public AccessPointBuilder WithSsid(string ssid, bool isSecret)
    {
        SetValue(SsidKey, ssid, isSecret);
        return this;
    }

    /// <summary>
    /// Sets the Wi-Fi access point password for the AccessPointBuilder.
    /// </summary>
    /// <param name="password">The password to be set for the Wi-Fi access point.</param>
    /// <returns>The current instance of <see cref="AccessPointBuilder"/>, allowing for method chaining.</returns>
    public AccessPointBuilder WithPassword(string password)
    {
        SetValue(PasswordKey, password, false);
        return this;
    }

    /// <summary>
    /// Sets the password for the access point.
    /// </summary>
    /// <param name="password">
    /// The password to be used for the access point.
    /// </param>
    /// <param name="isSecret">
    /// A boolean indicating whether the password should be treated as a secret.
    /// </param>
    /// <returns>
    /// Returns the <see cref="AccessPointBuilder"/> instance to allow for method chaining.
    /// </returns>
    public AccessPointBuilder WithPassword(string password, bool isSecret)
    {
        SetValue(PasswordKey, password, isSecret);
        return this;
    }

    /// <summary>
    /// Sets the password for the access point configuration.
    /// </summary>
    /// <param name="password">The password to set in plain text.</param>
    /// <returns>Returns an instance of <see cref="AccessPointBuilder"/> to allow method chaining.</returns>
    public AccessPointBuilder WithPassword(YamlSecret password)
    {
        _block[PasswordKey] = password;
        return this;
    }

    /// <summary>
    /// Adds a comment to the specified key in the YAML mapping.
    /// </summary>
    /// <param name="key">The key for which the comment should be added.</param>
    /// <param name="comment">The comment to associate with the specified key.</param>
    /// <returns>Returns the <see cref="AccessPointBuilder"/> instance to allow for method chaining.</returns>
    public AccessPointBuilder AddComment(string key, string comment)
    {
        if (_block.TryGetValue(key, out var node))
            node.Comment = comment;

        return this;
    }

    /// Builds the current configuration into an IYamlMapping object.
    /// The method ensures that all required fields are populated before building. Specifically,
    /// it verifies that the SSID key is present in the configuration. If the SSID key is missing,
    /// an InvalidOperationException is thrown.
    /// <returns>
    /// An IYamlMapping instance containing the configuration.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown when the SSID is not set in the Access Point configuration.
    /// </exception>
    internal IYamlMapping Build()
    {
        if (!_block.ContainsKey(SsidKey))
        {
            throw new InvalidOperationException("The SSID for the Access Point (AP) is required.");
        }

        return _block;
    }

    /// <summary>
    /// Assigns a value to a specified key within the YAML mapping, with an option to mark it as a secret.
    /// </summary>
    /// <param name="key">The key to assign the value to within the YAML mapping.</param>
    /// <param name="value">The value to assign to the specified key.</param>
    /// <param name="isSecret">Determines whether the value should be treated as a secret. If true, the value will be wrapped as a YamlSecret.</param>
    private void SetValue(string key, string value, bool isSecret) => _block[key] = isSecret ? new YamlSecret(value) : new YamlString(value);
}