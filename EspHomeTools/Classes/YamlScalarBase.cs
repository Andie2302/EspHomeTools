using System.Text;
using EspHomeTools.Interfaces;

namespace EspHomeTools.Classes;




public abstract class YamlScalarBase<TValue> : IYamlScalar<TValue>
{
    public TValue? Value { get; set; }
    public string? Name { get; set; }
    public string? Comment { get; set; }
    public string? Tag { get; set; }




    public string ToYaml(int indent = 0)
    {
        var sb = new StringBuilder();
        var prefix = new string(' ', indent);

        if (!string.IsNullOrWhiteSpace(Comment))
        {
            sb.Append(prefix).Append("# ").AppendLine(Comment);
        }

        if (!string.IsNullOrWhiteSpace(Name))
        {
            sb.Append(prefix).Append(Name).Append(':');

            if (!string.IsNullOrWhiteSpace(Tag))
            {
                sb.Append(' ').Append(Tag);
            }

            sb.Append(' ');
        }

        sb.Append(SerializeValue());

        return sb.ToString();
    }





    protected abstract string SerializeValue();
}