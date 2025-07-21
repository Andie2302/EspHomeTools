using System;
using EspHomeTools.Classes.Scalars;
using EspHomeTools.Classes.Structures;
using EspHomeTools.Interfaces;

namespace EspHomeTools.Builders;

/// <summary>
/// Provides a builder for configuring the "rp2040" platform options in a YAML mapping.
/// </summary>
/// <remarks>
/// The <see cref="Rp2040Builder"/> class enables users to specify configuration options
/// for the "rp2040" platform. It supports defining properties like the target board and
/// adding comments to specific configuration keys. The generated YAML mapping can be
/// integrated into an overarching configuration structure.
/// </remarks>
public class Rp2040Builder
{
    /// <summary>
    /// Represents the key used to identify the 'board' property in the YAML structure
    /// within the context of the Rp2040Builder class. This key is required to specify
    /// the 'board' field in the generated configuration and is utilized in methods
    /// such as WithBoard() to define or manipulate board-related values.
    /// </summary>
    private const string BoardKey = "board";

    /// <summary>
    /// Represents a YAML-based mapping structure used internally within the Rp2040Builder class
    /// to manage configuration data for building YAML documents.
    /// </summary>
    /// <remarks>
    /// This mapping is used to store key-value pairs, where the keys are strings and the values
    /// are instances of IYamlNode. It enables functionalities like setting, updating, and removing
    /// configuration entries in the context of generating configuration for RP2040-based devices.
    /// </remarks>
    private readonly YamlMapping _block = new();

    /// <summary>
    /// Specifies the board configuration for the RP2040 device.
    /// </summary>
    /// <param name="board">The name of the board to be used, represented as a string.</param>
    /// <returns>The current instance of <c>Rp2040Builder</c>, allowing for method chaining.</returns>
    public Rp2040Builder WithBoard(string board)
    {
        _block[BoardKey] = new YamlString(board);
        return this;
    }

    /// <summary>
    /// Configures the board information for the Rp2040 builder.
    /// </summary>
    /// <param name="board">The name of the board to be configured.</param>
    /// <returns>The current instance of <see cref="Rp2040Builder"/> for method chaining.</returns>
    public Rp2040Builder WithBoard(YamlSecret board)
    {
        _block[BoardKey] = board;
        return this;
    }

    /// <summary>
    /// Configures the board parameter for the RP2040 builder.
    /// </summary>
    /// <param name="board">The name of the board to configure.</param>
    /// <returns>The <see cref="Rp2040Builder"/> instance with the specified board configured.</returns>
    public Rp2040Builder WithBoard(string board, bool isSecret) =>
        isSecret ? WithBoard(new YamlSecret(board)) : WithBoard(board);

    /// Adds a comment to the specified key if it exists in the YAML mapping.
    /// <param name="key">The key in the YAML mapping to which the comment should be added.</param>
    /// <param name="comment">The comment to associate with the specified key.</param>
    /// <returns>An instance of <see cref="Rp2040Builder"/> for method chaining.</returns>
    public Rp2040Builder WithCommentOn(string key, string comment)
    {
        if (_block.TryGetValue(key, out var node))
            node.Comment = comment;

        return this;
    }

    /// <summary>
    /// Builds and returns an instance of <see cref="IYamlMapping"/> representing the current state
    /// of the builder, including all added configurations and mappings.
    /// </summary>
    /// <returns>
    /// An instance of <see cref="IYamlMapping"/> containing the accumulated key-value pairs
    /// and configurations defined in the builder.
    /// </returns>
    internal IYamlMapping Build()
    {
        ValidateRequiredFields();
        return _block;
    }

    /// <summary>
    /// Validates that all required fields have been specified in the YAML mapping block.
    /// </summary>
    /// <exception cref="InvalidOperationException">
    /// Thrown if a required field, such as the board key, has not been specified in the YAML mapping block.
    /// </exception>
    private void ValidateRequiredFields()
    {
        if (!_block.ContainsKey(BoardKey))
        {
            throw new InvalidOperationException("A board must be specified for the 'rp2040' block using WithBoard().");
        }
    }
}