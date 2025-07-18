using EspHomeTools.Classes.Scalars;

namespace TestEspHomeTools;

/// <summary>
/// A test class containing unit tests for validating the behavior of YAML scalar-related classes.
/// </summary>
/// <remarks>
/// Tests cover the correctness of YAML serialization for various scalar types including integers,
/// floating-point numbers, booleans, and strings.
/// </remarks>
[TestClass]
public class ScalarTests
{
    /// <summary>
    /// Verifies that the <see cref="YamlInt.ToYaml"/> method correctly serializes an integer value into a YAML-formatted string.
    /// </summary>
    /// <remarks>
    /// This test ensures that the output of the <see cref="YamlInt.ToYaml"/> method produces the expected YAML string
    /// representation of the integer value, without any unexpected formatting or modifications.
    /// </remarks>
    [TestMethod]
    public void YamlInt_ToYaml_ReturnsCorrectString()
    {
        var yamlInt = new YamlInt(42);
        var expected = "42";
        var actual = yamlInt.ToYaml();
        Assert.AreEqual(expected, actual);
    }

    /// Validates that the ToYaml method of the YamlFloat class correctly converts
    /// a floating-point number to its YAML string representation, ensuring the use
    /// of a dot as the decimal separator (e.g., "123.45").
    /// This test verifies that:
    /// - The value of a float, when converted to a YAML string, adheres to the standard convention for YAML serialization.
    /// - The decimal part is separated by a dot, regardless of the current culture.
    /// The test provides assurance that the serialization results are consistent and compatible
    /// with YAML formatting requirements for floating-point numbers.
    [TestMethod]
    public void YamlFloat_ToYaml_ReturnsCorrectStringWithDot()
    {
        var yamlFloat = new YamlFloat(123.45);
        var expected = "123.45";
        var actual = yamlFloat.ToYaml();
        Assert.AreEqual(expected, actual);
    }

    /// <summary>
    /// Validates that the YAML representation of a <c>YamlBool</c> instance with a value of <c>true</c>
    /// returns the lowercase string "true".
    /// </summary>
    /// <remarks>
    /// This test ensures that the <c>ToYaml</c> method of the <c>YamlBool</c> class correctly serializes
    /// a boolean value of <c>true</c> into its YAML-compatible string representation, which is "true".
    /// It validates the method's behavior by comparing the actual output with the expected value.
    /// </remarks>
    [TestMethod]
    public void YamlBool_ToYaml_TrueReturnsLowercaseString()
    {
        var yamlBool = new YamlBool(true);
        var expected = "true";
        var actual = yamlBool.ToYaml();
        Assert.AreEqual(expected, actual);
    }

    /// <summary>
    /// Verifies that the <c>ToYaml</c> method of the <c>YamlString</c> class
    /// returns the string value without quotes when the string does not
    /// contain special characters or numeric representations.
    /// </summary>
    /// <remarks>
    /// This test ensures that simple strings, which do not require additional
    /// formatting or quoting in YAML, are correctly serialized as-is.
    /// </remarks>
    [TestMethod]
    public void YamlString_ToYaml_SimpleStringIsNotQuoted()
    {
        var yamlString = new YamlString("hello world");
        var expected = "hello world";
        var actual = yamlString.ToYaml();
        Assert.AreEqual(expected, actual);
    }

    /// <summary>
    /// Tests that the <c>ToYaml</c> method of the <c>YamlString</c> class
    /// correctly quotes string values containing special characters.
    /// </summary>
    /// <remarks>
    /// This test ensures that when a string value contains special characters
    /// (e.g., ':', which has significance in YAML), the resulting YAML string
    /// is properly formatted by enclosing it in quotes. This behavior
    /// prevents potential misinterpretations or parsing errors in YAML.
    /// </remarks>
    [TestMethod]
    public void YamlString_ToYaml_StringWithSpecialCharIsQuoted()
    {
        var yamlString = new YamlString("hello: world");
        var expected = "\"hello: world\"";
        var actual = yamlString.ToYaml();
        Assert.AreEqual(expected, actual);
    }

    /// <summary>
    /// Verifies that a numeric string is properly quoted when serialized to YAML.
    /// </summary>
    /// <remarks>
    /// This test ensures that when a string containing numeric characters (e.g., "123") is serialized
    /// using the <c>ToYaml</c> method of the <c>YamlString</c> class, the resulting YAML output
    /// correctly quotes the string to prevent it from being interpreted as a number.
    /// </remarks>
    [TestMethod]
    public void YamlString_ToYaml_NumericStringIsQuoted()
    {
        var yamlString = new YamlString("123");
        var expected = "\"123\"";
        var actual = yamlString.ToYaml();
        Assert.AreEqual(expected, actual);
    }
}