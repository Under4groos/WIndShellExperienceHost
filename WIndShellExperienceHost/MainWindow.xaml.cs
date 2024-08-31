using System.Windows.Interop;

namespace WIndShellExperienceHost
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 


    public partial class MainWindow : System.Windows.Window
    {

        public MainWindow()
        {
            InitializeComponent();

            IntPtr HWND = new WindowInteropHelper(this).Handle;



            this.Loaded += MainWindow_Loaded;


        }

        private void MainWindow_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {

        }
    }
}