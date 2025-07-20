using EspHomeTools.Classes.Scalars;
using EspHomeTools.Classes.Structures;
using EspHomeTools.Interfaces;

namespace EspHomeTools.Builders;

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