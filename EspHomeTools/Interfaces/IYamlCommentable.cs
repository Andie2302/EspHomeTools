using EspHomeTools.Classes;
using EspHomeTools.Classes.Comment;

namespace EspHomeTools.Interfaces;

public interface IYamlCommentable
{
    IYamlComment Comment { get; }
}