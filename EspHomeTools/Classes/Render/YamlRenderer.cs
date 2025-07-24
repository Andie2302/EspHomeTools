using System.Linq;
using System.Text;

namespace EspHomeTools.Classes.Render;

public static class EspHomeDeviceNameValidator
{
    private const int MaxDeviceNameLength = 24;

    public static bool IsValidEsphomeDeviceName(string name) =>
        name.Length <= MaxDeviceNameLength && name.All(c => char.IsLower(c) || char.IsDigit(c) || c == '-');

    public static string ToValidEsphomeDeviceName(string name) =>
        name.ToLowerInvariant().Replace(" ", "-");

    public static string ToValidDeviceName(string name)
    {
        var validName = name.ToLowerInvariant().Where(c => char.IsLetterOrDigit(c) || c == '-' || c == '_').Aggregate(new StringBuilder(), (sb, c) => sb.Append(c)).ToString().Replace('_', '-');
        return validName.Length > MaxDeviceNameLength ? validName.Substring(0, MaxDeviceNameLength) : validName;
    }
}