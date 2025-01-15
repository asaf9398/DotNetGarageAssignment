using Ex03.GarageLogic.Enums;
using Ex03.GarageLogic.Exceptions;
using Ex03.GarageLogic.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Ex03.GarageLogic.Model
{
    public class Garage
    {
        private Dictionary<string, GarageVehicle> m_Vehicles;

        public Garage()
        {
            m_Vehicles = new Dictionary<string, GarageVehicle>();
        }

        public Vehicle AddVehicleToGarage(string i_LicenseNumber, string i_OwnerName, string i_OwnerPhone, eVehicleType i_VehicleType)
        {
            if (m_Vehicles.ContainsKey(i_LicenseNumber))
            {
                m_Vehicles[i_LicenseNumber].Status = eVehicleStatus.InRepair;
                throw new VehicleAlreadyInGarageException($"The vehicle with the plate: {i_LicenseNumber} is already in the garage!{Environment.NewLine}The vehicle status changed to \"In Repair\"");
            }
            else
            {
                Vehicle vehicle = VehicleFactory.CreateVehicle(i_VehicleType);
                GarageVehicle newGarageVehicle = new GarageVehicle(vehicle, i_OwnerName, i_OwnerPhone);
                m_Vehicles.Add(i_LicenseNumber, newGarageVehicle);
                return vehicle;
            }
        }


        // public bool AddVehicle(Vehicle i_Vehicle, string i_OwnerName, string i_OwnerPhone)
        // {
        //     bool hasBeenAdded = false;
        //
        //     if (m_Vehicles.ContainsKey(i_Vehicle.LicenseNumber))
        //     {
        //         m_Vehicles[i_Vehicle.LicenseNumber].Status = eVehicleStatus.InRepair;
        //         hasBeenAdded = false;
        //     }
        //     else
        //     {
        //         m_Vehicles.Add(i_Vehicle.LicenseNumber, new GarageVehicle(i_Vehicle, i_OwnerName, i_OwnerPhone));
        //         hasBeenAdded = true;
        //     }
        //     return hasBeenAdded;
        // }

        public List<string> DisplayVehicles(eVehicleStatus i_Filter)
        {
            List<string> vehicleList = new List<string>();
            foreach (var vehicleEntry in m_Vehicles)
            {
                if (i_Filter == eVehicleStatus.Everyone || vehicleEntry.Value.Status == i_Filter)
                {
                    vehicleList.Add(vehicleEntry.Key);
                }
            }

            return vehicleList;
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
        public List<string> GetVehicleDetails(string i_LicenseNumber)
        {
            if (!m_Vehicles.ContainsKey(i_LicenseNumber))
            {
                throw new ArgumentException($"Vehicle with license number {i_LicenseNumber} not found.");
            }

            // GarageVehicle garageVehicle = m_Vehicles[i_LicenseNumber];
            // Vehicle vehicle = garageVehicle.Vehicle;
            //
            // Type garageVehicleType = garageVehicle.GetType();
            // PropertyInfo[] garageVehicleProperties = garageVehicleType.GetProperties();
            //
            // Type vehicleType = garageVehicle.Vehicle.GetType();
            // PropertyInfo[] vehicleProperties = vehicleType.GetProperties();


            Type vehicleType = m_Vehicles[i_LicenseNumber].GetType();
            PropertyInfo[] vehicleProperties = vehicleType
             .GetProperties()
            .Where(p => p.GetSetMethod() != null) // רק מאפיינים עם set ציבורי
            .ToArray();
            List<string> propertiesList = new List<string>();
            propertiesList.Add($"License Number ({i_LicenseNumber})");
            if (vehicleProperties != null)
            {
                for (int i = 0; i < vehicleProperties.Length; i++)
                {
                    try
                    {
                        propertiesList.Add($"{(vehicleProperties[i].Name)} ({vehicleProperties[i].GetValue(m_Vehicles[i_LicenseNumber], null)})");
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }

            vehicleType = m_Vehicles[i_LicenseNumber].Vehicle.GetType();
            vehicleProperties = vehicleType
             .GetProperties()
            .Where(p => p.GetSetMethod() != null) // רק מאפיינים עם set ציבורי
            .ToArray();
            if (vehicleProperties != null)
            {
                for (int i = 0; i < vehicleProperties.Length; i++)
                {
                    try
                    {
                        propertiesList.Add($"{(vehicleProperties[i].Name)} ({vehicleProperties[i].GetValue(m_Vehicles[i_LicenseNumber].Vehicle, null)})");
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }

            vehicleType = m_Vehicles[i_LicenseNumber].Vehicle.Engine.GetType();
            vehicleProperties = vehicleType
             .GetProperties()
            .Where(p => p.GetSetMethod() != null) // רק מאפיינים עם set ציבורי
            .ToArray();
            if (vehicleProperties != null)
            {
                for (int i = 0; i < vehicleProperties.Length; i++)
                {
                    try
                    {
                        propertiesList.Add($"{(vehicleProperties[i].Name)} ({vehicleProperties[i].GetValue(m_Vehicles[i_LicenseNumber].Vehicle.Engine, null)})");
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
            propertiesList.Add($"EnergyPercentage ({m_Vehicles[i_LicenseNumber].Vehicle.GetEnergyPercentage()}%)");

            return propertiesList;
        }
        //StringBuilder detailsBuilder = new StringBuilder();
        //detailsBuilder.AppendLine($"License Number: {vehicle.LicenseNumber}");
        //detailsBuilder.AppendLine($"Model Name: {vehicle.ModelName}");
        //detailsBuilder.AppendLine($"Owner Name: {garageVehicle.OwnerName}");
        //detailsBuilder.AppendLine($"Owner Phone: {garageVehicle.OwnerPhone}");
        //detailsBuilder.AppendLine($"Vehicle Status: {garageVehicle.Status}");
        //
        //detailsBuilder.AppendLine("Wheels:");
        //foreach (Wheel wheel in vehicle.Wheels)
        //{
        //    detailsBuilder.AppendLine($"   Manufacturer: {wheel.ManufacturerName}, Air Pressure: {wheel.CurrentAirPressure}/{wheel.MaxAirPressure}");
        //}
        //
        //if (vehicle.Engine is FuelEngine fuelEngine)
        //{
        //    detailsBuilder.AppendLine($"Fuel Type: {fuelEngine.FuelType}");
        //    detailsBuilder.AppendLine($"Fuel Level: {fuelEngine.CurrentEnergy}/{fuelEngine.MaxEnergy}");
        //}
        //else if (vehicle.Engine is ElectricEngine electricEngine)
        //{
        //    detailsBuilder.AppendLine($"Battery Level: {electricEngine.CurrentEnergy}/{electricEngine.MaxEnergy}");
        //}
        //
        //if (vehicle is Car car)
        //{
        //    detailsBuilder.AppendLine($"Color: {car.Color}");
        //    detailsBuilder.AppendLine($"Number of Doors: {car.NumberOfDoors}");
        //}
        //else if (vehicle is Motorcycle motorcycle)
        //{
        //    detailsBuilder.AppendLine($"License Type: {motorcycle.LicenseType}");
        //    detailsBuilder.AppendLine($"Engine Volume: {motorcycle.EngineVolume}");
        //}
        //else if (vehicle is Truck truck)
        //{
        //    detailsBuilder.AppendLine($"Refrigerated: {(truck.IsRefrigerated ? "Yes" : "No")}");
        //    detailsBuilder.AppendLine($"Cargo Volume: {truck.CargoVolume}");
        //}
        //
        //return detailsBuilder.ToString();
    }
}

