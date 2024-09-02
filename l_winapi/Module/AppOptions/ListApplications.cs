namespace l_winapi.Module.AppOptions
{
    public class ListApplications
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


        public List<Application> apps = new List<Application>();

        public int Count => apps.Count;

    }
}
