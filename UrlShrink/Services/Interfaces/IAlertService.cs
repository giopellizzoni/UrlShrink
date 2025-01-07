namespace UrlShrink.Services.Interfaces;

public interface IAlertService
{
    Task ShowAlertMessageAsync(string title, string message, string buttonText);
}
