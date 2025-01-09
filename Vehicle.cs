using GarageLogic2.enums;

namespace GarageLogic2
{
    abstract class Vehicle : Engine
    {
        protected readonly string r_ModelName;
        protected readonly string r_LicenseNumber;
        protected readonly Engine r_Engine;
        protected List<Wheel> m_Wheels { get; set; }

        protected Vehicle(
            string i_ModelName,
            string i_LicenseNumber,
            eEnergyType i_EnergyType,
            float i_MaxCapacity,
            float i_CurrentEnergyAmount,
            List<Wheel> i_Wheels)
            : base(i_EnergyType, i_MaxCapacity, i_CurrentEnergyAmount) // קריאה לקונסטרקטור של Engine
        {
            r_ModelName = i_ModelName;
            r_LicenseNumber = i_LicenseNumber;
            r_Engine = this; // מחזיק הפניה לעצמו בתור המנוע
            m_Wheels = new List<Wheel>(i_Wheels);
        }
    }
}
