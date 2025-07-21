using System;
using EspHomeTools.Classes.Scalars;
using EspHomeTools.Classes.Structures;
using EspHomeTools.Interfaces;

namespace EspHomeTools.Builders;

public class I2CBuilder: IYamlBuilder<IYamlMapping>, IYamlBuilder
{
    private const string SdaKey = "sda";

    private const string SclKey = "scl";

    private const string ScanKey = "scan";

    private const string IdKey = "id";

    private const string FrequencyKey = "frequency";

    private readonly YamlMapping _config = new();

    public I2CBuilder SetSdaPin(string pin)
    {
        SetConfigValue(SdaKey, new YamlString(pin));
        return this;
    }

    public I2CBuilder SetSclPin(string pin)
    {
        SetConfigValue(SclKey, new YamlString(pin));
        return this;
    }

    public I2CBuilder WithScan(bool scan)
    {
        SetConfigValue(ScanKey, new YamlBool(scan));
        return this;
    }

    public I2CBuilder WithId(string id)
    {
        SetConfigValue(IdKey, new YamlString(id));
        return this;
    }

    public I2CBuilder WithFrequency(string frequency)
    {
        SetConfigValue(FrequencyKey, new YamlString(frequency));
        return this;
    }

    public I2CBuilder WithCommentOn(string key, string comment)
    {
        if (_config.TryGetValue(key, out var node))
            node.Comment = comment;

        return this;
    }

    public IYamlMapping Build()
    {
        if (!_config.ContainsKey(IdKey))
        {
            Console.WriteLine("Warning: I2C bus created without an ID. Other components might not be able to use it.");
        }

        return _config;
    }

    private void SetConfigValue(string key, IYamlNode value)
    {
        _config[key] = value;
    }
}