using System;
using EspHomeTools.Classes.Scalars;
using EspHomeTools.Classes.Structures;
using EspHomeTools.Interfaces;

namespace EspHomeTools.Builders;

public class BinarySensorBuilder
{
    private readonly YamlMapping _config = new();

    public BinarySensorBuilder()
    {
        _config["platform"] = new YamlString("gpio");
    }

    public BinarySensorBuilder WithPlatform(string platform)
    {
        _config["platform"] = new YamlString(platform);
        return this;
    }

    public BinarySensorBuilder UsePin(string pin)
    {
        _config["pin"] = new YamlString(pin);
        return this;
    }

    public BinarySensorBuilder UsePin(YamlSecret pin)
    {
        _config["pin"] = pin;
        return this;
    }

    public BinarySensorBuilder UsePin(string pin, bool isSecret) => isSecret ? UsePin(new YamlSecret(pin)) : UsePin(pin);

    public BinarySensorBuilder WithName(string name)
    {
        _config["name"] = new YamlString(name);
        return this;
    }

    public BinarySensorBuilder WithName(YamlSecret name)
    {
        _config["name"] = name;
        return this;
    }

    public BinarySensorBuilder WithName(string name, bool isSecret) => isSecret ? WithName(new YamlSecret(name)) : WithName(name);

    public BinarySensorBuilder WithId(string id)
    {
        _config["id"] = new YamlString(id);
        return this;
    }

    public BinarySensorBuilder WithDeviceClass(string deviceClass)
    {
        _config["device_class"] = new YamlString(deviceClass);
        return this;
    }

    public BinarySensorBuilder WithCommentOn(string key, string comment)
    {
        if (_config.TryGetValue(key, out var node))
            node.Comment = comment;

        return this;
    }

    internal IYamlMapping Build()
    {
        if (!_config.ContainsKey("pin"))
        {
            throw new InvalidOperationException("Ein Pin muss für den Binary Sensor mit UsePin() angegeben werden.");
        }

        if (!_config.ContainsKey("name"))
        {
            throw new InvalidOperationException("Ein Name muss für den Binary Sensor mit WithName() angegeben werden.");
        }

        return _config;
    }
}