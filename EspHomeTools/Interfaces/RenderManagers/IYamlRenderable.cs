namespace EspHomeTools.Interfaces.Render;

/// <summary>
/// Defines a contract for objects that can render their state into YAML format.
/// </summary>
public interface IYamlRenderable
{
    /// <summary>
    /// Renders the YAML representation of an object using the specified render manager and indentation level.
    /// </summary>
    /// <param name="yamlRenderManager">The render manager used to generate YAML content.</param>
    /// <param name="indentationLevel">The level of indentation to apply during rendering.</param>
    void Render(IYamlRenderManager yamlRenderManager, int indentationLevel);
}