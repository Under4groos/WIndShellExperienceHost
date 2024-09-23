using System.IO;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using WIndShellExperienceHost.ViewModel;

namespace WIndShellExperienceHost.View
{

    public partial class FilePanel : System.Windows.Controls.UserControl
    {
        private DoubleAnimation buttonAnimation = new DoubleAnimation()
        {
            DecelerationRatio = 0.1,
            From = 0,

            To = 1,
            Duration = TimeSpan.FromSeconds(0.2)
        };
        private ThicknessAnimation LabelAnimations;
        private DoubleAnimation anim_ = new DoubleAnimation()
        {

            From = 1,

            To = 0,
            Duration = TimeSpan.FromSeconds(0.2)
        };

        public string SystemPath
        {
            get; set;
        }
        public Shell.IconExtractor.Enumes.ItemType Type;

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


            this.BeginAnimation(FilePanel.OpacityProperty, buttonAnimation);
            this.DataContext = new VM_FilePanel();

            this.Loaded += FilePanel_Loaded;
            this.MouseEnter += FilePanel_MouseEnter;
        }

        private void FilePanel_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            //_name.BeginAnimation(System.Windows.Controls.Label.MarginProperty, LabelAnimations);
        }

        private void FilePanel_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            LabelAnimations = new ThicknessAnimation()
            {
                From = new System.Windows.Thickness(),
                To = new System.Windows.Thickness(-(this.ActualWidth + 60), 0, 0, 0),
                Duration = TimeSpan.FromSeconds(1)
            };
        }

        public void AnimationRemove(Action action = null)
        {

            anim_.Completed += (o, e) => { action?.Invoke(); };
            this.BeginAnimation(FilePanel.OpacityProperty, anim_);
        }
    }
}
