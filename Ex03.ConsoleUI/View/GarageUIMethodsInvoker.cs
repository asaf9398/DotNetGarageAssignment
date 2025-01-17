using Ex03.ConsoleUI.Utils;
using Ex03.GarageLogic.Enums;
using Ex03.GarageLogic.Model;
using Ex03.GarageLogic.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Text;

namespace Ex03.ConsoleUI
{
    internal static class GarageUIMethodsInvoker
    {
        private const string k_InputStringPrefix = "i_";
        public static void InvokeFunction(MethodInfo i_Method, Garage i_Garage)
        {
            ParameterInfo[] parameters = i_Method.GetParameters();
            object[] arguments = new object[parameters.Length];
            GetInputToParameters(parameters, arguments);

            try
            {
                object returnedValue = i_Method.Invoke(i_Garage, arguments);

                if (returnedValue is List<PropertyDefinition> properties)
                {
                    Dictionary<string, object> filledProperties = CollectPropertiesFromUser(properties);
                    i_Garage.GetType().GetMethod("FillVehicleProperties", BindingFlags.NonPublic | BindingFlags.Instance)?.Invoke(i_Garage, new object[] { arguments[0], filledProperties });
                }
                else if (returnedValue is List<string>)
                {
                    Console.WriteLine("The wanted details:");
                    List<string> currentStringList = (List<string>)returnedValue;

                    foreach (string currentString in currentStringList)
                    {
                        Console.WriteLine($"{currentString}");
                    }
                }

                Console.WriteLine("Operation completed successfully.");
            }
            catch (Exception ex)
            {
                string message = ex.Message;

                if (ex.InnerException != null)
                {
                    message = ex.InnerException.Message;
                }

                Console.WriteLine($"Error: {message}");
            }
        }

        public static void GetInputToParameters(ParameterInfo[] i_Parameters, object[] i_Arguments)
        {
            for (int i = 0; i < i_Parameters.Length; i++)
            {
                string paramName = StringUtils.RemovePrefixIfExists(i_Parameters[i].Name, k_InputStringPrefix);
                paramName = StringUtils.SplitCamelCase(paramName);
                string paramType = "";

                if (i_Parameters[i].ParameterType.IsEnum)
                {
                    paramType = StringUtils.EnumOptionsToString(i_Parameters[i].ParameterType);
                }
                else
                {
                    paramType = i_Parameters[i].ParameterType.Name;
                }

                Console.WriteLine($"Enter {paramName} ({paramType}):");
                string input = Console.ReadLine();

                try
                {
                    if (i_Parameters[i].ParameterType.IsEnum)
                    {
                        i_Arguments[i] = Enum.Parse(i_Parameters[i].ParameterType, input, ignoreCase: true);
                    }
                    else
                    {
                        i_Arguments[i] = Convert.ChangeType(input, i_Parameters[i].ParameterType);
                    }
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

        private static Dictionary<string, object> CollectPropertiesFromUser(List<PropertyDefinition> properties)
        {
            Dictionary<string, object> filledProperties = new Dictionary<string, object>();
            object convertedValue = null;

            for (int i = 0; i < properties.Count; i++)
            {
                PropertyDefinition property = properties[i];

                if (property.IsEnum)
                {
                    Console.WriteLine($"Enter {property.DisplayName} ({StringUtils.EnumOptionsToString(property.PropertyType)}):");
                }
                else
                {
                    Console.WriteLine($"Enter {property.DisplayName} ({property.PropertyType.Name}):");
                }

                string input = Console.ReadLine();

                try
                {
                    convertedValue = property.IsEnum ? Enum.Parse(property.PropertyType, input, true) : Convert.ChangeType(input, property.PropertyType);
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
                    continue;
                }

                filledProperties[property.Name] = convertedValue;
            }

            return filledProperties;
        }
    }
}
