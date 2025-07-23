using EspHomeTools.Interfaces.RenderManagers;

namespace EspHomeTools.Interfaces;

/// <summary>
/// Represents a base interface for objects that can be serialized into YAML format.
/// </summary>
/// <remarks>
/// This interface extends the capabilities of <see cref="IYamlRenderable"/>
/// by serving as the foundation for YAML object representation.
/// </remarks>
public interface IYamlObject : IYamlRenderable { }