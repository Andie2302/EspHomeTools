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

    public WifiBlockBuilder WithPassword(string password)
    {
        _block["password"] = new YamlString(password);
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
    public WifiBlockBuilder WithPassword(YamlSecret password)
    {
        _block["password"] = password;
        return this;
    }
}