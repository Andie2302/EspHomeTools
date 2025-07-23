using EspHomeTools.Interfaces;

namespace EspHomeTools.Scratch.Interfaces.Scalars;

public interface IYamlScalar;

public interface IYamlScalar<T> : IYamlScalar, IYamlValue<T>;