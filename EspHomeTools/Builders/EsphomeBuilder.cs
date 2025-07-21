using System;
using EspHomeTools.Classes.Scalars;
using EspHomeTools.Classes.Structures;
using EspHomeTools.Interfaces;

namespace EspHomeTools.Builders;

public class EsphomeBuilder: IYamlBuilder<IYamlMapping>, IYamlBuilder
{
    private const string NameKey = "name";

    private const string OnBootKey = "on_boot";

    private readonly YamlMapping _block = new();

    public EsphomeBuilder WithName(string name)
    {
        ValidateNameInput(name);
        _block[NameKey] = new YamlString(name);
        return this;
    }

    public EsphomeBuilder WithName(YamlSecret name)
    {
        ValidateNameInput(name);
        _block[NameKey] = name;
        return this;
    }

    public EsphomeBuilder WithSecretName(string secretName)
    {
        ValidateNameInput(secretName);
        return WithName(new YamlSecret(secretName));
    }

    public EsphomeBuilder WithComment(string comment)
    {
        SetCommentOnKey(NameKey, comment);
        return this;
    }

    public EsphomeBuilder WithCommentOn(string key, string comment)
    {
        SetCommentOnKey(key, comment);
        return this;
    }

    public EsphomeBuilder OnBoot(Action<ActionSequenceBuilder> configurator)
    {
        ValidateInput(configurator, nameof(configurator));
        var builder = new ActionSequenceBuilder();
        configurator(builder);
        _block[OnBootKey] = builder.Build();
        return this;
    }

    public IYamlMapping Build()
    {
        ValidateRequiredFields();
        return _block;
    }

    private void SetCommentOnKey(string key, string comment)
    {
        if (_block.TryGetValue(key, out var node))
        {
            node.Comment = comment;
        }
    }

    private static void ValidateNameInput(object nameInput)
    {
        ValidateInput(nameInput, nameof(nameInput));
    }

    private static void ValidateInput(object input, string parameterName)
    {
        if (input == null)
        {
            throw new ArgumentNullException(parameterName);
        }
    }

    private void ValidateRequiredFields()
    {
        if (!_block.ContainsKey(NameKey))
        {
            throw new InvalidOperationException("The name in the 'esphome' block is required. Use the WithName() method.");
        }
    }
}