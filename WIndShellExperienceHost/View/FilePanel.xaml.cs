using System.Windows;

namespace WIndShellExperienceHost.View
{
    /// <summary>
    /// Логика взаимодействия для FilePanel.xaml
    /// </summary>
    public partial class FilePanel : System.Windows.Controls.UserControl
    {

        public static readonly DependencyProperty FilePathProperty = DependencyProperty.Register("FilePath", typeof(string), typeof(FilePanel), new FrameworkPropertyMetadata(OnBindingChanged) { });

        public string FilePath
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
        private static void OnBindingChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var uc = d as FilePanel;
            if (uc == null)
                return;
            uc._name.Content = (string)e.NewValue;
        }
        public FilePanel()
        {
            InitializeComponent();


        }

    }
}
