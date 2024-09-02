﻿using l_winapi.Module;
using l_winapi.Module.HotKey;
using l_winapi.Screens;
using System.Windows.Interop;

namespace WIndShellExperienceHost
{


    public partial class MainWindow : System.Windows.Window
    {
        Task_Screens screens = new Task_Screens();
        HotKeyBinder hotKeyBinder = new HotKeyBinder();
        public MainWindow()
        {
            InitializeComponent();

            IntPtr HWND = new WindowInteropHelper(this).Handle;

            hotKeyBinder.AddHotKey(new STRUCT_HotKey()
            {
                fsModifiers = l_winapi.Enums.ModEnums.MOD_ALT,
                vk = l_winapi.Enums.WinFormKeys.X,
            });

            hotKeyBinder.Init();
            hotKeyBinder.event_HotKey = (key) =>
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

            };
            //screens.Event_GetMonitorEnum += () =>
            //{
            //    Debug.WriteLine(string.Join(" | ", screens.RECTMonitors));
            //    return true;
            //};

            this.Loaded += MainWindow_Loaded;

        }

        private void MainWindow_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            screens.UpdateScreens();
        }
    }
}