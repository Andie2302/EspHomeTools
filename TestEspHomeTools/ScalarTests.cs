using EspHomeTools.Classes.Scalars;

namespace TestEspHomeTools;

/// <summary>
/// A test class containing unit tests for validating the behavior of YAML scalar-related classes.
/// </summary>
/// <remarks>
/// This class includes tests for serialization correctness across various scalar data types, such as integers,
/// floating-point numbers, boolean values, and strings. The tests ensure compliance with YAML formatting conventions
/// and provide consistency for scalar values during the serialization process.
/// </remarks>
[TestClass]
public class ScalarTests
{
    /// <summary>
    /// Ensures that the <see cref="YamlInt.ToYaml"/> method produces the correct YAML representation
    /// for an integer value passed to it.
    /// </summary>
    /// <remarks>
    /// This test validates the serialization logic of the <see cref="YamlInt.ToYaml"/> method, confirming
    /// that the method outputs a string correctly formatted as an unquoted integer that aligns with YAML specification.
    /// </remarks>
    [TestMethod]
    public void YamlInt_ToYaml_ReturnsCorrectString()
    {
        var yamlInt = new YamlInt(42);
        var expected = "42";
        var actual = yamlInt.ToYaml();
        Assert.AreEqual(expected, actual);
    }

    /// <summary>
    /// Verifies that the <see cref="YamlFloat.ToYaml"/> method correctly serializes a floating-point value into a YAML-formatted string
    /// using a dot as the decimal separator.
    /// </summary>
    /// <remarks>
    /// This test ensures that the <see cref="YamlFloat.ToYaml"/> method adheres to YAML's formatting standards for floating-point numbers,
    /// producing a consistent and culture-independent output where the decimal part is separated by a dot (e.g., "123.45").
    /// </remarks>
    [TestMethod]
    public void YamlFloat_ToYaml_ReturnsCorrectStringWithDot()
    {
        var yamlFloat = new YamlFloat(123.45);
        var expected = "123.45";
        var actual = yamlFloat.ToYaml();
        Assert.AreEqual(expected, actual);
    }

    /// <summary>
    /// Ensures that the <c>ToYaml</c> method of the <c>YamlBool</c> class correctly generates the lowercase string
    /// "true" when the boolean value is set to <c>true</c>.
    /// </summary>
    /// <remarks>
    /// This test verifies that the YAML serialization of the <c>YamlBool</c> class adheres to the expected format for
    /// boolean values, ensuring compatibility with YAML standards. Specifically, it checks that the <c>true</c> value
    /// is correctly converted to the lowercase representation "true".
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
    /// correctly serializes simple string values without enclosing quotes, provided
    /// the string does not include special YAML characters or numeric formats.
    /// </summary>
    /// <remarks>
    /// This test ensures that plain text strings are serialized as-is in YAML format
    /// unless quoting is necessary due to special formatting rules or reserved characters.
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
    /// Verifies that the <see cref="YamlString.ToYaml"/> method correctly quotes string values that include special characters.
    /// </summary>
    /// <remarks>
    /// This test ensures that strings incorporating special characters, such as colons or other YAML-reserved symbols,
    /// are enclosed in quotes when serialized. Proper quoting safeguards against incorrect interpretation or parsing
    /// issues in the resulting YAML output.
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
    /// Validates that numeric strings are correctly quoted when converted to YAML.
    /// </summary>
    /// <remarks>
    /// Ensures that when the <see cref="YamlString.ToYaml"/> method is used to serialize a string
    /// containing numeric characters, such as "123", the output YAML correctly encapsulates the string
    /// in quotes. This prevents the numeric string from being misinterpreted as a numeric value in YAML format.
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