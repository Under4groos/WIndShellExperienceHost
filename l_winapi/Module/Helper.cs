using l_winapi.Enums;
using l_winapi.Screens;
using System.Runtime.InteropServices;

namespace l_winapi.Module
{
    public static partial class Helper
    {

        #region import custom winapi 
        [DllImport(WinApiLibs.c_win_module)]
        public static extern int _GetMessage();



        #endregion



        [DllImport(WinApiLibs.c_win_module)]
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
