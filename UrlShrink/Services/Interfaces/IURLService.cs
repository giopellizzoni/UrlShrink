using UrlShrink.Models;

namespace UrlShrink.Services.Interfaces;

public interface IURLService
{
    Task<UrlData?> CreateAShortenedURL(string url);
}
