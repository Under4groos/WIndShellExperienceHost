using l_winapi.Module.AppOptions;

namespace WIndShellExperienceHost.Module
{
    public static class G_
    {
        public static MainWindow MainWindow => (MainWindow)App.Current.MainWindow;


        public static Options AllOptions = new l_winapi.Module.AppOptions.Options();

    }
}
