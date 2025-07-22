using System;
using EspHomeTools.Classes.Scalars;
using EspHomeTools.Classes.Structures;
using EspHomeTools.Interfaces;

namespace EspHomeTools.Builders;


public interface IWithSsid
{
    AccessPointBuilder WithSsid(string ssid);
    AccessPointBuilder WithSsid(YamlSecret ssid);
    AccessPointBuilder WithSsid(string ssid, bool isSecret);
}

public class AccessPointBuilder : IYamlBuilder<IYamlMapping>
{
    private const string SsidKey = "ssid";

    private const string PasswordKey = "password";

    private readonly YamlMapping _block = new();

    public AccessPointBuilder WithSsid(string ssid)
    {
        SetValue(SsidKey, ssid, false);
        return this;
    }

    public AccessPointBuilder WithSsid(YamlSecret ssid)
    {
        _block[SsidKey] = ssid;
        return this;
    }

    public AccessPointBuilder WithSsid(string ssid, bool isSecret)
    {
        SetValue(SsidKey, ssid, isSecret);
        return this;
    }

    public AccessPointBuilder WithPassword(string password)
    {
        SetValue(PasswordKey, password, false);
        return this;
    }

    public AccessPointBuilder WithPassword(string password, bool isSecret)
    {
        SetValue(PasswordKey, password, isSecret);
        return this;
    }

    public AccessPointBuilder WithPassword(YamlSecret password)
    {
        _block[PasswordKey] = password;
        return this;
    }

    public AccessPointBuilder AddComment(string key, string comment)
    {
        if (_block.TryGetValue(key, out var node))
            node.Comment = comment;

        return this;
    }

    public IYamlMapping Build()
    {
        if (!_block.ContainsKey(SsidKey))
        {
            throw new InvalidOperationException("The SSID for the Access Point (AP) is required.");
        }

        return _block;
    }

    private void SetValue(string key, string value, bool isSecret) => _block[key] = isSecret ? new YamlSecret(value) : new YamlString(value);
}