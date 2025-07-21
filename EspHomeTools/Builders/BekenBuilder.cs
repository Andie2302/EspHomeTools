using System;
using EspHomeTools.Classes.Scalars;
using EspHomeTools.Classes.Structures;
using EspHomeTools.Interfaces;

namespace EspHomeTools.Builders;

public class BekenBuilder: IYamlBuilder
{
    private const string BoardKey = "board";

    private const string BoardRequiredErrorMessage = "`A board must be specified with WithBoard() for the 'bk72xx' block.`\n";

    private readonly YamlMapping _block = new();

    public BekenBuilder WithBoard(string board)
    {
        _block[BoardKey] = new YamlString(board);
        return this;
    }

    public BekenBuilder WithBoard(YamlSecret board)
    {
        _block[BoardKey] = board;
        return this;
    }

    public BekenBuilder WithBoard(string board, bool isSecret)
    {
        return isSecret ? WithBoard(new YamlSecret(board)) : WithBoard(board);
    }

    public BekenBuilder WithCommentOn(string key, string comment)
    {
        if (_block.TryGetValue(key, out var node))
            node.Comment = comment;

        return this;
    }

    internal IYamlMapping Build()
    {
        ValidateBoardIsSet();
        return _block;
    }

    private void ValidateBoardIsSet()
    {
        if (!_block.ContainsKey(BoardKey))
        {
            throw new InvalidOperationException(BoardRequiredErrorMessage);
        }
    }
}