using System.Diagnostics;
using System.IO;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using WIndShellExperienceHost.ViewModel;

namespace WIndShellExperienceHost.View
{

    public partial class FilePanel : System.Windows.Controls.UserControl
    {

        public string SystemPath
        {
            get; set;
        }
        private Shell.IconExtractor.Enumes.ItemType Type;

        public void SetData(string SysName, string SysPath)
        {
            _name.Content = SysName;
            SystemPath = SysPath;
            (this.DataContext as VM_FilePanel).FilePath = SystemPath;
        }



        public void SetImage(string SysPathImage)
        {
            if (!File.Exists(SysPathImage))
            {
                this._icon.Source = null;
                return;
            }

            BitmapImage image = new BitmapImage();
            image.BeginInit();
            image.UriSource = new Uri(SysPathImage);
            image.EndInit();

            this._icon.Source = image;
        }

        public FilePanel()
        {
            InitializeComponent();
            this.Opacity = 0;

            DoubleAnimation buttonAnimation = new DoubleAnimation()
            {
                DecelerationRatio = 0.1,
                From = 0,
                // SpeedRatio = 4,
                To = 1,
                Duration = TimeSpan.FromSeconds(1)
            };
            this.BeginAnimation(FilePanel.OpacityProperty, buttonAnimation);

            this.DataContext = new VM_FilePanel();
            this.MouseRightButtonDown += FilePanel_MouseRightButtonDown;
            this.MouseLeftButtonDown += FilePanel_MouseLeftButtonDown;

        }

        private void FilePanel_MouseRightButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            l_winapi.Module.Trycatch.trycatch(() =>
            {
                switch (Type)
                {
                    case Shell.IconExtractor.Enumes.ItemType.Drive:

                        break;
                    case Shell.IconExtractor.Enumes.ItemType.Folder:
                        //int status_ = Helper.ShellExplorer(IntPtr.Zero, "open", this.SystemPath);

                        //Debug.WriteLine(status_);

                        break;
                    case Shell.IconExtractor.Enumes.ItemType.File:

                        break;
                    default:
                        break;
                }


            });
        }

        private void FilePanel_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            l_winapi.Module.Trycatch.trycatch(() =>
            {
                switch (Type)
                {
                    case Shell.IconExtractor.Enumes.ItemType.Drive:

                        break;
                    case Shell.IconExtractor.Enumes.ItemType.Folder:
                        Process.Start("explorer.exe", $"/select, \"{this.SystemPath}\"");
                        break;
                    case Shell.IconExtractor.Enumes.ItemType.File:
                        Process.Start("explorer.exe", this.SystemPath);
                        break;
                    default:
                        break;
                }


            });
        }
    }
}
