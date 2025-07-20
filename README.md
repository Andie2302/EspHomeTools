# EspHomeTools

**EspHomeTools** is a .NET library for programmatically creating [ESPHome](https://esphome.io/) configurations using C#. Instead of writing YAML files by hand, you can leverage a type-safe and intuitive fluent builder API to generate your configurations.

## Why EspHomeTools?

Manually writing YAML can be error-prone—a single misplaced space can invalidate the entire configuration. EspHomeTools solves this by allowing you to define your device configurations in C#.

* **Type-Safety:** Catch errors at compile time, not when you're trying to flash your device.
* **Intuitive API:** The self-documenting builder methods guide you through the configuration process.
* **Automation:** Programmatically generate device configurations based on your application's logic.
* **Maintainability:** Keep your configurations clean, well-structured, and version-controlled right alongside your C# code.

## Features

* A fluent builder API for clean and readable configuration code.
* Built-in support for all core ESPHome components (`esphome`, `wifi`, `logger`, `api`, `ota`).
* Integrated builders for major hardware platforms (`esp32`, `esp8266`, `rp2040`, `bk72xx`).
* High-level builders for common components like `dht` sensors and `gpio` switches.
* Correctly handles YAML intricacies like quoting and `!secret` tags.
* Easily extensible to support any ESPHome component.

## Installation

*This project is not yet available as a NuGet package.*

To use the library, clone this repository and add a project reference to `EspHomeTools.csproj` in your solution.

```bash
git clone [https://github.com/andie2302/esphometools.git](https://github.com/andie2302/esphometools.git)
```

## Quick Start & Example

Here’s how you can easily generate a complete ESPHome configuration for a device with a DHT sensor and a switch.

```csharp
using System;
using EspHomeTools.Classes.Scalars;
using EspHomeTools.Classes.Structures;
using EspHomeTools.Interfaces;

namespace EspHomeTools.Builders;

public class WifiBlockBuilder
{
    private readonly YamlMapping _block = new();

    public WifiBlockBuilder WithSsid(string ssid)
    {
        _block["ssid"] = new YamlString(ssid);
        return this;
    }

    public WifiBlockBuilder WithSsid(YamlSecret ssid)
    {
        _block["ssid"] = ssid;
        return this;
    }

    public WifiBlockBuilder WithSsid(string ssid, bool isSecret) => isSecret ? WithSsid(new YamlSecret(ssid)) : WithSsid(ssid);

    public WifiBlockBuilder WithPassword(string password)
    {
        _block["password"] = new YamlString(password);
        return this;
    }

    public WifiBlockBuilder WithPassword(YamlSecret password)
    {
        _block["password"] = password;
        return this;
    }

    public WifiBlockBuilder WithPassword(string password, bool isSecret) => isSecret ? WithPassword(new YamlSecret(password)) : WithPassword(password);

    public WifiBlockBuilder WithAccessPoint(Action<AccessPointBlockBuilder> configurator)
    {
        var builder = new AccessPointBlockBuilder();
        configurator(builder);
        _block["ap"] = builder.Build();
        return this;
    }

    public WifiBlockBuilder WithCommentOn(string key, string comment)
    {
        if (_block.TryGetValue(key, out var node))
            node.Comment = comment;

        return this;
    }

    internal IYamlMapping Build()
    {
        if (!_block.ContainsKey("ssid") || !_block.ContainsKey("password"))
        {
            throw new InvalidOperationException("SSID und Passwort sind im 'wifi'-Block erforderlich.");
        }

        return _block;
    }
}

```

### Generated YAML Output

The C# code above generates the following perfectly formatted YAML file:

```yaml
esphome:
  # This is the unique name for the device on the network.
  name: living_room_sensor
esp32:
  # Using a standard ESP32 development kit.
  board: esp32dev
wifi:
  # The SSID of your primary WiFi network.
  # Must be 2.4 GHz.
  ssid: MySuperWiFi
  # The WiFi password, stored securely in 'secrets.yaml'.
  password: !secret wifi_password
mqtt:
  # IP address of the Mosquitto MQTT broker.
  broker: 192.168.1.100
  username: !secret mqtt_user
  password: !secret mqtt_pass
logger:
api:
ota:
i2c:
  sda: D21
  scl: D22
  # Scans for I2C devices on startup, useful for debugging.
  scan: true
  id: bus_a
spi:
  clk_pin: D18
  mosi_pin: D23
  miso_pin: D19
  # SPI bus for high-speed components like displays.
  id: bus_b
time:
  - # Use Home Assistant as the source for the current time.
    platform: homeassistant
    id: ha_time
sensor:
  - platform: dht
    # The data pin for the DHT22 sensor.
    pin: D2
    temperature:
      name: Living Room Temperature
    humidity:
      name: Living Room Humidity
    # Read sensor data every 60 seconds.
    update_interval: 60s
  - # Environmental sensor for temp, humidity, and pressure.
    platform: bme280
    address: 118
    temperature:
      name: BME280 Temperature
      oversampling: 16x
    pressure:
      name: BME280 Pressure
      oversampling: 16x
    humidity:
      name: BME280 Humidity
      oversampling: 16x
    update_interval: 60s
switch:
  - platform: gpio
    pin: D1
    # Friendly name for the switch in Home Assistant.
    name: Living Room Lamp
    id: living_room_lamp
    icon: "mdi:lightbulb"
binary_sensor:
  - platform: gpio
    pin: D5
    # PIR sensor in the hallway.
    name: Motion Sensor
    device_class: motion
  - platform: gpio
    pin: D6
    # Magnetic contact sensor on the living room window.
    name: Window Contact
    device_class: window
output:
  - platform: gpio
    pin: D4
    # This PWM output controls the dimmable LED strip.
    id: dimmable_led_output
light:
  - platform: monochromatic
    name: Dimmable LED Strip
    # Links this light to the PWM output defined above.
    output: dimmable_led_output

```

## Project Goals (may change)

* <details>
    <summary><strong>Maximum Type-Safety:</strong></summary>
    By using C#, errors are caught during development (compile-time), not when you're trying to flash your device. Manually writing YAML can be error-prone, as a single misplaced space can invalidate the entire configuration.
    </details>
* <details>
    <summary><strong>Intuitive Fluent API:</strong></summary>
    The self-documenting builder methods guide you through the configuration process, often eliminating the need to manually consult the ESPHome documentation.
    </details>
* <details>
    <summary><strong>Automation and Scalability:</strong></summary>
    Enables the programmatic creation of configurations. This is ideal for projects where configurations need to be generated dynamically based on application logic or for a large number of devices.
    </details>
* <details>
    <summary><strong>Improved Maintainability and Readability:</strong></summary>
    Keep your configurations clean, well-structured, and version-controlled—right alongside your C# code. This improves clarity compared to long, cumbersome YAML files.
    </details>
* <details>
    <summary><strong>Easy Extensibility:</strong></summary>
    The existing design makes it straightforward to add new ESPHome components. New builders can be easily created and integrated into the existing structure.
    </details>

## License

This project is licensed under the [MIT License](LICENSE).
