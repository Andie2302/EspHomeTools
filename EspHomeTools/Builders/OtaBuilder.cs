using System;
using EspHomeTools.Classes.Scalars;
using EspHomeTools.Classes.Structures;
using EspHomeTools.Interfaces;

namespace EspHomeTools.Builders;

public class OtaBuilder
{
    private readonly YamlMapping _block = new();

    public OtaBuilder()
    {
        // Set the default platform for OTA
        _block["platform"] = new YamlString("esphome");
    }

    public OtaBuilder WithPlatform(string platform)
    {
        _block["platform"] = new YamlString(platform);
        return this;
    }

    public OtaBuilder WithPassword(string password)
    {
        _block["password"] = new YamlString(password);
        return this;
    }

    public OtaBuilder WithPassword(YamlSecret password)
    {
        _block["password"] = password;
        return this;
    }

    public OtaBuilder WithPassword(string password, bool isSecret) => isSecret ? WithPassword(new YamlSecret(password)) : WithPassword(password);

    public OtaBuilder WithCommentOn(string key, string comment)
    {
        if (_block.TryGetValue(key, out var node))
            node.Comment = comment;

        return this;
    }

    internal IYamlMapping Build()
    {
        return _block;
    }
}