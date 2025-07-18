using System;
using EspHomeTools.Builder;
using EspHomeTools.Interfaces;

namespace EspHomeTools.Classes;

public static partial class EspHomeExtensions
{
    /// <summary>
    /// Fügt einen GPIO-Schalter hinzu und konfiguriert ihn.
    /// </summary>
    public static IYamlMapping WithGpioSwitch(this IYamlMapping root, Action<GpioSwitchBuilder> configurator)
    {
        var builder = new GpioSwitchBuilder();
        configurator(builder); // Führt die Benutzerkonfiguration aus
        var switchConfig = builder.Build();

        // Verwendet die Helfermethode, um die Komponente unter dem Schlüssel 'switch:' hinzuzufügen
        return root.WithComponent("switch", switchConfig);
    }
}