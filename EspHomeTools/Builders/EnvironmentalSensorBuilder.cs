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
        // Set a common default, can be overwritten.
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
        var tempConfig = new YamlMapping { { "name", new YamlString(name) } };
        if (!string.IsNullOrEmpty(oversampling))
        {
            tempConfig["oversampling"] = new YamlString(oversampling);
        }

        _config["temperature"] = tempConfig;
        return this;
    }

    public EnvironmentalSensorBuilder WithPressure(string name, string oversampling = "16x")
    {
        var pressureConfig = new YamlMapping { { "name", new YamlString(name) } };
        if (!string.IsNullOrEmpty(oversampling))
        {
            pressureConfig["oversampling"] = new YamlString(oversampling);
        }

        _config["pressure"] = pressureConfig;
        return this;
    }

    public EnvironmentalSensorBuilder WithHumidity(string name, string oversampling = "16x")
    {
        var humidityConfig = new YamlMapping { { "name", new YamlString(name) } };
        if (!string.IsNullOrEmpty(oversampling))
        {
            humidityConfig["oversampling"] = new YamlString(oversampling);
        }

        _config["humidity"] = humidityConfig;
        return this;
    }

    public EnvironmentalSensorBuilder WithGasResistance(string name)
    {
        // Specific to BME680
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

    internal IYamlMapping Build()
    {
        if (!_config.ContainsKey("platform"))
        {
            throw new InvalidOperationException("Eine Platform muss für den Bosch-Sensor angegeben werden (z.B. 'bme280').");
        }

        if (!_config.ContainsKey("temperature") && !_config.ContainsKey("pressure") && !_config.ContainsKey("humidity"))
        {
            throw new InvalidOperationException("Es muss mindestens ein Messwert (Temperatur, Druck, Feuchtigkeit) für den Sensor konfiguriert werden.");
        }

        return _config;
    }
}