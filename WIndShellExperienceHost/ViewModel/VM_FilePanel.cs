using System.ComponentModel;

namespace WIndShellExperienceHost.ViewModel
{
    public class VM_FilePanel : INotifyPropertyChanged
    {
        private object _FilePath = string.Empty;
        public object FilePath
        {
            get
            {

                return _FilePath;
            }
            set
            {
                _FilePath = value;
            }
        }


        event PropertyChangedEventHandler? INotifyPropertyChanged.PropertyChanged
        {
            add
            {

            }

            remove
            {

            }
        }
    }
}
