namespace l_winapi.Attributes
{
    public class IconValueAttribute : Attribute
    {
        public object Value { get; protected set; }

        public bool IsMSapp
        {
            get; set;
        } = false;
        public string StrMS
        {
            get; set;
        } = string.Empty;
        public IconValueAttribute(object value, bool _IsMSapp = false, string _StrMS = "")
        {
            this.Value = value;
            this.IsMSapp = _IsMSapp;
            this.StrMS = _StrMS;
        }
    }
}
