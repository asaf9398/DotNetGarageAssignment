using Ex03.GarageLogic.Enums;
using Ex03.GarageLogic.Exceptions;
using Ex03.GarageLogic.Factory;
using Ex03.GarageLogic.Utils;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Ex03.GarageLogic.Model
{
    public class Garage
    {
        private Dictionary<string, GarageVehicle> m_Vehicles;

        public Garage()
        {
            m_Vehicles = new Dictionary<string, GarageVehicle>();
        }

        public List<PropertyDefinition> AddVehicleToGarage(string i_LicenseNumber, string i_OwnerName, string i_OwnerPhone, eVehicleType i_VehicleType)
        {
            if (!Enum.IsDefined(typeof(eVehicleType), i_VehicleType))
            {
                throw new FormatException("The vehicle type that entered is illegal");
            }

            if (m_Vehicles.ContainsKey(i_LicenseNumber))
            {
                m_Vehicles[i_LicenseNumber].Status = eVehicleStatus.InRepair;
                throw new ArgumentException($"The vehicle with the plate: {i_LicenseNumber} is already in the garage!{Environment.NewLine}The vehicle status changed to \"In Repair\"");
            }
            else
            {
                Vehicle vehicle = VehicleFactory.CreateVehicle(i_VehicleType);
                GarageVehicle newGarageVehicle = new GarageVehicle(vehicle, i_OwnerName, i_OwnerPhone);
                m_Vehicles.Add(i_LicenseNumber, newGarageVehicle);
                return ReflectionHelper.GetProperties(vehicle);
            }
        }

        private void fillVehicleProperties(string i_LicenseNumber, Dictionary<string, object> i_PropertyValues)
        {
            if (m_Vehicles.TryGetValue(i_LicenseNumber, out GarageVehicle garageVehicle))
            {
                foreach (PropertyInfo property in garageVehicle.Vehicle.GetType().GetProperties())
                {
                    if (i_PropertyValues.ContainsKey(property.Name))
                    {
                        try
                        {
                            property.SetValue(garageVehicle.Vehicle, i_PropertyValues[property.Name], null);
                        }
                        catch (Exception ex)
                        {
                            string message = ex.Message;

                            if (ex.InnerException != null)
                            {
                                message = ex.InnerException.Message;
                            }

                            m_Vehicles.Remove(i_LicenseNumber);
                            throw new Exception(message);
                        }
                    }
                }
            }
            else
            {
                throw new ArgumentException("Vehicle not found.");
            }
        }

        public List<string> DisplayVehicles(eVehicleStatus i_Filter)
        {
            if (!Enum.IsDefined(typeof(eVehicleStatus), i_Filter))
            {
                throw new FormatException("The entered value is illegal");
            }

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
            if (!Enum.IsDefined(typeof(eVehicleStatus), i_NewStatus))
            {
                throw new FormatException("The new status that entered is illegal");
            }

            if (m_Vehicles.TryGetValue(i_LicenseNumber, out GarageVehicle garageVehicle))
            {
                if (i_NewStatus != eVehicleStatus.Everyone)
                {
                    garageVehicle.Status = i_NewStatus;
                }
                else
                {
                    throw new ArgumentException($"{i_NewStatus} cannot be inserted as a new status!");
                }
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
            if (!Enum.IsDefined(typeof(eFuelType), i_FuelType))
            {
                throw new FormatException("The fuel type entered is illegal");
            }

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

            PropertyInfo[] vehicleProperties = ReflectionHelper.GetPropertiesFromObject(m_Vehicles[i_LicenseNumber]);
            List<string> propertiesList = new List<string>();
            propertiesList.Add($"License Number ({i_LicenseNumber})");
            propertiesList.Add($"Vehicle Type ({m_Vehicles[i_LicenseNumber].Vehicle.GetType().Name})");
            propertiesList.Add($"Enging Type ({m_Vehicles[i_LicenseNumber].Vehicle.Engine.GetType().Name})");

            if (vehicleProperties != null)
            {
                for (int i = 0; i < vehicleProperties.Length; i++)
                {
                    try
                    {
                        propertiesList.Add($"{(StringUtils.SplitCamelCase(vehicleProperties[i].Name))} ({vehicleProperties[i].GetValue(m_Vehicles[i_LicenseNumber], null)})");
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }

            vehicleProperties = ReflectionHelper.GetPropertiesFromObject(m_Vehicles[i_LicenseNumber].Vehicle);

            if (vehicleProperties != null)
            {
                for (int i = 0; i < vehicleProperties.Length; i++)
                {
                    try
                    {
                        propertiesList.Add($"{(StringUtils.SplitCamelCase(vehicleProperties[i].Name))} ({vehicleProperties[i].GetValue(m_Vehicles[i_LicenseNumber].Vehicle, null)})");
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }

            vehicleProperties = ReflectionHelper.GetPropertiesFromObject(m_Vehicles[i_LicenseNumber].Vehicle.Engine);

            if (vehicleProperties != null)
            {
                for (int i = 0; i < vehicleProperties.Length; i++)
                {
                    try
                    {
                        propertiesList.Add($"{(StringUtils.SplitCamelCase(vehicleProperties[i].Name))} ({vehicleProperties[i].GetValue(m_Vehicles[i_LicenseNumber].Vehicle.Engine, null)})");
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }

            propertiesList.Add($"Energy Percentage ({m_Vehicles[i_LicenseNumber].Vehicle.GetEnergyPercentage()}%)");
            return propertiesList;
        }
    }
}

