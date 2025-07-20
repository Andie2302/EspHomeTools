using System;
using System.Text;
using EspHomeTools.Classes.Scalars;
using EspHomeTools.Classes.Structures;
using EspHomeTools.Interfaces;

namespace EspHomeTools.Builders;

/// <summary>
/// Represents a YAML scalar that is serialized as a multi-line literal block (using |-).
/// This is primarily used for lambda scripts in ESPHome.
/// </summary>
public class YamlLambda : YamlScalar<string>
{
    public YamlLambda(string value)
    {
        Value = value;
    }

    /// <summary>
    /// Overrides the default serialization to produce a YAML literal block style.
    /// </summary>
    public override string ToYaml(int indent = 0)
    {
        var sb = new StringBuilder();
        var prefix = new string(' ', indent);

        // Handle comments if they exist
        if (!string.IsNullOrWhiteSpace(Comment))
        {
            var commentLines = Comment?.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            foreach (var line in commentLines)
            {
                sb.Append(prefix).Append("# ").AppendLine(line);
            }
        }

        // Append the key (e.g., "lambda:") and the literal block indicator
        sb.Append(prefix);
        if (!string.IsNullOrWhiteSpace(Name))
        {
            sb.Append(Name).Append(':');
        }

        sb.AppendLine(" |-");

        // Append the indented code lines
        var codeLines = (Value ?? string.Empty).Trim().Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
        var codeIndent = new string(' ', indent + 2);
        foreach (var line in codeLines)
        {
            sb.Append(codeIndent).AppendLine(line);
        }

        return sb.ToString().TrimEnd('\r', '\n', ' ');
    }

    protected override string SerializeValue()
    {
        // This method is not used because ToYaml is fully overridden.
        throw new NotImplementedException();
    }
}
public class LedcBuilder
{
    private readonly YamlMapping _config = new();

    public LedcBuilder UsePin(string pin)
    {
        _config["pin"] = new YamlString(pin);
        return this;
    }

    public LedcBuilder WithId(string id)
    {
        _config["id"] = new YamlString(id);
        return this;
    }

    public LedcBuilder WithFrequency(string frequency)
    {
        _config["frequency"] = new YamlString(frequency);
        return this;
    }

    public LedcBuilder WithCommentOn(string key, string comment)
    {
        if (_config.TryGetValue(key, out var node))
            node.Comment = comment;

        return this;
    }

    internal IYamlMapping Build()
    {
        if (!_config.ContainsKey("pin"))
        {
            throw new InvalidOperationException("Ein Pin muss für die 'ledc'-Komponente mit UsePin() angegeben werden.");
        }

        if (!_config.ContainsKey("id"))
        {
            throw new InvalidOperationException("Eine ID muss für die 'ledc'-Komponente mit WithId() angegeben werden.");
        }

        return _config;
    }
}