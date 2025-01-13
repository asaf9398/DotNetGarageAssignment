using Ex03.GarageLogic.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex03.GarageLogic.Model
{
    internal class Garage
    {
        private readonly Dictionary<string, GarageVehicle> m_Vehicles;

        public Garage()
        {
            m_Vehicles = new Dictionary<string, GarageVehicle>();
        }

        public bool AddVehicle(Vehicle i_Vehicle, string i_OwnerName, string i_OwnerPhone)
        {
            bool hasBeenAdded = false;

            if (m_Vehicles.ContainsKey(i_Vehicle.LicenseNumber))
            {
                m_Vehicles[i_Vehicle.LicenseNumber].Status = eVehicleStatus.InRepair;
                hasBeenAdded = false;
            }
            else
            {
                m_Vehicles.Add(i_Vehicle.LicenseNumber, new GarageVehicle(i_Vehicle, i_OwnerName, i_OwnerPhone));
                hasBeenAdded = true;
            }
            return hasBeenAdded;
        }

        public void ChangeVehicleStatus(string i_LicenseNumber, eVehicleStatus i_NewStatus)
        {
            if (m_Vehicles.TryGetValue(i_LicenseNumber, out GarageVehicle garageVehicle))
            {
                garageVehicle.Status = i_NewStatus;
            }
            else
            {
                throw new ArgumentException("Vehicle not found.");
            }
        }

        public List<string> DisplayVehicles(eVehicleStatus? i_Filter = null)
        {
            List<string> vehicleList = new List<string>();

            foreach (var vehicleEntry in m_Vehicles)
            {
                if (i_Filter == null || vehicleEntry.Value.Status == i_Filter)
                {
                    vehicleList.Add(vehicleEntry.Key);
                }
            }

            return vehicleList;
        }

        public void InflateWheelsToMax(string i_LicenseNumber)
        {
            if (m_Vehicles.TryGetValue(i_LicenseNumber, out GarageVehicle garageVehicle))
            {
                float maxPressure = garageVehicle.Vehicle.Wheels[0].MaxAirPressure;

                foreach (Wheel wheel in garageVehicle.Vehicle.Wheels)
                {
                    wheel.Inflate(maxPressure - wheel.CurrentAirPressure);
                }
            }
            else
            {
                throw new ArgumentException("Vehicle not found.");
            }
        }

        public void RefuelVehicle(string i_LicenseNumber, eFuelType i_FuelType, float i_Amount)
        {
            if (m_Vehicles.TryGetValue(i_LicenseNumber, out GarageVehicle garageVehicle) &&
                garageVehicle.Vehicle.Engine is FuelEngine fuelEngine)
            {
                if (fuelEngine.FuelType != i_FuelType)
                {
                    throw new ArgumentException("Incorrect fuel type.");
                }

                fuelEngine.Refill(i_Amount);
            }
            else
            {
                throw new ArgumentException("Vehicle not found or does not have a fuel engine.");
            }
        }

        public void ChargeVehicle(string i_LicenseNumber, float i_Hours)
        {
            if (m_Vehicles.TryGetValue(i_LicenseNumber, out GarageVehicle garageVehicle) &&
                garageVehicle.Vehicle.Engine is ElectricEngine electricEngine)
            {
                electricEngine.Refill(i_Hours);
            }
            else
            {
                throw new ArgumentException("Vehicle not found or does not have an electric engine.");
            }
        }
    }
}
