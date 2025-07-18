using System;
using EspHomeTools.Classes;
using EspHomeTools.Interfaces;

namespace EspHomeTools.Builder;

public class GpioSwitchBuilder
{
    private readonly YamlMapping _config = new();

    public GpioSwitchBuilder()
    {
        _config["platform"] = new YamlString("gpio");
    }

    public GpioSwitchBuilder UsePin(string pin)
    {
        _config["pin"] = new YamlString(pin);
        return this;
    }

    public GpioSwitchBuilder WithName(string name)
    {
        _config["name"] = new YamlString(name);
        return this;
    }

    public GpioSwitchBuilder WithId(string id)
    {
        _config["id"] = new YamlString(id);
        return this;
    }

    public GpioSwitchBuilder WithIcon(string icon)
    {
        _config["icon"] = new YamlString(icon);
        return this;
    }

    internal IYamlMapping Build()
    {
        if (!_config.ContainsKey("pin"))
        {
            throw new InvalidOperationException("Ein Pin muss für den GPIO-Schalter mit UsePin() angegeben werden.");
        }

        if (!_config.ContainsKey("name"))
        {
            throw new InvalidOperationException("Ein Name muss für den GPIO-Schalter mit WithName() angegeben werden.");
        }

        return _config;
    }
}