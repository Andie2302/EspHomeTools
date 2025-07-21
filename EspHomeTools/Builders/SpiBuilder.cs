using System;
using EspHomeTools.Classes.Scalars;
using EspHomeTools.Classes.Structures;
using EspHomeTools.Interfaces;

namespace EspHomeTools.Builders;

public class SpiBuilder
{
    private const string ClkPinKey = "clk_pin";
    private readonly YamlMapping _config = new();

    public SpiBuilder SetClkPin(string pin) => SetPin(ClkPinKey, pin);

    public SpiBuilder SetMosiPin(string pin) => SetPin("mosi_pin", pin);

    public SpiBuilder SetMisoPin(string pin) => SetPin("miso_pin", pin);

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
        if (!_config.ContainsKey(ClkPinKey))
        {
            throw new InvalidOperationException("The clock pin (clk_pin) must be specified for the SPI bus.");
        }

        return _config;
    }

    private SpiBuilder SetPin(string pinKey, string pin)
    {
        _config[pinKey] = new YamlString(pin);
        return this;
    }
}