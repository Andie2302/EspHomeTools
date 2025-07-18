using EspHomeTools.Builders;
using EspHomeTools.Classes.Structures;

namespace TestEspHomeTools;

/// <summary>
/// Test class for validating the functionality and behavior of the EsphomeBlockBuilder
/// and its interaction with the YAML generation system.
/// Includes tests for correct YAML generation and expected exception handling when
/// required properties are not provided.
/// </summary>
[TestClass]
public class EsphomeBlockBuilderTests
{
    /// <summary>
    /// Validates that a YAML structure is correctly generated for a given valid device name input.
    /// </summary>
    /// <remarks>
    /// This test ensures that invoking the YAML block builder with a valid device name produces the expected YAML output.
    /// The generated output is compared against the expected YAML format for correctness with respect to the device name.
    /// </remarks>
    /// <exception cref="AssertFailedException">
    /// Thrown when the actual generated YAML does not match the expected YAML structure.
    /// </exception>
    [TestMethod]
    public void Build_WithValidName_ShouldGenerateCorrectYaml()
    {
        var root = new YamlMapping();
        var expectedDeviceName = "test-device";
        var expectedYaml = $"esphome:{Environment.NewLine}  name: \"{expectedDeviceName}\"";
        root.WithEsphome(esphome => { esphome.WithName(expectedDeviceName); });
        var actualYaml = root.ToYaml().Trim();
        Assert.AreEqual(expectedYaml, actualYaml, "Der generierte YAML-Code für den esphome-Block ist nicht korrekt.");
    }

    /// <summary>
    /// Tests whether building an Esphome YAML mapping without specifying a name
    /// throws an <see cref="InvalidOperationException"/>.
    /// </summary>
    /// <remarks>
    /// This test verifies that an <see cref="InvalidOperationException"/> is thrown when attempting
    /// to create an Esphome YAML mapping without setting a name. The name is expected to be a required property
    /// for the Esphome block, and the absence of it should lead to an exception to enforce proper configuration.
    /// </remarks>
    /// <exception cref="InvalidOperationException">
    /// Thrown when the Esphome YAML mapping is built without a specified name.
    /// </exception>
    [TestMethod]
    public void Build_WithoutName_ShouldThrowInvalidOperationException()
    {
        var root = new YamlMapping();
        Assert.ThrowsException<InvalidOperationException>(() =>
        {
            root.WithEsphome(esphome =>
            { });
        }, "Eine InvalidOperationException wurde erwartet, wenn kein Name gesetzt wird.");
    }
}

