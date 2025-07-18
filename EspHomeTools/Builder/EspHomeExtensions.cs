using System;
using EspHomeTools.Classes;
using EspHomeTools.Interfaces;

namespace EspHomeTools.Builder;

public static class EspHomeExtensions
{
    public static IYamlMapping WithEsphome(this IYamlMapping root, Action<EsphomeBlockBuilder> configurator)
    {
        var builder = new EsphomeBlockBuilder();
        configurator(builder); // Führt die vom Benutzer definierte Konfiguration aus
        root["esphome"] = builder.Build();
        return root;
    }

    public static IYamlMapping WithWifi(this IYamlMapping root, Action<WifiBlockBuilder> configurator)
    {
        var builder = new WifiBlockBuilder();
        configurator(builder);
        root["wifi"] = builder.Build();
        return root;
    }

    public static IYamlMapping WithLogger(this IYamlMapping mapping)
    {
        mapping.Add("logger", new YamlMapping());
        return mapping;
    }

    public static IYamlMapping WithApi(this IYamlMapping mapping)
    {
        mapping.Add("api", new YamlMapping());
        return mapping;
    }

    public static IYamlMapping WithOta(this IYamlMapping mapping)
    {
        mapping.Add("ota", new YamlMapping());
        return mapping;
    }

    public static IYamlMapping WithComponent(this IYamlMapping root, string componentType, IYamlMapping componentConfig)
    {
        if (!root.TryGetValue(componentType, out var node) || node is not IYamlSequence sequence)
        {
            sequence = new YamlSequence();
            root.Add(componentType, sequence);
        }

        sequence.Add(componentConfig);
        return root;
    }

    /// <summary>
    /// Fügt einen DHT-Sensor hinzu und konfiguriert ihn.
    /// </summary>
    public static IYamlMapping WithDhtSensor(this IYamlMapping root, Action<DhtSensorBuilder> configurator)
    {
        var builder = new DhtSensorBuilder();
        configurator(builder); // Führt die Benutzerkonfiguration aus
        var sensorConfig = builder.Build();

        // Verwendet die bereits existierende Helfermethode, um die Komponente hinzuzufügen
        return root.WithComponent("sensor", sensorConfig);
    }

}



// ============== NEUER SENSOR-BUILDER ==============

/// <summary>
/// Erstellt und konfiguriert eine DHT-Sensor-Komponente.
/// </summary>
public class DhtSensorBuilder
{
    private readonly YamlMapping _config = new();

    public DhtSensorBuilder()
    {
        // Die Plattform wird automatisch gesetzt, der Benutzer muss das nicht wissen.
        _config["platform"] = new YamlString("dht");
    }

    /// <summary>
    /// Setzt den Pin, an dem der Sensor angeschlossen ist (erforderlich).
    /// </summary>
    public DhtSensorBuilder UsePin(string pin)
    {
        _config["pin"] = new YamlString(pin);
        return this;
    }

    /// <summary>
    /// Konfiguriert den Temperatur-Sensor.
    /// </summary>
    public DhtSensorBuilder WithTemperature(string name)
    {
        _config["temperature"] = new YamlMapping { { "name", new YamlString(name) } };
        return this;
    }

    /// <summary>
    /// Konfiguriert den Feuchtigkeits-Sensor.
    /// </summary>
    public DhtSensorBuilder WithHumidity(string name)
    {
        _config["humidity"] = new YamlMapping { { "name", new YamlString(name) } };
        return this;
    }

    /// <summary>
    /// Setzt das Intervall für die Messungen (z.B. "60s").
    /// </summary>
    public DhtSensorBuilder WithUpdateInterval(string interval)
    {
        _config["update_interval"] = new YamlString(interval);
        return this;
    }

    /// <summary>
    /// Setzt das Sensormodell (z.B. "DHT22", "AM2302"). Standardmäßig wird es automatisch erkannt.
    /// </summary>
    public DhtSensorBuilder WithModel(string model)
    {
        _config["model"] = new YamlString(model);
        return this;
    }

    /// <summary>
    /// Interne Methode, um den fertigen YAML-Knoten zu erstellen.
    /// </summary>
    internal IYamlMapping Build()
    {
        if (!_config.ContainsKey("pin"))
        {
            throw new InvalidOperationException("Ein Pin muss für den DHT-Sensor mit UsePin() angegeben werden.");
        }
        if (!_config.ContainsKey("temperature") && !_config.ContainsKey("humidity"))
        {
            throw new InvalidOperationException("Für einen DHT-Sensor muss mindestens Temperatur (WithTemperature) oder Feuchtigkeit (WithHumidity) konfiguriert werden.");
        }
        return _config;
    }
}

