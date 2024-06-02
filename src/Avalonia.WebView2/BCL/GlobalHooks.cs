namespace Avalonia.WebView.BCL;

sealed class GlobalHooks
{
    static bool IsInitialized;

    internal static readonly Dictionary<nint, GlobalHooks> _HookedWindows = new();
    static readonly List<WeakReference<WebView2>> _Views = new();

    internal static void Initialize(WebView2 view)
    {
        if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            return;

        lock (_Views)
            _Views.Add(new WeakReference<WebView2>(view));

        if (IsInitialized)
            return;

        IsInitialized = true;

        if (Application.Current?.ApplicationLifetime is not IClassicDesktopStyleApplicationLifetime lifetime)
            return;

        Window.WindowOpenedEvent.AddClassHandler(typeof(Window), TryAddGlobalHook, handledEventsToo: true);
        //EventManager.RegisterClassHandler(typeof(Window), FrameworkElement.SizeChangedEvent, new RoutedEventHandler(TryAddGlobalHook));

        foreach (var window in lifetime.Windows)
            TryAddGlobalHook(window);
    }

    readonly WindowsHwndSource _source;
    readonly WeakReference<Window> _windowRef;

    GlobalHooks(WindowsHwndSource source, Window window)
    {
        _source = source;
        _windowRef = new WeakReference<Window>(window);
        source.WndProcCallback = WndProc;
    }

    nint WndProc(nint hwnd, int message, nint wParam, nint lParam, ref bool handled)
    {
        foreach ((var webView, var _) in GetViews(hwnd))
            webView.WndProc(hwnd, message, wParam, lParam, ref handled);
        return nint.Zero;
    }

    internal IEnumerable<(WebView2 webView, Window window)> GetViews(nint hwnd)
    {
        if (hwnd != _source.Handle)
            yield break;

        if (!_windowRef.TryGetTarget(out var window))
            yield break;

        lock (_Views)
            for (var i = 0; i < _Views.Count; i++)
            {
                var viewRef = _Views[i];
                if (viewRef.TryGetTarget(out WebView2? view))
                    if (view.GetVisualRoot() == window)
                        yield return (view, window);
                    else
                        _Views.RemoveAt(i--);
            }
    }

    static void TryAddGlobalHook(object? sender, RoutedEventArgs e)
    {
        if (e.Source is Window window) TryAddGlobalHook(window);
    }

    static void TryAddGlobalHook(Window window)
    {
        if (window == null)
            return;

        var hwnd = window.TryGetPlatformHandle()?.Handle ?? throw new("failed to get handle");

        if (_HookedWindows.ContainsKey(hwnd))
            return;

        var source = WindowsHwndSource.FromHwnd(hwnd);
        if (source == null)
            return;

        _HookedWindows.Add(hwnd, new GlobalHooks(source, window));
    }

}