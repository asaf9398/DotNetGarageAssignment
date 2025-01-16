using System;
using Ex03.GarageLogic.Enums;
using Ex03.GarageLogic.Exceptions;

namespace Ex03.GarageLogic.Model
{
    public abstract class Engine
    {
        protected float m_CurrentEnergy;
        protected float m_MaxEnergy;
        public void Refill(float i_AmountOfEnergySource)
        {
            if (m_CurrentEnergy + i_AmountOfEnergySource > m_MaxEnergy || i_AmountOfEnergySource < 0)
            {
                throw new ValueOutOfRangeException(0, m_MaxEnergy, $"The amount that entered is illegal or exceeds tank capacity(0 to {m_MaxEnergy}) current amount:{m_CurrentEnergy}.");
            }

            m_CurrentEnergy += i_AmountOfEnergySource;
        }

        public float CurrentEnergy
        {
            get { return m_CurrentEnergy; }
        }

        public float MaxEnergy
        {
            get { return m_MaxEnergy; }
        }
    }
}

