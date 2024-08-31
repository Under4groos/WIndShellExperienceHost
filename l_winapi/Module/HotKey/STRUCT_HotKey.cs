using l_winapi.Enums;

namespace l_winapi.Module.HotKey
{
    public struct STRUCT_HotKey
    {
        public bool RegStatus;
        public nint hWnd;
        public ModEnums fsModifiers;
        public WinFormKeys vk;
        // public Event_HotKey event_HotKey;
    }
}
