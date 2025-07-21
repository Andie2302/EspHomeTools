using System;
using EspHomeTools.Classes.Scalars;
using EspHomeTools.Classes.Structures;
using EspHomeTools.Interfaces;

namespace EspHomeTools.Builders;

/// <summary>
/// The <c>BekenBuilder</c> class is a builder for configuring and managing
/// settings specific to devices that use Beken microcontrollers. It facilitates
/// the creation of YAML configuration mappings for these devices.
/// </summary>
public class BekenBuilder
{
    /// <summary>
    /// Represents the key used to identify the "board" field within a YAML mapping structure.
    /// </summary>
    /// <remarks>
    /// This constant is utilized within the <c>BekenBuilder</c> class to define the key for specifying
    /// the board type or configuration. It plays a crucial role in the YAML serialization process
    /// by ensuring a consistent naming convention for the "board" key during interactions.
    /// </remarks>
    private const string BoardKey = "board";

    /// <summary>
    /// Error message indicating that a board must be specified for the 'bk72xx' block
    /// when using the <see cref="BekenBuilder"/> class.
    /// </summary>
    /// <remarks>
    /// This message is used in <see cref="BekenBuilder.ValidateBoardIsSet"/> to inform
    /// the user that the "board" key must be set via any of the overloaded
    /// <see cref="BekenBuilder.WithBoard(string)"/> method calls before building the block.
    /// Failing to specify a board results in an <see cref="InvalidOperationException"/>.
    /// </remarks>
    private const string BoardRequiredErrorMessage = "`A board must be specified with WithBoard() for the 'bk72xx' block.`\n";

    /// <summary>
    /// Represents a private instance of <see cref="YamlMapping"/> used to construct
    /// and store YAML key-value pairs within the BekenBuilder class. Primarily manages
    /// configuration data such as board settings and other related attributes.
    /// </summary>
    private readonly YamlMapping _block = new();

    /// <summary>
    /// Sets the board value in the YAML mapping and returns the current instance of the builder.
    /// </summary>
    /// <param name="board">The name of the board to be set in the YAML mapping.</param>
    /// <returns>The current instance of <see cref="BekenBuilder"/> for method chaining.</returns>
    public BekenBuilder WithBoard(string board)
    {
        _block[BoardKey] = new YamlString(board);
        return this;
    }

    /// <summary>
    /// Sets the "board" key in the internal YamlMapping block, associating it with the provided value.
    /// </summary>
    /// <param name="board">The board name to set, represented as a plain string.</param>
    /// <returns>The current instance of <see cref="BekenBuilder"/> for method chaining.</returns>
    public BekenBuilder WithBoard(YamlSecret board)
    {
        _block[BoardKey] = board;
        return this;
    }

    /// <summary>
    /// Sets the board configuration for the BekenBuilder instance.
    /// Allows specifying whether the board name should be treated as a secret.
    /// </summary>
    /// <param name="board">The name of the board being configured.</param>
    /// <param name="isSecret">A boolean value indicating whether the board name should be handled as a secret.</param>
    /// <returns>The current instance of <see cref="BekenBuilder"/> for method chaining.</returns>
    public BekenBuilder WithBoard(string board, bool isSecret)
    {
        return isSecret ? WithBoard(new YamlSecret(board)) : WithBoard(board);
    }

    /// <summary>
    /// Adds or updates a comment for the specified key in the YAML mapping.
    /// </summary>
    /// <param name="key">The key for which the comment should be added or updated.</param>
    /// <param name="comment">The comment to associate with the specified key.</param>
    /// <returns>The current instance of <see cref="BekenBuilder"/> to allow for method chaining.</returns>
    public BekenBuilder WithCommentOn(string key, string comment)
    {
        if (_block.TryGetValue(key, out var node))
            node.Comment = comment;

        return this;
    }

    /// Builds the current configuration and returns a YAML mapping representation of it.
    /// This method finalizes the building process by validating mandatory values
    /// and assembling the configuration as an object implementing the IYamlMapping interface.
    /// <returns>Returns an IYamlMapping instance representing the YAML structure built by this builder.</returns>
    internal IYamlMapping Build()
    {
        ValidateBoardIsSet();
        return _block;
    }

    /// Validates that the board configuration is set in the internal YamlMapping.
    /// Throws an InvalidOperationException if the "board" key is not present in the YamlMapping.
    /// This ensures that a board is properly defined before building the final block
    /// to prevent creating an incomplete or invalid configuration.
    private void ValidateBoardIsSet()
    {
        if (!_block.ContainsKey(BoardKey))
        {
            throw new InvalidOperationException(BoardRequiredErrorMessage);
        }
    }
}