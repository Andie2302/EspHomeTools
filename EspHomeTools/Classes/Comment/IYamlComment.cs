namespace EspHomeTools.Classes.Comment;

public interface IYamlComment
{
    void SetInline(string comment);
    void AddAbove(string comment);
    void AddBelow(string comment);
    void ClearAbove();
    void ClearBelow();
    void ClearInline();
    void Clear();
    bool HasInline { get; }
    bool HasAbove { get; }
    bool HasBelow { get; }
    bool HasAny { get; }
}