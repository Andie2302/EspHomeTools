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
using EspHomeTools.Builders;
using EspHomeTools.Classes.Scalars;
using EspHomeTools.Classes.Structures; // Required for YamlSecret

// 1. Create the root node for the YAML file
var root = new YamlMapping();

// 2. Use the fluent builder API to construct the configuration
root.WithEsphome(esphome =>
    {
        esphome.WithName("living_room_sensor");
    })
    .WithEsp32(esp32 => // Or WithEsp8266, WithRp2040, etc.
    {
        esp32.WithBoard("esp32dev");
    })
    .WithWifi(wifi =>
    {
        // Use !secret for sensitive data like passwords
        wifi.WithSsid("MySuperWiFi")
            .WithPassword(new YamlSecret("wifi_password")); // Generates: !secret wifi_password
    })
    .WithLogger()
    .WithApi()
    .WithOta() // This now matches your implementation
    .WithDhtSensor(dht =>
    {
        dht.UsePin("D2")
            .WithTemperature("Living Room Temperature")
            .WithHumidity("Living Room Humidity")
            .WithUpdateInterval("60s");
    })
    .WithGpioSwitch(sw =>
    {
        sw.UsePin("D1")
            .WithName("Living Room Lamp")
            .WithId("living_room_lamp")
            .WithIcon("mdi:lightbulb");
    });

// 3. Generate and print the final YAML string
Console.WriteLine(root.ToYaml().Trim());
```

### Generated YAML Output

The C# code above generates the following perfectly formatted YAML file:

```yaml
esphome:
  name: living_room_sensor
esp32:
  board: esp32dev
wifi:
  ssid: MySuperWiFi
  password: !secret wifi_password
logger:
api:
ota:
sensor:
  - platform: dht
    pin: D2
    temperature:
      name: Living Room Temperature
    humidity:
      name: Living Room Humidity
    update_interval: 60s
switch:
  - platform: gpio
    pin: D1
    name: Living Room Lamp
    id: living_room_lamp
    icon: "mdi:lightbulb"
```

## Project Goals (may change)

* **Single-line comments**
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
