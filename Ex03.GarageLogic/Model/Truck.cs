using Ex03.GarageLogic.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic.Model
{
    internal class Truck : Vehicle
    {
        // Protected properties
        protected bool Refrigerator { get; set; }
        protected float CargoVolume { get; }

        // Constructor
        public Truck(
            string i_ModelName,
            string i_LicenseNumber,
            List<Wheel> i_Wheels,
            eEnergyType i_EnergyType,
            float i_EnergyMaxCapacity,
            bool i_Refrigerator,
            float i_CargoVolume, float i_CurrentEnergyAmount = 0)
             : base(i_ModelName, i_LicenseNumber, i_EnergyType, i_EnergyMaxCapacity, i_Wheels, i_CurrentEnergyAmount)
        {
            // Validate cargo volume
            if (i_CargoVolume <= 0)
            {
                throw new ArgumentException("Cargo volume must be a positive value.");
            }

            Refrigerator = i_Refrigerator;
            CargoVolume = i_CargoVolume;
        }
    }
}

