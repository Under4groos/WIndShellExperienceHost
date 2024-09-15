using l_winapi.InputOutput;
using l_winapi.Module;
using l_winapi.Module.AppOptions;
using l_winapi.Module.HotKey;
using l_winapi.Screens;
using Newtonsoft.Json;
using Shell.IconExtractor;
using System.Diagnostics;
using System.IO;
using System.Windows.Interop;
using WIndShellExperienceHost.View;

namespace WIndShellExperienceHost
{


    public partial class MainWindow : System.Windows.Window
    {
        Task_Screens screens = new Task_Screens();
        HotKeyBinder hotKeyBinder = new HotKeyBinder();
        AppOptions __List_Applications = new AppOptions();
        private const string filedata_json = "__applications.json";
        Task TaskBackShell;
        bool status_cl = false;
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

                        status_cl = false;
                        this.ResatructWindow();
                        this.ShellIconExtractorTask();
                        await this.SaveData();

                    }



                });

            };

            #endregion

            this.Loaded += MainWindow_Loaded;
            this.SizeChanged += (o, e) =>
            {
                this.ResatructWindow();
            };

        }
        private void MainWindow_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            this.SetVisibility(false);
            if (File.Exists(filedata_json))
            {
                Trycatch.trycatch(() =>
                {

                    __List_Applications = JsonConvert.DeserializeObject<AppOptions>(File.ReadAllText(filedata_json)) ?? new AppOptions();
                    this.Width = __List_Applications.WindowSize.Width;
                    this.Height = __List_Applications.WindowSize.Height;

                });
            }



            this.SetCenterPosition();
            //this.SizeChanged += MainWindow_SizeChanged;
        }

        private void ShellIconExtractorTask()
        {
            _wrappanel.Children.Clear();
            if (TaskBackShell != null)
            {

                if ((int)TaskBackShell.Status == 3)
                {
                    status_cl = true;
                    TaskBackShell.Dispose();

                }
            }


            TaskBackShell = new Task(() =>
             {

                 var stru = new Shell.IconExtractor.Strucrure.IcoExtractorOptions()
                 {
                     iconSize = Shell.IconExtractor.Enumes.IconSize.ExtraLarge,
                     path = "",
                     state = Shell.IconExtractor.Enumes.ItemState.Undefined,
                     type = Shell.IconExtractor.Enumes.ItemType.File,
                 };

                 foreach (var item in __List_Applications.apps)
                 {
                     if (status_cl)
                     {
                         this.Dispatcher.Invoke(() =>
                         {
                             _wrappanel.Children.Clear();
                         });

                         status_cl = false;
                         break;
                     }

                     Thread.Sleep(5);
                     if (Directory.Exists(item.SysPath))
                     {
                         stru.type = Shell.IconExtractor.Enumes.ItemType.Folder;
                     }
                     else
                     {
                         stru.type = Shell.IconExtractor.Enumes.ItemType.File;
                     }
                     stru.path = item.SysPath;


                     string SysPathImage = Path.GetFullPath(Path.Combine("Data", $"{item.SysName}.png"));
                     if (File.Exists(SysPathImage))
                     {
                         this.Dispatcher.Invoke(() =>
                         {

                             FilePanel c_ = new View.FilePanel();
                             c_.SetData(item.SysName, item.SysPath);
                             c_.SetImage(SysPathImage);
                             _wrappanel.Children.Add(c_);
                         });
                         continue;
                     }


                     using (IcoExtractor extr = new IcoExtractor(stru))
                     {
                         if (extr.GetIcon != null)
                         {

                             Debug.WriteLine(SysPathImage);
                             extr.SaveToFile(SysPathImage);


                             extr.Dispose();
                             this.Dispatcher.Invoke(() =>
                             {

                                 FilePanel c_ = new View.FilePanel();
                                 c_.SetData(item.SysName, item.SysPath);
                                 c_.SetImage(SysPathImage);
                                 _wrappanel.Children.Add(c_);
                             });
                         }
                     }


                 }
                 status_cl = false;
             });

            TaskBackShell.Start();
        }




        public async Task SaveData()
        {
            __List_Applications.WindowSize = new System.Windows.Size(this.ActualWidth, this.ActualHeight);
            FIO.WriteFileToJsonObject("__applications.json", __List_Applications);

        }



        public void SetVisibility(bool Status)
        {
            this.Visibility = Status ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed;


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
                this.ShellIconExtractorTask();
                await this.SaveData();

            }
        }
    }
}