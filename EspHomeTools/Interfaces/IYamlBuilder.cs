namespace EspHomeTools.Interfaces;

public interface IYamlBuilder<T> : IYamlBuilder
{
    T Build();
}

public interface IYamlBuilder { }