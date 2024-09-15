using l_winapi.Enums;

namespace WIndShellExperienceHost.View.Controls
{
    /// <summary>
    /// Логика взаимодействия для SegoeFluentIcons.xaml
    /// </summary>
    public partial class SegoeFluentIcons : System.Windows.Controls.UserControl
    {
        public EnumSFIcons Icon
        {

            set
            {
                _label.Content = value.GetIconValue();
            }
        }
        public SegoeFluentIcons()
        {
            InitializeComponent();
        }
    }
}
