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
        private bool m_IsRefrigerated;
        private float m_CargoVolume;

        public bool IsRefrigerated
        {
            get { return m_IsRefrigerated; }
            set { m_IsRefrigerated = value; }
        }

        public float CargoVolume
        {
            get { return m_CargoVolume; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Cargo volume must be greater than zero.");
                }

                m_CargoVolume = value;
            }
        }

        public Truck(Engine i_Engine, bool i_IsRefrigerated = false, float i_CargoVolume = 0) : base(i_Engine, i_NumberOfWheels: 14, i_MaxAirPressurePerWheel: 29.0f)
        {
            IsRefrigerated = i_IsRefrigerated;
            CargoVolume = i_CargoVolume;
        }

        public override string ToString()
        {
            return $"{base.ToString()}, Refrigerated: {isRefrigerated()}, Cargo Volume: {m_CargoVolume} cubic meters";
        }

        private string isRefrigerated()
        {
            return m_IsRefrigerated ? "Yes" : "No";
        }
    }

}

