using System;
using EspHomeTools.Classes.Scalars;
using EspHomeTools.Classes.Structures;
using EspHomeTools.Interfaces;

namespace EspHomeTools.Builders;

public class BekenBuilder
{
    private readonly YamlMapping _block = new();

    public BekenBuilder WithBoard(string board)
    {
        _block["board"] = new YamlString(board);
        return this;
    }

    public BekenBuilder WithBoard(YamlSecret board)
    {
        _block["board"] = board;
        return this;
    }

    public BekenBuilder WithBoard(string board, bool isSecret) => isSecret ? WithBoard(new YamlSecret(board)) : WithBoard(board);

    public BekenBuilder WithCommentOn(string key, string comment)
    {
        if (_block.TryGetValue(key, out var node))
            node.Comment = comment;

        return this;
    }

    internal IYamlMapping Build()
    {
        if (!_block.ContainsKey("board"))
        {
            throw new InvalidOperationException("Für den 'bk72xx'-Block muss ein Board mit WithBoard() angegeben werden.");
        }

        return _block;
    }
}