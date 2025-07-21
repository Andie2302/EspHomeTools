using System;
using EspHomeTools.Classes.Scalars;
using EspHomeTools.Classes.Structures;
using EspHomeTools.Interfaces;

namespace EspHomeTools.Builders;

/// <summary>
/// Represents a builder for configuring SPI (Serial Peripheral Interface) settings in an ESPHome configuration.
/// </summary>
public class SpiBuilder
{
    /// <summary>
    /// Represents the key used to specify the clock pin configuration for an SPI bus in the YAML mapping.
    /// </summary>
    /// <remarks>
    /// This constant is used internally within the <c>SpiBuilder</c> class to identify and manage
    /// the clock pin key ("clk_pin"). It is a required field when building the SPI configuration,
    /// and an exception will be thrown if the clock pin is not specified during the build process.
    /// </remarks>
    private const string ClkPinKey = "clk_pin";

    /// <summary>
    /// Represents a configuration mapping used to construct and store settings
    /// for the SPI (Serial Peripheral Interface) communication in the <c>SpiBuilder</c>.
    /// </summary>
    /// <remarks>
    /// This variable serves as a container for dynamically building key-value configuration
    /// pairs required for SPI setup. The mapping allows adding and managing configuration
    /// properties such as pins (e.g., "clk_pin", "mosi_pin", "miso_pin") and identifiers.
    /// It is utilized internally within the builder to assemble the final configuration object.
    /// </remarks>
    private readonly YamlMapping _config = new();

    /// Sets the Clock (CLK) pin for the SPI bus configuration.
    /// <param name="pin">The GPIO pin to be used as the Clock (CLK) pin.</param>
    /// <return>Returns the current instance of <see cref="SpiBuilder"/>, allowing method chaining.</return>
    public SpiBuilder SetClkPin(string pin) => SetPin(ClkPinKey, pin);

    /// Sets the MOSI (Master Out Slave In) pin for the SPI configuration.
    /// <param name="pin">The GPIO pin to be used as the MOSI pin.</param>
    /// <return>Returns the current instance of <c>SpiBuilder</c> to allow method chaining.</return>
    public SpiBuilder SetMosiPin(string pin) => SetPin("mosi_pin", pin);

    /// Sets the MISO (Master In Slave Out) pin for the SPI (Serial Peripheral Interface) configuration.
    /// <param name="pin">The GPIO pin to be configured as the MISO pin.</param>
    /// <returns>The updated <c>SpiBuilder</c> instance for method chaining.</returns>
    public SpiBuilder SetMisoPin(string pin) => SetPin("miso_pin", pin);

    /// <summary>
    /// Assigns an ID to the SPI configuration.
    /// </summary>
    /// <param name="id">The identifier for the SPI configuration.</param>
    /// <returns>The current instance of <c>SpiBuilder</c> for method chaining.</returns>
    public SpiBuilder WithId(string id)
    {
        _config["id"] = new YamlString(id);
        return this;
    }

    /// <summary>
    /// Adds a comment to the specified key in the SPI configuration if the key exists.
    /// </summary>
    /// <param name="key">The key to which the comment should be added.</param>
    /// <param name="comment">The comment to associate with the specified key.</param>
    /// <returns>Returns the current <see cref="SpiBuilder"/> instance for method chaining.</returns>
    public SpiBuilder WithCommentOn(string key, string comment)
    {
        if (_config.TryGetValue(key, out var node))
            node.Comment = comment;

        return this;
    }

    /// <summary>
    /// Builds and returns the SPI bus configuration as a YAML mapping.
    /// </summary>
    /// <returns>
    /// An instance of <see cref="IYamlMapping"/> representing the SPI bus configuration.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown when the required clock pin (clk_pin) has not been specified.
    /// </exception>
    internal IYamlMapping Build()
    {
        if (!_config.ContainsKey(ClkPinKey))
        {
            throw new InvalidOperationException("The clock pin (clk_pin) must be specified for the SPI bus.");
        }

        return _config;
    }

    /// <summary>
    /// Sets a specified pin with a given key in the configuration.
    /// </summary>
    /// <param name="pinKey">The key identifying the pin (e.g., "clk_pin", "mosi_pin").</param>
    /// <param name="pin">The pin value to be set in the configuration.</param>
    /// <returns>The current instance of <see cref="SpiBuilder"/> to allow method chaining.</returns>
    private SpiBuilder SetPin(string pinKey, string pin)
    {
        _config[pinKey] = new YamlString(pin);
        return this;
    }
}