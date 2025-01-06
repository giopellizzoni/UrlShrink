using System.Collections.ObjectModel;
using System.Windows.Input;

using UrlShrink.Models;
using UrlShrink.Services;

namespace UrlShrink.ViewModels;

public sealed class MainPageViewModel
{
    private readonly IURLService _urlService;

    public ObservableCollection<ShortenedUrl> Urls { get; set; } = [];

    public string Url { get; set; }

    public MainPageViewModel(IURLService urlService)
    {
        _urlService = urlService;
    }

    public ICommand ShortenUrlCommand => new Command(async () =>
    {
        if (string.IsNullOrEmpty(Url))
        {
            // Better Create an DialogService to handle this
            Application.Current?.MainPage?.DisplayAlert("Error", "Please enter a URL", "OK");
            return;
        }

        var shortenedUrl = await _urlService.CreateAShortenedURL(Url);
        if (shortenedUrl == null)
        {
            Application.Current?.MainPage?.DisplayAlert("Error", "Failed to shorten the URL", "OK");
            return;
        }

        // We can write the shortened URL to a cache or database
        Urls.Add(new ShortenedUrl { ShortenedURL = shortenedUrl.tiny_url });
        Url = string.Empty;
    });

    public ICommand DeleteCommand => new Command((url) =>
    {
        if (url is not string urlToDelete)
        {
            return;
        }

        var foundUrl = Urls.FirstOrDefault(x => x.ShortenedURL == urlToDelete);
        if (foundUrl != null)
        {
            Urls.Remove(foundUrl);
        }
    });
}
