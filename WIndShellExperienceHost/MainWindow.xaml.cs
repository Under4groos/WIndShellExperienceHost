using l_winapi.InputOutput;
using l_winapi.Module;
using l_winapi.Module.AppOptions;
using l_winapi.Module.HotKey;
using l_winapi.Screens;
using Newtonsoft.Json;
using System.Diagnostics;
using System.IO;
using System.Windows.Interop;

namespace WIndShellExperienceHost
{


    public partial class MainWindow : System.Windows.Window
    {
        Task_Screens screens = new Task_Screens();
        HotKeyBinder hotKeyBinder = new HotKeyBinder();
        ListApplications __List_Applications = new ListApplications();


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
                __List_Applications = JsonConvert.DeserializeObject<ListApplications>(json_str) ?? new ListApplications();




            });

            this.Loaded += MainWindow_Loaded;



        }

        public async Task SaveData()
        {
            FIO.WriteFileToJsonObject("__applications.json", __List_Applications);


            await Rebuild();

        }

        public async Task Rebuild()
        {
            Dispatcher.Invoke(() =>
            {
                _wrappanel.Children.Clear();
            });
            await Task.Run(() =>
            {
                Parallel.ForAsync(0, __List_Applications.apps.Count, async (i, d) =>
                {
                    var item = __List_Applications.apps[i];
                    Debug.WriteLine($"C: {item.SysPath}");
                    await Dispatcher.InvokeAsync(() =>
                    {
                        _wrappanel.Children.Add(new View.FilePanel()
                        {
                            SysPath = item.SysPath,
                            SysName = item.SysName,
                        });
                    });

                });
            });
        }



        public async Task ResatructWindow()
        {
            await Rebuild();
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


                GC.Collect();
            }));
        }

        private async void MainWindow_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            await this.ResatructWindow();
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