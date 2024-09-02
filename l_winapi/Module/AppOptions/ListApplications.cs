namespace l_winapi.Module.AppOptions
{
    public struct ListApplications
    {
        public Application this[int index]
        {
            get
            {
                return (Application)apps[index];
            }
            set
            {
                apps[index] = value;
            }
        }


        public List<Application> apps;

        public int Count => apps.Count;

    }
}
