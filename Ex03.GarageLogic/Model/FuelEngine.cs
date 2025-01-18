using Ex03.GarageLogic.Enums;
using Ex03.GarageLogic.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex03.GarageLogic.Model
{
    internal class FuelEngine : Engine
    {
        private eFuelType m_FuelType;

        public eFuelType FuelType
        {
            get { return m_FuelType; }
            set { m_FuelType = value; }
        }

        public FuelEngine(eFuelType i_FuelType = eFuelType.Octan95, float i_MaxEnergy = 0)
        {
            m_FuelType = i_FuelType;
            m_MaxEnergy = i_MaxEnergy;
            m_CurrentEnergy = 0;
        }
    }

}
