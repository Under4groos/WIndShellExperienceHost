using l_winapi.Enums;
using l_winapi.Screens;
using System.Runtime.InteropServices;

namespace l_winapi.Module
{
    public static partial class Helper
    {

        #region lib
#if DEBUG

        public const string c_win_module = "C:\\Users\\UnderKo\\source\\repos\\WIndShellExperienceHost\\x64\\Debug\\c_win_module.dll";
#else
         public const string c_win_module = "c_win_module.dll";

#endif
        #endregion



        [DllImport(WinApiLibs.USER)]
        public static extern bool RegisterHotKey(nint hWnd, int id, int fsModifiers, int vk);

        public static bool RegisterHotKeyBind(nint hWnd, ModEnums fsModifiers, WinFormKeys vk)
        {
            return RegisterHotKey(hWnd, 0, (int)fsModifiers, (int)vk);
        }


        #region Screens 
        [DllImport(WinApiLibs.USER, CharSet = CharSet.Unicode)]
        private static extern bool GetMonitorInfo(IntPtr hMonitor, ref MONITORINFOEX lpmi);

        private delegate bool MonitorEnumDelegate(IntPtr hMonitor, IntPtr hdcMonitor, ref RECT lprcMonitor, IntPtr dwData);

        [DllImport(WinApiLibs.USER)]
        private static extern bool EnumDisplayMonitors(IntPtr hdc, IntPtr lprcClip, MonitorEnumDelegate lpfnEnum, IntPtr dwData);

        #endregion

    }
}
