using System.Text.Json.Serialization;
using System.Text.Json;

namespace QuickClubs.AdminUI.Extensions;

internal static class HttpResponseMessageExtensions
{
    internal static async Task<T?> ToResult<T>(this HttpResponseMessage response)
    {
        var responseAsString = await response.Content.ReadAsStringAsync();
        var responseObject = JsonSerializer.Deserialize<T>(responseAsString, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            ReferenceHandler = ReferenceHandler.Preserve
        });
        return responseObject;
    }
}
