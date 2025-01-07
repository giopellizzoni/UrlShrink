using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

using UrlShrink.Models;
using UrlShrink.Services;
using UrlShrink.Services.Interfaces;

namespace UrlShrink.ViewModels;

public sealed class MainPageViewModel : ObservableViewModel
{
    private readonly IURLService _urlService;
    private string _url;
    private bool _isRunning;

    public ObservableCollection<ShortenedUrl> Urls { get; set; } = [];
    public string Url
    {
        get => _url;
        set => SetField(ref _url, value);
    }
    public bool IsRunning
    {
        get => _isRunning;
        set => SetField(ref _isRunning, value);
    }

    public MainPageViewModel(IURLService urlService, IAlertService alertService) : base(alertService)
    {
        _urlService = urlService;
    }

    public ICommand ShortenUrlCommand => new Command(async () =>
    {
        IsRunning = true;
        if (string.IsNullOrEmpty(Url))
        {
            await _alertService.ShowAlertMessageAsync("Error", "Please enter a URL", "OK");
            IsRunning = false;
            return;
        }

        var shortenedUrl = await _urlService.CreateAShortenedURL(Url);
        IsRunning = false;

        if (shortenedUrl == null)
        {
            await _alertService.ShowAlertMessageAsync("Error", "Failed to shorten the URL", "OK");
        }
        else
        {
            // We can write the shortened URL to a cache or database
            Urls.Add(new ShortenedUrl { ShortenedURL = shortenedUrl.ShortenedUrl });
        }

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
