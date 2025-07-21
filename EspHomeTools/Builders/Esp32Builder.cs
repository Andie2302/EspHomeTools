using System;
using EspHomeTools.Classes.Scalars;
using EspHomeTools.Classes.Structures;
using EspHomeTools.Interfaces;

namespace EspHomeTools.Builders;

public class Esp32Builder: IYamlBuilder<IYamlMapping>, IYamlBuilder
{
    private readonly YamlMapping _block = new();

    public Esp32Builder WithBoard(string board)
    {
        SetValue("board", board);
        return this;
    }

    public Esp32Builder WithBoard(YamlSecret board)
    {
        SetValue("board", board);
        return this;
    }

    public Esp32Builder WithBoard(string board, bool isSecret) =>
        isSecret ? WithBoard(new YamlSecret(board)) : WithBoard(board);

    public Esp32Builder WithFramework(string framework)
    {
        SetValue("framework", framework);
        return this;
    }

    public Esp32Builder WithFramework(YamlSecret framework)
    {
        SetValue("framework", framework);
        return this;
    }

    public Esp32Builder WithFramework(string framework, bool isSecret) =>
        isSecret ? WithFramework(new YamlSecret(framework)) : WithFramework(framework);

    public Esp32Builder WithCommentOn(string key, string comment)
    {
        if (_block.TryGetValue(key, out var node))
            node.Comment = comment;

        return this;
    }

    public IYamlMapping Build()
    {
        if (!_block.ContainsKey("board"))
        {
            throw new InvalidOperationException("A board must be specified for the 'esp32' block using WithBoard().");
        }

        return _block;
    }

    private void SetValue(string key, string value)
    {
        _block[key] = new YamlString(value);
    }

    private void SetValue(string key, IYamlNode value)
    {
        _block[key] = value;
    }
}