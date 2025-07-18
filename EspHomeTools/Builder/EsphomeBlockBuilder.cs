using System;
using EspHomeTools.Classes;
using EspHomeTools.Classes.Scalars;
using EspHomeTools.Classes.Structures;
using EspHomeTools.Interfaces;

namespace EspHomeTools.Builder;

public class EsphomeBlockBuilder
{
    private readonly YamlMapping _block = new();

    public EsphomeBlockBuilder WithName(string name)
    {
        _block["name"] = new YamlString(name);
        return this;
    }

    public EsphomeBlockBuilder WithComment(string comment)
    {
        if (_block.TryGetValue("name", out var nameNode))
        {
            nameNode.Comment = comment;
        }

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
}