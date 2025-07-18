using EspHomeTools.Builders;
using EspHomeTools.Classes.Structures;

namespace TestEspHomeTools;

[TestClass]
public class EsphomeBlockBuilderTests
{
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

