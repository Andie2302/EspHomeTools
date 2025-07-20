using System;
using EspHomeTools.Classes.Scalars;
using EspHomeTools.Classes.Structures;
using EspHomeTools.Interfaces;

namespace EspHomeTools.Builders;

public class I2CBuilder
{
    private readonly YamlMapping _config = new();

    public I2CBuilder SetSdaPin(string pin)
    {
        _config["sda"] = new YamlString(pin);
        return this;
    }

    public I2CBuilder SetSclPin(string pin)
    {
        _config["scl"] = new YamlString(pin);
        return this;
    }

    public I2CBuilder WithScan(bool scan)
    {
        _config["scan"] = new YamlBool(scan);
        return this;
    }

    public I2CBuilder WithId(string id)
    {
        _config["id"] = new YamlString(id);
        return this;
    }

    public I2CBuilder WithFrequency(string frequency)
    {
        _config["frequency"] = new YamlString(frequency);
        return this;
    }

    public I2CBuilder WithCommentOn(string key, string comment)
    {
        if (_config.TryGetValue(key, out var node))
            node.Comment = comment;

        return this;
    }

    internal IYamlMapping Build()
    {
        if (!_config.ContainsKey("id"))
        {
            Console.WriteLine("Warning: I2C bus created without an ID. Other components might not be able to use it.");
        }

        return _config;
    }
}