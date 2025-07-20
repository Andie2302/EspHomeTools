using EspHomeTools.Classes.Scalars;
using EspHomeTools.Classes.Structures;
using EspHomeTools.Interfaces;

namespace EspHomeTools.Builders;

public class OtaBlockBuilder
{
    private readonly YamlMapping _block = new();

    public OtaBlockBuilder WithPassword(string password)
    {
        _block["password"] = new YamlString(password);
        return this;
    }

    public OtaBlockBuilder WithPassword(YamlSecret password)
    {
        _block["password"] = password;
        return this;
    }

    public OtaBlockBuilder WithPassword(string password, bool isSecret) => isSecret ? WithPassword(new YamlSecret(password)) : WithPassword(password);

    public OtaBlockBuilder WithCommentOn(string key, string comment)
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