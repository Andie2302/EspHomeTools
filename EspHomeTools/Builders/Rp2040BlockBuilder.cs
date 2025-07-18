using System;
using EspHomeTools.Classes.Scalars;
using EspHomeTools.Classes.Structures;
using EspHomeTools.Interfaces;

namespace EspHomeTools.Builders;

public class Rp2040BlockBuilder
{
    private readonly YamlMapping _block = new();

    public Rp2040BlockBuilder WithBoard(string board)
    {
        _block["board"] = new YamlString(board);
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