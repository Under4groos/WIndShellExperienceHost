namespace l_winapi.Attributes
{
    public class IconValueAttribute : Attribute
    {
        public object Value { get; protected set; }

        public IconValueAttribute(object value)
        {
            this.Value = value;

        }
    }
}
