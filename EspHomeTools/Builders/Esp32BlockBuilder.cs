using System;
using EspHomeTools.Classes.Scalars;
using EspHomeTools.Classes.Structures;
using EspHomeTools.Interfaces;

namespace EspHomeTools.Builders;

public class Esp32BlockBuilder
{
    private readonly YamlMapping _block = new();

    public Esp32BlockBuilder WithBoard(string board)
    {
        _block["board"] = new YamlString(board);
        return this;
    }

    public Esp32BlockBuilder WithFramework(string framework)
    {
        _block["framework"] = new YamlString(framework);
        return this;
    }

    internal IYamlMapping Build()
    {
        if (!_block.ContainsKey("board"))
        {
            throw new InvalidOperationException("Für den 'esp32'-Block muss ein Board mit WithBoard() angegeben werden.");
        }

        return _block;
    }
}