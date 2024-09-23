using System.Diagnostics;
using System.Security.Principal;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace l_winapi.Module
{
    public static class Util
    {
        public static void StartExplorer(string command)
        {

            local_ProcessStart("explorer.exe", command);
        }
        public static void StartExplorer()
        {

            local_ProcessStart("explorer.exe");
        }
        public static void StartExplorerSelect(string command = "")
        {
            local_ProcessStart("explorer.exe", $"/select, \"{command}\"");
        }

        public static void local_ProcessStart(string path, string args = "", bool isadmin = false)
        {

            ProcessStartInfo proc = new ProcessStartInfo();
            proc.UseShellExecute = true;
            //if (File.Exists(path))
            //{
            //    proc.WorkingDirectory = new FileInfo(path).DirectoryName;
            //}
            //if (Directory.Exists(path))
            //{
            //    proc.WorkingDirectory = path;
            //}
            proc.FileName = path;
            if (isadmin)
            {
                if (IsRunAsAdmin())
                {
                    proc.Verb = "runas";
                }
                else
                {
                    MessageBox.Show("Admin status:false");
                }
            }
            proc.Arguments = args;
            Trycatch.trycatch(() =>
            {
                Process.Start(proc);
            });

        }

        public static bool IsRunAsAdmin()
        {
            WindowsIdentity id = WindowsIdentity.GetCurrent();
            WindowsPrincipal principal = new WindowsPrincipal(id);

            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }
        public static ContextMenu ShowContextMenu(this ContextMenu cm, UIElement ui)
        {

            cm.Background = Brushes.Transparent;
            cm.PlacementTarget = ui;
            cm.IsOpen = true;
            return cm;
        }
    }
}
