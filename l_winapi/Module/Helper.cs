using l_winapi.Enums;
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
#test


        [DllImport(WinApiLibs.USER)]
        public static extern bool RegisterHotKey(nint hWnd, int id, int fsModifiers, int vk);

        public static bool RegisterHotKeyBind(nint hWnd, ModEnums fsModifiers, WinFormKeys vk)
        {
            return RegisterHotKey(hWnd, 0, (int)fsModifiers, (int)vk);
        }




    }
}
