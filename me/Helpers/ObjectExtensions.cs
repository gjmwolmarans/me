using System.Buffers.Text;
using System.Text;
using System.Text.Json;

namespace me.Helpers;

public static class ObjectExtensions
{
    public static string Encode<T>(T data)
    {
        var json = JsonSerializer.Serialize(data);

        var plainTextBytes = Encoding.UTF8.GetBytes(json);
        return System.Convert.ToBase64String(plainTextBytes);
    }

    public static T? Decode<T>(string data)
    {
        var base64EncodedBytes = Convert.FromBase64String(data);
        var decoded = System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        return JsonSerializer.Deserialize<T>(decoded);
    }
}