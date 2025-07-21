using EspHomeTools.Classes.Scalars;
using EspHomeTools.Classes.Structures;
using EspHomeTools.Interfaces;

namespace EspHomeTools.Builders;

public class ApiBuilder
{
    private readonly YamlMapping _block = new();

    public ApiBuilder WithPassword(string password)
    {
        _block["password"] = CreateYamlNode(password, false);
        return this;
    }

    public ApiBuilder WithPassword(YamlSecret password)
    {
        _block["password"] = password;
        return this;
    }

    public ApiBuilder WithPassword(string password, bool isSecret)
    {
        _block["password"] = CreateYamlNode(password, isSecret);
        return this;
    }

    public ApiBuilder WithEncryptionKey(string key)
    {
        _block["encryption"] = CreateEncryptionMapping(key, false);
        return this;
    }

    public ApiBuilder WithEncryptionKey(YamlSecret key)
    {
        _block["encryption"] = CreateEncryptionMapping(key);
        return this;
    }

    public ApiBuilder WithEncryptionKey(string key, bool isSecret)
    {
        _block["encryption"] = CreateEncryptionMapping(key, isSecret);
        return this;
    }

    public ApiBuilder WithCommentOn(string key, string comment)
    {
        if (_block.TryGetValue(key, out var node))
            node.Comment = comment;

        return this;
    }

    internal IYamlMapping Build()
    {
        return _block;
    }

    private static IYamlNode CreateYamlNode(string value, bool isSecret)
    {
        return isSecret ? new YamlSecret(value) : new YamlString(value);
    }

    private static YamlMapping CreateEncryptionMapping(string key, bool isSecret)
    {
        return new YamlMapping { { "key", CreateYamlNode(key, isSecret) } };
    }

    private static YamlMapping CreateEncryptionMapping(YamlSecret key)
    {
        return new YamlMapping { { "key", key } };
    }
}