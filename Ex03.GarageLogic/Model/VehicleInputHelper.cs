using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Ex03.GarageLogic.Model
{
    internal static class VehicleInputHelper
    {
        public static void FillVehicleProperties(Vehicle i_Vehicle)
        {
            PropertyInfo[] properties = i_Vehicle.GetType().GetProperties();
            Dictionary<string, string> userInput = new Dictionary<string, string>();

            foreach (PropertyInfo property in properties)
            {
                if (property.CanWrite)
                {
                    Console.WriteLine($"Enter {property.Name} ({property.PropertyType.Name}):");
                    string input = Console.ReadLine();
                    userInput[property.Name] = input;
                }
            }

            SetVehicleProperties(i_Vehicle, userInput);
        }

        private static void SetVehicleProperties(Vehicle i_Vehicle, Dictionary<string, string> i_UserInput)
        {
            PropertyInfo[] properties = i_Vehicle.GetType().GetProperties();

            foreach (PropertyInfo property in properties)
            {
                if (i_UserInput.ContainsKey(property.Name))
                {
                    try
                    {
                        object convertedValue;

                        if (property.PropertyType.IsEnum)
                        {
                            convertedValue = Enum.Parse(property.PropertyType, i_UserInput[property.Name], true);
                        }
                        else
                        {
                            convertedValue = Convert.ChangeType(i_UserInput[property.Name], property.PropertyType);
                        }

                        property.SetValue(i_Vehicle, convertedValue, null);
                    }
                    catch
                    {
                        Console.WriteLine($"Invalid input for {property.Name}. Please try again.");
                        throw new FormatException($"Failed to convert input for {property.Name}");
                    }
                }
            }
        }

    }
}
