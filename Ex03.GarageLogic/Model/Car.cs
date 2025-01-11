

using Ex03.GarageLogic.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ex03.GarageLogic.Model
{
    internal class Car : Vehicle
    {
        // Protected properties
        protected eCarColor Color { get; }
        protected eNumberOfDoors NumberOfDoors { get; }

        // Constructor
        public Car(string i_ModelName, string i_LicenseNumber, eEnergyType i_EnergyType, List<Wheel> i_Wheels, float i_EnergyMaxCapacity, eCarColor i_Color, eNumberOfDoors i_NumberOfDoors, float i_CurrentEnergyAmount = 0) : base(i_ModelName, i_LicenseNumber, i_EnergyType, i_EnergyMaxCapacity, i_Wheels, i_CurrentEnergyAmount)
        {
            // Validate the number of doors
            if (!Enum.IsDefined(typeof(eNumberOfDoors), i_NumberOfDoors))
            {
                // Dynamically generate the allowed values from the enum
                var allowedValues = string.Join(", ", Enum.GetValues(typeof(eNumberOfDoors)).Cast<eNumberOfDoors>());
                throw new ArgumentException($"Invalid number of doors. Allowed values are: {allowedValues}.");
            }

            Color = i_Color;
            NumberOfDoors = i_NumberOfDoors;
        }
    }
}
