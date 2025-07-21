using EspHomeTools.Builders;
using EspHomeTools.Classes.Structures;

namespace TestEspHomeTools;

/// <summary>
/// Unit test class for verifying the behavior and functionality of the EsphomeBlockBuilder.
/// Includes tests for proper YAML generation under valid conditions and appropriate exception
/// handling when required inputs are missing or invalid.
/// </summary>
[TestClass]
public class EsphomeBuilderTests
{
    /// <summary>
    /// Validates that a YAML structure is correctly generated for a given valid device name input.
    /// </summary>
    /// <remarks>
    /// Ensures that the EsphomeBuilder correctly constructs a YAML block when provided with
    /// a valid device name. Compares the generated YAML output to the expected format to confirm
    /// the behavior of the builder logic.
    /// </remarks>
    /// <exception cref="AssertFailedException">
    /// Thrown when the generated YAML output does not match the expected YAML structure.
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
    /// Ensures that an <see cref="InvalidOperationException"/> is thrown if a YAML mapping is built
    /// without specifying a required name property.
    /// </summary>
    /// <remarks>
    /// This test validates that the Esphome block builder enforces the presence of a required name property.
    /// When the name is not specified, the builder should throw an exception to signal improper configuration.
    /// This ensures that invalid YAML mappings cannot be generated.
    /// </remarks>
    /// <exception cref="InvalidOperationException">
    /// Thrown when attempting to build an Esphome YAML block without providing a name.
    /// </exception>
    [TestMethod]
    public void Build_WithoutName_ShouldThrowInvalidOperationException()
    {
        var root = new YamlMapping();
        Assert.ThrowsException<InvalidOperationException>(() => { root.WithEsphome(esphome => { }); }, "Eine InvalidOperationException wurde erwartet, wenn kein Name gesetzt wird.");
    }
}

