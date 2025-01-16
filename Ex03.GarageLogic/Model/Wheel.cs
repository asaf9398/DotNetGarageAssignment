using Ex03.GarageLogic.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic.Model
{
    public class Wheel
    {
        private string m_ManufacturerName;
        private float m_CurrentAirPressure;
        private float m_MaxAirPressure;

        public float MaxAirPressure
        {
            get { return m_MaxAirPressure; }
        }

        public float CurrentAirPressure
        {
            get { return m_CurrentAirPressure; }
            set { m_CurrentAirPressure = value; }
        }
        public string ManufacturerName { get { return m_ManufacturerName; } }


        public Wheel(string i_ManufacturerName, float i_MaxAirPressure, float i_InitializeAirPressure = 0)
        {
            m_ManufacturerName = i_ManufacturerName;
            m_MaxAirPressure = i_MaxAirPressure;
            m_CurrentAirPressure = i_InitializeAirPressure;
        }
        public void Inflate(float i_PressureToAdd)
        {
            if (m_CurrentAirPressure + i_PressureToAdd > m_MaxAirPressure || i_PressureToAdd < 0)
            {
                throw new ValueOutOfRangeException(0, m_MaxAirPressure, $"Air pressure is not valid(0 to {m_MaxAirPressure}).");
            }

            m_CurrentAirPressure += i_PressureToAdd;
        }
        public override string ToString()
        {
            return $"Manufacturer: {m_ManufacturerName}, Current Air Pressure: {m_CurrentAirPressure}, Max Air Pressure: {m_MaxAirPressure}";
        }
    }
}


