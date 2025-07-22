namespace EspHomeTools.Interfaces;

public interface IYamlBuilder<out T> : IYamlBuilder
{
    T Build();
}

public interface IYamlBuilder { }
