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

    private GpioSwitchBuilder SetConfigValue(string key, string value)
    {
        _config[key] = new YamlString(value);
        return this;
    }

    private GpioSwitchBuilder SetConfigValue(string key, YamlSecret value)
    {
        _config[key] = value;
        return this;
    }

    private GpioSwitchBuilder SetConfigValue(string key, string value, bool isSecret)
    {
        return isSecret ? SetConfigValue(key, new YamlSecret(value)) : SetConfigValue(key, value);
    }

    public GpioSwitchBuilder UsePin(string pin) => SetConfigValue("pin", pin);

    public GpioSwitchBuilder UsePin(YamlSecret pin) => SetConfigValue("pin", pin);

    public GpioSwitchBuilder UsePin(string pin, bool isSecret) => SetConfigValue("pin", pin, isSecret);

    public GpioSwitchBuilder WithName(string name) => SetConfigValue("name", name);

    public GpioSwitchBuilder WithName(YamlSecret name) => SetConfigValue("name", name);

    public GpioSwitchBuilder WithName(string name, bool isSecret) => SetConfigValue("name", name, isSecret);

    public GpioSwitchBuilder WithId(string id) => SetConfigValue("id", id);

    public GpioSwitchBuilder WithId(YamlSecret id) => SetConfigValue("id", id);

    public GpioSwitchBuilder WithId(string id, bool isSecret) => SetConfigValue("id", id, isSecret);

    public GpioSwitchBuilder WithIcon(string icon) => SetConfigValue("icon", icon);

    public GpioSwitchBuilder WithIcon(YamlSecret icon) => SetConfigValue("icon", icon);

    public GpioSwitchBuilder WithIcon(string icon, bool isSecret) => SetConfigValue("icon", icon, isSecret);

    public GpioSwitchBuilder WithCommentOn(string key, string comment)
    {
        if (_config.TryGetValue(key, out var node))
            node.Comment = comment;

        return this;
    }

    internal IYamlMapping Build()
    {
        if (!_config.ContainsKey("pin"))
        {
            throw new InvalidOperationException("A pin must be specified for the GPIO switch using UsePin().");
        }

        if (!_config.ContainsKey("name"))
        {
            throw new InvalidOperationException("A name must be specified for the GPIO switch using WithName().");
        }

        return _config;
    }
}