using l_winapi.Module.HotKey;
using System.Diagnostics;
using System.Windows.Interop;

namespace WIndShellExperienceHost
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 


    public partial class MainWindow : System.Windows.Window
    {
        HotKeyBinder hotKeyBinder = new HotKeyBinder();
        public MainWindow()
        {
            InitializeComponent();

            IntPtr HWND = new WindowInteropHelper(this).Handle;

            hotKeyBinder.AddHotKey(new STRUCT_HotKey()
            {
                fsModifiers = l_winapi.Enums.ModEnums.MOD_ALT,
                vk = l_winapi.Enums.WinFormKeys.G,


            });
            hotKeyBinder.Init();
            hotKeyBinder.event_HotKey = (key) =>
            {
                Debug.WriteLine((l_winapi.Enums.WinFormKeys)(key));

            };
            this.Loaded += MainWindow_Loaded;


        }

        private void MainWindow_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {

        }
    }
}