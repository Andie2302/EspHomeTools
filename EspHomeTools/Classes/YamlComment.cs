using System;
using System.Collections.Generic;
using System.Linq;

namespace EspHomeTools.Classes;

public class YamlComment : IYamlComment
{
    private readonly List<string> _above = [];
    private readonly List<string> _below = [];
    private string? Inline { get; set; }

    public static IEnumerable<string> Check(IEnumerable<string> list)
    {
        foreach (var item in list)
        {
            if (string.IsNullOrWhiteSpace(item))
            {
                yield return string.Empty;
            }
            else
            {
                var trimed = item.Trim();
                if (trimed.Contains('\n') || trimed.Contains('\r')) // char statt string
                {
                    var lines = trimed.Split(["\r\n", "\n", "\r"], StringSplitOptions.None);
                    foreach (var line in lines)
                    {
                        if (string.IsNullOrWhiteSpace(line))
                        {
                            yield return string.Empty;
                        }
                        else
                        {
                            yield return line.Trim();
                        }
                    }
                }
                else
                {
                    yield return trimed;
                }
            }
        }
    }
    public static IEnumerable<string> Check(string comment) => Check([comment]);
    public void SetInline(string comment)
    {
        if (string.IsNullOrWhiteSpace(comment))
        {
            Inline = null;
            return;
        }

        if (comment.Contains('\n') || comment.Contains('\r'))
        {
            Inline = string.Join(" ", Check(comment));
        }
        else
        {
            Inline = comment;
        }
    }

    public void AddAbove(string comment)
    {
        _above.Add(comment);
    }

    public void AddBelow(string comment)
    {
        _below.Add(comment);
    }

    public void ClearAbove()
    {
        _above.Clear();
    }

    public void ClearBelow()
    {
        _below.Clear();
    }

    public void ClearInline()
    {
        Inline = null;
    }

    public void Clear()
    {
        ClearInline();
        ClearAbove();
        ClearBelow();
    }
    public bool HasInline => !string.IsNullOrEmpty(Inline);
    public bool HasAbove => _above.Count > 0;
    public bool HasBelow => _below.Count > 0;
    public bool HasAny => HasInline || HasAbove || HasBelow;
    public IEnumerable<string> Above => _above.AsReadOnly();
    public IEnumerable<string> Below => _below.AsReadOnly();
}