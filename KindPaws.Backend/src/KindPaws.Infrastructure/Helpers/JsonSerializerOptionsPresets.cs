using System.Text.Json;

namespace KindPaws.Infrastructure.Helpers;

public static class JsonSerializerOptionsPresets
{
    public static readonly JsonSerializerOptions Default = new()
    {
        WriteIndented = false,
        PropertyNameCaseInsensitive = true
    };
}