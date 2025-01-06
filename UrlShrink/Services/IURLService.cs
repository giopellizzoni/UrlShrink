namespace UrlShrink.Services;

public interface IURLService
{
    Task<UrlData?> CreateAShortenedURL(string url);
}
