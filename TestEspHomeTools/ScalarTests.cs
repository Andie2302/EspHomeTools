using EspHomeTools.Classes.Scalars;

namespace TestEspHomeTools;

[TestClass]
public class ScalarTests
{
    [TestMethod]
    public void YamlInt_ToYaml_ReturnsCorrectString()
    {
        var yamlInt = new YamlInt(42);
        var expected = "42";
        var actual = yamlInt.ToYaml(0, null);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void YamlFloat_ToYaml_ReturnsCorrectStringWithDot()
    {
        var yamlFloat = new YamlFloat(123.45);
        var expected = "123.45";
        var actual = yamlFloat.ToYaml(0, null);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void YamlBool_ToYaml_TrueReturnsLowercaseString()
    {
        var yamlBool = new YamlBool(true);
        var expected = "true";
        var actual = yamlBool.ToYaml(0, null);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void YamlString_ToYaml_SimpleStringIsNotQuoted()
    {
        var yamlString = new YamlString("hello world");
        var expected = "hello world";
        var actual = yamlString.ToYaml(0, null);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void YamlString_ToYaml_StringWithSpecialCharIsQuoted()
    {
        var yamlString = new YamlString("hello: world");
        var expected = "\"hello: world\"";
        var actual = yamlString.ToYaml(0, null);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void YamlString_ToYaml_NumericStringIsQuoted()
    {
        var yamlString = new YamlString("123");
        var expected = "\"123\"";
        var actual = yamlString.ToYaml(0, null);
        Assert.AreEqual(expected, actual);
    }
}