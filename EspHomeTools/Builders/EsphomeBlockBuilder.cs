using System;
using EspHomeTools.Classes.Scalars;
using EspHomeTools.Classes.Structures;
using EspHomeTools.Interfaces;

namespace EspHomeTools.Builders;

public class EsphomeBlockBuilder
{
    private readonly YamlMapping _block = new();

    public EsphomeBlockBuilder WithName(string name)
    {
        _block["name"] = new YamlString(name);
        return this;
    }

    public EsphomeBlockBuilder WithName(YamlSecret name)
    {
        _block["name"] = name;
        return this;
    }

    public EsphomeBlockBuilder WithName(string name, bool isSecret) => isSecret ? WithName(new YamlSecret(name)) : WithName(name);

    public EsphomeBlockBuilder WithComment(string comment)
    {
        if (_block.TryGetValue("name", out var nameNode))
        {
            nameNode.Comment = comment;
        }

        return this;
    }

    public EsphomeBlockBuilder WithCommentOn(string key, string comment)
    {
        if (_block.TryGetValue(key, out var node))
            node.Comment = comment;

        return this;
    }

    internal IYamlMapping Build()
    {
        if (!_block.ContainsKey("name"))
        {
            throw new InvalidOperationException("Der Name im 'esphome'-Block ist erforderlich. Benutze die WithName()-Methode.");
        }

        return _block;
    }
    public EsphomeBlockBuilder OnBoot(Action<ActionSequenceBuilder> configurator)
    {
        var builder = new ActionSequenceBuilder();
        configurator(builder);
        _block["on_boot"] = builder.Build();
        return this;
    }
}