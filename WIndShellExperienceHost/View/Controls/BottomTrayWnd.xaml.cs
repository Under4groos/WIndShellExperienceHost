using l_winapi.Enums;
using System.Diagnostics;

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
            var icon = ((SegoeFluentIcons)(sender)).Icon;
            Debug.WriteLine(icon.GetIconValueAll());
            //switch (icon)
            //{
            //    case l_winapi.Enums.EnumSFIcons.Downloads:
            //        break;
            //    case l_winapi.Enums.EnumSFIcons.Drives:
            //        break;
            //    case l_winapi.Enums.EnumSFIcons.Setting:
            //        break;
            //    case l_winapi.Enums.EnumSFIcons.Power:
            //        break;
            //    case l_winapi.Enums.EnumSFIcons.Console:
            //        break;
            //    case l_winapi.Enums.EnumSFIcons.WiFi:
            //        break;
            //    case l_winapi.Enums.EnumSFIcons.Bluetooth:
            //        break;
            //    case l_winapi.Enums.EnumSFIcons.Sound:
            //        break;
            //    case l_winapi.Enums.EnumSFIcons.SoundDevices:
            //        break;
            //    default:
            //        break;
            //}

        }
    }
}
