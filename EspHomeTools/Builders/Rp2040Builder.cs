using System;
using EspHomeTools.Classes.Scalars;
using EspHomeTools.Classes.Structures;
using EspHomeTools.Interfaces;

namespace EspHomeTools.Builders;

public class Rp2040Builder: IYamlBuilder
{
    private const string BoardKey = "board";

    private readonly YamlMapping _block = new();

    public Rp2040Builder WithBoard(string board)
    {
        _block[BoardKey] = new YamlString(board);
        return this;
    }

    public Rp2040Builder WithBoard(YamlSecret board)
    {
        _block[BoardKey] = board;
        return this;
    }

    public Rp2040Builder WithBoard(string board, bool isSecret) =>
        isSecret ? WithBoard(new YamlSecret(board)) : WithBoard(board);

    public Rp2040Builder WithCommentOn(string key, string comment)
    {
        if (_block.TryGetValue(key, out var node))
            node.Comment = comment;

        return this;
    }

    internal IYamlMapping Build()
    {
        ValidateRequiredFields();
        return _block;
    }

    private void ValidateRequiredFields()
    {
        if (!_block.ContainsKey(BoardKey))
        {
            throw new InvalidOperationException("A board must be specified for the 'rp2040' block using WithBoard().");
        }
    }
}