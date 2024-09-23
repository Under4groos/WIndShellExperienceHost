using l_winapi.InputOutput;
using Newtonsoft.Json;
using System.Diagnostics;
using System.IO;

namespace l_winapi.Module.AppOptions
{
    public class Options
    {
        public AppOptions List_Applications = new AppOptions();
        public const string filedata_json = "__applications.json";
        public Action Loaded;
        public void Load()
        {
            if (File.Exists(filedata_json))
            {
                Trycatch.trycatch(() =>
                {

                    List_Applications = JsonConvert.DeserializeObject<AppOptions>(File.ReadAllText(filedata_json)) ?? new AppOptions();
                    Loaded?.Invoke();
                    Debug.WriteLine($"Load from file: {filedata_json}");
                });
            }
        }
        public void SaveData()
        {

            FIO.WriteFileToJsonObject(filedata_json, this.List_Applications);

        }
    }
}
