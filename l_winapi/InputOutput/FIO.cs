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
            return Trycath.trycath(() =>
            {

                var obj_ = JsonConvert.DeserializeObject(
                    File.ReadAllText(path));
                fileJsonValid?.Invoke(obj_, path);
            });

        }
    }
}
