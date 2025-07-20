using System;
using EspHomeTools.Classes.Scalars;
using EspHomeTools.Classes.Structures;
using EspHomeTools.Interfaces;

namespace EspHomeTools.Builders;

public class Rp2040Builder
{
    private readonly YamlMapping _block = new();

    public Rp2040Builder WithBoard(string board)
    {
        _block["board"] = new YamlString(board);
        return this;
    }

    public Rp2040Builder WithBoard(YamlSecret board)
    {
        _block["board"] = board;
        return this;
    }

    public Rp2040Builder WithBoard(string board, bool isSecret) => isSecret ? WithBoard(new YamlSecret(board)) : WithBoard(board);

    public Rp2040Builder WithCommentOn(string key, string comment)
    {
        if (_block.TryGetValue(key, out var node))
            node.Comment = comment;

        return this;
    }

    internal IYamlMapping Build()
    {
        if (!_block.ContainsKey("board"))
        {
            throw new InvalidOperationException("Für den 'rp2040'-Block muss ein Board mit WithBoard() angegeben werden.");
        }

        return _block;
    }
}