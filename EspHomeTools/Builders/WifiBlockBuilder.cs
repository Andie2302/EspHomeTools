using System;
using EspHomeTools.Classes.Scalars;
using EspHomeTools.Classes.Structures;
using EspHomeTools.Interfaces;

namespace EspHomeTools.Builders;

public class WifiBlockBuilder
{
    private const string SsidKey = "ssid";
    private const string PasswordKey = "password";
    private const string AccessPointKey = "ap";
    private readonly YamlMapping _block = new();
    public WifiBlockBuilder WithSsid(string ssid)
    {
        SetValue(SsidKey, ssid, false);
        return this;
    }
    public WifiBlockBuilder WithSsid(YamlSecret ssid)
    {
        _block[SsidKey] = ssid;
        return this;
    }
    public WifiBlockBuilder WithSsid(string ssid, bool isSecret)
    {
        SetValue(SsidKey, ssid, isSecret);
        return this;
    }
    public WifiBlockBuilder WithPassword(string password)
    {
        SetValue(PasswordKey, password, false);
        return this;
    }
    public WifiBlockBuilder WithPassword(YamlSecret password)
    {
        _block[PasswordKey] = password;
        return this;
    }
    public WifiBlockBuilder WithPassword(string password, bool isSecret)
    {
        SetValue(PasswordKey, password, isSecret);
        return this;
    }
    public WifiBlockBuilder WithAccessPoint(Action<AccessPointBuilder> configurator)
    {
        var builder = new AccessPointBuilder();
        configurator(builder);
        _block[AccessPointKey] = builder.Build();
        return this;
    }
    public WifiBlockBuilder AddComment(string key, string comment)
    {
        if (_block.TryGetValue(key, out var node))
            node.Comment = comment;

        return this;
    }
    internal IYamlMapping Build()
    {
        if (!_block.ContainsKey(SsidKey) || !_block.ContainsKey(PasswordKey))
        {
            throw new InvalidOperationException("SSID and password are required in the 'wifi' block.");
        }

        return _block;
    }
    private void SetValue(string key, string value, bool isSecret) => _block[key] = isSecret ? new YamlSecret(value) : new YamlString(value);
}