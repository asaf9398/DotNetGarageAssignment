using Ex03.GarageLogic.Enums;
using Ex03.GarageLogic.Exceptions;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;

namespace Ex03.GarageLogic.Model
{
    public abstract class Vehicle
    {
        private string m_ModelName;
        private List<Wheel> m_Wheels;
        private Engine m_Engine;

        public string ModelName
        {
            get { return m_ModelName; }
            set { m_ModelName = value; }
        }

        public float CurrentWheelsAir
        {
            set
            {
                foreach (Wheel wheel in m_Wheels)
                {
                    if (value <= wheel.MaxAirPressure && value >= 0)
                    {
                        wheel.CurrentAirPressure = value;
                    }
                    else
                    {
                        throw new ValueOutOfRangeException(0, wheel.MaxAirPressure, $"Air pressure not in limits(0 to {wheel.MaxAirPressure}).");
                    }
                }
            }
            get
            {
                return m_Wheels[0].CurrentAirPressure;
            }
        }
        public float MaxWheelsAir
        {
            get { return m_Wheels[0].MaxAirPressure; }
        }
        public List<Wheel> Wheels
        {
            get { return m_Wheels; }
        }

        public Engine Engine
        {
            get { return m_Engine; }
            private set { m_Engine = value; }
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
            return $"Model Name: {m_ModelName}, Energy Percentage: {GetEnergyPercentage()}%";
        }
    }

}
