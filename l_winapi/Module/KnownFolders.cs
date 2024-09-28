namespace l_winapi.Module
{
    public class KnownFolders
    {
        public static readonly Dictionary<string, Guid> FolderGuids = new()
        {
            { "Documents" , new ("FDD39AD0-238F-46AF-ADB4-6C85480369C7") },
            { "Downloads" , new ("374DE290-123F-4565-9164-39C4925E467B") },
            { "Music" , new ("4BD8D571-6D19-48D3-BE97-422220080E43") },
            { "Pictures" , new ("33E28130-4E1E-4676-835A-98395C3BC3BB") },
            { "SavedGames" , new ("4C5C32FF-BB9D-43B0-B5B4-2D72E54EAAA4") },
        };
        public static string GetPath(string Name)
        {
            return Helper.SHGetKnownFolderPath(FolderGuids[Name], 0);
        }
    }
}
