using l_winapi.Delegates;
using System.Diagnostics;


namespace l_winapi.Module
{
    public class Trycath
    {

        public static bool trycath(trycath_init init, trycath_error err = null)
        {
            try
            {
                init?.Invoke();
                return true;
            }
            catch (Exception e)
            {
#if DEBUG
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
