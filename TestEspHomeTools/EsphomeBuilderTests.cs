using EspHomeTools.Builders;
using EspHomeTools.Classes.Structures;

namespace TestEspHomeTools;

[TestClass]
public class EsphomeBuilderTests
{
    [TestMethod]
    public void Build_WithValidName_ShouldGenerateCorrectYaml()
    {
        var root = new YamlMapping();
        var expectedDeviceName = "test-device";
        var expectedYaml = $"esphome:{Environment.NewLine}  name: \"{expectedDeviceName}\"";
        root.WithEsphome(esphome => { esphome.WithName(expectedDeviceName); });
        var actualYaml = root.ToYaml(0, null).Trim();
        Assert.AreEqual(expectedYaml, actualYaml, "The generated YAML code for the esphome block is not correct.");
    }

    [TestMethod]
    public void Build_WithoutName_ShouldThrowInvalidOperationException()
    {
        var root = new YamlMapping();
        Assert.ThrowsException<InvalidOperationException>(() => { root.WithEsphome(esphome => { }); }, "An InvalidOperationException was expected when no name is set.");
    }
}