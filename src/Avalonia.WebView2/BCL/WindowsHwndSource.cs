// MIT License Copyright(c) 2020 CefNet
// https://github.com/CefNet/CefNet/blob/103.0.22181.155/CefNet.Avalonia/Internal/WindowsHwndSource.cs

namespace Avalonia.WebView.BCL;

internal delegate nint WindowsWindowProcDelegate(nint hwnd, int message, nint wParam, nint lParam, ref bool handled);

internal sealed class WindowsHwndSource : CriticalFinalizerObject, IDisposable
{
    delegate nint WindowProc(nint hwnd, int message, nint wParam, nint lParam);

    bool _disposed;
    nint hWndProcHook;
    readonly WindowProc fnWndProcHook;

    public static WindowsHwndSource FromHwnd(nint hwnd)
    {
        const int GWLP_WNDPROC = -4;

        var tid = NativeMethods.GetWindowThreadProcessId(hwnd, nint.Zero);
        if (tid == 0)
            throw new Win32Exception(Marshal.GetLastWin32Error());

        var source = new WindowsHwndSource(hwnd);
        source.hWndProcHook = NativeMethods.SetWindowLong(hwnd, GWLP_WNDPROC, Marshal.GetFunctionPointerForDelegate(source.fnWndProcHook));
        if (source.hWndProcHook == nint.Zero)
            throw new Win32Exception(Marshal.GetLastWin32Error());

        return source;
    }

    WindowsHwndSource(nint hwnd)
    {
        hWndProcHook = nint.Zero;
        Handle = hwnd;
        fnWndProcHook = new WindowProc(WndProcHook);
    }

    ~WindowsHwndSource()
    {
        Dispose(false);
    }

    void Dispose(bool disposing)
    {
        if (_disposed)
            return;

        if (hWndProcHook != nint.Zero)
        {
            const int GWLP_WNDPROC = -4;
            NativeMethods.SetWindowLong(Handle, GWLP_WNDPROC, hWndProcHook);
        }
        _disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    public nint Handle { get; }

    public WindowsWindowProcDelegate? WndProcCallback { get; set; }

    nint WndProcHook(nint hwnd, int msg, nint wParam, nint lParam)
    {
        var retval = nint.Zero;
        var wndproc = WndProcCallback;
        if (wndproc != null)
        {
            var handled = false;
            retval = wndproc(hwnd, msg, wParam, lParam, ref handled);
            if (handled)
                return retval;
        }
        return NativeMethods.CallWindowProc(hWndProcHook, hwnd, msg, wParam, lParam);
    }
}