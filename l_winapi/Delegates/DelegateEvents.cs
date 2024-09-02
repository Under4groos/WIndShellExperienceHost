using l_winapi.Screens;

namespace l_winapi.Delegates
{
    public delegate void FileJsonValid(string json_str, string path);


    public delegate void trycath_init();
    public delegate void trycath_error(string error);

    public delegate void Event_HotKey(int key);

    #region Screens
    public delegate bool MonitorEnumDelegate(IntPtr hMonitor, IntPtr hdcMonitor, ref RECT lprcMonitor, IntPtr dwData);
    public delegate bool MonitorEnumDelegateData();
    #endregion

}
