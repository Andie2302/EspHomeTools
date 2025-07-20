using System;
using EspHomeTools.Classes.Scalars;
using EspHomeTools.Classes.Structures;
using EspHomeTools.Interfaces;

namespace EspHomeTools.Builders;

public class OutputBuilder
{
    private readonly YamlMapping _config = new();

    public OutputBuilder()
    {
        // Set a default platform, which can be changed.
        _config["platform"] = new YamlString("gpio");
    }

    public OutputBuilder WithPlatform(string platform)
    {
        _config["platform"] = new YamlString(platform);
        return this;
    }

    public OutputBuilder UsePin(string pin)
    {
        _config["pin"] = new YamlString(pin);
        return this;
    }

    public OutputBuilder UsePin(YamlSecret pin)
    {
        _config["pin"] = pin;
        return this;
    }

    public OutputBuilder UsePin(string pin, bool isSecret) => isSecret ? UsePin(new YamlSecret(pin)) : UsePin(pin);

    public OutputBuilder WithId(string id)
    {
        _config["id"] = new YamlString(id);
        return this;
    }

    public OutputBuilder WithCommentOn(string key, string comment)
    {
        if (_config.TryGetValue(key, out var node))
            node.Comment = comment;
        return this;
    }

    internal IYamlMapping Build()
    {
        if (!_config.ContainsKey("pin"))
        {
            throw new InvalidOperationException("Ein Pin muss für die 'output'-Komponente mit UsePin() angegeben werden.");
        }
        if (!_config.ContainsKey("id"))
        {
            throw new InvalidOperationException("Eine ID muss für die 'output'-Komponente mit WithId() angegeben werden, damit andere Komponenten darauf verweisen können.");
        }

        return _config;
    }
}