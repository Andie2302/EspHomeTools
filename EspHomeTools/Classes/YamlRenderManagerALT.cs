  /*

namespace EspHomeTools.Classes;

public class YamlRenderManagerALT
{
  
    public static void RenderItems(List<YamlObject> items, YamlRenderManager manager, int indentationLevel)
    {
        if (items.Count != 0)
        {
            for (var index = 0; index < items.Count; index++)
            {
                if (index > 0)
                {
                    manager.AppendLine();
                }

                manager.AppendIndentation(indentationLevel);
                manager.AppendListItemPrefix();
                items[index].Render(manager, indentationLevel + ItemIndentationIncrease);
            }
        }
        else
        {
            manager.AppendEmptyList();
        }
    }
   
} //*/