using l_winapi.InputOutput;
using l_winapi.Module;
using l_winapi.Module.AppOptions;
using l_winapi.Module.HotKey;
using l_winapi.Screens;
using Newtonsoft.Json;
using System.IO;
using System.Windows.Interop;

namespace WIndShellExperienceHost
{


    public partial class MainWindow : System.Windows.Window
    {
        Task_Screens screens = new Task_Screens();
        HotKeyBinder hotKeyBinder = new HotKeyBinder();
        AppOptions __List_Applications = new AppOptions();


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
                this.ResatructWindow();

            };

            #endregion


            FIO.ReadFileToJsonObject("__applications.json", (string json_str, string data) =>
            {
                __List_Applications = JsonConvert.DeserializeObject<AppOptions>(json_str) ?? new AppOptions();




            });

            this.Loaded += MainWindow_Loaded;
            this.Closed += MainWindow_Closed;
            this.Closing += MainWindow_Closing;
            this.SizeChanged += MainWindow_SizeChanged;
        }

        private void MainWindow_SizeChanged(object sender, System.Windows.SizeChangedEventArgs e)
        {
            SetCenterPosition();
        }

        private async void MainWindow_Closing(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            await this.SaveData();


        }

        private async void MainWindow_Closed(object? sender, EventArgs e)
        {
            await this.SaveData();
        }

        public async Task SaveData()
        {
            __List_Applications.WindowSize = new System.Windows.Size(this.Width, this.Height);
            FIO.WriteFileToJsonObject("__applications.json", __List_Applications);


            await Rebuild();

        }

        public async Task Rebuild()
        {
            Dispatcher.Invoke(() =>
            {
                _wrappanel.Children.Clear();

                foreach (var item in __List_Applications.apps)
                {
                    var c_ = new View.FilePanel();
                    c_.Refresh(item.SysName, item.SysPath);

                    _wrappanel.Children.Add(c_);

                }
            });

        }



        public async Task ResatructWindow()
        {

            await this.Dispatcher.InvokeAsync(new Action(() =>
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

                SetCenterPosition();





                GC.Collect();
            }));
        }
        private void SetCenterPosition()
        {
            this.Left = screens.CuretWindow.Left + (screens.CuretWindow.GetSize().Width / 2 - this.Width / 2);
            this.Top = (screens.CuretWindow.Bottom - this.Height) - 2;
        }

        private async void MainWindow_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {

            await Rebuild();
            await this.ResatructWindow();
            this.Width = __List_Applications.WindowSize.Width;
            this.Height = __List_Applications.WindowSize.Height;
        }



        private async void Window_Drop(object sender, System.Windows.DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {

                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

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
                    if (!__List_Applications.apps.Contains(new_app))
                        __List_Applications.apps.Add(new_app);
                }

                await this.SaveData();
                await Rebuild();
            }
        }
    }
}