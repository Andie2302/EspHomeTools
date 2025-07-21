using EspHomeTools.Classes.Scalars;
using EspHomeTools.Classes.Structures;
using System.Text;

namespace TestEspHomeTools;

[TestClass]
public class StructureTests
{
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
        var actual = mapping.ToYaml(0, null).Trim();
        Assert.AreEqual(expected.ToString(), actual);
    }

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
        var actual = rootMapping.ToYaml(0, null).Trim();
        Assert.AreEqual(expected.ToString(), actual);
    }

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
        var actual = sequence.ToYaml(0, null).Trim();
        Assert.AreEqual(expected.ToString(), actual);
    }

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
        var actual = sequence.ToYaml(0, null).Trim();
        Assert.AreEqual(expected.ToString(), actual);
    }
}