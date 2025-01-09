using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageLogic2
{
    internal class Wheel
    {
        protected string m_ManufacturerName;
        protected float m_CurrentAirPressure;
        protected float m_MaxAirPressure;

        // Constructor
        public Wheel(string i_ManufacturerName, float i_MaxAirPressure)
        {
            m_ManufacturerName = i_ManufacturerName;
            m_MaxAirPressure = i_MaxAirPressure;
            m_CurrentAirPressure = 0; // Default starting pressure
        }

        // Inflate method
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


