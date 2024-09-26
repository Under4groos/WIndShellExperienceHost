using l_winapi.Enums;

namespace WIndShellExperienceHost.View.Controls
{
    /// <summary>
    /// Логика взаимодействия для SegoeFluentIcons.xaml
    /// </summary>
    public partial class SegoeFluentIcons : System.Windows.Controls.UserControl
    {
        private EnumSFIcons _icon;
        public EnumSFIcons Icon
        {

            set
            {
                _icon = value;
                _label.Content = value.GetIconValue();
            }
            get
            {
                return _icon;
            }
        }
        public SegoeFluentIcons()
        {
            InitializeComponent();
        }
    }
}
