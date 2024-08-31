namespace l_winapi.Module
{
    public static class WinApiLibs
    {
        public const string USER = "user32.dll";
        public const string DWMAPI = "dwmapi.dll";
        public const string SHELL = "shell32.dll";
        public const string KERNEL = "kernel32.dll";
        public const string OLE = "ole32.dll";

#if DEBUG

        public const string c_win_module = @"C:\Users\UnderKo\source\repos\WIndShellExperienceHost\x64\Release\c_win_module.dll";
#else
         public const string c_win_module = "c_win_module.dll";

#endif


    }
}
