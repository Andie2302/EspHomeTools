using System;
using EspHomeTools.Classes.Scalars;
using EspHomeTools.Classes.Structures;
using EspHomeTools.Interfaces;

namespace EspHomeTools.Builders;

public class SpiBuilder
{
    private readonly YamlMapping _config = new();

    public SpiBuilder SetClkPin(string pin)
    {
        _config["clk_pin"] = new YamlString(pin);
        return this;
    }

    public SpiBuilder SetMosiPin(string pin)
    {
        _config["mosi_pin"] = new YamlString(pin);
        return this;
    }

    public SpiBuilder SetMisoPin(string pin)
    {
        _config["miso_pin"] = new YamlString(pin);
        return this;
    }

    public SpiBuilder WithId(string id)
    {
        _config["id"] = new YamlString(id);
        return this;
    }

    public SpiBuilder WithCommentOn(string key, string comment)
    {
        if (_config.TryGetValue(key, out var node))
            node.Comment = comment;

        return this;
    }

    internal IYamlMapping Build()
    {
        if (!_config.ContainsKey("clk_pin"))
        {
            throw new InvalidOperationException("Der Clock-Pin (clk_pin) muss für den SPI-Bus angegeben werden.");
        }

        return _config;
    }
}