using EspHomeTools.Classes.Scalars;
using EspHomeTools.Classes.Structures;
using System.Text;

namespace TestEspHomeTools;

/// <summary>
/// A test class that provides unit tests for validating the behavior and correctness of various structure-related classes,
/// specifically focusing on YAML mappings and sequences.
/// </summary>
[TestClass]
public class StructureTests
{
    /// Unit test that verifies the functionality of generating a YAML representation
    /// from a YamlMapping instance containing simple scalar values.
    /// This test:
    /// - Creates a YamlMapping instance and populates it with scalar key-value pairs.
    /// - Verifies that the YAML output produced by the ToYaml method matches the expected format.
    /// Expected behavior:
    /// - Simple scalar values such as strings, booleans, and integers are serialized correctly into YAML format.
    /// - The output respects proper YAML formatting rules.
    [TestMethod]
    public void YamlMapping_WithSimpleScalars_GeneratesCorrectYaml()
    {
        var mapping = new YamlMapping
        {
            { "name", new YamlString("My Device") },
            { "enabled", new YamlBool(true) },
            { "retries", new YamlInt(5) }
        };

        var expected = new StringBuilder();
        expected.AppendLine("name: My Device");
        expected.AppendLine("enabled: true");
        expected.Append("retries: 5");
        var actual = mapping.ToYaml().Trim();
        Assert.AreEqual(expected.ToString(), actual);
    }

    /// <summary>
    /// Tests that a YAML mapping containing a nested mapping is serialized correctly
    /// into expected YAML format. The test ensures that the nesting in the YAML structure
    /// is properly represented with indentation and formatting rules respected.
    /// Specifically, the method verifies that:
    /// - The outer mapping contains a nested mapping as a value.
    /// - The nested mapping's keys and values are serialized correctly and indented.
    /// - The resulting YAML string matches the expected structure.
    /// </summary>
    [TestMethod]
    public void YamlMapping_WithNestedMapping_GeneratesCorrectYaml()
    {
        var nestedMapping = new YamlMapping
        {
            { "ssid", new YamlString("MyWifi") },
            { "password", new YamlString("secret") }
        };
        var rootMapping = new YamlMapping
        {
            { "wifi", nestedMapping }
        };

        var expected = new StringBuilder();
        expected.AppendLine("wifi:");
        expected.AppendLine("  ssid: MyWifi");
        expected.Append("  password: secret");
        var actual = rootMapping.ToYaml().Trim();
        Assert.AreEqual(expected.ToString(), actual);
    }

    /// <summary>
    /// Verifies that a YAML sequence containing simple scalar elements (strings and integers) generates
    /// the correct YAML representation. This test ensures that the <see cref="YamlSequence"/> class
    /// produces an accurate and properly formatted YAML output when populated with basic scalar values.
    /// </summary>
    /// <remarks>
    /// The method tests a case where the YAML sequence includes scalar elements such as strings and integers.
    /// It compares the generated YAML output with the expected string representation to validate correctness.
    /// </remarks>
    /// <example>
    /// A YAML sequence with elements ["item1", "item2", 3] should produce a YAML string of:
    /// - item1
    /// - item2
    /// - 3
    /// </example>
    [TestMethod]
    public void YamlSequence_WithSimpleScalars_GeneratesCorrectYaml()
    {
        var sequence = new YamlSequence
        {
            new YamlString("item1"),
            new YamlString("item2"),
            new YamlInt(3)
        };

        var expected = new StringBuilder();
        expected.AppendLine("- item1");
        expected.AppendLine("- item2");
        expected.Append("- 3");
        var actual = sequence.ToYaml().Trim();
        Assert.AreEqual(expected.ToString(), actual);
    }

    /// <summary>
    /// Tests the generation of a YAML sequence containing multiple YAML mappings and validates
    /// that the output matches the expected YAML structure.
    /// </summary>
    /// <remarks>
    /// This test ensures that a <see cref="YamlSequence"/> containing nested
    /// <see cref="YamlMapping"/> objects produces a correctly formatted YAML output.
    /// It validates serialization logic where each mapping in the sequence is represented
    /// as a set of key-value pairs, ensuring proper indentation and representation.
    /// </remarks>
    /// <example>
    /// The sequence includes multiple mappings, each with its own set of key-value pairs.
    /// Typical usage involves verifying that the sequence is correctly converted
    /// to YAML syntax, preserving structure integrity.
    /// </example>
    /// <seealso cref="YamlSequence"/>
    /// <seealso cref="YamlMapping"/>
    /// <seealso cref="YamlString"/>
    [TestMethod]
    public void YamlSequence_WithMappings_GeneratesCorrectYaml()
    {
        var sequence = new YamlSequence
        {
            new YamlMapping { { "platform", new YamlString("dht") }, { "pin", new YamlString("D1") } },
            new YamlMapping { { "platform", new YamlString("gpio") }, { "pin", new YamlString("D2") } }
        };

        var expected = new StringBuilder();
        expected.AppendLine("- platform: dht");
        expected.AppendLine("  pin: D1");
        expected.AppendLine("- platform: gpio");
        expected.Append("  pin: D2");
        var actual = sequence.ToYaml().Trim();
        Assert.AreEqual(expected.ToString(), actual);
    }
}
