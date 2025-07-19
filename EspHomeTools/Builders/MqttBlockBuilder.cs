using System;
using EspHomeTools.Classes.Scalars;
using EspHomeTools.Classes.Structures;
using EspHomeTools.Interfaces;

namespace EspHomeTools.Builders;

public class MqttBlockBuilder
{
    private readonly YamlMapping _block = new();

    public MqttBlockBuilder WithBroker(string broker)
    {
        _block["broker"] = new YamlString(broker);
        return this;
    }

    public MqttBlockBuilder WithBroker(YamlSecret broker)
    {
        _block["broker"] = broker;
        return this;
    }

    public MqttBlockBuilder WithBroker(string broker, bool isSecret) => isSecret ? WithBroker(new YamlSecret(broker)) : WithBroker(broker);

    public MqttBlockBuilder WithPort(int port)
    {
        _block["port"] = new YamlInt(port);
        return this;
    }

    public MqttBlockBuilder WithUsername(string username)
    {
        _block["username"] = new YamlString(username);
        return this;
    }

    public MqttBlockBuilder WithUsername(YamlSecret username)
    {
        _block["username"] = username;
        return this;
    }

    public MqttBlockBuilder WithUsername(string username, bool isSecret) => isSecret ? WithUsername(new YamlSecret(username)) : WithUsername(username);

    public MqttBlockBuilder WithPassword(string password)
    {
        _block["password"] = new YamlString(password);
        return this;
    }

    public MqttBlockBuilder WithPassword(YamlSecret password)
    {
        _block["password"] = password;
        return this;
    }

    public MqttBlockBuilder WithPassword(string password, bool isSecret) => isSecret ? WithPassword(new YamlSecret(password)) : WithPassword(password);

    public MqttBlockBuilder WithClientId(string clientId)
    {
        _block["client_id"] = new YamlString(clientId);
        return this;
    }

    public MqttBlockBuilder WithCommentOn(string key, string comment)
    {
        if (_block.TryGetValue(key, out var node))
            node.Comment = comment;

        return this;
    }

    internal IYamlMapping Build()
    {
        if (!_block.ContainsKey("broker"))
        {
            throw new InvalidOperationException("Ein Broker muss für den 'mqtt'-Block mit WithBroker() angegeben werden.");
        }

        return _block;
    }
}