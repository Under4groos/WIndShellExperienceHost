using l_winapi.Module.AppOptions;
using Newtonsoft.Json;

AppOptions __List_Applications = new AppOptions();


foreach (var app in File.ReadAllLines(@"C:\Users\UnderKo\source\repos\WIndShellExperienceHost-\Build\JsonApplications.json"))
{
    __List_Applications.apps.Add(new Application()
    {
        SysPath = app,
        SysName = Path.GetFileName(app),
    });
}

File.WriteAllText("data.json", JsonConvert.SerializeObject(__List_Applications));
Console.WriteLine("Hello, World!");