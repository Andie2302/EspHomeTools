using EspHomeTools.Classes.Scalars;

namespace TestEspHomeTools;

/// <summary>
/// Eine Testklasse, die Unit-Tests zur Validierung des Verhaltens von YAML-Skalar-bezogenen Klassen enthält.
/// </summary>
[TestClass]
public class ScalarTests
{
    /// <summary>
    /// Stellt sicher, dass die Methode <see cref="YamlInt.ToYaml"/> die korrekte YAML-Darstellung
    /// für einen ihr übergebenen ganzzahligen Wert erzeugt.
    /// </summary>
    [TestMethod]
    public void YamlInt_ToYaml_ReturnsCorrectString()
    {
        var yamlInt = new YamlInt(42);
        var expected = "42";
        // KORREKTUR: Wir rufen die parameterlose Methode auf, die die Standardwerte (indent: 0, name: null) verwendet.
        var actual = yamlInt.ToYaml(0, null);
        Assert.AreEqual(expected, actual);
    }

    /// <summary>
    /// Überprüft, ob die Methode <see cref="YamlFloat.ToYaml"/> einen Fließkommawert korrekt in eine YAML-formatierte Zeichenfolge
    /// mit einem Punkt als Dezimaltrennzeichen serialisiert.
    /// </summary>
    [TestMethod]
    public void YamlFloat_ToYaml_ReturnsCorrectStringWithDot()
    {
        var yamlFloat = new YamlFloat(123.45);
        var expected = "123.45";
        // KORREKTUR: Parameterloser Aufruf.
        var actual = yamlFloat.ToYaml(0, null);
        Assert.AreEqual(expected, actual);
    }

    /// <summary>
    /// Stellt sicher, dass die <c>ToYaml</c>-Methode der <c>YamlBool</c>-Klasse die kleingeschriebene Zeichenfolge
    /// "true" korrekt generiert, wenn der boolesche Wert auf <c>true</c> gesetzt ist.
    /// </summary>
    [TestMethod]
    public void YamlBool_ToYaml_TrueReturnsLowercaseString()
    {
        var yamlBool = new YamlBool(true);
        var expected = "true";
        // KORREKTUR: Parameterloser Aufruf.
        var actual = yamlBool.ToYaml(0, null);
        Assert.AreEqual(expected, actual);
    }

    /// <summary>
    /// Überprüft, ob die <c>ToYaml</c>-Methode der <c>YamlString</c>-Klasse
    /// einfache Zeichenfolgenwerte ohne umschließende Anführungszeichen korrekt serialisiert.
    /// </summary>
    [TestMethod]
    public void YamlString_ToYaml_SimpleStringIsNotQuoted()
    {
        var yamlString = new YamlString("hello world");
        var expected = "hello world";
        // KORREKTUR: Parameterloser Aufruf.
        var actual = yamlString.ToYaml(0, null);
        Assert.AreEqual(expected, actual);
    }

    /// <summary>
    /// Überprüft, ob die Methode <see cref="YamlString.ToYaml"/> Zeichenfolgenwerte, die Sonderzeichen enthalten, korrekt in Anführungszeichen setzt.
    /// </summary>
    [TestMethod]
    public void YamlString_ToYaml_StringWithSpecialCharIsQuoted()
    {
        var yamlString = new YamlString("hello: world");
        var expected = "\"hello: world\"";
        // KORREKTUR: Parameterloser Aufruf.
        var actual = yamlString.ToYaml(0, null);
        Assert.AreEqual(expected, actual);
    }

    /// <summary>
    /// Validiert, dass numerische Zeichenfolgen beim Konvertieren in YAML korrekt in Anführungszeichen gesetzt werden.
    /// </summary>
    [TestMethod]
    public void YamlString_ToYaml_NumericStringIsQuoted()
    {
        var yamlString = new YamlString("123");
        var expected = "\"123\"";
        // KORREKTUR: Parameterloser Aufruf.
        var actual = yamlString.ToYaml(0, null);
        Assert.AreEqual(expected, actual);
    }
}