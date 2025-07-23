namespace EspHomeTools.Interfaces;

public interface IYamlKey : IYamlNode
{
    public string? Key { get; set; }
    public bool HasKey { get; }
}

