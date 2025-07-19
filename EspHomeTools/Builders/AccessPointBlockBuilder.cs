using System;
using EspHomeTools.Classes.Scalars;
using EspHomeTools.Classes.Structures;
using EspHomeTools.Interfaces;

namespace EspHomeTools.Builders;

public class AccessPointBlockBuilder
{
    private readonly YamlMapping _block = new();

    public AccessPointBlockBuilder WithSsid(string ssid)
    {
        _block["ssid"] = new YamlString(ssid);
        return this;
    }

    public AccessPointBlockBuilder WithSsid(YamlSecret ssid)
    {
        _block["ssid"] = ssid;
        return this;
    }

    public AccessPointBlockBuilder WithSsid(string ssid, bool isSecret) => isSecret ? WithSsid(new YamlSecret(ssid)) : WithSsid(ssid);

    public AccessPointBlockBuilder WithPassword(string password)
    {
        _block["password"] = new YamlString(password);
        return this;
    }

    public AccessPointBlockBuilder WithPassword(string password, bool isSecret) => isSecret ? WithPassword(new YamlSecret(password)) : WithPassword(password);

    public AccessPointBlockBuilder WithPassword(YamlSecret password)
    {
        _block["password"] = password;
        return this;
    }

    internal IYamlMapping Build()
    {
        if (!_block.ContainsKey("ssid"))
        {
            throw new InvalidOperationException("Die SSID für den Access Point (AP) ist erforderlich.");
        }

        return _block;
    }
}