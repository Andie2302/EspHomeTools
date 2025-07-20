using System;
using EspHomeTools.Classes.Scalars;
using EspHomeTools.Classes.Structures;
using EspHomeTools.Interfaces;

namespace EspHomeTools.Builders;

public class TimeBlockBuilder
{
    private readonly YamlMapping _config = new();

    public TimeBlockBuilder WithPlatform(string platform)
    {
        _config["platform"] = new YamlString(platform);
        return this;
    }

    public TimeBlockBuilder WithId(string id)
    {
        _config["id"] = new YamlString(id);
        return this;
    }

    public TimeBlockBuilder WithTimezone(string timezone)
    {
        _config["timezone"] = new YamlString(timezone);
        return this;
    }

    public TimeBlockBuilder WithTimezone(YamlSecret timezone)
    {
        _config["timezone"] = timezone;
        return this;
    }

    public TimeBlockBuilder WithTimezone(string timezone, bool isSecret) => isSecret ? WithTimezone(new YamlSecret(timezone)) : WithTimezone(timezone);

    public TimeBlockBuilder WithServers(params string[] servers)
    {
        var serverSequence = new YamlSequence();
        foreach (var server in servers)
        {
            serverSequence.Add(new YamlString(server));
        }

        _config["servers"] = serverSequence;
        return this;
    }

    public TimeBlockBuilder WithCommentOn(string key, string comment)
    {
        if (_config.TryGetValue(key, out var node))
            node.Comment = comment;

        return this;
    }

    internal IYamlMapping Build()
    {
        if (!_config.ContainsKey("platform"))
        {
            throw new InvalidOperationException("Eine Platform muss für die 'time'-Komponente mit WithPlatform() angegeben werden (z.B. 'homeassistant' oder 'sntp').");
        }

        return _config;
    }
}