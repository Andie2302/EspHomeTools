namespace EspHomeTools.Interfaces;

public interface IYamlScalar<T> : IYamlScalar
{
}

public interface IYamlScalar : IYamlObject, IYamlRenderable { }