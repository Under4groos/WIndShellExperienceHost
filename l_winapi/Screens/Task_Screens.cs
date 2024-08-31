using l_winapi.Delegates;
using l_winapi.Module;

namespace l_winapi.Screens
{
    public class Task_Screens : IDisposable
    {
        public Task? task;

        public MonitorEnumDelegate? Event_GetMonitorEnum = null;

        public Task_Screens()
        {


            task = new Task(async () =>
            {
                while (true)
                {
                    if (Event_GetMonitorEnum != null)
                        Helper.w_EnumDisplayMonitors(Event_GetMonitorEnum, 0);
                    await Task.Delay(5000);
                }
            });
            task.Start();
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
            GC.SuppressFinalize(this);
        }
    }
}
