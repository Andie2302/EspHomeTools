using EspHomeTools.Classes.Scalars;
using EspHomeTools.Classes.Structures;
using EspHomeTools.Interfaces;

namespace EspHomeTools.Builders;

/// <summary>
/// Provides a fluent interface for building action sequences in ESPHome automations.
/// </summary>
/// <remarks>
/// This builder allows the creation and customization of action sequences through predefined
/// methods such as toggling lights or switches, delaying execution, adding lambda functions,
/// and including custom action nodes.
/// </remarks>
/// <example>
/// Use the available methods to define and chain actions. Once all desired actions are added,
/// the sequence can be built for further usage in ESPHome configurations.
/// </example>
public class ActionSequenceBuilder
{
    /// <summary>
    /// Represents a constant used to define the "light.toggle" action key
    /// within the context of ESPHome automation sequences. This action is
    /// used to toggle the state of a specified light between on and off.
    /// </summary>
    private const string LightToggleAction = "light.toggle";

    /// <summary>
    /// Represents the action key for turning on a light in ESPHome automation sequences.
    /// </summary>
    /// <remarks>
    /// This constant is used within the ActionSequenceBuilder to specify the "light.turn_on" action
    /// in automation sequences for controlling light entities.
    /// </remarks>
    private const string LightTurnOnAction = "light.turn_on";

    /// <summary>
    /// Identifier for the "light.turn_off" action used to define
    /// an action within the ESPHome automation sequence for turning off a light.
    /// </summary>
    private const string LightTurnOffAction = "light.turn_off";

    /// <summary>
    /// A constant string representing the key for toggling a switch action in an ESPHome automation sequence.
    /// </summary>
    private const string SwitchToggleAction = "switch.toggle";

    /// <summary>
    /// Represents the constant string key used to define the "switch.turn_on" action
    /// for turning on a switch in ESPHome automations. This key is used when
    /// constructing sequences of commands for automating switches.
    /// </summary>
    private const string SwitchTurnOnAction = "switch.turn_on";

    /// <summary>
    /// Represents the action key used to turn off a switch in ESPHome automations.
    /// </summary>
    private const string SwitchTurnOffAction = "switch.turn_off";

    /// <summary>
    /// Represents the identifier for the "delay" action within the <see cref="ActionSequenceBuilder"/> class.
    /// This value is used to define a delay action as part of a sequence in ESPHome automations.
    /// </summary>
    private const string DelayAction = "delay";

    /// <summary>
    /// The constant string representing the name of the lambda node
    /// used in ESPHome action sequences for integrating custom C++
    /// lambda functions into the automation workflow.
    /// </summary>
    private const string LambdaNodeName = "lambda";

    /// <summary>
    /// A private instance field that stores the sequence of actions being constructed
    /// within the <see cref="ActionSequenceBuilder"/>. It is implemented as a
    /// <see cref="YamlSequence"/> which allows for the organization and serialization
    /// of the actions in a YAML-compatible format.
    /// </summary>
    private readonly YamlSequence _sequence = [];

    /// <summary>
    /// Adds a simple key-value action to the action sequence.
    /// </summary>
    /// <param name="actionKey">The key representing the action (e.g., "light.toggle").</param>
    /// <param name="entityId">The identifier of the target entity associated with the action.</param>
    /// <returns>The current instance of <see cref="ActionSequenceBuilder"/> for fluent chaining.</returns>
    private ActionSequenceBuilder AddSimpleAction(string actionKey, string entityId)
    {
        var mapping = new YamlMapping { { actionKey, new YamlString(entityId) } };
        _sequence.Add(mapping);
        return this;
    }

    /// <summary>
    /// Adds an action to toggle the state of a light.
    /// </summary>
    /// <param name="lightId">The ID of the light to be toggled.</param>
    /// <returns>The current instance of the <see cref="ActionSequenceBuilder"/> for method chaining.</returns>
    public ActionSequenceBuilder LightToggle(string lightId) => AddSimpleAction(LightToggleAction, lightId);
    /// <summary>
    /// Adds an action to the sequence to turn on a specified light.
    /// </summary>
    /// <param name="lightId">The ID of the light to be turned on.</param>
    /// <returns>The updated action sequence builder.</returns>
    public ActionSequenceBuilder LightTurnOn(string lightId) => AddSimpleAction(LightTurnOnAction, lightId);
    /// <summary>
    /// Adds an action to turn off a specified light in the action sequence.
    /// </summary>
    /// <param name="lightId">The ID of the light to turn off.</param>
    /// <returns>A reference to the current instance of <see cref="ActionSequenceBuilder"/> for chaining additional actions.</returns>
    public ActionSequenceBuilder LightTurnOff(string lightId) => AddSimpleAction(LightTurnOffAction, lightId);
    /// <summary>
    /// Toggles the state of the specified switch in the action sequence.
    /// </summary>
    /// <param name="switchId">The entity ID of the switch to toggle.</param>
    /// <returns>The updated action sequence builder.</returns>
    public ActionSequenceBuilder SwitchToggle(string switchId) => AddSimpleAction(SwitchToggleAction, switchId);
    /// <summary>
    /// Adds a switch turn-on action to the sequence.
    /// </summary>
    /// <param name="switchId">The entity ID of the switch to be turned on.</param>
    /// <returns>The current instance of the <c>ActionSequenceBuilder</c> to allow method chaining.</returns>
    public ActionSequenceBuilder SwitchTurnOn(string switchId) => AddSimpleAction(SwitchTurnOnAction, switchId);
    /// <summary>
    /// Adds an action to turn off a switch to the sequence.
    /// </summary>
    /// <param name="switchId">The ID of the switch to turn off.</param>
    /// <returns>The updated instance of the ActionSequenceBuilder for chaining.</returns>
    public ActionSequenceBuilder SwitchTurnOff(string switchId) => AddSimpleAction(SwitchTurnOffAction, switchId);
    /// <summary>
    /// Adds a delay action to the sequence.
    /// </summary>
    /// <param name="delay">The delay duration (e.g., "2s" for 2 seconds).</param>
    /// <returns>The updated <see cref="ActionSequenceBuilder"/> instance.</returns>
    public ActionSequenceBuilder Delay(string delay) => AddSimpleAction(DelayAction, delay);

    /// <summary>
    /// Adds a multi-line lambda action to the action sequence.
    /// </summary>
    /// <param name="cppLambdaCode">The C++ code to be executed in the lambda action.</param>
    /// <returns>An instance of <see cref="ActionSequenceBuilder"/> to allow for fluent chaining of additional actions.</returns>
    public ActionSequenceBuilder Lambda(string cppLambdaCode)
    {
        var lambdaNode = new YamlLambda(cppLambdaCode) { Name = LambdaNodeName };
        _sequence.Add(lambdaNode);
        return this;
    }

    /// <summary>
    /// Adds a generic, custom action node to the sequence.
    /// </summary>
    /// <param name="actionNode">The action node, represented by an <see cref="IYamlNode"/>, to be added to the action sequence.</param>
    /// <return>Returns the <see cref="ActionSequenceBuilder"/> instance to allow method chaining.</return>
    public ActionSequenceBuilder AddAction(IYamlNode actionNode)
    {
        _sequence.Add(actionNode);
        return this;
    }

    /// <summary>
    /// Builds and returns the constructed sequence of actions.
    /// </summary>
    /// <returns>A sequence of actions structured as an IYamlSequence.</returns>
    internal IYamlSequence Build() => _sequence;
}