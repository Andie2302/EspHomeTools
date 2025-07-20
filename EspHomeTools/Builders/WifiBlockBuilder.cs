using System;
using EspHomeTools.Classes.Scalars;
using EspHomeTools.Classes.Structures;
using EspHomeTools.Interfaces;

namespace EspHomeTools.Builders;

public class WifiBlockBuilder
{
    private readonly YamlMapping _block = new();

    public WifiBlockBuilder WithSsid(string ssid)
    {
        _block["ssid"] = new YamlString(ssid);
        return this;
    }

    public WifiBlockBuilder WithSsid(YamlSecret ssid)
    {
        _block["ssid"] = ssid;
        return this;
    }

    public WifiBlockBuilder WithSsid(string ssid, bool isSecret) => isSecret ? WithSsid(new YamlSecret(ssid)) : WithSsid(ssid);

    public WifiBlockBuilder WithPassword(string password)
    {
        _block["password"] = new YamlString(password);
        return this;
    }

    public WifiBlockBuilder WithPassword(YamlSecret password)
    {
        _block["password"] = password;
        return this;
    }

    public WifiBlockBuilder WithPassword(string password, bool isSecret) => isSecret ? WithPassword(new YamlSecret(password)) : WithPassword(password);

    public WifiBlockBuilder WithAccessPoint(Action<AccessPointBlockBuilder> configurator)
    {
        var builder = new AccessPointBlockBuilder();
        configurator(builder);
        _block["ap"] = builder.Build();
        return this;
    }

    public WifiBlockBuilder WithCommentOn(string key, string comment)
    {
        if (_block.TryGetValue(key, out var node))
            node.Comment = comment;

        return this;
    }

    internal IYamlMapping Build()
    {
        if (!_block.ContainsKey("ssid") || !_block.ContainsKey("password"))
        {
            throw new InvalidOperationException("SSID und Passwort sind im 'wifi'-Block erforderlich.");
        }

        return _block;
    }
}
public class TimeBlockBuilder
{
    private readonly YamlMapping _config = new();

    public TimeBlockBuilder WithPlatform(string platform)
    {
        _config["platform"] = new YamlString(platform);
        return this;
    }

    public TimeBlockBuilder WithId(string id)
    {
        _config["id"] = new YamlString(id);
        return this;
    }

    public TimeBlockBuilder WithTimezone(string timezone)
    {
        _config["timezone"] = new YamlString(timezone);
        return this;
    }

    public TimeBlockBuilder WithTimezone(YamlSecret timezone)
    {
        _config["timezone"] = timezone;
        return this;
    }

    public TimeBlockBuilder WithTimezone(string timezone, bool isSecret) => isSecret ? WithTimezone(new YamlSecret(timezone)) : WithTimezone(timezone);

    public TimeBlockBuilder WithServers(params string[] servers)
    {
        var serverSequence = new YamlSequence();
        foreach (var server in servers)
        {
            serverSequence.Add(new YamlString(server));
        }

        _config["servers"] = serverSequence;
        return this;
    }

    public TimeBlockBuilder WithCommentOn(string key, string comment)
    {
        if (_config.TryGetValue(key, out var node))
            node.Comment = comment;

        return this;
    }

    internal IYamlMapping Build()
    {
        if (!_config.ContainsKey("platform"))
        {
            throw new InvalidOperationException("Eine Platform muss für die 'time'-Komponente mit WithPlatform() angegeben werden (z.B. 'homeassistant' oder 'sntp').");
        }

        return _config;
    }
}
public class OutputBuilder
{
    private readonly YamlMapping _config = new();

    public OutputBuilder()
    {
        // Default platform is gpio
        _config["platform"] = new YamlString("gpio");
    }

    public OutputBuilder WithPlatform(string platform)
    {
        _config["platform"] = new YamlString(platform);
        return this;
    }

    public OutputBuilder UsePin(string pin)
    {
        _config["pin"] = new YamlString(pin);
        return this;
    }

    public OutputBuilder UsePin(YamlSecret pin)
    {
        _config["pin"] = pin;
        return this;
    }

    public OutputBuilder UsePin(string pin, bool isSecret) => isSecret ? UsePin(new YamlSecret(pin)) : UsePin(pin);

    public OutputBuilder WithId(string id)
    {
        _config["id"] = new YamlString(id);
        return this;
    }

    public OutputBuilder WithCommentOn(string key, string comment)
    {
        if (_config.TryGetValue(key, out var node))
            node.Comment = comment;

        return this;
    }

    internal IYamlMapping Build()
    {
        if (!_config.ContainsKey("pin"))
        {
            throw new InvalidOperationException("Ein Pin muss für die 'output'-Komponente mit UsePin() angegeben werden.");
        }

        if (!_config.ContainsKey("id"))
        {
            throw new InvalidOperationException("Eine ID muss für die 'output'-Komponente mit WithId() angegeben werden, damit andere Komponenten darauf verweisen können.");
        }

        return _config;
    }
}
public class LightBlockBuilder
{
    private readonly YamlMapping _config = new();

    public LightBlockBuilder()
    {
        // Default platform for a simple dimmable light
        _config["platform"] = new YamlString("monochromatic");
    }

    public LightBlockBuilder WithPlatform(string platform)
    {
        _config["platform"] = new YamlString(platform);
        return this;
    }

    public LightBlockBuilder WithName(string name)
    {
        _config["name"] = new YamlString(name);
        return this;
    }

    public LightBlockBuilder WithName(YamlSecret name)
    {
        _config["name"] = name;
        return this;
    }

    public LightBlockBuilder WithName(string name, bool isSecret) => isSecret ? WithName(new YamlSecret(name)) : WithName(name);

    public LightBlockBuilder WithId(string id)
    {
        _config["id"] = new YamlString(id);
        return this;
    }

    public LightBlockBuilder UseOutput(string outputId)
    {
        _config["output"] = new YamlString(outputId);
        return this;
    }

    public LightBlockBuilder WithCommentOn(string key, string comment)
    {
        if (_config.TryGetValue(key, out var node))
            node.Comment = comment;

        return this;
    }

    internal IYamlMapping Build()
    {
        if (!_config.ContainsKey("platform"))
        {
            throw new InvalidOperationException("Eine Platform muss für die 'light'-Komponente angegeben werden.");
        }

        if (!_config.ContainsKey("name"))
        {
            throw new InvalidOperationException("Ein Name muss für die 'light'-Komponente mit WithName() angegeben werden.");
        }

        if (!_config.ContainsKey("output"))
        {
            throw new InvalidOperationException("Ein Output muss für die 'light'-Komponente mit UseOutput() angegeben werden.");
        }

        return _config;
    }
}
public class I2CBlockBuilder
{
    private readonly YamlMapping _config = new();

    public I2CBlockBuilder SetSdaPin(string pin)
    {
        _config["sda"] = new YamlString(pin);
        return this;
    }

    public I2CBlockBuilder SetSclPin(string pin)
    {
        _config["scl"] = new YamlString(pin);
        return this;
    }

    public I2CBlockBuilder WithScan(bool scan)
    {
        _config["scan"] = new YamlBool(scan);
        return this;
    }

    public I2CBlockBuilder WithId(string id)
    {
        _config["id"] = new YamlString(id);
        return this;
    }

    public I2CBlockBuilder WithFrequency(string frequency)
    {
        _config["frequency"] = new YamlString(frequency);
        return this;
    }

    public I2CBlockBuilder WithCommentOn(string key, string comment)
    {
        if (_config.TryGetValue(key, out var node))
            node.Comment = comment;

        return this;
    }

    internal IYamlMapping Build()
    {
        // SDA and SCL are often optional as ESPHome can use board defaults,
        // so we don't enforce their presence with an exception.
        // An ID is highly recommended if other components will use this bus.
        if (!_config.ContainsKey("id"))
        {
            Console.WriteLine("Warning: I2C bus created without an ID. Other components might not be able to use it.");
        }

        return _config;
    }
}