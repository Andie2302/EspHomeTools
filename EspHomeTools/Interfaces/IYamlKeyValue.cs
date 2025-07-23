namespace EspHomeTools.Interfaces;

public interface IYamlKeyValue : IYamlKey, IYamlValue, IYamlNode;
public interface IYamlKeyValue<T> : IYamlKeyValue, IYamlValue<T>;