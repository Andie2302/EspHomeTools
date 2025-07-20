using System;
using EspHomeTools.Classes.Scalars;
using EspHomeTools.Classes.Structures;
using EspHomeTools.Interfaces;

namespace EspHomeTools.Builders;

public class LedcBuilder
{
    private readonly YamlMapping _config = new();

    public LedcBuilder UsePin(string pin)
    {
        _config["pin"] = new YamlString(pin);
        return this;
    }

    public LedcBuilder WithId(string id)
    {
        _config["id"] = new YamlString(id);
        return this;
    }

    public LedcBuilder WithFrequency(string frequency)
    {
        _config["frequency"] = new YamlString(frequency);
        return this;
    }

    public LedcBuilder WithCommentOn(string key, string comment)
    {
        if (_config.TryGetValue(key, out var node))
            node.Comment = comment;

        return this;
    }

    internal IYamlMapping Build()
    {
        if (!_config.ContainsKey("pin"))
        {
            throw new InvalidOperationException("Ein Pin muss für die 'ledc'-Komponente mit UsePin() angegeben werden.");
        }

        if (!_config.ContainsKey("id"))
        {
            throw new InvalidOperationException("Eine ID muss für die 'ledc'-Komponente mit WithId() angegeben werden.");
        }

        return _config;
    }
}