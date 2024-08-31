using l_winapi.Enums;

namespace l_winapi.Module.HotKey
{
    public struct STRUCT_HotKey
    {
        public nint hWnd;
        public ModEnums fsModifiers;
        public WinFormKeys vk;
    }
}
