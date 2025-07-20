using System;
using EspHomeTools.Classes.Scalars;
using EspHomeTools.Classes.Structures;
using EspHomeTools.Interfaces;

namespace EspHomeTools.Builders;

public class AccessPointBuilder
{
    private readonly YamlMapping _block = new();

    public AccessPointBuilder WithSsid(string ssid)
    {
        _block["ssid"] = new YamlString(ssid);
        return this;
    }

    public AccessPointBuilder WithSsid(YamlSecret ssid)
    {
        _block["ssid"] = ssid;
        return this;
    }

    public AccessPointBuilder WithSsid(string ssid, bool isSecret) => isSecret ? WithSsid(new YamlSecret(ssid)) : WithSsid(ssid);

    public AccessPointBuilder WithPassword(string password)
    {
        _block["password"] = new YamlString(password);
        return this;
    }

    public AccessPointBuilder WithPassword(string password, bool isSecret) => isSecret ? WithPassword(new YamlSecret(password)) : WithPassword(password);

    public AccessPointBuilder WithPassword(YamlSecret password)
    {
        _block["password"] = password;
        return this;
    }

    public AccessPointBuilder WithCommentOn(string key, string comment)
    {
        if (_block.TryGetValue(key, out var node))
            node.Comment = comment;

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