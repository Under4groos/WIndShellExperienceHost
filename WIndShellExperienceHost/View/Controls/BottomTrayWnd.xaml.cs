using l_winapi.Enums;
using l_winapi.Module;
using WIndShellExperienceHost.Module;

namespace WIndShellExperienceHost.View.Controls
{
    /// <summary>
    /// Логика взаимодействия для BottomTrayWnd.xaml
    /// </summary>
    public partial class BottomTrayWnd : System.Windows.Controls.UserControl
    {
        public BottomTrayWnd()
        {
            InitializeComponent();

            foreach (var control in _buttons.Children)
            {
                if (control is SegoeFluentIcons _button)
                {
                    _button.PreviewMouseLeftButtonDown += _button_PreviewMouseLeftButtonDown;
                }
            }
        }

        private void _button_PreviewMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            G_.MainWindow.SetVisibility(false);
            SegoeFluentIcons _control = ((SegoeFluentIcons)(sender));

            switch (_control.Icon)
            {
                case EnumSFIcons.Downloads:
                    string path = KnownFolders.GetPath("Downloads");
                    Util.StartExplorer(path);
                    break;
                case EnumSFIcons.Drives:

                    Util.StartExplorer();
                    break;
                default:
                    var icon = _control.Icon.GetIconValues().First();
                    Util.MS_Start(icon.StrMS);
                    break;
            }




        }
    }
}
