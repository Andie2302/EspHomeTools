using System;
using EspHomeTools.Classes.Scalars;
using EspHomeTools.Classes.Structures;
using EspHomeTools.Interfaces;

namespace EspHomeTools.Builders;

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

    public GpioSwitchBuilder UsePin(YamlSecret pin)
    {
        _config["pin"] = pin;
        return this;
    }

    public GpioSwitchBuilder UsePin(string pin, bool isSecret) => isSecret ? UsePin(new YamlSecret(pin)) : UsePin(pin);

    public GpioSwitchBuilder WithName(string name)
    {
        _config["name"] = new YamlString(name);
        return this;
    }

    public GpioSwitchBuilder WithName(YamlSecret name)
    {
        _config["name"] = name;
        return this;
    }

    public GpioSwitchBuilder WithName(string name, bool isSecret) => isSecret ? WithName(new YamlSecret(name)) : WithName(name);

    public GpioSwitchBuilder WithId(string id)
    {
        _config["id"] = new YamlString(id);
        return this;
    }

    public GpioSwitchBuilder WithId(YamlSecret id)
    {
        _config["id"] = id;
        return this;
    }

    public GpioSwitchBuilder WithId(string id, bool isSecret) => isSecret ? WithId(new YamlSecret(id)) : WithId(id);

    public GpioSwitchBuilder WithIcon(string icon)
    {
        _config["icon"] = new YamlString(icon);
        return this;
    }

    public GpioSwitchBuilder WithIcon(YamlSecret icon)
    {
        _config["icon"] = icon;
        return this;
    }

    public GpioSwitchBuilder WithIcon(string icon, bool isSecret) => isSecret ? WithIcon(new YamlSecret(icon)) : WithIcon(icon);

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