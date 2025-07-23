using EspHomeTools.Scratch.Interfaces.RenderManagers;

namespace EspHomeTools.Interfaces;

/// <summary>
/// Represents an interface defining the ability of an object to render its state into a YAML representation.
/// </summary>
public interface IYamlRenderable
{
    /// <summary>
    /// Renders the YAML representation of an object using the specified render manager and indentation level.
    /// </summary>
    /// <param name="yamlRenderManager">The render manager utilized for generating YAML content in a formatted manner.</param>
    /// <param name="indentationLevel">The level of indentation applied during the rendering process.</param>
    void Render(IYamlRenderManager yamlRenderManager, int indentationLevel);
}