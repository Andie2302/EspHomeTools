using System.Diagnostics.CodeAnalysis;

namespace EspHomeTools.Classes;

[SuppressMessage("ReSharper", "UnusedType.Global")]
public static class YamlRenderManagerExtensions
{
   public static void AppendComment(this YamlRenderManager manager, string comment, int indentationLevel,bool linebreak)
   {
      if (linebreak)
      {
         manager.AppendLine($"# {comment}", indentationLevel);
      }
      else
      {
         manager.Append($"# {comment}", indentationLevel);
      }
   }
}