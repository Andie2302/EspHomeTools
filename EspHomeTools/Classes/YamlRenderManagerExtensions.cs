using System.Diagnostics.CodeAnalysis;
using EspHomeTools.Interfaces;

namespace EspHomeTools.Classes;

[SuppressMessage("ReSharper", "UnusedType.Global")]
public static class YamlRenderManagerExtensions
{
   public static void AppendComment(this IYamlRenderManager manager, string comment, int indentationLevel)
   {
      manager.AppendLine($"# {comment}", indentationLevel);
   }
}