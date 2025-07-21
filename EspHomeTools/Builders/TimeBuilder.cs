using System;
using EspHomeTools.Classes.Scalars;
using EspHomeTools.Classes.Structures;
using EspHomeTools.Interfaces;

namespace EspHomeTools.Builders;

/// <summary>
/// Provides a fluent API for building and configuring a 'time' component in YAML configurations.
/// </summary>
/// <remarks>
/// The TimeBuilder class is designed to simplify the creation and configuration of YAML mappings
/// specific to the 'time' component in ESPHome configurations. It supports the specification of
/// required and optional parameters for the time component, such as platform, ID, timezone, and servers.
/// </remarks>
public class TimeBuilder
{
    /// <summary>
    /// Represents the key used to identify the platform configuration in a YAML mapping for the time component.
    /// </summary>
    /// <remarks>
    /// This constant is utilized as a key in the YAML mapping to specify the platform type (e.g., "homeassistant" or "sntp")
    /// when configuring the time component. Its value is "platform".
    /// </remarks>
    private const string PlatformKey = "platform";

    /// <summary>
    /// Represents the constant key used to identify the "id" field in YAML configurations.
    /// </summary>
    /// <remarks>
    /// This key is utilized in association with the configuration property keys within the TimeBuilder
    /// to set or retrieve the identifier ("id") value for a specific YAML mapping.
    /// </remarks>
    private const string IdKey = "id";

    /// <summary>
    /// Represents the configuration key for specifying the timezone in a YAML mapping.
    /// </summary>
    /// <remarks>
    /// This constant is used as the key for storing timezone settings in a <see cref="YamlMapping"/> object.
    /// It is commonly assigned with a string value or a <see cref="YamlSecret"/> to represent the timezone.
    /// </remarks>
    private const string TimezoneKey = "timezone";

    /// <summary>
    /// Represents the configuration key used to specify server details in the YAML mappings.
    /// </summary>
    /// <remarks>
    /// This key is utilized to include and manage server-specific configuration
    /// as part of the YAML structure. It is used in operations such as adding server
    /// details to the builder for constructing configurations.
    /// </remarks>
    private const string ServersKey = "servers";

    /// <summary>
    /// Represents the internal YAML mapping configuration used by the <see cref="TimeBuilder"/> class
    /// to store and manipulate keys and values for constructing YAML-based configurations.
    /// </summary>
    /// <remarks>
    /// This variable serves as a core component for dynamically building YAML mappings
    /// in a structured format. The collected configuration details are further utilized
    /// to generate valid YAML outputs for various constructs such as `platform`, `id`,
    /// `timezone`, and `servers`.
    /// </remarks>
    private readonly YamlMapping _config = new();

    /// <summary>
    /// Sets the platform for the time configuration in the YAML mapping.
    /// </summary>
    /// <param name="platform">The platform to be used for the time configuration (e.g., "homeassistant").</param>
    /// <returns>An instance of <c>TimeBuilder</c> to allow for method chaining.</returns>
    public TimeBuilder WithPlatform(string platform)
    {
        _config[PlatformKey] = new YamlString(platform);
        return this;
    }

    /// <summary>
    /// Sets the ID for the time configuration.
    /// </summary>
    /// <param name="id">The unique identifier for the time configuration.</param>
    /// <returns>The current instance of <c>TimeBuilder</c> for method chaining.</returns>
    public TimeBuilder WithId(string id)
    {
        _config[IdKey] = new YamlString(id);
        return this;
    }

    /// <summary>
    /// Sets the timezone value for the current <see cref="TimeBuilder"/> instance.
    /// </summary>
    /// <param name="timezone">The timezone string to be set, typically in a format such as <c>"UTC+2"</c> or <c>"America/New_York"</c>.</param>
    /// <returns>The current instance of the <see cref="TimeBuilder"/> class for method chaining.</returns>
    public TimeBuilder WithTimezone(string timezone)
    {
        _config[TimezoneKey] = new YamlString(timezone);
        return this;
    }

    /// <summary>
    /// Sets the timezone for this TimeBuilder instance.
    /// </summary>
    /// <param name="timezone">
    /// The timezone to be set. This can either be a plain string or a wrapped secret
    /// using the YamlSecret class, enabling secure handling of sensitive information.
    /// </param>
    /// <returns>
    /// The current instance of the TimeBuilder class for method chaining.
    /// </returns>
    public TimeBuilder WithTimezone(YamlSecret timezone)
    {
        _config[TimezoneKey] = timezone;
        return this;
    }

    /// <summary>
    /// Configures the timezone for the TimeBuilder instance.
    /// </summary>
    /// <param name="timezone">
    /// The timezone string to set. This could be a standard timezone identifier, such as "UTC" or "America/New_York".
    /// </param>
    /// <param name="isSecret">
    /// Indicates whether the timezone should be treated as a secret. If true, the timezone will be wrapped in a <see cref="YamlSecret"/> object for secure handling.
    /// </param>
    /// <returns>
    /// The <see cref="TimeBuilder"/> instance with the configured timezone, allowing for method chaining.
    /// </returns>
    public TimeBuilder WithTimezone(string timezone, bool isSecret) =>
        isSecret ? WithTimezone(new YamlSecret(timezone)) : WithTimezone(timezone);

    /// <summary>
    /// Configures the builder with a list of server addresses.
    /// </summary>
    /// <param name="servers">An array of server addresses to configure.</param>
    /// <returns>Returns the current instance of the <see cref="TimeBuilder"/> for method chaining.</returns>
    public TimeBuilder WithServers(params string[] servers)
    {
        _config[ServersKey] = CreateServerSequence(servers);
        return this;
    }

    /// <summary>
    /// Adds a comment to the specified configuration key if it exists in the current configuration.
    /// </summary>
    /// <param name="key">The key in the configuration to associate the comment with.</param>
    /// <param name="comment">The comment to attach to the specified key.</param>
    /// <returns>The current instance of <see cref="TimeBuilder"/> to allow for method chaining.</returns>
    public TimeBuilder WithComment(string key, string comment)
    {
        if (_config.TryGetValue(key, out var node))
            node.Comment = comment;

        return this;
    }

    /// <summary>
    /// Creates a YAML sequence from the provided array of server strings.
    /// </summary>
    /// <param name="servers">An array of strings representing server addresses to be included in the YAML sequence.</param>
    /// <returns>An instance of <see cref="YamlSequence"/> containing the provided server addresses.</returns>
    private static YamlSequence CreateServerSequence(string[] servers)
    {
        var serverSequence = new YamlSequence();
        foreach (var server in servers)
        {
            serverSequence.Add(new YamlString(server));
        }

        return serverSequence;
    }

    /// Builds and returns the finalized configuration represented as an IYamlMapping object.
    /// This method ensures that all required configurations are set before the build process completes.
    /// If the mandatory 'platform' key is not provided, an exception will be thrown to signal the incomplete configuration.
    /// <returns>
    /// An instance of IYamlMapping representing the built configuration.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown when the required 'platform' is not set in the configuration.
    /// </exception>
    internal IYamlMapping Build()
    {
        if (!_config.ContainsKey(PlatformKey))
        {
            throw new InvalidOperationException("A platform must be specified for the 'time' component using WithPlatform() (e.g. 'homeassistant' or 'sntp').");
        }

        return _config;
    }
}