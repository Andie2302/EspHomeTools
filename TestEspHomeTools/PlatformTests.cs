using EspHomeTools.Builders;
using System.Text;
using EspHomeTools.Classes.Structures;

namespace TestEspHomeTools;

/// <summary>
/// Test class containing unit tests for verifying YAML generation
/// for various platform configurations using the EspHomeTools library.
/// </summary>
/// <remarks>
/// This class is decorated with the [TestClass] attribute and includes
/// multiple [TestMethod] implementations. It tests functionality for
/// generating YAML mappings for different platforms and ensures proper
/// exception handling for invalid configurations.
/// </remarks>
[TestClass]
public class PlatformTests
{
    /// Tests that the YAML generator correctly produces the expected YAML output
    /// when an ESP8266 platform with a specified board is configured.
    /// This method verifies the behavior of the `WithEsp8266` extension method
    /// when a board is provided. It ensures that the generated YAML string
    /// matches the expected format, specifically including the ESP8266 section
    /// and the specified board value.
    /// Assertions:
    /// - The generated YAML string is compared to the expected string, ensuring
    /// the output is correct.
    /// Dependencies:
    /// - EspHomeExtensions.WithEsp8266
    /// - YamlMapping.ToYaml
    /// Exceptions:
    /// - If the output YAML does not match the expected format, an assertion
    /// failure will occur.
    [TestMethod]
    public void WithEsp8266_WithBoard_GeneratesCorrectYaml()
    {
        var root = new YamlMapping();
        var expected = $"esp8266:{Environment.NewLine}  board: d1_mini";
        root.WithEsp8266(esp => { esp.WithBoard("d1_mini"); });
        var actual = root.ToYaml().Trim();
        Assert.AreEqual(expected, actual);
    }

    /// Tests the behavior when attempting to use the `WithEsp8266` extension method without specifying a board.
    /// This test ensures that an `InvalidOperationException` is thrown when the `WithEsp8266` method is called,
    /// but no board is configured within the provided `Esp8266Builder` instance. The test helps verify that
    /// proper validation is in place to prevent incomplete or invalid configurations.
    [TestMethod]
    public void WithEsp8266_WithoutBoard_ThrowsException()
    {
        var root = new YamlMapping();
        Assert.ThrowsException<InvalidOperationException>(() => { root.WithEsp8266(esp => { }); });
    }

    /// <summary>
    /// Verifies that the method `WithEsp32` generates the correct YAML structure when a board is specified.
    /// </summary>
    /// <remarks>
    /// This test initializes a YAML mapping and applies the `WithEsp32` configuration with a specified board.
    /// It validates that the resultant YAML matches the expected structure, ensuring the proper generation of
    /// the YAML content for ESP32 with the provided board.
    /// </remarks>
    /// <exception cref="Microsoft.VisualStudio.TestTools.UnitTesting.AssertFailedException">
    /// Thrown if the generated YAML does not match the expected output.
    /// </exception>
    [TestMethod]
    public void WithEsp32_WithBoard_GeneratesCorrectYaml()
    {
        var root = new YamlMapping();
        var expected = $"esp32:{Environment.NewLine}  board: esp32dev";
        root.WithEsp32(esp => { esp.WithBoard("esp32dev"); });
        var actual = root.ToYaml().Trim();
        Assert.AreEqual(expected, actual);
    }

    /// <summary>
    /// Verifies that the appropriate YAML is generated for an ESP32 configuration
    /// when both board and framework settings are specified.
    /// </summary>
    /// <remarks>
    /// This method initializes a YAML mapping structure, applies ESP32-specific
    /// configurations including board and framework settings, and asserts the
    /// resulting YAML output against the expected format.
    /// </remarks>
    /// <example>
    /// This test ensures the generated YAML includes the "esp32" node,
    /// with properly indented "board" and "framework" fields.
    /// </example>
    /// <exception cref="AssertFailedException">
    /// Thrown if the generated YAML does not match the expected format.
    /// </exception>
    [TestMethod]
    public void WithEsp32_WithBoardAndFramework_GeneratesCorrectYaml()
    {
        var root = new YamlMapping();
        var expected = new StringBuilder();
        expected.AppendLine("esp32:");
        expected.AppendLine("  board: esp32dev");
        expected.Append("  framework: \"esp-idf\"");
        root.WithEsp32(esp => { esp.WithBoard("esp32dev").WithFramework("esp-idf"); });
        var actual = root.ToYaml().Trim();
        Assert.AreEqual(expected.ToString(), actual);
    }

    /// Tests the behavior of the `WithEsp32` method when no board is specified.
    /// This test verifies that calling the `WithEsp32` method without providing
    /// a board configuration throws an `InvalidOperationException`. It ensures
    /// the method enforces the requirement for a valid board definition.
    [TestMethod]
    public void WithEsp32_WithoutBoard_ThrowsException()
    {
        var root = new YamlMapping();
        Assert.ThrowsException<InvalidOperationException>(() => { root.WithEsp32(esp => { }); });
    }

    /// <summary>
    /// Validates that the YAML generated for an RP2040 configuration with a specified board
    /// matches the expected structure.
    /// </summary>
    /// <remarks>
    /// This test specifically ensures that a root-level "rp2040" YAML entry is created with
    /// a "board" key containing the correct board name. It compares the generated output to
    /// a pre-defined expected YAML string for accuracy.
    /// </remarks>
    /// <exception cref="AssertFailedException">
    /// Thrown when the generated YAML does not match the expected YAML structure.
    /// </exception>
    [TestMethod]
    public void WithRp2040_WithBoard_GeneratesCorrectYaml()
    {
        var root = new YamlMapping();
        var expected = $"rp2040:{Environment.NewLine}  board: rpi_pico_w";
        root.WithRp2040(rp => { rp.WithBoard("rpi_pico_w"); });
        var actual = root.ToYaml().Trim();
        Assert.AreEqual(expected, actual);
    }

    /// Tests the behavior of the `WithRp2040` extension method when used without configuring a board.
    /// Verifies that the method throws an `InvalidOperationException` if a board is not specified
    /// during the configuration process. This ensures that the method enforces the requirement
    /// for a valid board configuration when using the Rp2040 platform.
    [TestMethod]
    public void WithRp2040_WithoutBoard_ThrowsException()
    {
        var root = new YamlMapping();
        Assert.ThrowsException<InvalidOperationException>(() => { root.WithRp2040(rp => { }); });
    }

    /// <summary>
    /// Validates that calling the <c>WithBeken</c> extension method with a specified board results in generating the correct YAML structure.
    /// </summary>
    /// <remarks>
    /// This test ensures that the <c>WithBeken</c> method properly maps the specified board to the generated YAML output.
    /// The generated YAML is validated against the expected format to confirm proper compliance with the structure.
    /// </remarks>
    /// <exception cref="AssertFailedException">
    /// Thrown if the generated YAML does not match the expected structure.
    /// </exception>
    [TestMethod]
    public void WithBeken_WithBoard_GeneratesCorrectYaml()
    {
        var root = new YamlMapping();
        var expected = $"bk72xx:{Environment.NewLine}  board: \"generic-bk7231n-qfn32-tuya\"";
        root.WithBeken(beken => { beken.WithBoard("generic-bk7231n-qfn32-tuya"); });
        var actual = root.ToYaml().Trim();
        Assert.AreEqual(expected, actual);
    }

    /// <summary>
    /// Validates that calling the <c>WithBeken</c> method without providing a board configuration
    /// throws an <c>InvalidOperationException</c>.
    /// </summary>
    /// <remarks>
    /// This method ensures that the <c>WithBeken</c> method in the <c>YamlMapping</c> class
    /// behaves correctly when required parameters, such as a board, are not provided.
    /// The exception indicates that the configuration is incomplete or invalid.
    /// </remarks>
    /// <exception cref="InvalidOperationException">
    /// Thrown when the <c>WithBeken</c> method is called without specifying the necessary board configuration.
    /// </exception>
    [TestMethod]
    public void WithBeken_WithoutBoard_ThrowsException()
    {
        var root = new YamlMapping();
        Assert.ThrowsException<InvalidOperationException>(() => { root.WithBeken(beken => { }); });
    }
}