using Ex03.GarageLogic.Enums;
using System.Collections.Generic;

namespace Ex03.GarageLogic.Model
{
    abstract class Vehicle
    {
        protected readonly string r_ModelName;
        protected readonly string r_LicenseNumber;
        protected float r_PowerSourcePercentage;
        protected List<Wheel> m_Wheels { get; set; }
        protected readonly Engine r_Engine;

        protected Vehicle(string i_ModelName, string i_LicenseNumber, eEnergyType i_EnergyType, float i_MaxCapacity, List<Wheel> i_Wheels, float i_CurrentEnergyAmount = 0)
        {
            r_ModelName = i_ModelName;
            r_LicenseNumber = i_LicenseNumber;
            r_Engine = new Engine(i_EnergyType, i_MaxCapacity, i_CurrentEnergyAmount);
            m_Wheels = i_Wheels;
        }
    }
}
