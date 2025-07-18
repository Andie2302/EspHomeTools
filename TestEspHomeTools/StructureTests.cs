using EspHomeTools.Classes;
using EspHomeTools.Classes.Scalars;
using EspHomeTools.Classes.Structures;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Text;

namespace TestEspHomeTools;

[TestClass]
public class StructureTests
{
    // ================== YamlMapping Tests ==================

    [TestMethod]
    public void YamlMapping_WithSimpleScalars_GeneratesCorrectYaml()
    {
        // Arrange
        var mapping = new YamlMapping
        {
            { "name", new YamlString("My Device") },
            { "enabled", new YamlBool(true) },
            { "retries", new YamlInt(5) }
        };

        var expected = new StringBuilder();
        // KORREKTUR: "My Device" benötigt laut deinen Regeln keine Anführungszeichen.
        expected.AppendLine("name: My Device");
        expected.AppendLine("enabled: true");
        expected.Append("retries: 5");

        // Act
        var actual = mapping.ToYaml().Trim();

        // Assert
        Assert.AreEqual(expected.ToString(), actual);
    }

    [TestMethod]
    public void YamlMapping_WithNestedMapping_GeneratesCorrectYaml()
    {
        // Arrange
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

        // Act
        var actual = rootMapping.ToYaml().Trim();

        // Assert
        Assert.AreEqual(expected.ToString(), actual);
    }

    // ================== YamlSequence Tests ==================

    [TestMethod]
    public void YamlSequence_WithSimpleScalars_GeneratesCorrectYaml()
    {
        // Arrange
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

        // Act
        var actual = sequence.ToYaml().Trim();

        // Assert
        Assert.AreEqual(expected.ToString(), actual);
    }

    [TestMethod]
    public void YamlSequence_WithMappings_GeneratesCorrectYaml()
    {
        // Arrange
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

        // Act
        var actual = sequence.ToYaml().Trim();

        // Assert
        Assert.AreEqual(expected.ToString(), actual);
    }
}
