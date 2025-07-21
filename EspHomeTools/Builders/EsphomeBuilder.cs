using System;
using EspHomeTools.Classes.Scalars;
using EspHomeTools.Classes.Structures;
using EspHomeTools.Interfaces;

namespace EspHomeTools.Builders;

/// <summary>
/// Provides a fluent builder for constructing an Esphome YAML configuration block.
/// </summary>
public class EsphomeBuilder
{
    /// <summary>
    /// Represents the constant key used to refer to the name attribute in the YAML mapping structure for the EsphomeBuilder class.
    /// </summary>
    /// <remarks>
    /// This key is utilized in methods such as WithName, WithSecretName, and WithComment to interact with the respective property
    /// in the YAML representation for ESPHome configurations. It identifies the "name" field and is essential for ensuring
    /// the proper structuring and validation of the YAML document.
    /// </remarks>
    private const string NameKey = "name";

    /// <summary>
    /// Represents the key used to configure actions triggered during the device boot process.
    /// </summary>
    /// <remarks>
    /// It is a constant string key ("on_boot") utilized within the YAML mapping to define behavior
    /// or actions that should run when the system boots.
    /// This key is referenced internally and is critical for associating actions with the
    /// "on_boot" directive in ESPHome configurations.
    /// </remarks>
    private const string OnBootKey = "on_boot";

    /// <summary>
    /// Represents a private instance of a YAML mapping structure used to build configuration blocks
    /// in the <see cref="EsphomeBuilder"/> class. This mapping is utilized to store key-value pairs
    /// that define various configuration parameters, such as name, secrets, and comments.
    /// </summary>
    /// <remarks>
    /// The <c>_block</c> variable is the central storage mechanism within the <see cref="EsphomeBuilder"/> class
    /// for accumulating configuration data. Each method in the builder class contributes to this
    /// mapping by adding or modifying specific keys and their associated values.
    /// </remarks>
    private readonly YamlMapping _block = new();

    /// Specifies the name of the device to be included in the YAML configuration.
    /// <param name="name">
    /// The name of the device as a string, which must be unique to identify the device on the network.
    /// </param>
    /// <returns>
    /// Returns the current instance of the EsphomeBuilder, allowing for method chaining.
    /// </returns>
    public EsphomeBuilder WithName(string name)
    {
        ValidateNameInput(name);
        _block[NameKey] = new YamlString(name);
        return this;
    }

    /// <summary>
    /// Sets the "name" property in the underlying YAML structure.
    /// </summary>
    /// <param name="name">The name value to be assigned, representing a standard YAML string.</param>
    /// <returns>The current instance of <see cref="EsphomeBuilder"/>, allowing method chaining.</returns>
    public EsphomeBuilder WithName(YamlSecret name)
    {
        ValidateNameInput(name);
        _block[NameKey] = name;
        return this;
    }

    /// <summary>
    /// Configures the builder to use a secret name for the device.
    /// This method wraps the provided secret name in a <see cref="YamlSecret"/> object
    /// to mark it as a sensitive value in the resulting YAML configuration.
    /// </summary>
    /// <param name="secretName">The name of the secret to use as the device name.</param>
    /// <returns>The current instance of <see cref="EsphomeBuilder"/> for method chaining.</returns>
    public EsphomeBuilder WithSecretName(string secretName)
    {
        ValidateNameInput(secretName);
        return WithName(new YamlSecret(secretName));
    }

    /// <summary>
    /// Adds a comment associated with the "name" key in the YAML configuration block.
    /// </summary>
    /// <param name="comment">The comment to be added for the "name" key.</param>
    /// <returns>The current <see cref="EsphomeBuilder"/> instance for method chaining.</returns>
    public EsphomeBuilder WithComment(string comment)
    {
        SetCommentOnKey(NameKey, comment);
        return this;
    }

    /// Adds a comment to a specific key in the configuration.
    /// <param name="key">The key for which the comment should be added. This must be a valid key present in the configuration.</param>
    /// <param name="comment">The comment text to associate with the specified key.</param>
    /// <return>Returns the current instance of <see cref="EsphomeBuilder"/> allowing for method chaining.</return>
    public EsphomeBuilder WithCommentOn(string key, string comment)
    {
        SetCommentOnKey(key, comment);
        return this;
    }

    /// Configures the actions to be executed during the boot sequence of the ESPHome device.
    /// <param name="configurator">A delegate that allows configuring the sequence of actions to be executed on boot.</param>
    /// <return>The current instance of <see cref="EsphomeBuilder"/> to support method chaining.</return>
    public EsphomeBuilder OnBoot(Action<ActionSequenceBuilder> configurator)
    {
        ValidateInput(configurator, nameof(configurator));
        var builder = new ActionSequenceBuilder();
        configurator(builder);
        _block[OnBootKey] = builder.Build();
        return this;
    }

    /// Builds and returns an object of type IYamlMapping containing the current configuration of the builder.
    /// <returns>
    /// An object of type IYamlMapping that represents the YAML mapping generated by the builder.
    /// </returns>
    internal IYamlMapping Build()
    {
        ValidateRequiredFields();
        return _block;
    }

    /// <summary>
    /// Sets a comment on a specific key in the YAML mapping.
    /// </summary>
    /// <param name="key">The key in the YAML mapping for which the comment will be set.</param>
    /// <param name="comment">The comment to associate with the specified key.</param>
    private void SetCommentOnKey(string key, string comment)
    {
        if (_block.TryGetValue(key, out var node))
        {
            node.Comment = comment;
        }
    }

    /// <summary>
    /// Validates the input provided for a name to ensure it meets the required criteria.
    /// </summary>
    /// <param name="nameInput">The input to validate, which can represent a name in various forms.</param>
    private static void ValidateNameInput(object nameInput)
    {
        ValidateInput(nameInput, nameof(nameInput));
    }

    /// <summary>
    /// Validates the provided input to ensure it is not null.
    /// </summary>
    /// <param name="input">The input object to validate.</param>
    /// <param name="parameterName">The name of the parameter being validated, used for exception messages.</param>
    /// <exception cref="ArgumentNullException">Thrown when the input object is null.</exception>
    private static void ValidateInput(object input, string parameterName)
    {
        if (input == null)
        {
            throw new ArgumentNullException(parameterName);
        }
    }

    /// <summary>
    /// Validates that all required fields in the current EsphomeBuilder instance are properly set.
    /// </summary>
    /// <exception cref="InvalidOperationException">
    /// Thrown when the required "name" field in the 'esphome' block is not set.
    /// Use the WithName() method to set the name field.
    /// </exception>
    private void ValidateRequiredFields()
    {
        if (!_block.ContainsKey(NameKey))
        {
            throw new InvalidOperationException("The name in the 'esphome' block is required. Use the WithName() method.");
        }
    }
}