using System;
using System.Text;
using EspHomeTools.Classes.Scalars;
using EspHomeTools.Classes.Structures;
using EspHomeTools.Interfaces;

namespace EspHomeTools.Builders;

public class WifiBlockBuilder
{
    private readonly YamlMapping _block = new();

    public WifiBlockBuilder WithSsid(string ssid)
    {
        _block["ssid"] = new YamlString(ssid);
        return this;
    }

    public WifiBlockBuilder WithSsid(YamlSecret ssid)
    {
        _block["ssid"] = ssid;
        return this;
    }

    public WifiBlockBuilder WithSsid(string ssid, bool isSecret) => isSecret ? WithSsid(new YamlSecret(ssid)) : WithSsid(ssid);

    public WifiBlockBuilder WithPassword(string password)
    {
        _block["password"] = new YamlString(password);
        return this;
    }

    public WifiBlockBuilder WithPassword(YamlSecret password)
    {
        _block["password"] = password;
        return this;
    }

    public WifiBlockBuilder WithPassword(string password, bool isSecret) => isSecret ? WithPassword(new YamlSecret(password)) : WithPassword(password);

    public WifiBlockBuilder WithAccessPoint(Action<AccessPointBlockBuilder> configurator)
    {
        var builder = new AccessPointBlockBuilder();
        configurator(builder);
        _block["ap"] = builder.Build();
        return this;
    }

    public WifiBlockBuilder WithCommentOn(string key, string comment)
    {
        if (_block.TryGetValue(key, out var node))
            node.Comment = comment;

        return this;
    }

    internal IYamlMapping Build()
    {
        if (!_block.ContainsKey("ssid") || !_block.ContainsKey("password"))
        {
            throw new InvalidOperationException("SSID und Passwort sind im 'wifi'-Block erforderlich.");
        }

        return _block;
    }
}



/// <summary>
/// A fluent builder for creating a sequence of actions for ESPHome automations.
/// </summary>
public class ActionSequenceBuilder
{
    private readonly YamlSequence _sequence = new();

    /// <summary>
    /// Adds a simple key-value action to the sequence.
    /// </summary>
    /// <param name="key">The action key (e.g., "light.toggle").</param>
    /// <param name="value">The action value (e.g., the ID of the light).</param>
    private ActionSequenceBuilder AddSimpleAction(string key, string value)
    {
        var mapping = new YamlMapping { { key, new YamlString(value) } };
        _sequence.Add(mapping);
        return this;
    }

    // --- Common Pre-defined Actions ---

    public ActionSequenceBuilder LightToggle(string lightId) => AddSimpleAction("light.toggle", lightId);
    public ActionSequenceBuilder LightTurnOn(string lightId) => AddSimpleAction("light.turn_on", lightId);
    public ActionSequenceBuilder LightTurnOff(string lightId) => AddSimpleAction("light.turn_off", lightId);
    public ActionSequenceBuilder SwitchToggle(string switchId) => AddSimpleAction("switch.toggle", switchId);
    public ActionSequenceBuilder SwitchTurnOn(string switchId) => AddSimpleAction("switch.turn_on", switchId);
    public ActionSequenceBuilder SwitchTurnOff(string switchId) => AddSimpleAction("switch.turn_off", switchId);
    public ActionSequenceBuilder Delay(string delay) => AddSimpleAction("delay", delay);

    /// <summary>
    /// Adds a multi-line lambda action.
    /// </summary>
    /// <param name="csharpLambda">The C++ code to execute in the lambda.</param>
    public ActionSequenceBuilder Lambda(string csharpLambda)
    {
        // A lambda is a special mapping node where the key is "lambda"
        var lambdaNode = new YamlLambda(csharpLambda) { Name = "lambda" };
        _sequence.Add(lambdaNode);
        return this;
    }

    /// <summary>
    /// Adds a generic, custom action node.
    /// </summary>
    public ActionSequenceBuilder AddAction(IYamlNode actionNode)
    {
        _sequence.Add(actionNode);
        return this;
    }

    internal IYamlSequence Build() => _sequence;
}


/// <summary>
/// Represents a YAML scalar that is serialized as a multi-line literal block (using |-).
/// This is primarily used for lambda scripts in ESPHome.
/// </summary>
public class YamlLambda : YamlScalar<string>
{
    public YamlLambda(string value)
    {
        Value = value;
    }

    /// <summary>
    /// Overrides the default serialization to produce a YAML literal block style.
    /// </summary>
    public override string ToYaml(int indent = 0)
    {
        var sb = new StringBuilder();
        var prefix = new string(' ', indent);

        // Handle comments if they exist
        if (!string.IsNullOrWhiteSpace(Comment))
        {
            var commentLines = Comment?.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            foreach (var line in commentLines)
            {
                sb.Append(prefix).Append("# ").AppendLine(line);
            }
        }

        // Append the key (e.g., "lambda:") and the literal block indicator
        sb.Append(prefix);
        if (!string.IsNullOrWhiteSpace(Name))
        {
            sb.Append(Name).Append(':');
        }
        sb.AppendLine(" |-");

        // Append the indented code lines
        var codeLines = (Value ?? string.Empty).Trim().Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
        var codeIndent = new string(' ', indent + 2);
        foreach (var line in codeLines)
        {
            sb.Append(codeIndent).AppendLine(line);
        }

        return sb.ToString().TrimEnd('\r', '\n', ' ');
    }

    protected override string SerializeValue()
    {
        // This method is not used because ToYaml is fully overridden.
        throw new NotImplementedException();
    }
}