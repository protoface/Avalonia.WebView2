namespace Avalonia.WebView2.Sample;

public sealed partial class MainWindow : Window
{

    public MainWindow()
    {
        DataContext = this;
        InitializeComponent();

        if (AvaloniaWebView2.IsSupported)
        {
            UrlTextBox.KeyUp += UrlTextBox_KeyUp;
        }
    }

    void UrlTextBox_KeyUp(object? sender, KeyEventArgs e)
    {
        if (!UrlTextBox.Text.StartsWith(Prefix_HTTPS) && !UrlTextBox.Text.StartsWith(Prefix_HTTP))
        {
            UrlTextBox.Text = Prefix_HTTPS + UrlTextBox.Text;
            UrlTextBox.SelectionStart = UrlTextBox.SelectionEnd = UrlTextBox.Text.Length;
        }
        if (e.Key == Key.Enter)
        {
            var url = UrlTextBox.Text;
            if (Uri.IsWellFormedUriString(url, UriKind.Absolute))
                webview.Source = new(url);
        }

    }

    const string Prefix_HTTPS = "https://";
    const string Prefix_HTTP = "http://";

    static bool IsHttpUrl([NotNullWhen(true)] string? url, bool httpsOnly = false) => url != null &&
       (url.StartsWith(Prefix_HTTPS, StringComparison.OrdinalIgnoreCase) ||
             (!httpsOnly && url.StartsWith(Prefix_HTTP, StringComparison.OrdinalIgnoreCase)));
}
