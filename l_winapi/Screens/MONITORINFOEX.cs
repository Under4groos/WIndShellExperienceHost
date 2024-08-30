using System.Runtime.InteropServices;

namespace l_winapi.Screens
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct MONITORINFOEX
    {
        public int Size;
        public RECT Monitor;
        public RECT WorkArea;
        public uint Flags;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string DeviceName;

        public override string ToString()
        {
            return $"-----\n{DeviceName} \nWorkArea:{Monitor.Right - Monitor.Left}x{Monitor.Bottom - Monitor.Top}\nL:{Monitor.Left} T:{Monitor.Top} ";
        }
    }
}
