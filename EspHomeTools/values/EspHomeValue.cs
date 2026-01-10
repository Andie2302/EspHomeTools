namespace EspHomeTools.values;

public record EspHomeValue
{
    public object? Value { get; init; }
    public bool IsSecret { get; init; }

    public static implicit operator EspHomeValue(string s) => new(s,false) { Value = s };
    public static implicit operator EspHomeValue(int i) => new(i,false) { Value = i };

    public EspHomeValue(object value, bool isSecret)
    {
        Value = value;
        IsSecret = isSecret;
    }
}