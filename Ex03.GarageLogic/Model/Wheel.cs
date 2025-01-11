using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic.Model
{
    internal class Wheel
    {
        private string m_ManufacturerName;
        private float m_CurrentAirPressure;
        private float m_MaxAirPressure;

        public Wheel(string i_ManufacturerName, float i_MaxAirPressure, float i_InitializeAirPressure = 0)
        {
            m_ManufacturerName = i_ManufacturerName;
            m_MaxAirPressure = i_MaxAirPressure;
            m_CurrentAirPressure = i_InitializeAirPressure;
        }

        public void Inflate(float i_AirToAdd)
        {
            if (i_AirToAdd < 0)
            {
                throw new ArgumentException("The air to add must be a positive value.");
            }

            if (m_CurrentAirPressure + i_AirToAdd > m_MaxAirPressure)
            {
                throw new InvalidOperationException($"Cannot inflate. Adding {i_AirToAdd} exceeds the maximum air pressure of {m_MaxAirPressure}.");
            }

            m_CurrentAirPressure += i_AirToAdd;
            Console.WriteLine($"Wheel inflated successfully. Current air pressure: {m_CurrentAirPressure}.");
        }
    }
}


