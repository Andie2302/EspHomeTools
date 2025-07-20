using System;
using EspHomeTools.Classes.Scalars;
using EspHomeTools.Classes.Structures;
using EspHomeTools.Interfaces;

namespace EspHomeTools.Builders;

public class MqttBuilder
{
    private readonly YamlMapping _block = new();

    public MqttBuilder WithBroker(string broker)
    {
        _block["broker"] = new YamlString(broker);
        return this;
    }

    public MqttBuilder WithBroker(YamlSecret broker)
    {
        _block["broker"] = broker;
        return this;
    }

    public MqttBuilder WithBroker(string broker, bool isSecret) => isSecret ? WithBroker(new YamlSecret(broker)) : WithBroker(broker);

    public MqttBuilder WithPort(int port)
    {
        _block["port"] = new YamlInt(port);
        return this;
    }

    public MqttBuilder WithUsername(string username)
    {
        _block["username"] = new YamlString(username);
        return this;
    }

    public MqttBuilder WithUsername(YamlSecret username)
    {
        _block["username"] = username;
        return this;
    }

    public MqttBuilder WithUsername(string username, bool isSecret) => isSecret ? WithUsername(new YamlSecret(username)) : WithUsername(username);

    public MqttBuilder WithPassword(string password)
    {
        _block["password"] = new YamlString(password);
        return this;
    }

    public MqttBuilder WithPassword(YamlSecret password)
    {
        _block["password"] = password;
        return this;
    }

    public MqttBuilder WithPassword(string password, bool isSecret) => isSecret ? WithPassword(new YamlSecret(password)) : WithPassword(password);

    public MqttBuilder WithClientId(string clientId)
    {
        _block["client_id"] = new YamlString(clientId);
        return this;
    }

    public MqttBuilder WithCommentOn(string key, string comment)
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