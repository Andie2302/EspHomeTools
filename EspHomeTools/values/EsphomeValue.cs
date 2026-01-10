namespace EspHomeTools.values;

public record EsphomeValue
{
    public object? Value { get; init; }
    public bool IsSecret { get; init; }

    public static implicit operator EsphomeValue(string s) => new(s,false) { Value = s };
    public static implicit operator EsphomeValue(int i) => new(i,false) { Value = i };

    public EsphomeValue(object value, bool isSecret)
    {
        Value = value;
        IsSecret = isSecret;
    }
}