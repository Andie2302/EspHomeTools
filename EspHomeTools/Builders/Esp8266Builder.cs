using System;
using EspHomeTools.Classes.Scalars;
using EspHomeTools.Classes.Structures;
using EspHomeTools.Interfaces;

namespace EspHomeTools.Builders;

/// <summary>
/// Provides a builder for configuring ESP8266-specific YAML configurations.
/// </summary>
/// <remarks>
/// This class allows for setting up the board type and adding comments to specific keys
/// when constructing the YAML configuration for ESP8266 devices.
/// </remarks>
public class Esp8266Builder
{
    /// <summary>
    /// Represents a private instance of <see cref="YamlMapping"/> used to store configuration data
    /// for the ESP8266 builder during the YAML generation process.
    /// </summary>
    private readonly YamlMapping _block = new();

    /// <summary>
    /// Configures the ESP8266 board with the specified board type.
    /// </summary>
    /// <param name="board">The identifier of the ESP8266 board type to configure.</param>
    /// <returns>An instance of <c>Esp8266Builder</c> to allow for method chaining.</returns>
    public Esp8266Builder WithBoard(string board)
    {
        _block["board"] = new YamlString(board);
        return this;
    }

    /// <summary>
    /// Sets the board configuration for the ESP8266 device.
    /// </summary>
    /// <param name="board">The name of the board to be configured. This can be a string representing the board's model or identifier.</param>
    /// <returns>The current instance of <see cref="Esp8266Builder"/> to support method chaining.</returns>
    public Esp8266Builder WithBoard(YamlSecret board)
    {
        _block["board"] = board;
        return this;
    }

    /// <summary>
    /// Configures the ESP8266Builder instance with the specified board name.
    /// </summary>
    /// <param name="board">The name of the board to be assigned to the builder configuration.</param>
    /// <returns>The current instance of <see cref="Esp8266Builder"/>, allowing for method chaining.</returns>
    public Esp8266Builder WithBoard(string board, bool isSecret) => isSecret ? WithBoard(new YamlSecret(board)) : WithBoard(board);

    /// <summary>
    /// Adds or updates a comment for a specified key in the Yaml mapping.
    /// </summary>
    /// <param name="key">The key whose associated comment should be added or updated.</param>
    /// <param name="comment">The comment to associate with the specified key.</param>
    /// <returns>The <see cref="Esp8266Builder"/> instance to allow for method chaining.</returns>
    public Esp8266Builder WithCommentOn(string key, string comment)
    {
        if (_block.TryGetValue(key, out var node))
            node.Comment = comment;

        return this;
    }

    /// <summary>
    /// Builds and returns the finalized YAML mapping structure for the current ESP8266 configuration.
    /// </summary>
    /// <returns>
    /// An instance of <see cref="IYamlMapping"/> that represents the constructed YAML mapping.
    /// </returns>
    internal IYamlMapping Build()
    {
        ValidateRequiredBoard();
        return _block;
    }

    /// <summary>
    /// Validates that the required "board" field is specified in the underlying YAML mapping.
    /// </summary>
    /// <exception cref="InvalidOperationException">
    /// Thrown if the "board" field is not set in the YAML mapping. This ensures that a board is defined
    /// when constructing the 'esp8266' block.
    /// </exception>
    /// <remarks>
    /// The method is called internally during the build process to ensure that the configuration
    /// meets the requirements before producing the final output.
    /// </remarks>
    private void ValidateRequiredBoard()
    {
        if (!_block.ContainsKey("board"))
        {
            throw new InvalidOperationException(
                "A board must be specified with WithBoard() for the 'esp8266' block.");
        }
    }
}