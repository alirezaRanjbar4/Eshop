using System.Reflection;

namespace Eshop.Enum
{
    public static class EnumExtensions
    {
        public static TDest Convert<TDest>(this System.Enum value) where TDest : struct, IComparable, IConvertible, IFormattable
        {
            if (!typeof(TDest).IsEnum)
            {
                throw new Exception("This method can only convert to an enumerations.");
            }

            try
            {
                return (TDest)System.Enum.Parse(typeof(TDest), value.ToString(), true);
            }
            catch
            {
                throw new Exception(string.Format("Error converting enumeration {0} to enumeration {1} ", value, typeof(TDest)));
            }
        }


        public static string GetDescription(this System.Enum GenericEnum) //Hint: Change the method signature and input paramter to use the type parameter T
        {
            Type genericEnumType = GenericEnum.GetType();
            MemberInfo[] memberInfo = genericEnumType.GetMember(GenericEnum.ToString());
            if (memberInfo != null && memberInfo.Length > 0)
            {
                var _Attribs = memberInfo[0].GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false);
                if (_Attribs != null && _Attribs.Count() > 0)
                {
                    return ((System.ComponentModel.DescriptionAttribute)_Attribs.ElementAt(0)).Description;
                }
            }

            return GenericEnum.ToString();
        }

    }
}
