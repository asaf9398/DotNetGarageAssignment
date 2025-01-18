using Ex03.GarageLogic.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex03.GarageLogic.Model
{
    internal class ElectricEngine : Engine
    {
        public ElectricEngine(float i_MaxEnergy = 0)
        {
            m_MaxEnergy = i_MaxEnergy;
            m_CurrentEnergy = 0;
        }
    }
}
