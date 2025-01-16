using Ex03.ConsoleUI.Utils;
using Ex03.GarageLogic.Enums;
using Ex03.GarageLogic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Text;

namespace Ex03.ConsoleUI
{
    internal static class GarageUI
    {
        private static void getParametersInfo(ParameterInfo[] i_Parameters, object[] i_Arguments)
        {
            for (int i = 0; i < i_Parameters.Length; i++)
            {
                string paramName = StringUtils.RemovePrefixIfExists(i_Parameters[i].Name);
                paramName = StringUtils.SplitCamelCase(paramName);
                Console.WriteLine($"Enter {paramName} ({i_Parameters[i].ParameterType.Name}):");
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
        public static void InvokeFunction(MethodInfo i_Method, Garage i_Garage)
        {
            ParameterInfo[] parameters = i_Method.GetParameters();
            object[] arguments = new object[parameters.Length];
            //getParametersInfo(parameters, arguments);
            for (int i = 0; i < parameters.Length; i++)
            {
                string paramName = StringUtils.RemovePrefixIfExists(parameters[i].Name);
                paramName = StringUtils.SplitCamelCase(paramName);
                string paramType = "";
                if (parameters[i].ParameterType.IsEnum)
                {
                    Array enumValues = Enum.GetValues(parameters[i].ParameterType);
                    StringBuilder sb = new StringBuilder();
                    foreach (var enumValue in enumValues)
                    {
                        sb.Append(enumValue.ToString());
                        sb.Append(", ");
                    }
                    paramType = sb.ToString();
                }
                else
                {
                    paramType = parameters[i].ParameterType.Name;
                }

                Console.WriteLine($"Enter {paramName} ({paramType}):");
                string input = Console.ReadLine();
                try
                {
                    if (parameters[i].ParameterType.IsEnum)
                    {
                        arguments[i] = Enum.Parse(parameters[i].ParameterType, input, ignoreCase: true);
                    }
                    else
                    {
                        arguments[i] = Convert.ChangeType(input, parameters[i].ParameterType);
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

            try
            {
                object returnedValue = i_Method.Invoke(i_Garage, arguments);

                if (returnedValue is Vehicle)
                {
                    Type vehicleType = returnedValue.GetType();
                    //PropertyInfo[] vehicleProperties = vehicleType.GetProperties();
                    PropertyInfo[] vehicleProperties = vehicleType
                     .GetProperties()
                    .Where(p => p.GetSetMethod() != null) // רק מאפיינים עם set ציבורי
                    .ToArray();
                    if (vehicleProperties != null)
                    {
                        for (int i = 0; i < vehicleProperties.Length; i++)
                        {
                            try
                            {
                                string paramType = "";
                                if (vehicleProperties[i].PropertyType.IsEnum)
                                {
                                    Array enumValues = Enum.GetValues(vehicleProperties[i].PropertyType);
                                    StringBuilder sb = new StringBuilder();
                                    foreach (var enumValue in enumValues)
                                    {
                                        sb.Append(enumValue.ToString());
                                        sb.Append(", ");
                                    }
                                    paramType = sb.ToString();
                                }
                                else
                                {
                                    paramType = vehicleProperties[i].PropertyType.Name;
                                }
                                Console.WriteLine($"Enter {StringUtils.SplitCamelCase(vehicleProperties[i].Name)} ({paramType}):");
                                string input = Console.ReadLine();
                                object convertedValue;

                                if (vehicleProperties[i].PropertyType.IsEnum)
                                {
                                    convertedValue = Enum.Parse(vehicleProperties[i].PropertyType, input, ignoreCase: true);
                                }
                                else
                                {
                                    convertedValue = Convert.ChangeType(input, vehicleProperties[i].PropertyType);
                                }
                                vehicleProperties[i].SetValue(returnedValue, convertedValue, null);
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
                else if (returnedValue is List<string>)
                {
                    Console.WriteLine("Vehicles list:");
                    List<string> currentStringList = (List<string>)returnedValue;
                    foreach (var currentString in currentStringList)
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

        public static void GetParamsInput(object i_Object)
        {
            Type vehicleType = i_Object.GetType();
            PropertyInfo[] objectProperties = vehicleType.GetProperties();
            for (int i = 0; i < objectProperties.Length; i++)
            {
                Console.WriteLine($"Enter {objectProperties[i].Name} ({objectProperties[i].Name}):");
                string input = Console.ReadLine();
                objectProperties[i].SetValue(i_Object, input, null);
            }

        }


        //public static void AddVehicleUI(Garage i_Garage)
        //{
        //    Console.WriteLine("Enter license number:");
        //    string licenseNumber = Console.ReadLine();
        //
        //    if (i_Garage.IsVehicleExists(licenseNumber))
        //    {
        //        Console.WriteLine("Vehicle already exists in the garage.");
        //        return;
        //    }
        //
        //    Console.WriteLine("Select vehicle type (Car, Motorcycle, Truck):");
        //    string vehicleTypeInput = Console.ReadLine();
        //    eVehicleType vehicleType = (eVehicleType)Enum.Parse(typeof(eVehicleType), vehicleTypeInput, true);
        //
        //    PropertyInfo[] vehicleProperties = i_Garage.GetVehiclePropertiesByType(vehicleType);
        //    Dictionary<string, object> propertyValues = new Dictionary<string, object>();
        //
        //    foreach (PropertyInfo property in vehicleProperties)
        //    {
        //        Console.WriteLine($"Enter {property.Name} ({property.PropertyType.Name}):");
        //        string input = Console.ReadLine();
        //        object value = Convert.ChangeType(input, property.PropertyType);
        //        propertyValues[property.Name] = value;
        //    }
        //
        //    try
        //    {
        //        i_Garage.AddVehicle(licenseNumber, vehicleType, propertyValues);
        //        Console.WriteLine("Vehicle added successfully.");
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Failed to add vehicle: {ex.Message}");
        //    }
        //}
    }
}
