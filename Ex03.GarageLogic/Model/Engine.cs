using System;
using Ex03.GarageLogic.Enums;

namespace Ex03.GarageLogic.Model
{
    internal abstract class Engine
    {
        protected float m_CurrentEnergy;
        protected float m_MaxEnergy;
        public abstract void Refill(float i_Amount);

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

