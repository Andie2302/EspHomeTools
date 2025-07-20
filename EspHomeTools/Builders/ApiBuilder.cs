using EspHomeTools.Classes.Scalars;
using EspHomeTools.Classes.Structures;
using EspHomeTools.Interfaces;

namespace EspHomeTools.Builders;

public class ApiBuilder
{
    private readonly YamlMapping _block = new();

    public ApiBuilder WithPassword(string password)
    {
        _block["password"] = new YamlString(password);
        return this;
    }

    public ApiBuilder WithPassword(YamlSecret password)
    {
        _block["password"] = password;
        return this;
    }

    public ApiBuilder WithPassword(string password, bool isSecret) => isSecret ? WithPassword(new YamlSecret(password)) : WithPassword(password);

    public ApiBuilder WithEncryptionKey(string key)
    {
        var encryptionMapping = new YamlMapping { { "key", new YamlString(key) } };
        _block["encryption"] = encryptionMapping;
        return this;
    }

    public ApiBuilder WithEncryptionKey(YamlSecret key)
    {
        var encryptionMapping = new YamlMapping { { "key", key } };
        _block["encryption"] = encryptionMapping;
        return this;
    }

    public ApiBuilder WithEncryptionKey(string key, bool isSecret) => isSecret ? WithEncryptionKey(new YamlSecret(key)) : WithEncryptionKey(key);

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
}