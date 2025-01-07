using UrlShrink.Services.Interfaces;

namespace UrlShrink.Services;

public class AlertService : IAlertService
{
    public Task ShowAlertMessageAsync(
        string title,
        string message,
        string buttonText)
    {
        Application.Current?.MainPage?.DisplayAlert(title, message, buttonText);
        return Task.CompletedTask;
    }
}
