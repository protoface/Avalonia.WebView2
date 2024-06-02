namespace Avalonia.WebView;

public class WebView2KeyEventArgs : KeyEventArgs
{
    public long Timestamp { get; } = Environment.TickCount64;

    public WebView2KeyEventArgs(Key key) => Key = key;
}