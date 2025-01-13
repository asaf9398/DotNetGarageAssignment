

using Ex03.GarageLogic.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ex03.GarageLogic.Model
{
    internal class Car : Vehicle
    {
        private eVehicleColor m_Color;
        private eNumberOfDoors m_NumberOfDoors;

        public eVehicleColor Color
        {
            get { return m_Color; }
            set { m_Color = value; }
        }

        public eNumberOfDoors NumberOfDoors
        {
            get { return m_NumberOfDoors; }
            set { m_NumberOfDoors = value; }
        }

        public Car(Engine i_Engine, eVehicleColor i_Color = eVehicleColor.Black, eNumberOfDoors i_NumberOfDoors = eNumberOfDoors.Four) : base(i_Engine, i_NumberOfWheels: 5, i_MaxAirPressurePerWheel: 34.0f)
        {
            if (!Enum.IsDefined(typeof(eVehicleColor), i_Color))
            {
                throw new ArgumentException($"Invalid color value: {i_Color}");
            }

            if (!Enum.IsDefined(typeof(eNumberOfDoors), i_NumberOfDoors))
            {
                throw new ArgumentException($"Invalid number of doors: {i_NumberOfDoors}");
            }

            m_Color = i_Color;
            m_NumberOfDoors = i_NumberOfDoors;
        }

        public override string ToString()
        {
            return $"{base.ToString()}, Color: {m_Color}, Number of Doors: {(int)m_NumberOfDoors}";
        }
    }
}
