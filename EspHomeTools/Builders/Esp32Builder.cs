using System;
using EspHomeTools.Classes.Scalars;
using EspHomeTools.Classes.Structures;
using EspHomeTools.Interfaces;

namespace EspHomeTools.Builders;

public class Esp32Builder
{
    private readonly YamlMapping _block = new();

    public Esp32Builder WithBoard(string board)
    {
        _block["board"] = new YamlString(board);
        return this;
    }

    public Esp32Builder WithBoard(YamlSecret board)
    {
        _block["board"] = board;
        return this;
    }

    public Esp32Builder WithBoard(string board, bool isSecret) => isSecret ? WithBoard(new YamlSecret(board)) : WithBoard(board);

    public Esp32Builder WithFramework(string framework)
    {
        _block["framework"] = new YamlString(framework);
        return this;
    }

    public Esp32Builder WithFramework(YamlSecret framework)
    {
        _block["framework"] = framework;
        return this;
    }

    public Esp32Builder WithFramework(string framework, bool isSecret) => isSecret ? WithFramework(new YamlSecret(framework)) : WithFramework(framework);

    public Esp32Builder WithCommentOn(string key, string comment)
    {
        if (_block.TryGetValue(key, out var node))
            node.Comment = comment;

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