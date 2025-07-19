using EspHomeTools.Builders;
using System.Text;
using EspHomeTools.Classes.Structures;

namespace TestEspHomeTools;

[TestClass]
public class PlatformTests
{
    // --- ESP8266 Tests ---

    [TestMethod]
    public void WithEsp8266_WithBoard_GeneratesCorrectYaml()
    {
        // Arrange
        var root = new YamlMapping();
        var expected = $"esp8266:{Environment.NewLine}  board: d1_mini";

        // Act
        root.WithEsp8266(esp => { esp.WithBoard("d1_mini"); });
        var actual = root.ToYaml().Trim();

        // Assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void WithEsp8266_WithoutBoard_ThrowsException()
    {
        // Arrange
        var root = new YamlMapping();

        // Act & Assert
        Assert.ThrowsException<InvalidOperationException>(() => { root.WithEsp8266(esp => { }); });
    }

    // --- ESP32 Tests ---

    [TestMethod]
    public void WithEsp32_WithBoard_GeneratesCorrectYaml()
    {
        // Arrange
        var root = new YamlMapping();
        var expected = $"esp32:{Environment.NewLine}  board: esp32dev";

        // Act
        root.WithEsp32(esp => { esp.WithBoard("esp32dev"); });
        var actual = root.ToYaml().Trim();

        // Assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void WithEsp32_WithBoardAndFramework_GeneratesCorrectYaml()
    {
        // Arrange
        var root = new YamlMapping();
        var expected = new StringBuilder();
        expected.AppendLine("esp32:");
        expected.AppendLine("  board: esp32dev");

        // KORREKTUR: Die Erwartung muss die Anführungszeichen enthalten.
        expected.Append("  framework: \"esp-idf\"");

        // Act
        root.WithEsp32(esp => { esp.WithBoard("esp32dev").WithFramework("esp-idf"); });
        var actual = root.ToYaml().Trim();

        // Assert
        Assert.AreEqual(expected.ToString(), actual);
    }

    [TestMethod]
    public void WithEsp32_WithoutBoard_ThrowsException()
    {
        // Arrange
        var root = new YamlMapping();

        // Act & Assert
        Assert.ThrowsException<InvalidOperationException>(() => { root.WithEsp32(esp => { }); });
    }

    // --- RP2040 Tests ---

    [TestMethod]
    public void WithRp2040_WithBoard_GeneratesCorrectYaml()
    {
        // Arrange
        var root = new YamlMapping();
        var expected = $"rp2040:{Environment.NewLine}  board: rpi_pico_w";

        // Act
        root.WithRp2040(rp => { rp.WithBoard("rpi_pico_w"); });
        var actual = root.ToYaml().Trim();

        // Assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void WithRp2040_WithoutBoard_ThrowsException()
    {
        // Arrange
        var root = new YamlMapping();

        // Act & Assert
        Assert.ThrowsException<InvalidOperationException>(() => { root.WithRp2040(rp => { }); });
    }

    // --- Beken (BK72XX) Tests ---

    [TestMethod]
    public void WithBeken_WithBoard_GeneratesCorrectYaml()
    {
        // Arrange
        var root = new YamlMapping();

        // KORREKTUR: Die Erwartung muss die Anführungszeichen enthalten.
        var expected = $"bk72xx:{Environment.NewLine}  board: \"generic-bk7231n-qfn32-tuya\"";

        // Act
        root.WithBeken(beken => { beken.WithBoard("generic-bk7231n-qfn32-tuya"); });
        var actual = root.ToYaml().Trim();

        // Assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void WithBeken_WithoutBoard_ThrowsException()
    {
        // Arrange
        var root = new YamlMapping();

        // Act & Assert
        Assert.ThrowsException<InvalidOperationException>(() => { root.WithBeken(beken => { }); });
    }
}