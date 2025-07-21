using EspHomeTools.Classes.Scalars;
using EspHomeTools.Classes.Structures;
using EspHomeTools.Interfaces;

namespace EspHomeTools.Builders;

public class ActionSequenceBuilder: IYamlBuilder
{
    private const string LightToggleAction = "light.toggle";

    private const string LightTurnOnAction = "light.turn_on";

    private const string LightTurnOffAction = "light.turn_off";

    private const string SwitchToggleAction = "switch.toggle";

    private const string SwitchTurnOnAction = "switch.turn_on";

    private const string SwitchTurnOffAction = "switch.turn_off";

    private const string DelayAction = "delay";

    private const string LambdaNodeName = "lambda";

    private readonly YamlSequence _sequence = [];

    private ActionSequenceBuilder AddSimpleAction(string actionKey, string entityId)
    {
        var mapping = new YamlMapping { { actionKey, new YamlString(entityId) } };
        _sequence.Add(mapping);
        return this;
    }

    public ActionSequenceBuilder LightToggle(string lightId) => AddSimpleAction(LightToggleAction, lightId);

    public ActionSequenceBuilder LightTurnOn(string lightId) => AddSimpleAction(LightTurnOnAction, lightId);

    public ActionSequenceBuilder LightTurnOff(string lightId) => AddSimpleAction(LightTurnOffAction, lightId);

    public ActionSequenceBuilder SwitchToggle(string switchId) => AddSimpleAction(SwitchToggleAction, switchId);

    public ActionSequenceBuilder SwitchTurnOn(string switchId) => AddSimpleAction(SwitchTurnOnAction, switchId);

    public ActionSequenceBuilder SwitchTurnOff(string switchId) => AddSimpleAction(SwitchTurnOffAction, switchId);

    public ActionSequenceBuilder Delay(string delay) => AddSimpleAction(DelayAction, delay);

    public ActionSequenceBuilder Lambda(string cppLambdaCode)
    {
        var lambdaNode = new YamlLambda(cppLambdaCode) { Name = LambdaNodeName };
        _sequence.Add(lambdaNode);
        return this;
    }

    public ActionSequenceBuilder AddAction(IYamlNode actionNode)
    {
        _sequence.Add(actionNode);
        return this;
    }

    internal IYamlSequence Build() => _sequence;
}