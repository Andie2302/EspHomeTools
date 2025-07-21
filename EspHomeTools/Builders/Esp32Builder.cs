using System;
using EspHomeTools.Classes.Scalars;
using EspHomeTools.Classes.Structures;
using EspHomeTools.Interfaces;

namespace EspHomeTools.Builders;

/// <summary>
/// A builder class specifically designed for creating and configuring ESP32 YAML blocks in ESPHome configurations.
/// </summary>
/// <remarks>
/// This builder simplifies the creation of ESP32 configurations by providing methods for setting required properties,
/// such as the board and framework, as well as for adding optional comments to specific keys.
/// The class enforces that a board must be specified before the configuration can be built.
/// </remarks>
public class Esp32Builder
{
    /// <summary>
    /// Represents an instance of the <see cref="YamlMapping"/> used to store and manage
    /// key-value pairs for building the configuration of an Esp32 device.
    /// </summary>
    /// <remarks>
    /// The <c>_block</c> variable is internally used by the <see cref="Esp32Builder"/> class
    /// to construct the YAML mapping structure for the EspHome configuration.
    /// It is initialized as an empty instance of <see cref="YamlMapping"/> and allows
    /// setting and retrieving values associated with specific keys.
    /// This variable also supports adding comments to specific nodes and validating
    /// the presence of required keys such as "board" before finalizing the configuration.
    /// </remarks>
    /// <seealso cref="YamlMapping"/>
    private readonly YamlMapping _block = new();

    /// <summary>
    /// Specifies the board to be used in the configuration.
    /// </summary>
    /// <param name="board">The name of the board to configure.</param>
    /// <returns>The current instance of <see cref="Esp32Builder"/>, allowing for method chaining.</returns>
    public Esp32Builder WithBoard(string board)
    {
        SetValue("board", board);
        return this;
    }

    /// <summary>
    /// Sets the board name or configuration for the current ESP32 build process.
    /// </summary>
    /// <param name="board">The name or identifier of the board configuration.</param>
    /// <returns>An instance of <see cref="Esp32Builder"/> to allow method chaining.</returns>
    public Esp32Builder WithBoard(YamlSecret board)
    {
        SetValue("board", board);
        return this;
    }

    /// Configures the board for the ESP32 build process.
    /// This method allows setting the board for the ESP32 configuration.
    /// The board name can be specified directly as a string, as a YamlSecret object,
    /// or as a string with an option to specify whether it is a secret.
    /// <param name="board">The name of the board to configure.</param>
    /// <param name="isSecret">Indicates whether the board name should be treated as a secret. If true, it will use a YamlSecret object; otherwise, it will use the plain string value.</param>
    /// <return>Returns the modified Esp32Builder instance with the specified board configuration.</return>
    public Esp32Builder WithBoard(string board, bool isSecret) =>
        isSecret ? WithBoard(new YamlSecret(board)) : WithBoard(board);

    /// <summary>
    /// Sets the framework for the ESP32 board.
    /// </summary>
    /// <param name="framework">The framework to be set for the ESP32 board.</param>
    /// <returns>An instance of <see cref="Esp32Builder"/> for method chaining.</returns>
    public Esp32Builder WithFramework(string framework)
    {
        SetValue("framework", framework);
        return this;
    }

    /// <summary>
    /// Sets the framework for the ESP32 device being configured.
    /// </summary>
    /// <param name="framework">The framework name as a YamlSecret.</param>
    /// <returns>The current instance of <see cref="Esp32Builder"/> to allow method chaining.</returns>
    public Esp32Builder WithFramework(YamlSecret framework)
    {
        SetValue("framework", framework);
        return this;
    }

    /// <summary>
    /// Sets the framework for the ESP32 configuration.
    /// </summary>
    /// <param name="framework">The framework name to set.</param>
    /// <param name="isSecret">Indicates whether the framework value should be treated as a secret.</param>
    /// <returns>An instance of <see cref="Esp32Builder"/> to allow method chaining.</returns>
    public Esp32Builder WithFramework(string framework, bool isSecret) =>
        isSecret ? WithFramework(new YamlSecret(framework)) : WithFramework(framework);

    /// <summary>
    /// Adds a comment to a specific key in the YAML mapping if the key exists.
    /// </summary>
    /// <param name="key">The key for which the comment should be added.</param>
    /// <param name="comment">The comment to be added to the specified key.</param>
    /// <returns>The current instance of <see cref="Esp32Builder"/> to allow method chaining.</returns>
    public Esp32Builder WithCommentOn(string key, string comment)
    {
        if (_block.TryGetValue(key, out var node))
            node.Comment = comment;

        return this;
    }

    /// Builds the ESP32 configuration block by finalizing and returning the YAML mapping structure.
    /// The method ensures that all required configurations, such as the board, are properly specified
    /// before building the result.
    /// <returns>
    /// A finalized YAML mapping structure containing the ESP32 block configuration.
    /// Throws an InvalidOperationException if a required configuration is missing.
    /// </returns>
    internal IYamlMapping Build()
    {
        if (!_block.ContainsKey("board"))
        {
            throw new InvalidOperationException("A board must be specified for the 'esp32' block using WithBoard().");
        }

        return _block;
    }

    /// <summary>
    /// Assigns a string value to a specified key in a YAML mapping structure.
    /// </summary>
    /// <param name="key">The key to which the value should be assigned.</param>
    /// <param name="value">The value to associate with the specified key.</param>
    private void SetValue(string key, string value)
    {
        _block[key] = new YamlString(value);
    }

    /// <summary>
    /// Sets the value for a specified key in the internal YAML mapping block.
    /// </summary>
    /// <param name="key">The key where the value should be assigned.</param>
    /// <param name="value">The value to assign, represented as an <see cref="IYamlNode"/>.</param>
    private void SetValue(string key, IYamlNode value)
    {
        _block[key] = value;
    }
}