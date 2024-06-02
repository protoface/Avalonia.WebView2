namespace Avalonia.WebView.BCL;

static class NativeMethods
{
    [DllImport("user32.dll", SetLastError = true)]
    internal static extern nint BeginPaint(nint hwnd, out PaintStruct lpPaint);

    [DllImport("user32.dll", SetLastError = true)]
    internal static extern bool EndPaint(nint hwnd, ref PaintStruct lpPaint);

    [DllImport("user32.dll", SetLastError = true)]
    public static extern uint GetWindowThreadProcessId(nint hWnd, nint pPid);

    public static nint SetWindowLong(nint hWnd, int nIndex, nint dwNewLong)
    {
        if (nint.Size == 8)
            return SetWindowLongPtr64(hWnd, nIndex, dwNewLong);
        else
            return new nint(SetWindowLong32(hWnd, nIndex, dwNewLong.ToInt32()));
    }

    [DllImport("user32.dll", EntryPoint = "SetWindowLong", CharSet = CharSet.Auto, SetLastError = true)]
    static extern int SetWindowLong32(nint hWnd, int nIndex, int dwNewLong);

    [DllImport("user32.dll", EntryPoint = "SetWindowLongPtr", CharSet = CharSet.Auto, SetLastError = true)]
    static extern nint SetWindowLongPtr64(nint hWnd, int nIndex, nint dwNewLong);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern nint CallWindowProc(nint lpPrevWndFunc, nint hWnd, int msg, nint wParam, nint lParam);

    public enum WM : uint
    {
        SETFOCUS = 7,
        PAINT = 15, // 0x0000000F

        WINDOWPOSCHANGING = 0x0046,
        GETOBJECT = 0x003D,
        SHOWWINDOW = 0x0018,
    }

    public struct Rect
    {
        public int left;
        public int top;
        public int right;
        public int bottom;
    }

    public struct PaintStruct
    {
        public nint hdc;
        public bool fErase;
        public Rect rcPaint;
        public bool fRestore;
        public bool fIncUpdate;
        public byte[] rgbReserved;
    }

    /// <summary>
    ///     Convert a Win32 VirtualKey into our Key enum.
    /// </summary>
    public static Key KeyFromVirtualKey(NativeMethods_VirtualKeys virtualKey) => virtualKey switch
    {
        NativeMethods_VirtualKeys.VK_CANCEL => Key.Cancel,
        NativeMethods_VirtualKeys.VK_BACK => Key.Back,
        NativeMethods_VirtualKeys.VK_TAB => Key.Tab,
        NativeMethods_VirtualKeys.VK_CLEAR => Key.Clear,
        NativeMethods_VirtualKeys.VK_RETURN => Key.Return,
        NativeMethods_VirtualKeys.VK_PAUSE => Key.Pause,
        NativeMethods_VirtualKeys.VK_CAPITAL => Key.Capital,
        NativeMethods_VirtualKeys.VK_KANA => Key.KanaMode,
        NativeMethods_VirtualKeys.VK_JUNJA => Key.JunjaMode,
        NativeMethods_VirtualKeys.VK_FINAL => Key.FinalMode,
        NativeMethods_VirtualKeys.VK_KANJI => Key.KanjiMode,
        NativeMethods_VirtualKeys.VK_ESCAPE => Key.Escape,
        NativeMethods_VirtualKeys.VK_CONVERT => Key.ImeConvert,
        NativeMethods_VirtualKeys.VK_NONCONVERT => Key.ImeNonConvert,
        NativeMethods_VirtualKeys.VK_ACCEPT => Key.ImeAccept,
        NativeMethods_VirtualKeys.VK_MODECHANGE => Key.ImeModeChange,
        NativeMethods_VirtualKeys.VK_SPACE => Key.Space,
        NativeMethods_VirtualKeys.VK_PRIOR => Key.Prior,
        NativeMethods_VirtualKeys.VK_NEXT => Key.Next,
        NativeMethods_VirtualKeys.VK_END => Key.End,
        NativeMethods_VirtualKeys.VK_HOME => Key.Home,
        NativeMethods_VirtualKeys.VK_LEFT => Key.Left,
        NativeMethods_VirtualKeys.VK_UP => Key.Up,
        NativeMethods_VirtualKeys.VK_RIGHT => Key.Right,
        NativeMethods_VirtualKeys.VK_DOWN => Key.Down,
        NativeMethods_VirtualKeys.VK_SELECT => Key.Select,
        NativeMethods_VirtualKeys.VK_PRINT => Key.Print,
        NativeMethods_VirtualKeys.VK_EXECUTE => Key.Execute,
        NativeMethods_VirtualKeys.VK_SNAPSHOT => Key.Snapshot,
        NativeMethods_VirtualKeys.VK_INSERT => Key.Insert,
        NativeMethods_VirtualKeys.VK_DELETE => Key.Delete,
        NativeMethods_VirtualKeys.VK_HELP => Key.Help,
        NativeMethods_VirtualKeys.VK_0 => Key.D0,
        NativeMethods_VirtualKeys.VK_1 => Key.D1,
        NativeMethods_VirtualKeys.VK_2 => Key.D2,
        NativeMethods_VirtualKeys.VK_3 => Key.D3,
        NativeMethods_VirtualKeys.VK_4 => Key.D4,
        NativeMethods_VirtualKeys.VK_5 => Key.D5,
        NativeMethods_VirtualKeys.VK_6 => Key.D6,
        NativeMethods_VirtualKeys.VK_7 => Key.D7,
        NativeMethods_VirtualKeys.VK_8 => Key.D8,
        NativeMethods_VirtualKeys.VK_9 => Key.D9,
        NativeMethods_VirtualKeys.VK_A => Key.A,
        NativeMethods_VirtualKeys.VK_B => Key.B,
        NativeMethods_VirtualKeys.VK_C => Key.C,
        NativeMethods_VirtualKeys.VK_D => Key.D,
        NativeMethods_VirtualKeys.VK_E => Key.E,
        NativeMethods_VirtualKeys.VK_F => Key.F,
        NativeMethods_VirtualKeys.VK_G => Key.G,
        NativeMethods_VirtualKeys.VK_H => Key.H,
        NativeMethods_VirtualKeys.VK_I => Key.I,
        NativeMethods_VirtualKeys.VK_J => Key.J,
        NativeMethods_VirtualKeys.VK_K => Key.K,
        NativeMethods_VirtualKeys.VK_L => Key.L,
        NativeMethods_VirtualKeys.VK_M => Key.M,
        NativeMethods_VirtualKeys.VK_N => Key.N,
        NativeMethods_VirtualKeys.VK_O => Key.O,
        NativeMethods_VirtualKeys.VK_P => Key.P,
        NativeMethods_VirtualKeys.VK_Q => Key.Q,
        NativeMethods_VirtualKeys.VK_R => Key.R,
        NativeMethods_VirtualKeys.VK_S => Key.S,
        NativeMethods_VirtualKeys.VK_T => Key.T,
        NativeMethods_VirtualKeys.VK_U => Key.U,
        NativeMethods_VirtualKeys.VK_V => Key.V,
        NativeMethods_VirtualKeys.VK_W => Key.W,
        NativeMethods_VirtualKeys.VK_X => Key.X,
        NativeMethods_VirtualKeys.VK_Y => Key.Y,
        NativeMethods_VirtualKeys.VK_Z => Key.Z,
        NativeMethods_VirtualKeys.VK_LWIN => Key.LWin,
        NativeMethods_VirtualKeys.VK_RWIN => Key.RWin,
        NativeMethods_VirtualKeys.VK_APPS => Key.Apps,
        NativeMethods_VirtualKeys.VK_SLEEP => Key.Sleep,
        NativeMethods_VirtualKeys.VK_NUMPAD0 => Key.NumPad0,
        NativeMethods_VirtualKeys.VK_NUMPAD1 => Key.NumPad1,
        NativeMethods_VirtualKeys.VK_NUMPAD2 => Key.NumPad2,
        NativeMethods_VirtualKeys.VK_NUMPAD3 => Key.NumPad3,
        NativeMethods_VirtualKeys.VK_NUMPAD4 => Key.NumPad4,
        NativeMethods_VirtualKeys.VK_NUMPAD5 => Key.NumPad5,
        NativeMethods_VirtualKeys.VK_NUMPAD6 => Key.NumPad6,
        NativeMethods_VirtualKeys.VK_NUMPAD7 => Key.NumPad7,
        NativeMethods_VirtualKeys.VK_NUMPAD8 => Key.NumPad8,
        NativeMethods_VirtualKeys.VK_NUMPAD9 => Key.NumPad9,
        NativeMethods_VirtualKeys.VK_MULTIPLY => Key.Multiply,
        NativeMethods_VirtualKeys.VK_ADD => Key.Add,
        NativeMethods_VirtualKeys.VK_SEPARATOR => Key.Separator,
        NativeMethods_VirtualKeys.VK_SUBTRACT => Key.Subtract,
        NativeMethods_VirtualKeys.VK_DECIMAL => Key.Decimal,
        NativeMethods_VirtualKeys.VK_DIVIDE => Key.Divide,
        NativeMethods_VirtualKeys.VK_F1 => Key.F1,
        NativeMethods_VirtualKeys.VK_F2 => Key.F2,
        NativeMethods_VirtualKeys.VK_F3 => Key.F3,
        NativeMethods_VirtualKeys.VK_F4 => Key.F4,
        NativeMethods_VirtualKeys.VK_F5 => Key.F5,
        NativeMethods_VirtualKeys.VK_F6 => Key.F6,
        NativeMethods_VirtualKeys.VK_F7 => Key.F7,
        NativeMethods_VirtualKeys.VK_F8 => Key.F8,
        NativeMethods_VirtualKeys.VK_F9 => Key.F9,
        NativeMethods_VirtualKeys.VK_F10 => Key.F10,
        NativeMethods_VirtualKeys.VK_F11 => Key.F11,
        NativeMethods_VirtualKeys.VK_F12 => Key.F12,
        NativeMethods_VirtualKeys.VK_F13 => Key.F13,
        NativeMethods_VirtualKeys.VK_F14 => Key.F14,
        NativeMethods_VirtualKeys.VK_F15 => Key.F15,
        NativeMethods_VirtualKeys.VK_F16 => Key.F16,
        NativeMethods_VirtualKeys.VK_F17 => Key.F17,
        NativeMethods_VirtualKeys.VK_F18 => Key.F18,
        NativeMethods_VirtualKeys.VK_F19 => Key.F19,
        NativeMethods_VirtualKeys.VK_F20 => Key.F20,
        NativeMethods_VirtualKeys.VK_F21 => Key.F21,
        NativeMethods_VirtualKeys.VK_F22 => Key.F22,
        NativeMethods_VirtualKeys.VK_F23 => Key.F23,
        NativeMethods_VirtualKeys.VK_F24 => Key.F24,
        NativeMethods_VirtualKeys.VK_NUMLOCK => Key.NumLock,
        NativeMethods_VirtualKeys.VK_SCROLL => Key.Scroll,
        NativeMethods_VirtualKeys.VK_SHIFT or NativeMethods_VirtualKeys.VK_LSHIFT => Key.LeftShift,
        NativeMethods_VirtualKeys.VK_RSHIFT => Key.RightShift,
        NativeMethods_VirtualKeys.VK_CONTROL or NativeMethods_VirtualKeys.VK_LCONTROL => Key.LeftCtrl,
        NativeMethods_VirtualKeys.VK_RCONTROL => Key.RightCtrl,
        NativeMethods_VirtualKeys.VK_MENU or NativeMethods_VirtualKeys.VK_LMENU => Key.LeftAlt,
        NativeMethods_VirtualKeys.VK_RMENU => Key.RightAlt,
        NativeMethods_VirtualKeys.VK_BROWSER_BACK => Key.BrowserBack,
        NativeMethods_VirtualKeys.VK_BROWSER_FORWARD => Key.BrowserForward,
        NativeMethods_VirtualKeys.VK_BROWSER_REFRESH => Key.BrowserRefresh,
        NativeMethods_VirtualKeys.VK_BROWSER_STOP => Key.BrowserStop,
        NativeMethods_VirtualKeys.VK_BROWSER_SEARCH => Key.BrowserSearch,
        NativeMethods_VirtualKeys.VK_BROWSER_FAVORITES => Key.BrowserFavorites,
        NativeMethods_VirtualKeys.VK_BROWSER_HOME => Key.BrowserHome,
        NativeMethods_VirtualKeys.VK_VOLUME_MUTE => Key.VolumeMute,
        NativeMethods_VirtualKeys.VK_VOLUME_DOWN => Key.VolumeDown,
        NativeMethods_VirtualKeys.VK_VOLUME_UP => Key.VolumeUp,
        NativeMethods_VirtualKeys.VK_MEDIA_NEXT_TRACK => Key.MediaNextTrack,
        NativeMethods_VirtualKeys.VK_MEDIA_PREV_TRACK => Key.MediaPreviousTrack,
        NativeMethods_VirtualKeys.VK_MEDIA_STOP => Key.MediaStop,
        NativeMethods_VirtualKeys.VK_MEDIA_PLAY_PAUSE => Key.MediaPlayPause,
        NativeMethods_VirtualKeys.VK_LAUNCH_MAIL => Key.LaunchMail,
        NativeMethods_VirtualKeys.VK_LAUNCH_MEDIA_SELECT => Key.SelectMedia,
        NativeMethods_VirtualKeys.VK_LAUNCH_APP1 => Key.LaunchApplication1,
        NativeMethods_VirtualKeys.VK_LAUNCH_APP2 => Key.LaunchApplication2,
        NativeMethods_VirtualKeys.VK_OEM_1 => Key.OemSemicolon,
        NativeMethods_VirtualKeys.VK_OEM_PLUS => Key.OemPlus,
        NativeMethods_VirtualKeys.VK_OEM_COMMA => Key.OemComma,
        NativeMethods_VirtualKeys.VK_OEM_MINUS => Key.OemMinus,
        NativeMethods_VirtualKeys.VK_OEM_PERIOD => Key.OemPeriod,
        NativeMethods_VirtualKeys.VK_OEM_2 => Key.OemQuestion,
        NativeMethods_VirtualKeys.VK_OEM_3 => Key.OemTilde,
        NativeMethods_VirtualKeys.VK_C1 => Key.AbntC1,
        NativeMethods_VirtualKeys.VK_C2 => Key.AbntC2,
        NativeMethods_VirtualKeys.VK_OEM_4 => Key.OemOpenBrackets,
        NativeMethods_VirtualKeys.VK_OEM_5 => Key.OemPipe,
        NativeMethods_VirtualKeys.VK_OEM_6 => Key.OemCloseBrackets,
        NativeMethods_VirtualKeys.VK_OEM_7 => Key.OemQuotes,
        NativeMethods_VirtualKeys.VK_OEM_8 => Key.Oem8,
        NativeMethods_VirtualKeys.VK_OEM_102 => Key.OemBackslash,
        NativeMethods_VirtualKeys.VK_PROCESSKEY => Key.ImeProcessed,
        // VK_DBE_ALPHANUMERIC
        NativeMethods_VirtualKeys.VK_OEM_ATTN => Key.OemAttn, // DbeAlphanumeric
                                                              // VK_DBE_KATAKANA
        NativeMethods_VirtualKeys.VK_OEM_FINISH => Key.OemFinish, // DbeKatakana
                                                                  // VK_DBE_HIRAGANA
        NativeMethods_VirtualKeys.VK_OEM_COPY => Key.OemCopy, // DbeHiragana
                                                              // VK_DBE_SBCSCHAR
        NativeMethods_VirtualKeys.VK_OEM_AUTO => Key.OemAuto, // DbeSbcsChar
                                                              // VK_DBE_DBCSCHAR
        NativeMethods_VirtualKeys.VK_OEM_ENLW => Key.OemEnlw, // DbeDbcsChar
                                                              // VK_DBE_ROMAN
        NativeMethods_VirtualKeys.VK_OEM_BACKTAB => Key.OemBackTab, // DbeRoman
                                                                    // VK_DBE_NOROMAN
        NativeMethods_VirtualKeys.VK_ATTN => Key.Attn, // DbeNoRoman
                                                       // VK_DBE_ENTERWORDREGISTERMODE
        NativeMethods_VirtualKeys.VK_CRSEL => Key.CrSel, // DbeEnterWordRegisterMode
                                                         // VK_DBE_ENTERIMECONFIGMODE
        NativeMethods_VirtualKeys.VK_EXSEL => Key.ExSel, // DbeEnterImeConfigMode
                                                         // VK_DBE_FLUSHSTRING
        NativeMethods_VirtualKeys.VK_EREOF => Key.EraseEof, // DbeFlushString
                                                            // VK_DBE_CODEINPUT
        NativeMethods_VirtualKeys.VK_PLAY => Key.Play, // DbeCodeInput
                                                       // VK_DBE_NOCODEINPUT
        NativeMethods_VirtualKeys.VK_ZOOM => Key.Zoom, // DbeNoCodeInput
                                                       // VK_DBE_DETERMINESTRING
        NativeMethods_VirtualKeys.VK_NONAME => Key.NoName, // DbeDetermineString
                                                           // VK_DBE_ENTERDLGCONVERSIONMODE
        NativeMethods_VirtualKeys.VK_PA1 => Key.Pa1, // DbeEnterDlgConversionMode
        NativeMethods_VirtualKeys.VK_OEM_CLEAR => Key.OemClear,
        _ => Key.None,
    };

}