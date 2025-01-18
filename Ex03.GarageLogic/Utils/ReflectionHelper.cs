using Ex03.GarageLogic.Enums;
using Ex03.GarageLogic.Model;
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

        public static List<PropertyDefinition> GetProperties(object i_InputObject)
        {
            List<PropertyDefinition> propertyDefinitions = new List<PropertyDefinition>();
            PropertyInfo[] properties = i_InputObject.GetType().GetProperties();

            foreach (PropertyInfo property in properties)
            {
                MethodInfo setMethod = property.GetSetMethod();
                if (setMethod != null)
                {
                    propertyDefinitions.Add(new PropertyDefinition
                    {
                        Name = property.Name,
                        DisplayName = StringUtils.SplitCamelCase(property.Name),
                        PropertyType = property.PropertyType,
                        IsEnum = property.PropertyType.IsEnum,
                        EnumOptions = property.PropertyType.IsEnum ? StringUtils.EnumOptionsToString(property.PropertyType) : null
                    });
                }
            }

            return propertyDefinitions;
        }

    }
}
