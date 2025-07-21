using System;
using EspHomeTools.Classes.Scalars;
using EspHomeTools.Classes.Structures;
using EspHomeTools.Interfaces;

namespace EspHomeTools.Builders;

public class Esp8266Builder: IYamlBuilder<IYamlMapping>
{
    private readonly YamlMapping _block = new();

    public Esp8266Builder WithBoard(string board)
    {
        _block["board"] = new YamlString(board);
        return this;
    }

    public Esp8266Builder WithBoard(YamlSecret board)
    {
        _block["board"] = board;
        return this;
    }

    public Esp8266Builder WithBoard(string board, bool isSecret) => isSecret ? WithBoard(new YamlSecret(board)) : WithBoard(board);

    public Esp8266Builder WithCommentOn(string key, string comment)
    {
        if (_block.TryGetValue(key, out var node))
            node.Comment = comment;

        return this;
    }

    public IYamlMapping Build()
    {
        ValidateRequiredBoard();
        return _block;
    }

    private void ValidateRequiredBoard()
    {
        if (!_block.ContainsKey("board"))
        {
            throw new InvalidOperationException("A board must be specified with WithBoard() for the 'esp8266' block.");
        }
    }
}