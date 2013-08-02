using System;
using System.Linq;
using System.Reflection;

namespace EF.Interception
{
    internal static class CustomAttributeExtensions
    {
        public static TAttribute GetCustomAttribute<TAttribute>(this MemberInfo memberInfo, bool inherit = true)
            where TAttribute : Attribute
        {
            return memberInfo.GetCustomAttributes(typeof(TAttribute), inherit).Cast<TAttribute>().FirstOrDefault();
        }
    }
}