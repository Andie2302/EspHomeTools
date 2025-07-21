using System;
using EspHomeTools.Classes.Scalars;
using EspHomeTools.Classes.Structures;
using EspHomeTools.Interfaces;

namespace EspHomeTools.Builders;

public class OtaBuilder: IYamlBuilder<IYamlMapping>
{
    private const string DefaultPlatform = "esphome";

    private readonly YamlMapping _block = new();

    public OtaBuilder()
    {
        SetNodeValue("platform", DefaultPlatform);
    }

    public OtaBuilder WithPlatform(string platform)
    {
        SetNodeValue("platform", platform);
        return this;
    }

    public OtaBuilder WithPassword(string password)
    {
        SetNodeValue("password", password);
        return this;
    }

    public OtaBuilder WithPassword(YamlSecret password)
    {
        _block["password"] = password;
        return this;
    }

    public OtaBuilder WithPassword(string password, bool isSecret) => isSecret ? WithPassword(new YamlSecret(password)) : WithPassword(password);

    public OtaBuilder WithComment(string key, string comment)
    {
        if (_block.TryGetValue(key, out var node))
            node.Comment = comment;

        return this;
    }

    public IYamlMapping Build()
    {
        return _block;
    }

    private void SetNodeValue(string key, string value)
    {
        _block[key] = new YamlString(value);
    }
}