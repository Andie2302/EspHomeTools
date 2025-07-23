using EspHomeTools.Interfaces;

namespace EspHomeTools.Classes;

public abstract class YamlScalarBase<T> : IYamlScalar<T>
{
    public virtual void Render(IYamlRenderManager yamlRenderManager, int indentationLevel) => yamlRenderManager.AppendScalar(this, indentationLevel);

    public virtual T Value { get; set; }
}