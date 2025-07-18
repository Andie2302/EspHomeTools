# EspHomeTools

[![Build Status](https://img.shields.io/badge/build-passing-brightgreen)](https://github.com/andie2302/esphometools)

**EspHomeTools** ist eine .NET-Bibliothek, die das Erstellen von [ESPHome](https://esphome.io/)-Konfigurationen in C# ermöglicht. Anstatt YAML-Dateien manuell zu schreiben, kannst du eine typsichere und intuitive Fluent-Builder-API verwenden, um deine Konfigurationen programmatisch zu generieren.

## Warum EspHomeTools?

Das manuelle Schreiben von YAML-Dateien kann fehleranfällig sein – ein falsches Leerzeichen kann die gesamte Konfiguration unbrauchbar machen. EspHomeTools löst dieses Problem, indem es dir erlaubt, deine Konfigurationen in C# zu definieren.

- **Typsicherheit:** Finde Fehler bereits zur Kompilierzeit, nicht erst beim Hochladen auf dein Gerät.
- **Intuitive API:** Die Builder-Methoden sind selbsterklärend und führen dich durch die Konfiguration.
- **Automatisierung:** Generiere dynamisch Konfigurationen basierend auf anderen Programmlogiken.
- **Wartbarkeit:** Halte deine Konfigurationen sauber und gut strukturiert direkt in deinem C#-Code.

## Features

- Fluent-Builder-API für eine saubere und lesbare Konfiguration.
- Unterstützung für alle grundlegenden ESPHome-Blöcke (`esphome`, `wifi`, `logger`, `api`, `ota`).
- Integrierte Builder für gängige Komponenten wie `dht`-Sensoren und `gpio`-Schalter.
- Korrekte Handhabung von YAML-Besonderheiten wie Anführungszeichen und `!secret`-Tags.
- Einfach erweiterbar für jede ESPHome-Komponente.

## Installation

*Dieses Projekt ist derzeit noch nicht als NuGet-Paket verfügbar.*

Um es zu verwenden, klone dieses Repository und füge das `EspHomeTools`-Projekt als Referenz zu deiner Solution hinzu.

```bash
git clone [https://github.com/andie2302/esphometools.git](https://github.com/andie2302/esphometools.git)
```

## Schnellstart & Beispiel

So einfach erstellst du eine komplette ESPHome-Konfiguration für ein Gerät mit einem DHT-Sensor und einem Schalter.

```csharp
using System;
using EspHomeTools.Builders;
using EspHomeTools.Classes;
using EspHomeTools.Classes.Scalars; // Für YamlSecret

// 1. Erstelle das Wurzel-Element der YAML-Datei
var root = new YamlMapping();

// 2. Verwende die Fluent-Builder, um die Konfiguration zu erstellen
root.WithEsphome(esphome =>
    {
        esphome.WithName("wohnzimmer-sensor");
    })
    .WithWifi(wifi =>
    {
        // Verwende !secret für sensible Daten
        wifi.WithSsid("MeinSuperWLAN")
            .WithPassword(new YamlSecret("wifi_passwort")); // Erzeugt: !secret wifi_passwort
    })
    .WithLogger()
    .WithApi()
    .WithOta()
    .WithDhtSensor(dht =>
    {
        dht.UsePin("D2")
           .WithTemperature("Wohnzimmer Temperatur")
           .WithHumidity("Wohnzimmer Luftfeuchtigkeit")
           .WithUpdateInterval("60s");
    })
    .WithGpioSwitch(sw =>
    {
        sw.UsePin("D1")
          .WithName("Wohnzimmer Licht")
          .WithId("wohnzimmer_licht")
          .WithIcon("mdi:lightbulb");
    });

// 3. Gib die fertige YAML-Datei auf der Konsole aus
Console.WriteLine(root.ToYaml().Trim());
```

### Erzeugte YAML-Ausgabe

Der obige C#-Code generiert die folgende, perfekt formatierte YAML-Datei:

```yaml
esphome:
  name: wohnzimmer-sensor
wifi:
  ssid: MeinSuperWLAN
  password: !secret wifi_passwort
logger:
api:
ota:
sensor:
  - platform: dht
    pin: D2
    temperature:
      name: Wohnzimmer Temperatur
    humidity:
      name: Wohnzimmer Luftfeuchtigkeit
    update_interval: 60s
switch:
  - platform: gpio
    pin: D1
    name: Wohnzimmer Licht
    id: wohnzimmer_licht
    icon: mdi:lightbulb
```

## Beitragen (Contributing)

Beiträge sind herzlich willkommen! Wenn du eine neue Komponente hinzufügst, stelle bitte sicher, dass du auch entsprechende Unit Tests erstellst, um die Funktionalität zu gewährleisten.

1.  Forke das Repository.
2.  Erstelle einen neuen Branch (`git checkout -b feature/neue-komponente`).
3.  Implementiere deine Änderungen und Tests.
4.  Committe deine Änderungen (`git commit -am 'Füge neuen Builder für XYZ hinzu'`).
5.  Pushe zum Branch (`git push origin feature/neue-komponente`).
6.  Öffne einen Pull Request.

## Lizenz

Dieses Projekt steht unter der [MIT-Lizenz](LICENSE).
