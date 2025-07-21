using System;
using EspHomeTools.Classes.Scalars;
using EspHomeTools.Classes.Structures;
using EspHomeTools.Interfaces;

namespace EspHomeTools.Builders;

public class EnvironmentalSensorBuilder
{
    private readonly YamlMapping _config = new();

    public EnvironmentalSensorBuilder()
    {
        _config["platform"] = new YamlString("bme280");
    }

    public EnvironmentalSensorBuilder WithPlatform(string platform)
    {
        _config["platform"] = new YamlString(platform);
        return this;
    }

    public EnvironmentalSensorBuilder WithI2CAddress(int address)
    {
        _config["address"] = new YamlInt(address);
        return this;
    }

    public EnvironmentalSensorBuilder WithTemperature(string name, string oversampling = "16x")
    {
        _config["temperature"] = CreateSensorConfig(name, oversampling);
        return this;
    }

    public EnvironmentalSensorBuilder WithPressure(string name, string oversampling = "16x")
    {
        _config["pressure"] = CreateSensorConfig(name, oversampling);
        return this;
    }

    public EnvironmentalSensorBuilder WithHumidity(string name, string oversampling = "16x")
    {
        _config["humidity"] = CreateSensorConfig(name, oversampling);
        return this;
    }

    public EnvironmentalSensorBuilder WithGasResistance(string name)
    {
        _config["gas_resistance"] = new YamlMapping { { "name", new YamlString(name) } };
        return this;
    }

    public EnvironmentalSensorBuilder WithUpdateInterval(string interval)
    {
        _config["update_interval"] = new YamlString(interval);
        return this;
    }

    public EnvironmentalSensorBuilder WithCommentOn(string key, string comment)
    {
        if (_config.TryGetValue(key, out var node))
            node.Comment = comment;

        return this;
    }

    private static YamlMapping CreateSensorConfig(string name, string oversampling)
    {
        var sensorConfig = new YamlMapping { { "name", new YamlString(name) } };
        if (!string.IsNullOrEmpty(oversampling))
        {
            sensorConfig["oversampling"] = new YamlString(oversampling);
        }

        return sensorConfig;
    }

    internal IYamlMapping Build()
    {
        if (!_config.ContainsKey("platform"))
        {
            throw new InvalidOperationException("A platform must be specified for the Bosch sensor (e.g. 'bme280').");
        }

        if (!_config.ContainsKey("temperature") && !_config.ContainsKey("pressure") && !_config.ContainsKey("humidity"))
        {
            throw new InvalidOperationException("At least one measurement value (temperature, pressure, humidity) must be configured for the sensor.");
        }

        return _config;
    }
}