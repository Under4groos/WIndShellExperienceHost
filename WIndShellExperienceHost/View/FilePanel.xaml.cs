using Shell.IconExtractor;
using System.IO;
using System.Windows.Media.Imaging;

namespace WIndShellExperienceHost.View
{
    /// <summary>
    /// Логика взаимодействия для FilePanel.xaml
    /// </summary>
    public partial class FilePanel : System.Windows.Controls.UserControl
    {






        public void Refresh(string SysName, string SysPath)
        {
            _name.Content = SysName;
            var type_ = Shell.IconExtractor.Enumes.ItemType.File;
            if (Directory.Exists(SysPath))
            {
                type_ = Shell.IconExtractor.Enumes.ItemType.Folder;
            }

            var stru = new Shell.IconExtractor.Strucrure.IcoExtractorOptions()
            {
                iconSize = Shell.IconExtractor.Enumes.IconSize.ExtraLarge,
                path = SysPath,
                state = Shell.IconExtractor.Enumes.ItemState.Undefined,
                type = type_,
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


        }

    }
}
