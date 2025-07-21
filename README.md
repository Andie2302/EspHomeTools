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
using EspHomeTools.Classes.Structures;

// 1. Erstelle den Root-Knoten für die YAML-Datei
var root = new YamlMapping();

// 2. Nutze die Fluent-Builder-API, um die Konfiguration zu erstellen
root.WithEsphome(esphome =>
    {
        esphome.WithName("wohnzimmer_sensor")
               .WithCommentOn("name", "Der eindeutige Name des Geräts im Netzwerk.")
               // Füge einen on_boot-Trigger hinzu, um Aktionen beim Gerätestart auszuführen
               .OnBoot(actions =>
               {
                   actions.Lambda(@"ESP_LOGI(""main"", ""Gerät wurde erfolgreich gestartet!"");")
                          .Delay("2s")
                          // Die Aktion zielt auf die eindeutige ID des Lichts
                          .LightTurnOn("dimmbares_licht");
               });
    })
    .WithEsp32(esp32 =>
    {
        esp32.WithBoard("esp32dev")
             .WithCommentOn("board", "Verwendet ein Standard-ESP32-Entwicklungsboard.");
    })
    .WithWifi(wifi =>
    {
        wifi.WithSsid("MeinSuperWLAN")
            .AddComment("ssid", "Die SSID deines primären WLAN-Netzwerks.\nMuss 2,4 GHz sein.")
            .WithPassword(new YamlSecret("wifi_passwort"))
            .AddComment("password", "Das WLAN-Passwort, sicher in 'secrets.yaml' gespeichert.");
    })
    .WithMqtt(mqtt =>
    {
        mqtt.WithBroker("192.168.1.100")
            .WithCommentOn("broker", "IP-Adresse des Mosquitto MQTT-Brokers.")
            .WithUsername("mqtt_benutzer", isSecret: true)
            .WithPassword("mqtt_passwort", isSecret: true);
    })
    .WithLogger() // Fügt einen leeren Logger-Block hinzu
    .WithApi(api =>
    {
        api.WithEncryptionKey("DEIN_VERSCHLUESSELUNGSKEY_HIER", isSecret: true)
           .WithCommentOn("encryption", "Sichert die native API-Kommunikation mit Home Assistant.");
    })
    .WithOta(ota =>
    {
        // In der aktuellen Implementierung ist OTA eine Liste, kein einzelnes Objekt.
        // Die Konfiguration erfolgt direkt auf dem Builder.
    })
    .WithI2C(i2c =>
    {
        i2c.SetSdaPin("GPIO21")
           .SetSclPin("GPIO22")
           .WithScan(true)
           .WithId("i2c_bus")
           .WithCommentOn("scan", "Sucht beim Start nach I2C-Geräten, nützlich für das Debugging.");
    })
    .WithSpi(spi =>
    {
        spi.SetClkPin("GPIO18")
           .SetMosiPin("GPIO23")
           .SetMisoPin("GPIO19")
           .WithId("spi_bus")
           .WithCommentOn("id", "SPI-Bus für Hochgeschwindigkeitskomponenten wie Displays.");
    })
    .WithTime(time =>
    {
        time.WithPlatform("homeassistant")
            .WithId("ha_zeit")
            .WithComment("platform", "Verwendet Home Assistant als Quelle für die aktuelle Uhrzeit.");
    })
    .WithDhtSensor(dht =>
    {
        dht.UsePin("GPIO2")
           .WithCommentOn("pin", "Der Datenpin für den DHT22-Sensor.")
           .WithTemperature("Wohnzimmer Temperatur")
           .WithHumidity("Wohnzimmer Luftfeuchtigkeit")
           .WithUpdateInterval("60s")
           .WithCommentOn("update_interval", "Sensordaten alle 60 Sekunden auslesen.");
    })
    .WithEnvironmentalSensor(bme =>
    {
        bme.WithPlatform("bme280") // BME280 auf I2C
           .WithI2CAddress(0x76)
           .WithTemperature("BME280 Temperatur")
           .WithPressure("BME280 Luftdruck")
           .WithHumidity("BME280 Luftfeuchtigkeit")
           .WithUpdateInterval("60s")
           .WithCommentOn("platform", "Umweltsensor für Temperatur, Luftfeuchtigkeit und Luftdruck.");
    })
    .WithGpioSwitch(sw =>
    {
        sw.UsePin("GPIO1")
          .WithName("Wohnzimmerlampe")
          .WithCommentOn("name", "Anzeigename des Schalters in Home Assistant.")
          .WithId("wohnzimmer_lampe")
          .WithIcon("mdi:lightbulb");
    })
    .WithBinarySensor(bs =>
    {
        bs.UsePin("GPIO5")
          .WithName("Bewegungsmelder")
          .WithDeviceClass("motion")
          .WithCommentOn("name", "PIR-Sensor im Flur.")
          // Die Aktion zielt auf die eindeutige ID des Lichts
          .OnPress(actions => {
              actions.LightTurnOn("dimmbares_licht");
          });
    })
    .WithOutput(o =>
    {
        o.WithPlatform("ledc")
         .UsePin("GPIO4")
         .WithId("led_pwm_ausgang")
         .WithCommentOn("id", "Dieser PWM-Ausgang steuert den dimmbaren LED-Streifen.");
    })
    .WithLight(l =>
    {
        l.WithPlatform("monochromatic")
         .WithName("Dimmbarer LED-Streifen")
         .UseOutput("led_pwm_ausgang")
         // Das Licht erhält eine eigene ID, um von Aktionen gesteuert zu werden
         .WithId("dimmbares_licht")
         .WithCommentOn("output", "Verknüpft dieses Licht mit dem oben definierten 'ledc' PWM-Kanal.");
    });

// 3. Generiere und drucke den fertigen YAML-String
Console.WriteLine(root.ToYaml());
```

### Generated YAML Output

The C# code above generates the following perfectly formatted YAML file:

```yaml
esphome:
  # Der eindeutige Name des Geräts im Netzwerk.
  name: wohnzimmer_sensor
  on_boot:
    - lambda: |-
        ESP_LOGI("main", "Gerät wurde erfolgreich gestartet!");
    - delay: 2s
    - light.turn_on: dimmbares_licht
esp32:
  # Verwendet ein Standard-ESP32-Entwicklungsboard.
  board: esp32dev
wifi:
  # Die SSID deines primären WLAN-Netzwerks.
  # Muss 2,4 GHz sein.
  ssid: MeinSuperWLAN
  # Das WLAN-Passwort, sicher in 'secrets.yaml' gespeichert.
  password: !secret wifi_passwort
mqtt:
  # IP-Adresse des Mosquitto MQTT-Brokers.
  broker: 192.168.1.100
  username: !secret mqtt_benutzer
  password: !secret mqtt_passwort
logger:
api:
  # Sichert die native API-Kommunikation mit Home Assistant.
  encryption:
    key: !secret DEIN_VERSCHLUESSELUNGSKEY_HIER
ota:
  - platform: esphome
i2c:
  sda: GPIO21
  scl: GPIO22
  # Sucht beim Start nach I2C-Geräten, nützlich für das Debugging.
  scan: true
  id: i2c_bus
spi:
  clk_pin: GPIO18
  mosi_pin: GPIO23
  miso_pin: GPIO19
  # SPI-Bus für Hochgeschwindigkeitskomponenten wie Displays.
  id: spi_bus
time:
  - # Verwendet Home Assistant als Quelle für die aktuelle Uhrzeit.
    platform: homeassistant
    id: ha_zeit
sensor:
  - platform: dht
    # Der Datenpin für den DHT22-Sensor.
    pin: GPIO2
    temperature:
      name: Wohnzimmer Temperatur
    humidity:
      name: Wohnzimmer Luftfeuchtigkeit
    # Sensordaten alle 60 Sekunden auslesen.
    update_interval: 60s
  - # Umweltsensor für Temperatur, Luftfeuchtigkeit und Luftdruck.
    platform: bme280
    address: 118
    temperature:
      name: BME280 Temperatur
      oversampling: 16x
    pressure:
      name: BME280 Luftdruck
      oversampling: 16x
    humidity:
      name: BME280 Luftfeuchtigkeit
      oversampling: 16x
    update_interval: 60s
switch:
  - platform: gpio
    pin: GPIO1
    # Anzeigename des Schalters in Home Assistant.
    name: Wohnzimmerlampe
    id: wohnzimmer_lampe
    icon: "mdi:lightbulb"
binary_sensor:
  - platform: gpio
    pin: GPIO5
    # PIR-Sensor im Flur.
    name: Bewegungsmelder
    device_class: motion
    on_press:
      - light.turn_on: dimmbares_licht
output:
  - platform: ledc
    pin: GPIO4
    # Dieser PWM-Ausgang steuert den dimmbaren LED-Streifen.
    id: led_pwm_ausgang
light:
  - platform: monochromatic
    name: "Dimmbarer LED-Streifen"
    # Verknüpft dieses Licht mit dem oben definierten 'ledc' PWM-Kanal.
    output: led_pwm_ausgang
    id: dimmbares_licht
```

## Project Goals (may change)

* text-sensor builder
* web_server builder
* pulse_counter builder
* hx711 builder
* ina219 builder
* fan builder
* cover builder
* stepper builder
* comments
* test case

## maybe

* display builder
* ssd1306 builder
* sh1106 builder
* ethernet builder
* bluetooth_proxy builder
* font builder

## License

This project is licensed under the [MIT License](LICENSE).
