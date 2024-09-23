using l_winapi.Module;
using l_winapi.Module.HotKey;
using l_winapi.Screens;
using System.IO;
using System.Windows.Interop;
using System.Windows.Media.Animation;
using WIndShellExperienceHost.Module;

namespace WIndShellExperienceHost
{


    public partial class MainWindow : System.Windows.Window
    {
        private Task_Screens screens = new Task_Screens();
        private HotKeyBinder hotKeyBinder = new HotKeyBinder();


        private Task TaskBackShell;


        #region Animations

        DoubleAnimation WindowOpacityHide;
        DoubleAnimation WindowOpacityShow;


        #endregion


        public MainWindow()
        {
            InitializeComponent();
            Directory.CreateDirectory("Data");
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
                this.Dispatcher.Invoke(async () =>
                {
                    if (screens.LastCuretWindow.Left != screens.CuretWindow.Left)
                    {
                        screens.LastCuretWindow = screens.CuretWindow;
                        this.SetVisibility(true);
                    }
                    else
                    {
                        this.SetVisibilityStatus();
                    }

                    if (this.Visibility == System.Windows.Visibility.Visible)
                    {

                        ui_applications.status_cl = false;
                        this.ResatructWindow();
                        ui_applications.ShellIconExtractorTask();


                    }
                    else
                    {

                        G_.AllOptions.SaveData();
                        ui_applications.Clear();
                    }
                });

            };

            #endregion

            WindowOpacityHide = new DoubleAnimation()
            {
                DecelerationRatio = 0.1,
                From = 0,

                To = 1,
                Duration = TimeSpan.FromSeconds(1)
            };
            WindowOpacityHide.Completed += WindowOpacity_Completed_hide;


            WindowOpacityShow = new DoubleAnimation()
            {
                DecelerationRatio = 0.1,
                From = 0,

                To = 1,
                Duration = TimeSpan.FromSeconds(1)
            };
            WindowOpacityShow.Completed += WindowOpacity_Completed_Show;


            this.Loaded += MainWindow_Loaded;
            this.SizeChanged += (o, e) =>
            {
                this.ResatructWindow();
                G_.AllOptions.List_Applications.WindowSize = e.NewSize;

            };
            G_.AllOptions.Loaded += () =>
            {
                this.Width = G_.AllOptions.List_Applications.WindowSize.Width;
                this.Height = G_.AllOptions.List_Applications.WindowSize.Height;
                this.SetCenterPosition();
            };
        }
        private void WindowOpacity_Completed_Show(object? sender, EventArgs e)
        {
            this.Visibility = System.Windows.Visibility.Visible;

        }
        private void WindowOpacity_Completed_hide(object? sender, EventArgs e)
        {
            this.Visibility = System.Windows.Visibility.Collapsed;

        }

        private void MainWindow_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            this.SetVisibility(false);

            G_.AllOptions.Load();

        }















        public void SetVisibility(bool Status)
        {
            this.Visibility = Status == true ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed;
        }
        public void SetVisibilityStatus()
        {
            this.Visibility = this.Visibility == System.Windows.Visibility.Collapsed ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed;
        }
        public void ResatructWindow()
        {

            screens.UpdateScreens();
            foreach (RECT rect in screens.RECTMonitors)
            {
                if (Helper._GetCursorPosX() > rect.Left)
                {
                    screens.CuretWindow = rect;


                }
            }
            this.SetCenterPosition();
            GC.Collect();

        }
        private void SetCenterPosition()
        {
            this.Left = screens.CuretWindow.Left + (screens.CuretWindow.GetSize().Width / 2 - this.Width / 2);
            this.Top = (screens.CuretWindow.Bottom - this.Height) - 2;

        }





        private void Window_Drop(object sender, System.Windows.DragEventArgs e)
        {
            if (e.Data.GetDataPresent(System.Windows.DataFormats.FileDrop))
            {

                string[] files = (string[])e.Data.GetData(System.Windows.DataFormats.FileDrop);

                string name_ = "";

                foreach (string _path in files)
                {
                    if (File.Exists(_path))
                    {
                        name_ = new FileInfo(_path).Name;
                    }
                    if (Directory.Exists(_path))
                    {
                        name_ = new DirectoryInfo(_path).Name;
                    }

                    var new_app = new l_winapi.Module.AppOptions.Application()
                    {
                        SysPath = _path,
                        SysName = name_,
                    };
                    if (!G_.AllOptions.List_Applications.apps.Contains(new_app))
                        G_.AllOptions.List_Applications.apps.Add(new_app);
                }
                this.Dispatcher.Invoke(() =>
                {
                    ui_applications.ShellIconExtractorTask();
                    G_.AllOptions.SaveData();
                });


            }
        }
    }
}