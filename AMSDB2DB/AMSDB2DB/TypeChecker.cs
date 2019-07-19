using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMSDB2DB
{
   public static class TypeChecker
    {
        /// <summary>
        /// 判断是否为数值类型。
        /// </summary>
        /// <param name="t">要判断的类型</param>
        /// <returns>是否为数值类型</returns>
        public static bool IsNumbericType(this Type t)
        {
            var tc = Type.GetTypeCode(t);
            return (t.IsPrimitive && t.IsValueType && !t.IsEnum && tc != TypeCode.Char && tc != TypeCode.Boolean) || tc == TypeCode.Decimal;
        }

        /// <summary>
        /// 判断是否为可空数值类型。
        /// </summary>
        /// <param name="t">要判断的类型</param>
        /// <returns>是否为可空数值类型</returns>
        public static bool IsNumericOrNullableNumericType(this Type t)
        {
            return t.IsNumbericType() || (t.IsNullableType() && t.GetGenericArguments()[0].IsNumbericType());
        }

        /// <summary>
        /// 判断是否为可空类型。
        /// 注意，直接调用可空对象的.GetType()方法返回的会是其泛型值的实际类型，用其进行此判断肯定返回false。
        /// </summary>
        /// <param name="t">要判断的类型</param>
        /// <returns>是否为可空类型</returns>
        public static bool IsNullableType(this Type t)
        {
            return t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>);
        }


    }
}
