using Ex03.GarageLogic.Enums;
using Ex03.GarageLogic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Ex03.ConsoleUI
{
    internal static class GarageUI
    {
        public static void InvokeFunction(MethodInfo i_Method, Garage i_Garage)
        {
            ParameterInfo[] parameters = i_Method.GetParameters();
            object[] arguments = new object[parameters.Length];

            for (int i = 0; i < parameters.Length; i++)
            {
                Console.WriteLine($"Enter {parameters[i].Name} ({parameters[i].ParameterType.Name}):");
                string input = Console.ReadLine();
                if (parameters[i].ParameterType.IsEnum)
                {
                    arguments[i] = Enum.Parse(parameters[i].ParameterType, input, ignoreCase: true);
                }
                else
                {
                    arguments[i] = Convert.ChangeType(input, parameters[i].ParameterType);
                }
            }

            try
            {
                object returnedValue = i_Method.Invoke(i_Garage, arguments);

                if (returnedValue is Vehicle)
                {
                    Type vehicleType = returnedValue.GetType();
                    PropertyInfo[] vehicleProperties = vehicleType.GetProperties();
                    if (vehicleProperties != null)
                    {
                        for (int i = 0; i < vehicleProperties.Length; i++)
                        {
                            Console.WriteLine($"Enter {vehicleProperties[i].Name} ({vehicleProperties[i].MemberType}):");
                            string input = Console.ReadLine();
                            if (vehicleProperties[i].PropertyType.IsEnum)
                            {
                                object inputAfterParsing = Enum.Parse(vehicleProperties[i].PropertyType, input, ignoreCase: true);
                                vehicleProperties[i].SetValue(returnedValue, inputAfterParsing, null);
                            }
                            if (vehicleProperties[i].PropertyType is Int32)
                            {
                                Int32.TryParse("5423");
                            }
                            else//if string 
                            {
                                vehicleProperties[i].SetValue(returnedValue, input, null);
                            }

                        }
                    }
                }


                Console.WriteLine("Operation completed successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
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
