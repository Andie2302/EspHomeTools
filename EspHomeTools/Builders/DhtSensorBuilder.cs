using System;
using EspHomeTools.Classes.Scalars;
using EspHomeTools.Classes.Structures;
using EspHomeTools.Interfaces;

namespace EspHomeTools.Builders;

public class DhtSensorBuilder: IYamlBuilder
{
    private const string PlatformKey = "platform";

    private const string PinKey = "pin";

    private const string TemperatureKey = "temperature";

    private const string HumidityKey = "humidity";

    private const string UpdateIntervalKey = "update_interval";

    private const string ModelKey = "model";

    private const string NameKey = "name";

    private const string DhtPlatform = "dht";

    private readonly YamlMapping _config = new();

    public DhtSensorBuilder()
    {
        _config[PlatformKey] = new YamlString(DhtPlatform);
    }

    public DhtSensorBuilder UsePin(string pin)
    {
        _config[PinKey] = CreateYamlNode(pin);
        return this;
    }

    public DhtSensorBuilder UsePin(YamlSecret pin)
    {
        _config[PinKey] = pin;
        return this;
    }

    public DhtSensorBuilder UsePin(string pin, bool isSecret) =>
        SetConfigValue(PinKey, pin, isSecret);

    public DhtSensorBuilder WithTemperature(string name)
    {
        _config[TemperatureKey] = new YamlMapping { { NameKey, CreateYamlNode(name) } };
        return this;
    }

    public DhtSensorBuilder WithHumidity(string name)
    {
        _config[HumidityKey] = new YamlMapping { { NameKey, CreateYamlNode(name) } };
        return this;
    }

    public DhtSensorBuilder WithUpdateInterval(string interval)
    {
        _config[UpdateIntervalKey] = CreateYamlNode(interval);
        return this;
    }

    public DhtSensorBuilder WithUpdateInterval(YamlSecret interval)
    {
        _config[UpdateIntervalKey] = interval;
        return this;
    }

    public DhtSensorBuilder WithUpdateInterval(string interval, bool isSecret) =>
        SetConfigValue(UpdateIntervalKey, interval, isSecret);

    public DhtSensorBuilder WithModel(string model)
    {
        _config[ModelKey] = CreateYamlNode(model);
        return this;
    }

    public DhtSensorBuilder WithModel(YamlSecret model)
    {
        _config[ModelKey] = model;
        return this;
    }

    public DhtSensorBuilder WithModel(string model, bool isSecret) =>
        SetConfigValue(ModelKey, model, isSecret);

    public DhtSensorBuilder WithCommentOn(string key, string comment)
    {
        if (_config.TryGetValue(key, out var node))
            node.Comment = comment;

        return this;
    }

    internal IYamlMapping Build()
    {
        if (!_config.ContainsKey(PinKey))
        {
            throw new InvalidOperationException("A pin must be specified for the DHT sensor using UsePin().");
        }

        if (!_config.ContainsKey(TemperatureKey) && !_config.ContainsKey(HumidityKey))
        {
            throw new InvalidOperationException("For a DHT sensor, at least temperature (WithTemperature) or humidity (WithHumidity) must be configured.");
        }

        return _config;
    }

    private static YamlString CreateYamlNode(string value) => new(value);

    private DhtSensorBuilder SetConfigValue(string key, string value, bool isSecret)
    {
        _config[key] = isSecret ? new YamlSecret(value) : CreateYamlNode(value);
        return this;
    }
}