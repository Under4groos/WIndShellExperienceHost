using l_winapi.Delegates;
using System.Collections.ObjectModel;
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
            task.Dispose();
            GC.SuppressFinalize(this);
        }
        #endregion
        private ObservableCollection<STRUCT_HotKey> sTRUCT_HotKeys = new ObservableCollection<STRUCT_HotKey>();
        private Task task;

        public Event_HotKey event_HotKey;
        public HotKeyBinder()
        {

        }
        public void AddHotKey(STRUCT_HotKey hotKey)
        {
            sTRUCT_HotKeys.Add(hotKey);

        }
        public void RemoveHotKey(STRUCT_HotKey hotKey)
        {
            task.Dispose();
            sTRUCT_HotKeys.Remove(hotKey);
            this.Init();
        }
        public void Init()
        {
            Debug.WriteLine($"Init {this}");
            task = new Task(() =>
            {
                for (global::System.Int32 i = 0; i < sTRUCT_HotKeys.Count; i++)
                {
                    var hk = sTRUCT_HotKeys[i];
                    hk.RegStatus = Helper.p_HotKey_Register(0, hk.fsModifiers, hk.vk);
                    Debug.WriteLine($"[rg key][status: {hk.RegStatus}]");
                }

                int code = 0;
                while (true)
                {
                    code = Helper._GetMessage();
                    event_HotKey?.Invoke(code);
                }
            });
            task.Start();


        }
    }
}
