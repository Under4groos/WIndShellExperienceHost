using l_winapi.Delegates;
using l_winapi.Module;

namespace l_winapi.Screens
{
    public class Task_Screens : IDisposable
    {
        public Task? task;

        public MonitorEnumDelegateData? Event_GetMonitorEnum = null;
        public List<RECT> RECTMonitors = new List<RECT>();
        public Task_Screens()
        {



        }
        public void Init()
        {
            task = new Task(async () =>
            {
                while (true)
                {
                    RECTMonitors.Clear();
                    Helper.w_EnumDisplayMonitors(Updates, 0);
                    Event_GetMonitorEnum?.Invoke();
                    await Task.Delay(5000);

                }
            });
            task.Start();
        }
        bool Updates(IntPtr hMonitor, IntPtr hdcMonitor, ref RECT lprcMonitor, IntPtr dwData)
        {
            RECTMonitors.Add(lprcMonitor);

            return true;
        }

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
        // ~Task_Screens()
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
    }
}
