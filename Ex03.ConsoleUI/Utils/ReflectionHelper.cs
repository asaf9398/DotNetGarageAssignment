using Ex03.ConsoleUI.Utils;
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
        public static void GetInputToObject(object i_InputObject)
        {
            PropertyInfo[] objectProperties = ReflectionHelper.GetPropertiesFromObject(i_InputObject);

            if (objectProperties != null)
            {
                for (int i = 0; i < objectProperties.Length; i++)
                {
                    try
                    {
                        string paramType = "";

                        if (objectProperties[i].PropertyType.IsEnum)
                        {
                            paramType = StringUtils.EnumOptionsToString(objectProperties[i].PropertyType);
                        }
                        else
                        {
                            paramType = objectProperties[i].PropertyType.Name;
                        }

                        Console.WriteLine($"Enter {StringUtils.SplitCamelCase(objectProperties[i].Name)} ({paramType}):");
                        string input = Console.ReadLine();
                        object convertedValue;

                        if (objectProperties[i].PropertyType.IsEnum)
                        {
                            convertedValue = Enum.Parse(objectProperties[i].PropertyType, input, ignoreCase: true);
                        }
                        else
                        {
                            convertedValue = Convert.ChangeType(input, objectProperties[i].PropertyType);
                        }

                        objectProperties[i].SetValue(i_InputObject, convertedValue, null);
                    }
                    catch (Exception ex)
                    {
                        string message = ex.Message;

                        if (ex.InnerException != null)
                        {
                            message = ex.InnerException.Message;
                        }

                        Console.WriteLine($"Error: {message}");
                        Console.WriteLine($"Please try again!");
                        i--;
                    }
                }
            }
        }

    }
}
