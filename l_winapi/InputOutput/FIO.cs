using l_winapi.Delegates;
using l_winapi.Module;
using Newtonsoft.Json;
using System.IO;

namespace l_winapi.InputOutput
{
    public static class FIO
    {

        public static bool ReadFileToJsonObject(string path, FileJsonValid fileJsonValid)
        {
            if (!File.Exists(path))
                return false;
            return Trycatch.trycatch(() =>
            {




                fileJsonValid?.Invoke(File.ReadAllText(path), path);
            });

        }
        public static bool WriteFileToJsonObject(string path, object obj)
        {

            return Trycatch.trycatch(() =>
            {

                string obj_str = JsonConvert.SerializeObject(obj, Formatting.Indented);
                File.WriteAllText(path, obj_str);
            });

        }
    }
}
