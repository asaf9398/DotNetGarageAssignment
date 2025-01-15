using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Ex03.ConsoleUI.Utils
{
    internal static class ReflectionHelper
    {
        public static bool IsUserDefinedMethod(MethodInfo method)
        {
            if (method.IsSpecialName)
            {
                return false;
            }

            if (method.DeclaringType.Assembly.FullName.Contains("System.Private.CoreLib") ||
                method.DeclaringType.Assembly.FullName.Contains("mscorlib"))
            {
                return false;
            }

            if (method.GetBaseDefinition().DeclaringType == typeof(object))
            {
                return false;
            }

            return true;
        }
    }
}
