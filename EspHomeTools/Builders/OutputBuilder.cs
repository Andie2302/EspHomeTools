using System;
using EspHomeTools.Classes.Scalars;
using EspHomeTools.Classes.Structures;
using EspHomeTools.Interfaces;

namespace EspHomeTools.Builders;

public class OutputBuilder
{
    private const string PlatformKey = "platform";

    private const string PinKey = "pin";

    private const string IdKey = "id";

    private const string DefaultPlatform = "gpio";

    private readonly YamlMapping _config = new();

    public OutputBuilder()
    {
        _config[PlatformKey] = new YamlString(DefaultPlatform);
    }

    public OutputBuilder WithPlatform(string platform)
    {
        _config[PlatformKey] = new YamlString(platform);
        return this;
    }

    public OutputBuilder WithId(string id)
    {
        _config[IdKey] = new YamlString(id);
        return this;
    }

    public OutputBuilder UsePin(string pin)
    {
        SetPinValue(new YamlString(pin));
        return this;
    }

    public OutputBuilder UsePin(YamlSecret pin)
    {
        SetPinValue(pin);
        return this;
    }

    public OutputBuilder UsePin(string pin, bool isSecret)
    {
        return isSecret ? UsePin(new YamlSecret(pin)) : UsePin(pin);
    }

    public OutputBuilder WithCommentOn(string key, string comment)
    {
        if (_config.TryGetValue(key, out var node))
            node.Comment = comment;

        return this;
    }

    internal IYamlMapping Build()
    {
        ValidateRequiredFields();
        return _config;
    }

    private void SetPinValue(IYamlNode pinValue)
    {
        _config[PinKey] = pinValue;
    }

    private void ValidateRequiredFields()
    {
        if (!_config.ContainsKey(PinKey))
        {
            throw new InvalidOperationException("A pin must be specified for the 'output' component using UsePin().");
        }

        if (!_config.ContainsKey(IdKey))
        {
            throw new InvalidOperationException("An ID must be specified for the 'output' component using WithId() so other components can reference it.");
        }
    }
}