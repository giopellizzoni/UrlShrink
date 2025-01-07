using System.Text.Json.Serialization;

namespace UrlShrink.Models;

public class UrlResponse
{
    public UrlData? Data { get; set; }
}

public class UrlData
{
    [JsonPropertyName("tiny_url")]
    public string ShortenedUrl { get; set; }
}
