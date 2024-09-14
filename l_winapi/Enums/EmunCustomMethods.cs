using l_winapi.Attributes;
using System.Reflection;

namespace l_winapi.Enums
{
    public static class EmunCustomMethods
    {
        public static string GetIconValue(this Enum value)
        {
            Type type = value.GetType();
            FieldInfo fieldInfo = type.GetField(value.ToString());
            IconValueAttribute[] attribs = fieldInfo.GetCustomAttributes(typeof(IconValueAttribute), false) as IconValueAttribute[];

            return string.Join(",", (from atr in attribs select atr.Value));
        }


    }
}
