using System;
using EspHomeTools.Classes.Scalars;
using EspHomeTools.Classes.Structures;
using EspHomeTools.Interfaces;

namespace EspHomeTools.Builders;

public class LightBlockBuilder
{
    private readonly YamlMapping _config = new();

    public LightBlockBuilder()
    {
        _config["platform"] = new YamlString("monochromatic");
    }

    public LightBlockBuilder WithPlatform(string platform)
    {
        _config["platform"] = new YamlString(platform);
        return this;
    }

    public LightBlockBuilder WithName(string name)
    {
        _config["name"] = new YamlString(name);
        return this;
    }

    public LightBlockBuilder WithName(YamlSecret name)
    {
        _config["name"] = name;
        return this;
    }

    public LightBlockBuilder WithName(string name, bool isSecret) => isSecret ? WithName(new YamlSecret(name)) : WithName(name);

    public LightBlockBuilder WithId(string id)
    {
        _config["id"] = new YamlString(id);
        return this;
    }

    public LightBlockBuilder UseOutput(string outputId)
    {
        _config["output"] = new YamlString(outputId);
        return this;
    }

    public LightBlockBuilder WithCommentOn(string key, string comment)
    {
        if (_config.TryGetValue(key, out var node))
            node.Comment = comment;

        return this;
    }

    internal IYamlMapping Build()
    {
        if (!_config.ContainsKey("platform"))
        {
            throw new InvalidOperationException("Eine Platform muss für die 'light'-Komponente angegeben werden.");
        }

        if (!_config.ContainsKey("name"))
        {
            throw new InvalidOperationException("Ein Name muss für die 'light'-Komponente mit WithName() angegeben werden.");
        }

        if (!_config.ContainsKey("output"))
        {
            throw new InvalidOperationException("Ein Output muss für die 'light'-Komponente mit UseOutput() angegeben werden.");
        }

        return _config;
    }
}