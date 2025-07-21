using System;
using EspHomeTools.Classes.Scalars;
using EspHomeTools.Classes.Structures;
using EspHomeTools.Interfaces;

namespace EspHomeTools.Builders;

public class BinarySensorBuilder: IYamlBuilder
{
    private const string PlatformKey = "platform";

    private const string PinKey = "pin";

    private const string NameKey = "name";

    private const string IdKey = "id";

    private const string DeviceClassKey = "device_class";

    private const string OnPressKey = "on_press";

    private const string OnReleaseKey = "on_release";

    private const string DefaultPlatform = "gpio";

    private readonly YamlMapping _config = new();

    public BinarySensorBuilder()
    {
        _config[PlatformKey] = new YamlString(DefaultPlatform);
    }

    public BinarySensorBuilder WithPlatform(string platform)
    {
        _config[PlatformKey] = new YamlString(platform);
        return this;
    }

    public BinarySensorBuilder UsePin(string pin)
    {
        _config[PinKey] = new YamlString(pin);
        return this;
    }

    public BinarySensorBuilder UsePin(YamlSecret pin)
    {
        _config[PinKey] = pin;
        return this;
    }

    public BinarySensorBuilder UsePin(string pin, bool isSecret) =>
        isSecret ? UsePin(new YamlSecret(pin)) : UsePin(pin);

    public BinarySensorBuilder WithName(string name)
    {
        _config[NameKey] = new YamlString(name);
        return this;
    }

    public BinarySensorBuilder WithName(YamlSecret name)
    {
        _config[NameKey] = name;
        return this;
    }

    public BinarySensorBuilder WithName(string name, bool isSecret) =>
        isSecret ? WithName(new YamlSecret(name)) : WithName(name);

    public BinarySensorBuilder WithId(string id)
    {
        _config[IdKey] = new YamlString(id);
        return this;
    }

    public BinarySensorBuilder WithDeviceClass(string deviceClass)
    {
        _config[DeviceClassKey] = new YamlString(deviceClass);
        return this;
    }

    public BinarySensorBuilder WithCommentOn(string key, string comment)
    {
        if (_config.TryGetValue(key, out var node))
            node.Comment = comment;

        return this;
    }

    public BinarySensorBuilder OnPress(Action<ActionSequenceBuilder> configurator)
    {
        ConfigureAction(OnPressKey, configurator);
        return this;
    }

    public BinarySensorBuilder OnRelease(Action<ActionSequenceBuilder> configurator)
    {
        ConfigureAction(OnReleaseKey, configurator);
        return this;
    }

    internal IYamlMapping Build()
    {
        ValidateRequiredPin();
        ValidateRequiredName();
        return _config;
    }

    private void ConfigureAction(string actionKey, Action<ActionSequenceBuilder> configurator)
    {
        var builder = new ActionSequenceBuilder();
        configurator(builder);
        _config[actionKey] = builder.Build();
    }

    private void ValidateRequiredPin()
    {
        if (!_config.ContainsKey(PinKey))
        {
            throw new InvalidOperationException("A pin must be specified for the Binary Sensor with UsePin().");
        }
    }

    private void ValidateRequiredName()
    {
        if (!_config.ContainsKey(NameKey))
        {
            throw new InvalidOperationException("A name must be specified for the Binary Sensor using WithName().");
        }
    }
}