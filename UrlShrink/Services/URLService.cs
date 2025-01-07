using System.Text;
using System.Text.Json;

using UrlShrink.Models;
using UrlShrink.Services.Interfaces;
using UrlShrink.Utils;

namespace UrlShrink.Services;

public class URLService : IURLService
{
    private readonly JsonSerializerOptions serializerOptions;

    public URLService()
    {
        serializerOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
    }

    public async Task<UrlData?> CreateAShortenedURL(string url)
    {
        const string fullUrl = $"{Constants.CreateUrl}?api_token={Constants.ApiToken}";
        var content = new StringContent(JsonSerializer.Serialize(new { url }), Encoding.UTF8, "application/json");

        using var client = new HttpClient();
        var response = await client.PostAsync(fullUrl, content);

        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        using var responseStream = await response.Content.ReadAsStreamAsync();
        var urlResponse = await JsonSerializer.DeserializeAsync<UrlResponse>(responseStream, serializerOptions);
        return urlResponse?.Data;
    }
}
