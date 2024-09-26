namespace l_winapi.Attributes
{
    public class IconValueAttribute : Attribute
    {
        public object Value { get; protected set; }


        public string StrMS
        {
            get; set;
        } = string.Empty;
        public IconValueAttribute(object value, string _StrMS = "")
        {
            this.Value = value;

            this.StrMS = _StrMS;
        }
        public override string ToString()
        {
            return $"{this.Value}|{this.StrMS}";
        }
    }
}
