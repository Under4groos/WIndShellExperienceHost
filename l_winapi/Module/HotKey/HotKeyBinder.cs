using System.Diagnostics;

namespace l_winapi.Module.HotKey
{
    public class HotKeyBinder : IDisposable
    {
        #region Disposable
        private bool disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: освободить управляемое состояние (управляемые объекты)
                }

                // TODO: освободить неуправляемые ресурсы (неуправляемые объекты) и переопределить метод завершения
                // TODO: установить значение NULL для больших полей
                disposedValue = true;
            }
        }

        // // TODO: переопределить метод завершения, только если "Dispose(bool disposing)" содержит код для освобождения неуправляемых ресурсов
        // ~HotKeyBinder()
        // {
        //     // Не изменяйте этот код. Разместите код очистки в методе "Dispose(bool disposing)".
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Не изменяйте этот код. Разместите код очистки в методе "Dispose(bool disposing)".
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
        #endregion
        private List<STRUCT_HotKey> sTRUCT_HotKeys = new List<STRUCT_HotKey>();

        public HotKeyBinder()
        {

        }
        public void AddKeyBind(STRUCT_HotKey hotKey)
        {
            sTRUCT_HotKeys.Add(hotKey);

        }
        public void Init()
        {
            new Thread((() =>
            {

                bool status_reg = Helper.p_HotKey_Register(0, l_winapi.Enums.ModEnums.MOD_ALT, l_winapi.Enums.WinFormKeys.G);
                Debug.WriteLine(status_reg);
                int code = 0;
                while (true)
                {
                    code = Helper._GetMessage();
                    Debug.WriteLine(code);
                }

            })).Start();
        }
    }
}
