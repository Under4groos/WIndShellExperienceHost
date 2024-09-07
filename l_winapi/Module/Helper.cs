using l_winapi.Delegates;
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


        [DllImport(WinApiLibs.c_win_module, CallingConvention = CallingConvention.Cdecl, EntryPoint = "GetLastErrorAsString")]
        public static extern string LastError();

        [DllImport(WinApiLibs.USER, CallingConvention = CallingConvention.Cdecl, EntryPoint = "RegisterHotKey")]
        private static extern bool HotKey_Register(nint hWnd, int id, int fsModifiers, int vk);




        public static bool p_HotKey_Register(nint hWnd, ModEnums fsModifiers, WinFormKeys vk)
        {
            return HotKey_Register(hWnd, 0, (int)fsModifiers, (int)vk);
        }




        #endregion









        #region Screens 
        [DllImport(WinApiLibs.USER, CharSet = CharSet.Unicode)]
        public static extern bool GetMonitorInfo(IntPtr hMonitor, ref MONITORINFOEX lpmi);



        [DllImport(WinApiLibs.c_win_module, CallingConvention = CallingConvention.Cdecl, EntryPoint = "w_EnumDisplayMonitors")]
        public static extern bool w_EnumDisplayMonitors(MonitorEnumDelegate lpfnEnum, IntPtr dwData);

        [DllImport(WinApiLibs.c_win_module, CallingConvention = CallingConvention.Cdecl, EntryPoint = "_GetCursorPosX")]
        public static extern int _GetCursorPosX();


        [DllImport(WinApiLibs.c_win_module, CallingConvention = CallingConvention.Cdecl, EntryPoint = "DisplayMonitor")]
        public static extern bool DisplayMonitor(out RECT pThis);




        #endregion

        [DllImport(WinApiLibs.c_win_module, CallingConvention = CallingConvention.Cdecl, EntryPoint = "ShellExplorer")]
        public static extern int ShellExplorer(IntPtr hwnd, string lpOperation, string lpFile);


        #region MSG
        [DllImport(WinApiLibs.c_win_module, CallingConvention = CallingConvention.Cdecl, EntryPoint = "MessageBoxShowTopMost")]
        public static extern int MessageBoxShowTopMost(string lpText, string lpCaption);

        #endregion
    }
}
