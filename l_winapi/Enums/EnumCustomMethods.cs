using l_winapi.Attributes;
using System.Reflection;

namespace l_winapi.Enums
{
    public static class EnumCustomMethods
    {
        public static string GetIconValue(this Enum value)
        {
            return string.Join(",", (from atr in GetIconValues(value) select atr.Value));
        }
        public static string GetIconValueAll(this Enum value)
        {
            return string.Join(",", (from atr in GetIconValues(value) select atr));
        }
        public static IconValueAttribute[] GetIconValues(this Enum value)
        {
            Type type = value.GetType();
            FieldInfo fieldInfo = type.GetField(value.ToString());
            return fieldInfo.GetCustomAttributes(typeof(IconValueAttribute), false) as IconValueAttribute[];


        }
    }
}
