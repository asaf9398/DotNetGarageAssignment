using System;
using Ex03.GarageLogic.Enums;

namespace Ex03.GarageLogic.Model
{
    internal class Engine
    {
        protected eEnergyType m_EnergyType { get; }
        protected float m_MaxCapacity { get; }
        protected float m_CurrentEnergyAmount { get; set; }
        public float EnergyCapacity { get { return m_MaxCapacity; } }
        public float EnergyAmount { get { return m_CurrentEnergyAmount; } }

        public Engine(eEnergyType i_EnergyType, float i_MaxCapacity, float i_CurrentEnergyAmount = 0)
        {
            m_EnergyType = i_EnergyType;
            m_MaxCapacity = i_MaxCapacity;
            m_CurrentEnergyAmount = i_CurrentEnergyAmount;
        }

        // Method to refuel or recharge the engine
        public void RefuelOrRecharge(float i_AmountToAdd, eEnergyType i_EnergyTypeToAdd)
        {
            // בדיקה שהכמות להוספה היא חיובית
            if (i_AmountToAdd <= 0)
            {
                throw new ArgumentException("The amount to add must be a positive value.");
            }

            // בדיקת התאמה של סוג האנרגיה
            if (i_EnergyTypeToAdd != m_EnergyType)
            {
                throw new ArgumentException($"Invalid energy type. Expected {m_EnergyType}, but got {i_EnergyTypeToAdd}.");
            }

            // בדיקת חריגה מהמקסימום
            if (m_CurrentEnergyAmount + i_AmountToAdd > m_MaxCapacity)
            {
                throw new InvalidOperationException("Adding this amount exceeds the maximum capacity.");
            }

            // עדכון הכמות הנוכחית
            m_CurrentEnergyAmount += i_AmountToAdd;
        }
    }
}

