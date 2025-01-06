namespace UrlShrink.Services;

public class UrlResponse
{
    public UrlData? data { get; set; }
}

public class UrlData
{
    public string tiny_url { get; set; }
}
