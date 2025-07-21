namespace EspHomeTools.Classes.Scalars;

public class YamlString : YamlScalar<string>
{
    public YamlString(string value) => Value = value;

    public override sealed string? Value
    {
        get => base.Value;
        set => base.Value = YamlTools.Normalize(value);
    }

    protected override string SerializeValue() => ShouldQuoteValue() ? CreateQuotedString() : Value ?? string.Empty;

    private bool ShouldQuoteValue() => YamlTools.NeedsQuoting(Value);

    private string CreateQuotedString() => YamlTools.CreateQuotedValue(Value ?? string.Empty);
}