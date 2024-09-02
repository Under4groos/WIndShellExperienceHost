using l_winapi.InputOutput;
using l_winapi.Module;
using l_winapi.Module.AppOptions;
using l_winapi.Module.HotKey;
using l_winapi.Screens;
using System.Windows.Interop;

namespace WIndShellExperienceHost
{


    public partial class MainWindow : System.Windows.Window
    {
        Task_Screens screens = new Task_Screens();
        HotKeyBinder hotKeyBinder = new HotKeyBinder();
        ListApplications List_Applications = new ListApplications();
        public MainWindow()
        {
            InitializeComponent();

            IntPtr HWND = new WindowInteropHelper(this).Handle;

            #region hotkey
            hotKeyBinder.AddHotKey(new STRUCT_HotKey()
            {
                fsModifiers = l_winapi.Enums.ModEnums.MOD_ALT,
                vk = l_winapi.Enums.WinFormKeys.X,
            });

            hotKeyBinder.Init();
            hotKeyBinder.event_HotKey = (key) =>
            {
                this.ResatructWindow();

            };

            #endregion


            FIO.ReadFileToJsonObject("__applications.json", (object o, string data) =>
            {
                List_Applications = (ListApplications)o;


                _wrappanel.Children.Clear();
                foreach (var item in List_Applications.apps)
                {
                    _wrappanel.Children.Add(new View.FilePanel()
                    {
                        FilePath = item.Path,
                    });
                }


            });

            this.Loaded += MainWindow_Loaded;

        }

        public void SaveData()
        {
            FIO.ReadFileToJsonObject("__applications.json", List_Applications);
        }

        public void ResatructWindow()
        {
            this.Dispatcher.Invoke(new Action(() =>
            {
                this.Visibility = this.Visibility == System.Windows.Visibility.Visible ? System.Windows.Visibility.Collapsed : System.Windows.Visibility.Visible;

                screens.UpdateScreens();
                foreach (RECT rect in screens.RECTMonitors)
                {
                    if (Helper._GetCursorPosX() > rect.Left)
                    {
                        screens.CuretWindow = rect;
                    }
                }
                this.Left = screens.CuretWindow.Left + (screens.CuretWindow.GetSize().Width / 2 - this.Width / 2);
                this.Top = (screens.CuretWindow.Bottom - this.Height) - 2;
            }));
        }

        private void MainWindow_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            this.ResatructWindow();
        }
    }
}