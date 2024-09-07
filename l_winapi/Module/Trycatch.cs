using l_winapi.Delegates;
using System.Diagnostics;
using System.Windows;


namespace l_winapi.Module
{
    public class Trycatch
    {

        public static bool trycatch(trycath_init init, trycath_error err = null)
        {
            try
            {
                init?.Invoke();
                return true;
            }
            catch (Exception e)
            {
#if DEBUG


                // public static MessageBoxResult Show(
                // Window owner,
                // string messageBoxText,
                // string caption,
                // MessageBoxButton button,
                // MessageBoxImage icon,
                // MessageBoxResult defaultResult,
                // MessageBoxOptions options);

                MessageBox.Show(e.Message, caption: "[DEBUG] Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Debug.WriteLine(e.Message);
                Console.WriteLine(e.Message);

                err?.Invoke(e.Message);
#else

#endif
                return false;
            }
        }
    }
}
