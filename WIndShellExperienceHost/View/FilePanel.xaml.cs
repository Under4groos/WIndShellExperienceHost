using Shell.IconExtractor;
using System.Diagnostics;
using System.IO;
using System.Windows.Media.Imaging;
using WIndShellExperienceHost.ViewModel;

namespace WIndShellExperienceHost.View
{
    /// <summary>
    /// Логика взаимодействия для FilePanel.xaml
    /// </summary>
    public partial class FilePanel : System.Windows.Controls.UserControl
    {

        public string SystemPath
        {
            get; set;
        }
        private Shell.IconExtractor.Enumes.ItemType Type;

        public void Refresh(string SysName, string SysPath)
        {
            _name.Content = SysName;
            SystemPath = SysPath;
            Type = Shell.IconExtractor.Enumes.ItemType.File;

            (this.DataContext as VM_FilePanel).FilePath = SystemPath;

            if (Directory.Exists(SysPath))
            {
                Type = Shell.IconExtractor.Enumes.ItemType.Folder;
            }

            var stru = new Shell.IconExtractor.Strucrure.IcoExtractorOptions()
            {
                iconSize = Shell.IconExtractor.Enumes.IconSize.ExtraLarge,
                path = SystemPath,
                state = Shell.IconExtractor.Enumes.ItemState.Undefined,
                type = Type,
            };


            using (IcoExtractor extr = new IcoExtractor(stru))
            {
                if (extr.GetIcon != null)
                {
                    string SysPathImage = Path.GetFullPath(Path.Combine("Data", $"{SysName}.png"));

                    extr.SaveToFile(SysPathImage);


                    SetImage(SysPathImage);
                }
            }
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
