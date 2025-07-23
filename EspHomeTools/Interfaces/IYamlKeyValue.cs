namespace EspHomeTools.Interfaces;

public interface IYamlKeyValue : IYamlKey, IYamlValue;
public interface IYamlKeyValue<T> : IYamlKeyValue, IYamlValue<T>;