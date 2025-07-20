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
        esphome.WithName("living_room_sensor")
               .WithCommentOn("name", "This is the unique name for the device on the network.")
               // Add an on_boot trigger to run actions when the device starts
               .OnBoot(actions =>
               {
                   actions.Lambda(@"ESP_LOGI(""main"", ""Device has successfully booted!"");")
                          .Delay("2s")
                          // Action now targets the unique light ID
                          .LightTurnOn("dimmable_light");
               });
    })
    .WithEsp32(esp32 =>
    {
        esp32.WithBoard("esp32dev")
             .WithCommentOn("board", "Using a standard ESP32 development kit.");
    })
    .WithWifi(wifi =>
    {
        wifi.WithSsid("MySuperWiFi")
            .WithCommentOn("ssid", "The SSID of your primary WiFi network.\nMust be 2.4 GHz.")
            .WithPassword(new YamlSecret("wifi_password"))
            .WithCommentOn("password", "The WiFi password, stored securely in 'secrets.yaml'.");
    })
    .WithMqtt(mqtt =>
    {
        mqtt.WithBroker("192.168.1.100")
            .WithCommentOn("broker", "IP address of the Mosquitto MQTT broker.")
            .WithUsername("mqtt_user", isSecret: true)
            .WithPassword("mqtt_pass", isSecret: true);
    })
    .WithLogger()
    .WithApi(api =>
    {
        api.WithEncryptionKey("YOUR_ENCRYPTION_KEY_HERE", isSecret: true)
           .WithCommentOn("encryption", "Secures the native API communication with Home Assistant.");
    })
    .WithOta(ota =>
    {
        ota.WithPassword("YOUR_OTA_PASSWORD_HERE", isSecret: true)
           .WithCommentOn("password", "Secures over-the-air updates.");
    })
    .WithI2C(i2c =>
    {
        i2c.SetSdaPin("GPIO21") // Corrected pin name for esp32dev
           .SetSclPin("GPIO22") // Corrected pin name for esp32dev
           .WithScan(true)
           .WithId("bus_a")
           .WithCommentOn("scan", "Scans for I2C devices on startup, useful for debugging.");
    })
    .WithSpi(spi =>
    {
        spi.SetClkPin("GPIO18") // Using GPIO naming for consistency
           .SetMosiPin("GPIO23")
           .SetMisoPin("GPIO19")
           .WithId("bus_b")
           .WithCommentOn("id", "SPI bus for high-speed components like displays.");
    })
    .WithTime(time =>
    {
        time.WithPlatform("homeassistant")
            .WithId("ha_time")
            .WithCommentOn("platform", "Use Home Assistant as the source for the current time.");
    })
    .WithDhtSensor(dht =>
    {
        dht.UsePin("GPIO2") // Using GPIO naming for consistency
           .WithCommentOn("pin", "The data pin for the DHT22 sensor.")
           .WithTemperature("Living Room Temperature")
           .WithHumidity("Living Room Humidity")
           .WithUpdateInterval("60s")
           .WithCommentOn("update_interval", "Read sensor data every 60 seconds.");
    })
    .WithEnvironmentalSensor(bme =>
    {
        bme.WithPlatform("bme280_i2c") // Corrected platform for I2C
           .WithI2CAddress(0x76)
           .WithTemperature("BME280 Temperature")
           .WithPressure("BME280 Pressure")
           .WithHumidity("BME280 Humidity")
           .WithUpdateInterval("60s")
           .WithCommentOn("platform", "Environmental sensor for temp, humidity, and pressure.");
    })
    .WithGpioSwitch(sw =>
    {
        sw.UsePin("GPIO1") // Using GPIO naming for consistency
          .WithName("Living Room Lamp")
          .WithCommentOn("name", "Friendly name for the switch in Home Assistant.")
          .WithId("living_room_lamp")
          .WithIcon("mdi:lightbulb");
    })
    .WithBinarySensor(bs =>
    {
        bs.UsePin("GPIO5") // Using GPIO naming for consistency
          .WithName("Motion Sensor")
          .WithDeviceClass("motion")
          .WithCommentOn("name", "PIR sensor in the hallway.")
          // Action now targets the unique light ID
          .OnPress(actions => {
              actions.LightTurnOn("dimmable_light");
          });
    })
    .WithBinarySensor(bs =>
    {
        bs.UsePin("GPIO25") // Corrected from GPIO6, which is used by flash memory
          .WithName("Window Contact")
          .WithDeviceClass("window")
          .WithCommentOn("name", "Magnetic contact sensor on the living room window.");
    })
    .WithOutput(o =>
    {
        // For dimmable lights on ESP32, the 'output' platform must be 'ledc'.
        o.WithPlatform("ledc")
         .UsePin("GPIO4")
         .WithId("led_pwm_output")
         .WithCommentOn("id", "This PWM output controls the dimmable LED strip.");
    })
    .WithLight(l =>
    {
        l.WithPlatform("monochromatic")
         .WithName("Dimmable LED Strip")
         // The light uses the 'ledc' output's unique ID
         .UseOutput("led_pwm_output")
         // The light gets its own unique ID to be controlled by actions
         .WithId("dimmable_light")
         .WithCommentOn("output", "Links this light to the 'ledc' PWM channel defined above.");
    });

// 3. Generate and print the final YAML string
Console.WriteLine(root.ToYaml());


```

### Generated YAML Output

The C# code above generates the following perfectly formatted YAML file:

```yaml
esphome:
  # This is the unique name for the device on the network.
  name: living_room_sensor
  on_boot:
    - lambda: |-
        ESP_LOGI("main", "Device has successfully booted!");
    - delay: 2s
    - light.turn_on: dimmable_light
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
  # Secures the native API communication with Home Assistant.
  encryption:
    key: !secret YOUR_ENCRYPTION_KEY_HERE
ota:
  - platform: esphome
    # Secures over-the-air updates.
    password: !secret YOUR_OTA_PASSWORD_HERE
i2c:
  sda: GPIO21
  scl: GPIO22
  # Scans for I2C devices on startup, useful for debugging.
  scan: true
  id: bus_a
spi:
  clk_pin: GPIO18
  mosi_pin: GPIO23
  miso_pin: GPIO19
  # SPI bus for high-speed components like displays.
  id: bus_b
time:
  - # Use Home Assistant as the source for the current time.
    platform: homeassistant
    id: ha_time
sensor:
  - platform: dht
    # The data pin for the DHT22 sensor.
    pin: GPIO2
    temperature:
      name: Living Room Temperature
    humidity:
      name: Living Room Humidity
    # Read sensor data every 60 seconds.
    update_interval: 60s
  - # Environmental sensor for temp, humidity, and pressure.
    platform: bme280_i2c
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
    pin: GPIO1
    # Friendly name for the switch in Home Assistant.
    name: Living Room Lamp
    id: living_room_lamp
    icon: "mdi:lightbulb"
binary_sensor:
  - platform: gpio
    pin: GPIO5
    # PIR sensor in the hallway.
    name: Motion Sensor
    device_class: motion
    on_press:
      - light.turn_on: dimmable_light
  - platform: gpio
    pin: GPIO25
    # Magnetic contact sensor on the living room window.
    name: Window Contact
    device_class: window
output:
  - platform: ledc
    pin: GPIO4
    # This PWM output controls the dimmable LED strip.
    id: led_pwm_output
light:
  - platform: monochromatic
    name: Dimmable LED Strip
    # Links this light to the 'ledc' PWM channel defined above.
    output: led_pwm_output
    id: dimmable_light

```

## Project Goals (may change)

* text-sensor builder
* pulse_counter
* hx711
* ina219
* fan
* cover
* stepper

## maybe
* display
* font
* ssd1306
* sh1106
* web_server
* ethernet
* bluetooth_proxy


## License

This project is licensed under the [MIT License](LICENSE).
