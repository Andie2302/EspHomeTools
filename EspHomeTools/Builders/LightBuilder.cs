using System;
using EspHomeTools.Classes.Scalars;
using EspHomeTools.Classes.Structures;
using EspHomeTools.Interfaces;

namespace EspHomeTools.Builders;

public class LightBuilder
{
    private const string PlatformKey = "platform";
    private const string NameKey = "name";
    private const string OutputKey = "output";
    private const string IdKey = "id";
    private const string DefaultPlatform = "monochromatic";

    private const string PlatformRequiredError = "A platform must be specified for the 'light' component.";
    private const string NameRequiredError = "A name must be specified for the 'light' component with WithName().";
    private const string OutputRequiredError = "An output must be specified for the 'light' component with UseOutput().";

    private readonly YamlMapping _config = new();

    public LightBuilder()
    {
        _config[PlatformKey] = new YamlString(DefaultPlatform);
    }

    public LightBuilder WithPlatform(string platform)
    {
        _config[PlatformKey] = new YamlString(platform);
        return this;
    }

    public LightBuilder WithName(string name)
    {
        _config[NameKey] = new YamlString(name);
        return this;
    }

    public LightBuilder WithName(YamlSecret name)
    {
        _config[NameKey] = name;
        return this;
    }

    public LightBuilder WithName(string name, bool isSecret) =>
        isSecret ? WithName(new YamlSecret(name)) : WithName(name);

    public LightBuilder WithId(string id)
    {
        _config[IdKey] = new YamlString(id);
        return this;
    }

    public LightBuilder UseOutput(string outputId)
    {
        _config[OutputKey] = new YamlString(outputId);
        return this;
    }

    public LightBuilder WithCommentOn(string key, string comment)
    {
        if (_config.TryGetValue(key, out var node))
            node.Comment = comment;

        return this;
    }

    internal IYamlMapping Build()
    {
        ValidateRequiredFields();
        return _config;
    }

    private void ValidateRequiredFields()
    {
        ValidatePlatform();
        ValidateName();
        ValidateOutput();
    }

    private void ValidatePlatform()
    {
        if (!_config.ContainsKey(PlatformKey))
        {
            throw new InvalidOperationException(PlatformRequiredError);
        }
    }

    private void ValidateName()
    {
        if (!_config.ContainsKey(NameKey))
        {
            throw new InvalidOperationException(NameRequiredError);
        }
    }

    private void ValidateOutput()
    {
        if (!_config.ContainsKey(OutputKey))
        {
            throw new InvalidOperationException(OutputRequiredError);
        }
    }
}