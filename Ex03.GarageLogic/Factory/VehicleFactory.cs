using Ex03.GarageLogic.Enums;
using Ex03.GarageLogic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Ex03.GarageLogic.Factory
{
    internal static class VehicleFactory
    {
        private const eFuelType k_MotorcycleFuelType = eFuelType.Octan98;
        private const float k_MotorcycleFuelCapacity = 6.2f;
        private const float k_EMotorcycleElectricityCapacity = 2.9f;
        private const eFuelType k_CarFuelType = eFuelType.Octan95;
        private const float k_CarFuelCapacity = 52f;
        private const float k_ECarElectricityCapacity = 5.4f;
        private const eFuelType k_TruckFuelType = eFuelType.Soler;
        private const float k_TruckFuelCapacity = 125f;
        public static Vehicle CreateVehicle(eVehicleType i_VehicleType)
        {
            Vehicle vehicle = null;

            switch (i_VehicleType)
            {
                case eVehicleType.Motorcycle:
                    vehicle = new Motorcycle(new FuelEngine(k_MotorcycleFuelType, k_MotorcycleFuelCapacity));
                    break;
                case eVehicleType.EMotorcycle:
                    vehicle = new Motorcycle(new ElectricEngine(k_EMotorcycleElectricityCapacity));
                    break;
                case eVehicleType.Car:
                    vehicle = new Car(new FuelEngine(k_CarFuelType, k_CarFuelCapacity));
                    break;
                case eVehicleType.ECar:
                    vehicle = new Car(new ElectricEngine(k_ECarElectricityCapacity));
                    break;
                case eVehicleType.Truck:
                    vehicle = new Truck(new FuelEngine(k_TruckFuelType, k_TruckFuelCapacity));
                    break;
                default:
                    throw new ArgumentException($"Vehicle type '{i_VehicleType}' is not supported.");
            }

            return vehicle;
        }
    }
}
