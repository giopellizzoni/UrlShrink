using System.Text;
using System.Text.Json;
using UrlShrink.Utils;

namespace UrlShrink.Services;

public class URLService : IURLService
{
    public async Task<UrlData?> CreateAShortenedURL(string url)
    {
        using var client = new HttpClient();

        const string fullUrl = $"{Constants.CreateUrl}?api_token={Constants.ApiKey}";
        var content = new StringContent(JsonSerializer.Serialize(new { url }), Encoding.UTF8, "application/json");
        var response = await client.PostAsync(fullUrl, content);

        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        using var responseStream = await response.Content.ReadAsStreamAsync();
        var urlResponse = await JsonSerializer.DeserializeAsync<UrlResponse>(responseStream);
        return urlResponse?.data;

    }
}
