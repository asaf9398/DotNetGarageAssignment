using Ex03.GarageLogic.Enums;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;

namespace Ex03.GarageLogic.Model
{
    public abstract class Vehicle
    {
        private string m_ModelName;
        private string m_LicenseNumber;
        private List<Wheel> m_Wheels;
        private Engine m_Engine;

        public string ModelName
        {
            get { return m_ModelName; }
            set { m_ModelName = value; }
        }

        public string LicenseNumber
        {
            get { return m_LicenseNumber; }
            set { m_LicenseNumber = value; }
        }

        public List<Wheel> Wheels
        {
            get { return m_Wheels; }
        }

        public Engine Engine
        {
            get { return m_Engine; }
            set { m_Engine = value; }
        }

        public float GetEnergyPercentage()
        {
            return (m_Engine.CurrentEnergy / m_Engine.MaxEnergy) * 100;
        }

        public Vehicle(Engine i_Engine, int i_NumberOfWheels, float i_MaxAirPressurePerWheel)
        {
            Engine = i_Engine;
            m_Wheels = new List<Wheel>(i_NumberOfWheels);

            for (int i = 0; i < i_NumberOfWheels; i++)
            {
                m_Wheels.Add(new Wheel(string.Empty, i_MaxAirPressurePerWheel));
            }
        }

        public void InflateWheels(float i_PressureToAdd)
        {
            foreach (Wheel wheel in m_Wheels)
            {
                wheel.Inflate(i_PressureToAdd);
            }
        }

        public override string ToString()
        {
            return $"Model Name: {m_ModelName}, License Number: {m_LicenseNumber}, Energy Percentage: {GetEnergyPercentage()}%";
        }
    }

}
