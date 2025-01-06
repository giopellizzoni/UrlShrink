using UrlShrink.Services;

namespace UrlShrink.ViewModels;

public sealed class MainPageViewModel
{
    private readonly IURLService _urlService;

    public MainPageViewModel(IURLService urlService)
    {
        _urlService = urlService;
    }
}
