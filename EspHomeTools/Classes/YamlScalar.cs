using System;
using EspHomeTools.Interfaces;

namespace EspHomeTools.Classes;

public class YamlScalar : IYamlScalar
{
    public void Render(YamlRenderManager yamlRenderManager, int indentationLevel)
    {
        Console.WriteLine("Hallo von Render in YamlScalar!");
    }
}