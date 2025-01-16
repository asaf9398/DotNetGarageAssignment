using Ex03.GarageLogic.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Ex03.GarageLogic.Utils
{
    internal static class ReflectionHelper
    {
        public static PropertyInfo[] GetPropertiesFromObject(object i_InputObject)
        {
            Type objectType = i_InputObject.GetType();
            PropertyInfo[] allProperties = objectType.GetProperties();
            List<PropertyInfo> writableProperties = new List<PropertyInfo>();

            foreach (var property in allProperties)
            {
                MethodInfo setMethod = property.GetSetMethod();
                if (setMethod != null)
                {
                    writableProperties.Add(property);
                }
            }

            return writableProperties.ToArray();
        }
    }
}
