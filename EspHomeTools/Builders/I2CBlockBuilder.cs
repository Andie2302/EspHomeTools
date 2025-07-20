using System;
using EspHomeTools.Classes.Scalars;
using EspHomeTools.Classes.Structures;
using EspHomeTools.Interfaces;

namespace EspHomeTools.Builders;

public class I2CBlockBuilder
{
    private readonly YamlMapping _config = new();

    public I2CBlockBuilder SetSdaPin(string pin)
    {
        _config["sda"] = new YamlString(pin);
        return this;
    }

    public I2CBlockBuilder SetSclPin(string pin)
    {
        _config["scl"] = new YamlString(pin);
        return this;
    }

    public I2CBlockBuilder WithScan(bool scan)
    {
        _config["scan"] = new YamlBool(scan);
        return this;
    }

    public I2CBlockBuilder WithId(string id)
    {
        _config["id"] = new YamlString(id);
        return this;
    }

    public I2CBlockBuilder WithFrequency(string frequency)
    {
        _config["frequency"] = new YamlString(frequency);
        return this;
    }

    public I2CBlockBuilder WithCommentOn(string key, string comment)
    {
        if (_config.TryGetValue(key, out var node))
            node.Comment = comment;

        return this;
    }

    internal IYamlMapping Build()
    {
        // SDA and SCL are often optional as ESPHome can use board defaults,
        // so we don't enforce their presence with an exception.
        // An ID is highly recommended if other components will use this bus.
        if (!_config.ContainsKey("id"))
        {
            Console.WriteLine("Warning: I2C bus created without an ID. Other components might not be able to use it.");
        }

        return _config;
    }
}