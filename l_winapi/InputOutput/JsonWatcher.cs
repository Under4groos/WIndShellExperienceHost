using System.IO;

namespace l_winapi.InputOutput
{
    public delegate void FileEventChange(string path);
    public class JsonWatcher
    {
        // ХУИТА ПОЛНАЯ 
        public FileSystemWatcher FSW;
        public FileEventChange EventChange;
        public JsonWatcher(string path, FileEventChange _event = null)
        {
            if (_event != null)
                EventChange = _event;
            FSW = new FileSystemWatcher(path);
            //FSW.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.DirectoryName;
            //FSW.Filter = "*.*";

            FSW.EnableRaisingEvents = true;


            FSW.Changed += (o, e) =>
            {
                this.EventChange?.Invoke(e.FullPath);


            }; ;
            FSW.Created += (o, e) =>
            {
                this.EventChange?.Invoke(e.FullPath);


            }; ;
            FSW.Deleted += (o, e) =>
            {
                this.EventChange?.Invoke(e.FullPath);


            }; ;
            FSW.Renamed += (o, e) =>
            {
                this.EventChange?.Invoke(e.FullPath);


            }; ;

            //FileSystemWatcher watcher = new FileSystemWatcher();
            //watcher.Path = path;
            //watcher.NotifyFilter = NotifyFilters.LastWrite;
            //watcher.Filter = "*.*";
            //watcher.Changed += new FileSystemEventHandler(OnChanged);
            //watcher.EnableRaisingEvents = true;
        }

    }
}
