namespace EspHomeTools.Interfaces;

public interface IYamlScalar;

public interface IYamlScalar<T> : IYamlScalar, IYamlValue<T>;