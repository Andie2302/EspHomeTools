using System;
using EspHomeTools.Classes.Scalars;
using EspHomeTools.Classes.Structures;
using EspHomeTools.Interfaces;

namespace EspHomeTools.Builders;

public class DhtSensorBuilder
{
    private readonly YamlMapping _config = new();

    public DhtSensorBuilder()
    {
        _config["platform"] = new YamlString("dht");
    }

    public DhtSensorBuilder UsePin(string pin)
    {
        _config["pin"] = new YamlString(pin);
        return this;
    }

    public DhtSensorBuilder UsePin(YamlSecret pin)
    {
        _config["pin"] = pin;
        return this;
    }

    public DhtSensorBuilder UsePin(string pin, bool isSecret) => isSecret ? UsePin(new YamlSecret(pin)) : UsePin(pin);

    public DhtSensorBuilder WithTemperature(string name)
    {
        _config["temperature"] = new YamlMapping { { "name", new YamlString(name) } };
        return this;
    }

    public DhtSensorBuilder WithHumidity(string name)
    {
        _config["humidity"] = new YamlMapping { { "name", new YamlString(name) } };
        return this;
    }

    public DhtSensorBuilder WithUpdateInterval(string interval)
    {
        _config["update_interval"] = new YamlString(interval);
        return this;
    }

    public DhtSensorBuilder WithUpdateInterval(YamlSecret interval)
    {
        _config["update_interval"] = interval;
        return this;
    }

    public DhtSensorBuilder WithUpdateInterval(string interval, bool isSecret) => isSecret ? WithUpdateInterval(new YamlSecret(interval)) : WithUpdateInterval(interval);

    public DhtSensorBuilder WithModel(string model)
    {
        _config["model"] = new YamlString(model);
        return this;
    }

    public DhtSensorBuilder WithModel(YamlSecret model)
    {
        _config["model"] = model;
        return this;
    }

    public DhtSensorBuilder WithModel(string model, bool isSecret) => isSecret ? WithModel(new YamlSecret(model)) : WithModel(model);

    public DhtSensorBuilder WithCommentOn(string key, string comment)
    {
        if (_config.TryGetValue(key, out var node))
            node.Comment = comment;

        return this;
    }

    internal IYamlMapping Build()
    {
        if (!_config.ContainsKey("pin"))
        {
            throw new InvalidOperationException("Ein Pin muss für den DHT-Sensor mit UsePin() angegeben werden.");
        }

        if (!_config.ContainsKey("temperature") && !_config.ContainsKey("humidity"))
        {
            throw new InvalidOperationException("Für einen DHT-Sensor muss mindestens Temperatur (WithTemperature) oder Feuchtigkeit (WithHumidity) konfiguriert werden.");
        }

        return _config;
    }
}