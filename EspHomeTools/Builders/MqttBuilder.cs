using System;
using EspHomeTools.Classes.Scalars;
using EspHomeTools.Classes.Structures;
using EspHomeTools.Interfaces;

namespace EspHomeTools.Builders;

public class MqttBuilder: IYamlBuilder<IYamlMapping>, IYamlBuilder
{
    private readonly YamlMapping _block = new();

    public MqttBuilder WithBroker(string broker) => AddValueToBlock("broker", broker, false);

    public MqttBuilder WithBroker(YamlSecret broker) => AddValueToBlock("broker", broker);

    public MqttBuilder WithBroker(string broker, bool isSecret) => AddValueToBlock("broker", broker, isSecret);

    public MqttBuilder WithPort(int port)
    {
        _block["port"] = new YamlInt(port);
        return this;
    }

    public MqttBuilder WithUsername(string username) => AddValueToBlock("username", username, false);

    public MqttBuilder WithUsername(YamlSecret username) => AddValueToBlock("username", username);

    public MqttBuilder WithUsername(string username, bool isSecret) => AddValueToBlock("username", username, isSecret);

    public MqttBuilder WithPassword(string password) => AddValueToBlock("password", password, false);

    public MqttBuilder WithPassword(YamlSecret password) => AddValueToBlock("password", password);

    public MqttBuilder WithPassword(string password, bool isSecret) => AddValueToBlock("password", password, isSecret);

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

    public IYamlMapping Build()
    {
        if (!_block.ContainsKey("broker"))
        {
            throw new InvalidOperationException("A broker must be specified for the 'mqtt' block using WithBroker().");
        }

        return _block;
    }

    private MqttBuilder AddValueToBlock(string key, string value, bool isSecret)
    {
        _block[key] = isSecret ? new YamlSecret(value) : new YamlString(value);
        return this;
    }

    private MqttBuilder AddValueToBlock(string key, YamlSecret secret)
    {
        _block[key] = secret;
        return this;
    }
}