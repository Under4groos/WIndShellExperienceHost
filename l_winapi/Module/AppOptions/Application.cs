namespace l_winapi.Module.AppOptions
{
    public class Application
    {
        public string SysPath { get; set; } = string.Empty;
        public string SysName { get; set; } = string.Empty;

        public bool Equals(Application B)
        {
            return this.SysName == B.SysName && this.SysPath == B.SysPath;
        }
    }
}
