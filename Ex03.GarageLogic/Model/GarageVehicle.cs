using Ex03.GarageLogic.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex03.GarageLogic.Model
{
    internal class GarageVehicle
    {
        private readonly Vehicle m_Vehicle;
        private readonly string m_OwnerName;
        private readonly string m_OwnerPhone;
        private eVehicleStatus m_Status;

        public Vehicle Vehicle
        {
            get { return m_Vehicle; }
        }

        public string OwnerName
        {
            get { return m_OwnerName; }
        }

        public string OwnerPhone
        {
            get { return m_OwnerPhone; }
        }

        public eVehicleStatus Status
        {
            get { return m_Status; }
            set { m_Status = value; }
        }

        public GarageVehicle(Vehicle i_Vehicle, string i_OwnerName, string i_OwnerPhone)
        {
            m_Vehicle = i_Vehicle;
            m_OwnerName = i_OwnerName;
            m_OwnerPhone = i_OwnerPhone;
            m_Status = eVehicleStatus.InRepair;
        }
    }
}
