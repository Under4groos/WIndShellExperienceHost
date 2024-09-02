using Shell.IconExtractor;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;

namespace WIndShellExperienceHost.View
{
    /// <summary>
    /// Логика взаимодействия для FilePanel.xaml
    /// </summary>
    public partial class FilePanel : System.Windows.Controls.UserControl
    {
        private string SysPathImage = string.Empty;
        public static readonly DependencyProperty FilePathProperty = DependencyProperty.Register("SysPath", typeof(string), typeof(FilePanel), new FrameworkPropertyMetadata(OnBinding_SysPath_Changed) { });

        public static readonly DependencyProperty SysNameProperty = DependencyProperty.Register("SysName", typeof(string), typeof(FilePanel), new FrameworkPropertyMetadata(OnBinding_SysName_Changed) { });

        public string SysPath
        {
            get
            {
                return (string)GetValue(FilePathProperty);
            }
            set
            {
                SetValue(FilePathProperty, value);
            }
        }

        public string SysName
        {
            get
            {
                return (string)GetValue(SysNameProperty);
            }
            set
            {
                SetValue(SysNameProperty, value);
            }
        }
        private static void OnBinding_SysName_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var uc = d as FilePanel;
            if (uc == null)
                return;
            uc._name.Content = uc.SysName = (string)e.NewValue;

        }
        private static void OnBinding_SysPath_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var uc = d as FilePanel;
            if (uc == null)
                return;
            uc.SysPath = (string)e.NewValue;
            //if (!File.Exists(uc.SysPath))
            //    if (!Directory.Exists(uc.SysPath))
            //        return;


            Shell.IconExtractor.Enumes.ItemType type__ = Shell.IconExtractor.Enumes.ItemType.File;

            var stru = new Shell.IconExtractor.Strucrure.IcoExtractorOptions()
            {
                iconSize = Shell.IconExtractor.Enumes.IconSize.ExtraLarge,
                path = uc.SysPath,
                state = Shell.IconExtractor.Enumes.ItemState.Undefined,
                type = Shell.IconExtractor.Enumes.ItemType.File,
            };
            using (IcoExtractor extr = new IcoExtractor(stru))
            {

                if (extr.GetIcon != null)
                {
                    Debug.WriteLine(uc.SysName);
                    uc.SysPathImage = Path.GetFullPath(Path.Combine("Data", $"{uc.SysName}.png"));

                    extr.SaveToFile(uc.SysPathImage);



                    BitmapImage image = new BitmapImage();
                    image.BeginInit();
                    image.UriSource = new Uri(uc.SysPathImage);
                    image.EndInit();

                    uc._icon.Source = image;
                }
            };
        }


        public FilePanel()
        {
            InitializeComponent();


        }

    }
}
