using System;
using EspHomeTools.Classes.Scalars;
using EspHomeTools.Classes.Structures;
using EspHomeTools.Interfaces;

namespace EspHomeTools.Builders;

public class TimeBuilder
{
    private const string PlatformKey = "platform";

    private const string IdKey = "id";

    private const string TimezoneKey = "timezone";

    private const string ServersKey = "servers";

    private readonly YamlMapping _config = new();

    public TimeBuilder WithPlatform(string platform)
    {
        _config[PlatformKey] = new YamlString(platform);
        return this;
    }

    public TimeBuilder WithId(string id)
    {
        _config[IdKey] = new YamlString(id);
        return this;
    }

    public TimeBuilder WithTimezone(string timezone)
    {
        _config[TimezoneKey] = new YamlString(timezone);
        return this;
    }

    public TimeBuilder WithTimezone(YamlSecret timezone)
    {
        _config[TimezoneKey] = timezone;
        return this;
    }

    public TimeBuilder WithTimezone(string timezone, bool isSecret) =>
        isSecret ? WithTimezone(new YamlSecret(timezone)) : WithTimezone(timezone);

    public TimeBuilder WithServers(params string[] servers)
    {
        _config[ServersKey] = CreateServerSequence(servers);
        return this;
    }

    public TimeBuilder WithComment(string key, string comment)
    {
        if (_config.TryGetValue(key, out var node))
            node.Comment = comment;

        return this;
    }

    private static YamlSequence CreateServerSequence(string[] servers)
    {
        var serverSequence = new YamlSequence();
        foreach (var server in servers)
        {
            serverSequence.Add(new YamlString(server));
        }

        return serverSequence;
    }

    internal IYamlMapping Build()
    {
        if (!_config.ContainsKey(PlatformKey))
        {
            throw new InvalidOperationException("A platform must be specified for the 'time' component using WithPlatform() (e.g. 'homeassistant' or 'sntp').");
        }

        return _config;
    }
}